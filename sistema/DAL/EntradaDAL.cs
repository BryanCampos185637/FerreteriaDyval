using AdminFerreteria.Models;
using AdminFerreteria.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AdminFerreteria.DAL
{
    public class EntradaDAL
    {
        public List<ListEntrada> listarEntrada()
        {
            using (var db = new BDFERRETERIAContext())
            {
                List<ListEntrada> lst = (from entrada in db.Entrada
                                         join producto in db.Producto on
                                         entrada.Iidproducto equals producto.Iidproducto
                                         where entrada.Bhabilitado == "A"
                                         select new ListEntrada
                                         {
                                             iidentrada = entrada.Iidentrada,
                                             descripcionproducto = producto.Descripcion,
                                             existencias = entrada.Existenciasproducto,
                                             fechaexpedicionccf = entrada.Fechaexpedicionccf == null ? "SIN FACTURA" : entrada.Fechaexpedicionccf.Value.ToShortDateString(),
                                             fechainiciocredito = entrada.Fechainiciocredito == null ? "SIN FACTURA" : entrada.Fechainiciocredito.Value.ToShortDateString(),
                                             fechavencimiento = entrada.Fechavencimiento == null ? "SIN FACTURA" : entrada.Fechavencimiento.Value.ToShortDateString(),
                                             entrada = entrada.Cantidad,
                                             proveedor = entrada.Proveedor,
                                             preciocompra = (decimal)entrada.Preciocompra
                                         }).ToList();
                return lst;
            }
        }
        public List<Bitacoraentrada> obtenerListaBitacora(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Bitacoraentrada.Where(p => p.Iidentrada == id).ToList();
            }
        }
        public List<Stock> listarStock()
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = db.Stock.Where(p => p.Bhabilitado == "A").Select(p => new Stock
                {
                    Iidstock = p.Iidstock,
                    Nombrestock = p.Nombrestock
                }).ToList();
                return lst;
            }
        }
        /*
         elimina una entrada y los incrementos de las existencias que hubo en la sala de venta y bodegas
         */
        public int eliminarEntrada(Int64 id)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    using (var transaction = new TransactionScope())
                    {
                        #region eliminacion de la entrada
                        Entrada oEntrada = db.Entrada.Where(p => p.Iidentrada.Equals(id)).First();
                        oEntrada.Bhabilitado = "D";
                        db.SaveChanges();
                        #endregion
                        #region eliminacion del aumento en existencias
                        var bitacotaDeEntrada = obtenerListaBitacora(id);
                        for (int i = 0; i < bitacotaDeEntrada.Count(); i++)
                        {
                            if (bitacotaDeEntrada[i].Iidbodega == -1)//si es -1 es porque la entrada se hizo en la sala de venta
                            {
                                var producto = db.Producto.Where(p => p.Iidproducto == bitacotaDeEntrada[i].Iidproducto).First();
                                long? reducirExistencia = producto.Existencias - bitacotaDeEntrada[i].Cantidad;//hace la resta de existencia
                                producto.Existencias = reducirExistencia;
                                if (producto.Subexistencia != null)//si el producto tiene un subproducto
                                {
                                    //debe restar tambien en su sub-existencia
                                    decimal? reducirSubExistencia = producto.Subexistencia - bitacotaDeEntrada[i].Subcantidad;
                                    producto.Subexistencia = reducirSubExistencia;
                                }
                                db.SaveChanges();
                            }
                            else//si es mayor a 0 es una entrada en bodega
                            {
                                var inventario = db.Inventario.Where(p => p.Iidbodega == bitacotaDeEntrada[i].Iidbodega
                                && p.Iidproducto == bitacotaDeEntrada[i].Iidproducto && p.Iidstock == bitacotaDeEntrada[i].Iidstock).First();
                                inventario.Cantidad -= bitacotaDeEntrada[i].Cantidad;
                                db.SaveChanges();
                            }
                        }
                        #endregion

                        transaction.Complete();
                    }
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
