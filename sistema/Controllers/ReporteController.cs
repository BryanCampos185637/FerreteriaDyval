using System;
using System.Collections.Generic;
using System.Linq;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class ReporteController : Controller
    {
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            var empleado = EmpleadoBL.obtenerElPrimerEmpleado();
            DateTime fecha = Convert.ToDateTime(empleado.Fechacreacion);
            ViewBag.fechaInicioSistema = fecha.ToString("yyyy-MM-dd");//el primer usuario que se registro fue la primer funcion que se hizo en el sistema
            return View();
        }
        public IActionResult _FormularioReporteInventario()
        {
            return View();
        }
        public IActionResult _FormularioReporteVenta()
        {
            return View();
        }
        //crea la lista de reportes tomando las fechas establecidas por el usuario
        public int createListReportVenta(string desde, string hasta)
        {
            try
            {
                List<ListFactura> lstFactura = ReporteBL.CrearListaReporteVenta(desde, hasta);
                string listaSerializada = JsonConvert.SerializeObject(lstFactura);
                HttpContext.Session.SetString("lstFactura", listaSerializada);
                return 1; 
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        //accion para generar reporte
        public ActionResult ReporteVentas()
        {
            try
            {
                string lstSerializada = HttpContext.Session.GetString("lstFactura");
                List<ListFactura> lstFactura = JsonConvert.DeserializeObject<List<ListFactura>>(lstSerializada);
                byte[] buffer = UtilidadesController.crearReporteVenta(lstFactura);//obtenemos el reporte
                HttpContext.Session.Remove("lstFactura");//removemos el id que esta en la cookie
                return File(buffer, "application/pdf");
            }
            catch(Exception e)
            {
                TempData["Mensaje"] = e.Message;
                return RedirectToAction("Index");
            }
        }
        //genera el reporte
        public ActionResult ReporteInventario(string tipo)
        {
            byte[] buffer = null;
            try
            {
                #region obtenemos data y creamos variables 
                string lstSerializada = HttpContext.Session.GetString("ReporteInventario");//obtenemos la lista de la cookie
                List<ListReporteInventario> listReporteInventarios = JsonConvert.DeserializeObject<List<ListReporteInventario>>(lstSerializada);
                string[] cabecera = { "COD", "DESCRIPCIÓN", "PROVEEDOR", "BODEGA", "STOCK", "UM", "CANT", "PRECIO", "UM", "CANT", "PRECIO" };
                string[] propiedades = { "Codigoproducto", "Nombreproducto", "Proveedor", "Nombrebodega", "Nombrestock", "Nombreunidad", "Cantidad", "Precio", "Nombresubunidad", "Subcantidad", "Subprecio" };
                string tipoMime = "";
                #endregion

                #region generamos el documento
                if (tipo == "excel")
                {
                    buffer = UtilidadesController.crearReporteInventario(tipo, listReporteInventarios, cabecera, propiedades);//obtenemos el reporte
                    tipoMime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                }
                else
                {
                    buffer = UtilidadesController.crearReporteInventario(tipo, listReporteInventarios, cabecera, propiedades);//obtenemos el reporte
                    tipoMime = "application/pdf";
                }
                #endregion

                HttpContext.Session.Remove("ReporteInventario");//removemos la lista que esta en la cookie
                return File(buffer, tipoMime);
            }
            catch (Exception e)
            {
                return Redirect("Index");
            }
        }
        [HttpPost]
        public string crearListaReporteInventario(Inventario inventario, string nombrestock)
        {
            List<ListReporteInventario> lst = new List<ListReporteInventario>();
            try
            {
                if (inventario.Iidbodega == -1)//si el id de la bodega viene en -1 quiere decir que haremos una lista de la sala de ventas
                {
                    lst = UtilidadesController.crearListaReporteSala(inventario, nombrestock);
                }
                else if (inventario.Iidbodega > 0)
                {
                    lst = UtilidadesController.crearlistaReporteBodega(inventario,nombrestock);
                }
                else
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        #region obtenemos la data
                        lst = new ReporteBL().obtenerListarProductosReporteInventario();
                        var listaBodegas = new ReporteBL().obteneListaProductosDeInventarioReporteInventario();
                        lst.AddRange(listaBodegas);
                        #endregion
                        lst = ReporteBL.ObtenerProveedoresReporteInventario(db, lst);
                    }
                }
                //ordenamos la lista por el nombre del stock
                lst = lst.OrderBy(p => p.Nombrestock).ToList();
                //serializamos la lista y la guardamos en una cookie
                HttpContext.Session.SetString("ReporteInventario", JsonConvert.SerializeObject(lst));
                return "ok";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
