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
        UnidadMedidaBL dal = new UnidadMedidaBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listUnidad()
        {
            return Json(dal.listar());
        }
        [HttpPost]
        public int saveUnidad(Unidadmedida unidadmedida)
        {
            unidadmedida.Fechacreacion = DateTime.Now;
            return dal.guardar(unidadmedida);
        }
        [HttpGet]
        public JsonResult getUnidadById(int id)
        {
            return Json(dal.obtener(id));
        }
        [HttpGet]
        public int deleteUnidad(int id)
        {
            return dal.eliminar(id);
        }
    }
}
