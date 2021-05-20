using System.Linq;
using AdminFerreteria.DAL;
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
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
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
        LoginDAL dal = new LoginDAL();
        [HttpPost]
        public int validation(Usuario user)
        {
            int rpt = dal.Login(user);
            if (rpt >0) 
            {
                Usuario usuario = dal.obtenerDataUsuarioLog(user);
                HttpContext.Session.SetInt32("UsuarioLogueado", usuario.Iidusuario);
                HttpContext.Session.SetString("NombreUsuario", usuario.IidempleadoNavigation.Nombrecompleto);
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
            int idTipoUsuario = (int)HttpContext.Session.GetInt32("UsuarioLogueado");
            return Json(dal.listarPaginasMenu(idTipoUsuario));
        }
    }
}
