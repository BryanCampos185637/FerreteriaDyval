using AdminFerreteria.DAL;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class FacturaEmitidaController : Controller
    {
        FacturaEmitidaDAL dal = new FacturaEmitidaDAL();
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
