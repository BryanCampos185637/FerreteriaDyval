using AdminFerreteria.DAL;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class ConfiguracionController : Controller
    {
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("configuracion", "index", (int)idUsuario))
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
        ConfiguracionDAL dal = new ConfiguracionDAL();
        public JsonResult getConfiguration()
        {
            return Json(dal.obtenerConfiguracionSistema());
        }
        [HttpPost]
        public int updateConfiguracion(Configuracion configuracion, string usuario,string contra)
        {
            return dal.ActualizarConfiguracion(configuracion, usuario, contra);
        }
    }
}
