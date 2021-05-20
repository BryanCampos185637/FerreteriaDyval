using System;
using AdminFerreteria.DAL;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class UnidadMedidaController : Controller
    {
        UnidadMedidaDAL dal = new UnidadMedidaDAL();
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("unidadmedida", "index", (int)idUsuario))
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
