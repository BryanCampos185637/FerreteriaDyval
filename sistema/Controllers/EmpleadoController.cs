using System;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class EmpleadoController : Controller
    {
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        EmpleadoBL bl = new EmpleadoBL();
        [HttpGet]
        public JsonResult listEmpleado()
        {
            return Json(bl.listarEmpleados());
        }
        [HttpGet]
        public JsonResult geEmpleadotById(int id)
        {
            return Json(bl.obtenerEmpleado(id));
        }
        [HttpGet]
        public int deleteEmpleado(int id)
        {
            return bl.eliminar(id);
        }
        [HttpPost]
        public int saveEmpleado(Empleado empleado, Usuario usuario)
        {
            return bl.guardarEmpleado(empleado, usuario);
        }
        [HttpGet]
        public JsonResult listTipoUsuario()
        {
            return Json(bl.listarTipoUsuario());
        }
        [HttpGet]
        public JsonResult obtenerUsuario(Int64 id)
        {
            return Json(bl.obtenerUsuario(id));
        }
    }
}
