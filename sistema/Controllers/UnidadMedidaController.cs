using System;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class UnidadMedidaController : Controller
    {
        UnidadMedidaBL bl = new UnidadMedidaBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listUnidad()
        {
            return Json(bl.listar());
        }
        [HttpPost]
        public int saveUnidad(Unidadmedida unidadmedida)
        {
            unidadmedida.Fechacreacion = DateTime.Now;
            return bl.guardar(unidadmedida);
        }
        [HttpGet]
        public JsonResult getUnidadById(int id)
        {
            return Json(bl.obtener(id));
        }
        [HttpGet]
        public int deleteUnidad(int id)
        {
            return bl.eliminar(id);
        }
    }
}
