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
        [ServiceFilter(typeof(FiltroDePaginaTipoUsuario))]
        public IActionResult Index()
        {
            return View();
        }
        ProductoDAL dal = new ProductoDAL();
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
