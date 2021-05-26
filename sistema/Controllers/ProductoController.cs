using System;
using System.Data;
using AdminFerreteria.DAL;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class ProductoController : Controller
    {
        ProductoDAL dal = new ProductoDAL();
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
        public int saveProducto(Producto producto)
        {
            return dal.guardarProducto(producto);
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
