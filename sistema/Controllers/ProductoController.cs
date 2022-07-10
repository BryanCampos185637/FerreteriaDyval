using System;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperCalculoPrecio;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class ProductoController : Controller
    {
        ProductoBL bl = new ProductoBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            ViewBag.Rol = Helper.HelperSession.Cookies.obtenerObjetoSesion(HttpContext.Session, "Rol");
            return View();
        }
        [HttpGet]
        public JsonResult TotalProductos()
        {
            return Json(bl.cantidadDeProductos());
        }
        [HttpGet]
        public JsonResult filtrarProductos(string Codigo, string Nombre)
        {
            return Json(bl.buscarProductos(Codigo, Nombre));
        }
        [HttpPost]
        public JsonResult saveProducto(Producto producto)
        {
            try
            {
                return Json(bl.guardarProducto(CalcularPrecioProducto.calcular(producto)));
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        [HttpGet]
        public JsonResult getProductoById(Int64 id)
        {
            return Json(bl.obtenerProducto(id));
        }
        [HttpGet]
        public int deleteProducto(Int64 id)
        {
            return bl.eliminarProducto(id);
        }
        [HttpGet]
        public string ObtenerNombreUnidad(int id)
        {
            return bl.obtenerNombreUnidad(id);
        }

        public JsonResult CambiarExistenciaGeneral(long cantidad)
        {
            return Json(bl.CambiarExistenciaGeneral(cantidad));
        }
    }
}
