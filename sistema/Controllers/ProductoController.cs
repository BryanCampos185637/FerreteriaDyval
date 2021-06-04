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
        ProductoBL dal = new ProductoBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult TotalProductos()
        {
            return Json(dal.cantidadDeProductos());
        }
        [HttpGet]
        public JsonResult filtrarProductos(string Codigo, string Nombre)
        {
            return Json(dal.buscarProductos(Codigo, Nombre));
        }
        [HttpPost]
        public JsonResult saveProducto(Producto producto)
        {
            try
            {
                return Json(dal.guardarProducto(CalcularPrecioProducto.calcular(producto)));
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        [HttpGet]
        public JsonResult getProductoById(Int64 id)
        {
            return Json(dal.obtenerProducto(id));
        }
        [HttpGet]
        public int deleteProducto(Int64 id)
        {
            return dal.eliminarProducto(id);
        }
        [HttpGet]
        public string ObtenerNombreUnidad(int id)
        {
            return dal.obtenerNombreUnidad(id);
        }
    }
}
