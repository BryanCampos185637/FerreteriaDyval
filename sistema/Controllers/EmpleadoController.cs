using System;
using AdminFerreteria.DAL;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class EmpleadoController : Controller
    {
        [ServiceFilter(typeof(FiltroDePaginaTipoUsuario))]
        public IActionResult Index()
        {
            return View();
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
