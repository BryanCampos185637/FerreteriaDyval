using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class StockController : Controller
    {
        StockBL bl = new StockBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listStock()
        {
            return Json(bl.listarStock());   
        }
        [HttpPost]
        public int saveStock(Stock stock)
        {
            return bl.guardarStok(stock);
        }
        [HttpGet]
        public JsonResult getStockById(int id)
        {
            return Json(bl.obtenerStock(id));
        }
        [HttpGet]
        public int deleteStock(int id)
        {
            return bl.eliminar(id);
        }
        
    }
}
