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
        BodegaInventarioBL dal = new BodegaInventarioBL();
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
            var rpt = dal.guardarBodega(bodega);
            
            return rpt;
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
            var rpt = dal.moverproducto(cantidad, bodegaActual, producto, ubicacionnueva, stock);
            
            return rpt;
        }
        [HttpGet]
        public JsonResult listBodegaSelect(Int64 id)
        {
            return Json(dal.listarBodegaDiferenteDelParametroId(id));
        }
        [HttpGet]
        public string eliminarInventario(Int64 id)
        {
            var rpt = dal.eliminarInventario(id);
            
            return rpt;
        }
        [HttpGet]
        public string editarExistenciaInventario (Int64 id,Int64 cantidad)
        {
            var rpt = dal.editarExistenciasInventario(id,cantidad);
            
            return rpt;
        }
        [HttpPost]
        public string modificarexistenciaproducto(Producto producto)
        {
            var rpt = dal.modificarExistenciasSalaDeVenta(producto);
            
            return rpt;
        }
        [HttpGet]
        public JsonResult listarProductos()
        {
            return Json(dal.listarProductos()); 
        }
    }
}
