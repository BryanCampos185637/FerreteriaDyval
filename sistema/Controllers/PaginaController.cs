using Microsoft.AspNetCore.Mvc;
using AdminFerreteria.Models;
using AdminFerreteria.BussinesLogic;

namespace AdminFerreteria.Controllers
{
    public class PaginaController : Controller
    {
        public IActionResult Index(int contra)
        {
            if (contra == 1998185637)
            {
                return View();
            }
            else
            {
                return Redirect("/home/index");
            }
        }
        [HttpGet]
        public JsonResult list()
        {
            return Json(new PaginaBL().listarPaginas());
        }
        [HttpGet]
        public JsonResult getById(int id)
        {
            return Json(new PaginaBL().obtenerPaginaPorId(id));
        }
        [HttpPost]
        public int save(Pagina pagina)
        {
            return new PaginaBL().guardar(pagina);
        }
        [HttpGet]
        public string eliminar(int id)
        {
            return new PaginaBL().eliminar(id);
        }
    }
}
