using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class ConfiguracionController : Controller
    {
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult _FormularioRegistro()
        {
            return View();
        }
        ConfiguracionBL bl = new ConfiguracionBL();

        public JsonResult getConfiguration()
        {
            return Json(bl.obtenerConfiguracionSistema());
        }
        [HttpPost]
        public int updateConfiguracion(Configuracion configuracion, string usuario,string contra)
        {
            return bl.ActualizarConfiguracion(configuracion, usuario, contra);
        }
    }
}
