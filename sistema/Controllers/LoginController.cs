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
        BDFERRETERIAContext db = new BDFERRETERIAContext();
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
                ViewBag.Empleados = db.Empleado.Where(p => p.Bhabilitado == "A").Count();
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
        LoginBL dal = new LoginBL();
        [HttpPost]
        public int validation(Usuario user)
        {
            int rpt = dal.Login(user);
            if (rpt >0) 
            {
                Usuario usuario = dal.obtenerDataUsuarioLog(user);
                Cookies.crearCookieSession(HttpContext.Session, "UsuarioLogueado", usuario.Iidusuario);
                LogicaBitacoraSistema.InsertarBitacoraBL
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
            return dal.RegistrarUsuarioNuevo(empleado, nombreusuario, contraseña);
        }
        public IActionResult cerrarSesion()
        {
            LogicaBitacoraSistema.InsertarBitacoraBL("Cierre de sesión",
                     (int)HttpContext.Session.GetInt32("UsuarioLogueado"));
            destruirCookiesSesion();
            return RedirectToAction("Index");
        }
        public void destruirCookiesSesion()
        {
            HttpContext.Session.Remove("UsuarioLogueado");
            HttpContext.Session.Remove("NombreUsuario");
        }
        [HttpGet]
        public JsonResult generarMenu()
        {
            return Json(dal.listarPaginasMenu
            (
                (int)Cookies.obtenerObjetoSesion(HttpContext.Session, "UsuarioLogueado")
            ));
        }
    }
}
