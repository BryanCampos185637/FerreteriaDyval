using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class StockController : Controller
    {
        StockBL dal = new StockBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
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
