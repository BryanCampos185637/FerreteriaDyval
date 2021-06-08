using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using System;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Helper.HelperSession;
using AdminFerreteria.BussinesLogic;

namespace AdminFerreteria.Controllers
{
    public class HomeController : Controller
    {
        [ServiceFilter(typeof(FiltroDeAcciones))]
        public IActionResult Index()
        {
            obtenerData();
            return View();
        }
        private void obtenerData()
        {
            ViewBag.nombre = ObtenerNombreUsuarioLogueado.obtenerNombre(
                    (int)Cookies.obtenerObjetoSesion(HttpContext.Session, "UsuarioLogueado")
                );
            ViewBag.unidad = new UnidadMedidaBL().listar().Count();
            ViewBag.stock = new StockBL().listarStock().Count();
            ViewBag.configuracion = new ConfiguracionBL().existeConfiguracion();
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
