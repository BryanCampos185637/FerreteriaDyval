using System;
using AdminFerreteria.DAL;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class TipoUsuarioController : Controller
    {
        TipoUsuarioDAL dal = new TipoUsuarioDAL();
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("tipousuario", "index", (int)idUsuario))
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
        [HttpGet]
        public JsonResult getTipoUsuarioById(int id)
        {
            return Json(dal.ObtenerTipoUsuario(id));
        }
        [HttpGet]
        public JsonResult listTipoUsuario()
        {
            return Json(dal.Listar());
        }
        [HttpPost]
        public int saveTipoUsuario(Tipousuario tipousuario, int[] idPaginas)
        {
            return dal.guardar(tipousuario, idPaginas);
        }
        [HttpGet]
        public JsonResult listPaginasAsignadas(Int64 id)
        {
            return Json(dal.listarPaginasAsignadas(id));
        }
        [HttpGet]
        public JsonResult listPaginas()
        {
            return Json(dal.listarPaginasExistentes());
        }
        [HttpGet]
        public int deleteTipoUsuario(int id)
        {
            return dal.eliminar(id);
        }
    }
}
