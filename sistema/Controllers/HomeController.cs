using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using System;
using AdminFerreteria.Helper.HelperSeguridad;

namespace AdminFerreteria.Controllers
{
    public class HomeController : Controller
    {
        [ServiceFilter(typeof(FiltroDeAcciones))]
        public IActionResult Index()
        {
            BDFERRETERIAContext db = new BDFERRETERIAContext();
            ViewBag.nombre = HttpContext.Session.GetString("NombreUsuario");
            ViewBag.unidad = db.Unidadmedida.Where(p => p.Bhabilitado == "A").Count();
            ViewBag.stock = db.Stock.Where(p => p.Bhabilitado == "A").Count();
            ViewBag.configuracion = db.Configuracion.Count();
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult _ConfiguracionInicial()
        {
            return View();
        }
        public IActionResult _UnidadInicial()
        {
            return View();
        }
        public IActionResult _StockInicial()
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
