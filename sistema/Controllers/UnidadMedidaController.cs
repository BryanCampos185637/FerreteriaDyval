using System;
using AdminFerreteria.DAL;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class UnidadMedidaController : Controller
    {
        UnidadMedidaDAL dal = new UnidadMedidaDAL();
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
