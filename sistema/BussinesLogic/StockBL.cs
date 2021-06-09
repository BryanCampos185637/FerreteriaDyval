using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class StockBL
    {
        StockDAL dal = new StockDAL();
        public static int existStock(Stock stock)
        {
            return StockDAL.ExistStock(stock);
        }
        public List<Stock> listarStock()
        {
            return dal.ListarStock();
        }
        public int guardarStok(Stock stock)
        {
            return dal.GuardarStok(stock);
        }
        public Stock obtenerStock(int id)
        {
            return dal.ObtenerStock(id);
        }
        public int eliminar(int id)
        {
            return dal.Eliminar(id);
        }
    }
}
