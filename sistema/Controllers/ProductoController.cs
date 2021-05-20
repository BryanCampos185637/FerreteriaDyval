using System;
using System.Data;
using AdminFerreteria.DAL;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("producto", "index", (int)idUsuario))
                {
                    return View();
                }
                else
                {
                    return Redirect("/Home/error");
                }
            }
            else
            {
                return Redirect("/Login/index");
            }
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
