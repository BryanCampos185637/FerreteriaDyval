using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class StockDAL
    {
        public List<Stock> ListarStock()
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = db.Stock.Where(p => p.Bhabilitado == "A").ToList();
                return lst;
            }
        }
        public int GuardarStok(Stock stock)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    int nveces = UtilidadesController.existStock(stock);
                    if (nveces == 0)
                    {
                        if (stock.Iidstock == 0)
                        {
                            stock.Fechacreacion = DateTime.Now;
                            db.Stock.Add(stock);
                        }
                        else
                        {
                            var data = db.Stock.Where(p => p.Iidstock == stock.Iidstock).First();
                            data.Nombrestock = stock.Nombrestock;
                        }
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public Stock ObtenerStock(int id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = db.Stock.Where(p => p.Iidstock == id).First();
                return lst;
            }
        }
        public int Eliminar(int id)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var lst = db.Stock.Where(p => p.Iidstock == id).First();
                    lst.Bhabilitado = "D";
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static int ExistStock(Stock stock)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Stock.Where(p => p.Iidstock != stock.Iidstock &&
                    p.Nombrestock == stock.Nombrestock && p.Bhabilitado == "A").Count();
            }
        }
    }
}
