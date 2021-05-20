using System;
using AdminFerreteria.DAL;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("empleado", "index", (int)idUsuario))
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
        EmpleadoDAL dal = new EmpleadoDAL();
        [HttpGet]
        public JsonResult listEmpleado()
        {
            return Json(dal.listarEmpleados());
        }
        [HttpGet]
        public JsonResult geEmpleadotById(int id)
        {
            return Json(dal.obtenerEmpleado(id));
        }
        [HttpGet]
        public int deleteEmpleado(int id)
        {
            return dal.eliminar(id);
        }
        [HttpPost]
        public int saveEmpleado(Empleado empleado, Usuario usuario)
        {
            return dal.guardarEmpleado(empleado, usuario);
        }
        [HttpGet]
        public JsonResult listTipoUsuario()
        {
            return Json(dal.listarTipoUsuario());
        }
        [HttpGet]
        public JsonResult obtenerUsuario(Int64 id)
        {
            return Json(dal.obtenerUsuario(id));
        }
    }
}
