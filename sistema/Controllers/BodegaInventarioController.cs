using Microsoft.AspNetCore.Mvc;
using System;
using AdminFerreteria.Models;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.BussinesLogic;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class BodegaInventarioController : Controller
    {
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        BodegaInventarioBL bl = new BodegaInventarioBL();
        [HttpGet]
        public JsonResult listBodega()
        {
            return Json(bl.listarBodega());
        }
        [HttpGet]
        public JsonResult listInventario()
        {
            return Json(bl.listarInventario());
        }
        [HttpPost]
        public string GuardarBodega(Bodega bodega)
        {
            return bl.guardarBodega(bodega);
        }
        [HttpGet]
        public JsonResult obtenerBodega(int id)
        {
            return Json(bl.obtenerBodega(id));
        }
        [HttpGet]
        public string eliminarBodega(Int32 id)
        {
            return bl.eliminarBodega(id);
        }
        [HttpGet]
        public JsonResult obtenerInventario(int id)
        {
            return Json(bl.obtenerInventario(id));
        }
        [HttpPost]
        public string moverproducto(Int64 cantidad, Int64 bodegaActual, Int64 producto, Int64 ubicacionnueva, int stock)
        {
            return bl.moverproducto(cantidad, bodegaActual, producto, ubicacionnueva, stock);
        }
        [HttpGet]
        public JsonResult listBodegaSelect(Int64 id)
        {
            return Json(bl.listarBodegaDiferenteDelParametroId(id));
        }
        [HttpGet]
        public string eliminarInventario(Int64 id)
        {
            return bl.eliminarInventario(id);
        }
        [HttpGet]
        public string editarExistenciaInventario (Int64 id,Int64 cantidad)
        {
            return bl.editarExistenciasInventario(id,cantidad);
        }
        [HttpPost]
        public string modificarexistenciaproducto(Producto producto)
        {
            return bl.modificarExistenciasSalaDeVenta(producto);
        }
        [HttpGet]
        public JsonResult listarProductos()
        {
            return Json(bl.listarProductos()); 
        }
    }
}
