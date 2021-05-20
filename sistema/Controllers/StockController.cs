using AdminFerreteria.DAL;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class StockController : Controller
    {
        StockDAL dal = new StockDAL();
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("stock", "index", (int)idUsuario))
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
        public JsonResult listStock()
        {
            return Json(dal.listarStock());   
        }
        [HttpPost]
        public int saveStock(Stock stock)
        {
            return dal.guardarStok(stock);
        }
        [HttpGet]
        public JsonResult getStockById(int id)
        {
            return Json(dal.obtenerStock(id));
        }
        [HttpGet]
        public int deleteStock(int id)
        {
            return dal.eliminar(id);
        }
        
    }
}
