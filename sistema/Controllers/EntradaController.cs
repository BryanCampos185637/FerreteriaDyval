using System;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class EntradaController : Controller
    {
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        EntradaBL dal = new EntradaBL();
        [HttpGet]
        public JsonResult listEntrada()
        {
            return Json(dal.listarEntrada());
        }
        [HttpPost]
        public int saveEntrada(Entrada entrada, Int64[] bodegas, Int64[] cantidades, Int64 ventas, Int64[] stock, decimal precioCompra)
        {
            try
            {
                return dal.guardarEntrada(entrada, bodegas, cantidades, ventas, stock, precioCompra);
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        [HttpGet]
        public int deleteEntrada(Int64 id)
        {
            return dal.eliminarEntrada(id);
        }
        [HttpGet]
        public JsonResult listarStock()
        {
            return Json(dal.listarStock());
        }
    }
}
