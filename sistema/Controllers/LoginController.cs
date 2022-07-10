using System.Linq;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperBitacora;
using AdminFerreteria.Helper.HelperInicioSistema;
using AdminFerreteria.Helper.HelperSession;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            CreacionPaginasRol.iniciarSistema();
            int? idUsuario = Cookies.obtenerObjetoSesion(HttpContext.Session, "UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null) 
            {
                return Redirect("/home/Index");
            }
            else
            {
                ViewBag.Empleados = new EmpleadoBL().listarEmpleados().Count();
                return View();
            }
                
        }
        public IActionResult _RegistrarPrimerEmpleado()
        {
            return View();
        }
        public IActionResult _LogIn()
        {
            return View();
        }
        LoginBL bl = new LoginBL();
        [HttpPost]
        public int validation(Usuario user)
        {
            int rpt = bl.Login(user);
            if (rpt >0) 
            {
                Usuario usuario = bl.obtenerDataUsuarioLog(user);
                Cookies.crearCookieSession(HttpContext.Session, "UsuarioLogueado", usuario.Iidusuario);
                Cookies.crearCookieSession(HttpContext.Session, "Rol", usuario.Iidtipousuario);
                LogicaBitacoraSistema.InsertarBitacoraSistema
                (
                    "Inicio de sesión",
                    Cookies.obtenerObjetoSesion
                    (
                        HttpContext.Session, 
                        "UsuarioLogueado"
                    )
                 );
            }
            return rpt;
        }
        [HttpPost]
        public int Registrar(Empleado empleado, string nombreusuario, string contraseña)
        {
            return bl.RegistrarUsuarioNuevo(empleado, nombreusuario, contraseña);
        }
        public IActionResult cerrarSesion()
        {
            LogicaBitacoraSistema.InsertarBitacoraSistema("Cierre de sesión",
                     (int)HttpContext.Session.GetInt32("UsuarioLogueado"));
            destruirCookiesSesion();
            return RedirectToAction("Index");
        }
        public void destruirCookiesSesion()
        {
            HttpContext.Session.Remove("UsuarioLogueado");
            HttpContext.Session.Clear();
        }
        [HttpGet]
        public JsonResult generarMenu()
        {
            return Json(bl.listarPaginasMenu
            (
                (int)Cookies.obtenerObjetoSesion(HttpContext.Session, "UsuarioLogueado")
            ));
        }
    }
}
