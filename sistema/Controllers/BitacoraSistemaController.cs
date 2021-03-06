using Microsoft.AspNetCore.Mvc;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.BussinesLogic;

namespace AdminFerreteria.Controllers
{
    
    public class BitacoraSistemaController : Controller
    {
        BitacoraSistemaBL bl = new BitacoraSistemaBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index(string filtro = null, int pagina = 1, int cantidad = 5)
        {
            if (filtro == null) { filtro = ""; }
            ViewBag.filtro = filtro;
            return View(bl.paginar(pagina, filtro));
        }
        public IActionResult _Paginador()
        {
            return View();
        }
    }
}
