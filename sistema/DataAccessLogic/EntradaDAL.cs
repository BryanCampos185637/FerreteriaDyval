using AdminFerreteria.Controllers;
using AdminFerreteria.Helper.HelperCalculoPrecio;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AdminFerreteria.DataAccessLogic
{
    public class EntradaDAL
    {
        public List<ListEntrada> ListarEntrada()
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
        public List<Bitacoraentrada> ObtenerListaBitacora(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Bitacoraentrada.Where(p => p.Iidentrada == id).ToList();
            }
        }
        public List<Stock> ListarStock()
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
        public int EliminarEntrada(Int64 id)
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
                        var bitacotaDeEntrada = ObtenerListaBitacora(id);
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

        public int GuardarEntrada(Entrada entrada, Int64[] bodegas, Int64[] cantidades, Int64 ventas, Int64[] stock, decimal precioCompra)
        {
            using (var db = new BDFERRETERIAContext())
            {
                using (var transaction = new TransactionScope())
                {
                    if (entrada.Iidentrada == 0)
                    {
                        entrada.Fechacreacion = DateTime.Now;
                        entrada.Preciocompra = precioCompra;
                        db.Entrada.Add(entrada);
                        db.SaveChanges();
                        Producto producto = db.Producto.Where(p => p.Iidproducto == entrada.Iidproducto).First();
                        if (producto.Preciocompra != precioCompra)//si el precio de compra es diferente al actual entonces modificaremos
                        {
                            producto.Preciocompra = precioCompra;
                            producto = CalcularPrecioProducto.calcular(producto);
                            db.SaveChanges();
                        }
                        if (ventas > 0)//si ventas es mayor a 0 es porque se agregaran productos a la sala de venta
                        {
                            //vamos a modificar el producto ya que entraron productos
                            Int64 existenciasAumentadas = (Int64)producto.Existencias + ventas;
                            producto.Existencias = existenciasAumentadas;
                            decimal? subexistencia = 0;
                            if (producto.Subunidad != null)//si tiene una subunidad se le saca el porcentaje
                            {
                                subexistencia = ventas * producto.Equivalencia;//obtenemos cuantas partes entraron del producto original
                                if (producto.Subexistencia == null)// si es null solo agregamos el nuevo datos
                                {
                                    producto.Subexistencia = subexistencia;
                                }
                                else//si ya hay un registro entonces sumamos lo que hay con lo nuevo
                                {
                                    producto.Subexistencia = producto.Subexistencia + subexistencia;//y se las sumamos
                                }
                            }
                            #region creamos la bitacora de este producto
                            //-1 para mi significara que es la sala de venta
                            var bitacoraProducto = UtilidadesController.crearBitacora(-1, producto.Iidproducto,
                                existenciasAumentadas, -1, (decimal)subexistencia, entrada.Iidentrada);
                            db.Bitacoraentrada.Add(bitacoraProducto);
                            db.SaveChanges();
                            #endregion
                        }
                        //empezamos aguardar los datos de la entrada
                        for (int i = 0; i < bodegas.Length; i++)
                        {
                            var nveces = UtilidadesController.yaExiteEsteInventario((int)bodegas[i], (int)entrada.Iidproducto);
                            if (nveces > 0) //si ya existe este registro entonces solo aumentamos la cantidad en esa bodega
                            {
                                var data = db.Inventario.Where(p => p.Iidbodega == (int)bodegas[i] &&
                                    p.Iidproducto == (int)entrada.Iidproducto && p.Bhabilitado == "A").First();
                                data.Cantidad += cantidades[i];
                                data.Bhabilitado = "A";
                                db.SaveChanges();
                            }
                            else//si no tenemos que crear el registro
                            {
                                Inventario inventario = UtilidadesController.crearObjetInventario((int)bodegas[i],
                                    (int)entrada.Iidproducto, (int)cantidades[i], (int)stock[i]);
                                db.Inventario.Add(inventario);
                                db.SaveChanges();
                            }
                            #region creamos la bitacora de la entrada hacia las bodegas
                            //-1 para mi significara que es la sala de venta
                            var bitacora = UtilidadesController.crearBitacora((int)bodegas[i], (int)entrada.Iidproducto,
                               (int)cantidades[i], (int)stock[i], 0, entrada.Iidentrada);
                            db.Bitacoraentrada.Add(bitacora);
                            db.SaveChanges();
                            #endregion
                        }
                        db.SaveChanges();
                    }
                    transaction.Complete();
                    return 1;
                }
            }
        }
    }
}
