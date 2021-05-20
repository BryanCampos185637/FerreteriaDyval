using Microsoft.AspNetCore.Mvc;
using System;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using AdminFerreteria.DAL;

namespace AdminFerreteria.Controllers
{
    public class BodegaInventarioController : Controller
    {
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("bodegainventario", "index", (int)idUsuario))
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
        BodegainventarioDAL dal = new BodegainventarioDAL();
        [HttpGet]
        public JsonResult listBodega()
        {
            return Json(dal.listarBodega());
        }
        [HttpGet]
        public JsonResult listInventario()
        {
            return Json(dal.listarInventario());
        }
        [HttpPost]
        public string GuardarBodega(Bodega bodega)
        {
            return dal.guardarBodega(bodega);
        }
        [HttpGet]
        public JsonResult obtenerBodega(int id)
        {
            return Json(dal.obtenerBodega(id));
        }
        [HttpGet]
        public string eliminarBodega(Int32 id)
        {
            return dal.eliminarBodega(id);
        }
        [HttpGet]
        public JsonResult obtenerInventario(int id)
        {
            return Json(dal.obtenerInventario(id));
        }
        [HttpPost]
        public string moverproducto(Int64 cantidad, Int64 bodegaActual, Int64 producto, Int64 ubicacionnueva, int stock)
        {
            return dal.moverproducto(cantidad, bodegaActual, producto, ubicacionnueva, stock);
        }
        [HttpGet]
        public JsonResult listBodegaSelect(Int64 id)
        {
            return Json(dal.listarBodegaDiferenteDelParametroId(id));
        }
        [HttpGet]
        public string eliminarInventario(Int64 id)
        {
            return dal.eliminarInventario(id);
        }
        [HttpGet]
        public string editarExistenciaInventario (Int64 id,Int64 cantidad)
        {
            return dal.editarExistenciasInventario(id, cantidad);
        }
        [HttpPost]
        public string modificarexistenciaproducto(Producto producto)
        {
            return dal.modificarExistenciasSalaDeVenta(producto);
        }
        [HttpGet]
        public JsonResult listarProductos()
        {
            return Json(dal.listarProductos()); 
        }
    }
}
