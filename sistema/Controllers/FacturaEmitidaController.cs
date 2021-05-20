using AdminFerreteria.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class FacturaEmitidaController : Controller
    {
        FacturaEmitidaDAL dal = new FacturaEmitidaDAL();
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("FacturaEmitida", "index", (int)idUsuario))
                {
                    return View();
                }
                else
                {
                    return Redirect("/Home/error");
                }
            }
            else
            {
                return Redirect("/Login/index");
            }
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
