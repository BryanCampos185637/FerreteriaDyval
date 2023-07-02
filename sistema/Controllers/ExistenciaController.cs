using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class ExistenciaController : Controller
    {
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listProducto()
        {
            using (var db = new BDFERRETERIAContext())
            {
                #region obtener la lista de productos activos
                List<ListProducto> listaDeProductosConBajaExistencia = new List<ListProducto>();
                List<ListProducto> lst = new ExistenciaBL().listarProductosActivos();
                #endregion
                var inventarios = db.Inventario.Where(p => p.Bhabilitado.Equals("A")).ToList();
                #region usando la lista anterior contaremos la cantidad de productos existentes en bodega y venta
                foreach (var item in lst)//recorremos la lista
                {
                    Int64 contador = 0;
                    var lstInventario = inventarios.Where(p => p.Iidproducto == item.Iidproducto).ToList();//llamamos las bodegas que contengan el producto
                    foreach (var j in lstInventario)//iteramos para saber la cantidad
                    {
                        contador += j.Cantidad;
                    }
                    item.Existencias += contador;//incrementamos la cantidad que ya existia en la lista
                    if (item.Subexistencia != -1000 && item.Subexistencia != null)
                    {
                        item.Subexistencia += item.Equivalencia * contador;//si hay un sub producto tambien la incrementamos
                    }

                    if (item.Existencias <= 3)
                    {
                        listaDeProductosConBajaExistencia.Add(item);
                    }
                }
                #endregion

                #region creamos una cookie para almacenar la lista actual para el reporte
                string lista = JsonConvert.SerializeObject(listaDeProductosConBajaExistencia);
                HttpContext.Session.SetString("existenciasBajas", lista);
                #endregion

                return Json(listaDeProductosConBajaExistencia);
            }
        }

        public FileResult existenciasPDF()
        {
            var listaSerializada = HttpContext.Session.GetString("existenciasBajas");
            byte[] buffer = Helper.HelperReportes.ReportesSistema.generarPDFExistenciasBajas(listaSerializada);
            return File(buffer, "application/pdf");
        }
    }
}
