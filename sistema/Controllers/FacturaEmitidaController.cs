using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class FacturaEmitidaController : Controller
    {
        private readonly FacturaEmitidaBL dal = new FacturaEmitidaBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult filtrarFactura(string fecha)
        {
            return Json(dal.buscarFacturaPorFechas(fecha));
        }

        public JsonResult filtrarFacturaEnEspera()
        {
            return Json(dal.buscarFacturasEnEspera());
        }
    }
}
