using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace AdminFerreteria.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            BDFERRETERIAContext db = new BDFERRETERIAContext();
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                ViewBag.nombre = HttpContext.Session.GetString("NombreUsuario");
                ViewBag.unidad = db.Unidadmedida.Where(p => p.Bhabilitado == "A").Count();
                ViewBag.stock = db.Stock.Where(p => p.Bhabilitado == "A").Count();
                Configuracion confi = db.Configuracion.Where(p => p.Iidconfiguracion.Equals(1)).FirstOrDefault();
                ViewBag.configuracion = confi;
                return View();
            }
            else
            {
                return Redirect("/Login/index");
            }
        }
        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public int createConfiguracion(Configuracion configuracion)
        {
            try
            {
                BDFERRETERIAContext db = new BDFERRETERIAContext();
                db.Configuracion.Add(configuracion);
                db.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                return 0;
            }
        }
    }
}
