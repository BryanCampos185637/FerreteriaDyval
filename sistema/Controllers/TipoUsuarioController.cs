using System;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class TipoUsuarioController : Controller
    {
        TipoUsuarioBL bl = new TipoUsuarioBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult getTipoUsuarioById(int id)
        {
            return Json(bl.ObtenerTipoUsuario(id));
        }
        [HttpGet]
        public JsonResult listTipoUsuario()
        {
            return Json(bl.Listar());
        }
        [HttpPost]
        public int saveTipoUsuario(Tipousuario tipousuario, int[] idPaginas)
        {
            return bl.guardar(tipousuario, idPaginas);
        }
        [HttpGet]
        public JsonResult listPaginasAsignadas(Int64 id)
        {
            return Json(bl.listarPaginasAsignadas(id));
        }
        [HttpGet]
        public JsonResult listPaginas()
        {
            return Json(bl.listarPaginasExistentes());
        }
        [HttpGet]
        public int deleteTipoUsuario(int id)
        {
            return bl.eliminar(id);
        }
    }
}
