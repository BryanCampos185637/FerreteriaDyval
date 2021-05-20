using AdminFerreteria.DAL;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class StockController : Controller
    {
        StockDAL dal = new StockDAL();
        [ServiceFilter(typeof(FiltroDePaginaTipoUsuario))]
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
