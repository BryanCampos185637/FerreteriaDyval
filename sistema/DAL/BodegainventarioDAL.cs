using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AdminFerreteria.DAL
{
    public class BodegainventarioDAL
    {
        public string guardarBodega(Bodega bodega)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var nveces = db.Bodega.Where(p => p.Nombrebodega.Equals(bodega.Nombrebodega)
                    && p.Iidbodega != bodega.Iidbodega && p.Bhabilitado == "A").Count();
                    if (nveces == 0)
                    {
                        if (bodega.Iidbodega == 0)
                        {
                            db.Bodega.Add(bodega);
                            db.SaveChanges();
                            
                        }
                        else
                        {
                            var data = db.Bodega.Where(p => p.Iidbodega == bodega.Iidbodega).First();
                            data.Nombrebodega = bodega.Nombrebodega;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        return "rept";
                    }
                }
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public List<Bodega> listarBodega()
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = db.Bodega.Where(p => p.Bhabilitado == "A").ToList();
                return lst;
            }
        }
        public List<ListInventario> listarInventario()
        {
            List<ListInventario> lst = new List<ListInventario>();
            using (var db = new BDFERRETERIAContext())
            {
                lst = db.Inventario.Where(p => p.Bhabilitado == "A")
                    .Include(p => p.IidbodegaNavigation).Include(p => p.IidproductoNavigation)
                    .Include(p => p.IidstockNavigation)
                    .Select(p => new ListInventario
                    {
                        Iidinventario = p.Iidinventario,
                        Nombreproducto = p.IidproductoNavigation.Descripcion,
                        Nombrebodega = p.IidbodegaNavigation.Nombrebodega,
                        Cantidad = p.Cantidad,
                        Codigoproducto = p.IidproductoNavigation.Codigoproducto,
                        Iidbodega = p.Iidbodega,
                        Iidstock = p.Iidstock,
                        Nombrestock = p.IidstockNavigation.Nombrestock
                    }).ToList();
                return lst;
            }
        }
        public Bodega obtenerBodega(int id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var data = db.Bodega.Where(p => p.Iidbodega == id).First();
                return data;
            }
        }
        public List<ListProducto> listarProductos()
        {
            using (var db = new BDFERRETERIAContext())
            {
                List<ListProducto> lst = (from product in db.Producto
                                          join stock in db.Stock on
                                          product.Iidstock equals stock.Iidstock
                                          join unidad in db.Unidadmedida on
                                          product.Iidunidadmedida equals unidad.Iidunidadmedida
                                          where product.Bhabilitado == "A"
                                          select new ListProducto
                                          {
                                              Iidproducto = product.Iidproducto,
                                              Codigoproducto = product.Codigoproducto,
                                              Descripcion = product.Descripcion,
                                              Existencias = product.Existencias,
                                              Nombrestock = stock.Nombrestock,
                                          }).ToList();
                lst = lst.OrderByDescending(p => p.Iidproducto).ToList();
                return lst;
            }
        }
        public string modificarExistenciasSalaDeVenta(Producto producto)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var data = db.Producto.Where(p => p.Iidproducto == producto.Iidproducto).First();
                    data.Existencias = producto.Existencias;
                    if (data.Subunidad != null)
                    {
                        decimal totalSubProducto = (decimal)(producto.Existencias * data.Equivalencia);
                        data.Subexistencia = totalSubProducto;
                    }
                    db.SaveChanges();
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string editarExistenciasInventario(Int64 id, Int64 cantidad)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var data = db.Inventario.Where(p => p.Iidinventario == id).First();
                    data.Cantidad = cantidad;
                    db.SaveChanges();
                    return "ok";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string eliminarInventario(Int64 id)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var data = db.Inventario.Where(p => p.Iidinventario == id).First();
                    db.Inventario.Remove(data);
                    db.SaveChanges();
                    return "ok";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public List<Bodega> listarBodegaDiferenteDelParametroId(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = db.Bodega.Where(p => p.Bhabilitado == "A" && p.Iidbodega != id).ToList();
                return lst;
            }
        }
        public string moverproducto(Int64 cantidad, Int64 bodegaActual, Int64 producto, Int64 ubicacionnueva, int stock)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        Inventario bodegaDondeSeMovera = db.Inventario.Where(p => p.Iidbodega == ubicacionnueva
                                                      && p.Iidproducto == producto && p.Bhabilitado == "A").FirstOrDefault();
                        if (bodegaDondeSeMovera != null && ubicacionnueva != -1)//si existe la bodega con el producto y la ubicacion no es -1
                        {
                            #region se aumenta la cantidad al registro que coincida con el id del producto y el id de la bodega a donde se envia la cantidad
                            bodegaDondeSeMovera.Cantidad += cantidad;
                            db.SaveChanges();
                            #endregion

                            #region se descuenta del inventario anterior
                            var inventarioActual = db.Inventario.Where(p => p.Iidbodega == bodegaActual
                                                   && p.Iidproducto == producto && p.Bhabilitado == "A").FirstOrDefault();
                            inventarioActual.Cantidad -= cantidad;
                            db.SaveChanges();
                            #endregion

                            transaction.Complete();
                            return "ok";
                        }
                        else if (ubicacionnueva == -1) //se mueve a la sala de venta
                        {
                            #region se aumenta la cantidad al registro que coincida con el id del producto
                            var productoEncontrado = db.Producto.Where(p => p.Iidproducto == producto).First();
                            productoEncontrado.Existencias += cantidad;
                            if (productoEncontrado.Subunidad != null)//si tiene una subunidad se le saca el porcentaje
                            {
                                var subexistencia = cantidad * productoEncontrado.Equivalencia;//obtenemos cuantas partes entraron del producto original
                                if (productoEncontrado.Subexistencia == null)// si es null solo agregamos el nuevo datos
                                {
                                    productoEncontrado.Subexistencia = subexistencia;
                                }
                                else//si ya hay un registro entonces sumamos lo que hay con lo nuevo
                                {
                                    productoEncontrado.Subexistencia += subexistencia;//y se las sumamos
                                }
                            }
                            db.SaveChanges();
                            #endregion

                            #region se descuenta del inventario anterior
                            var inventarioActual = db.Inventario.Where(p => p.Iidbodega == bodegaActual
                                                   && p.Iidproducto == producto && p.Bhabilitado == "A").FirstOrDefault();
                            inventarioActual.Cantidad -= cantidad;
                            db.SaveChanges();
                            #endregion

                            transaction.Complete();
                            return "ok";
                        }
                        else //se crea un nuevo inventario utilizando los datos recibidos
                        {
                            #region creacion del nuevo inventario
                            Inventario inventarioNuevo = new Inventario();
                            inventarioNuevo.Bhabilitado = "A";
                            inventarioNuevo.Cantidad = cantidad;
                            inventarioNuevo.Iidbodega = (int)ubicacionnueva;
                            inventarioNuevo.Iidproducto = (int)producto;
                            inventarioNuevo.Iidstock = stock;
                            db.Inventario.Add(inventarioNuevo);
                            db.SaveChanges();
                            #endregion

                            #region descuento del inventario anterior
                            var inventarioActual = db.Inventario.Where(p => p.Iidbodega == bodegaActual
                                                   && p.Iidproducto == producto && p.Bhabilitado == "A").FirstOrDefault();
                            inventarioActual.Cantidad -= cantidad;
                            db.SaveChanges();
                            #endregion;

                            transaction.Complete();
                            return "ok";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public ListInventario obtenerInventario(int id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = db.Inventario.Where(p => p.Iidinventario == id)
                    .Include(p => p.IidbodegaNavigation).Include(p => p.IidproductoNavigation)
                    .Select(p => new ListInventario
                    {
                        Iidinventario = p.Iidinventario,
                        Nombreproducto = p.IidproductoNavigation.Descripcion,
                        Nombrebodega = p.IidbodegaNavigation.Nombrebodega,
                        Cantidad = p.Cantidad,
                        Codigoproducto = p.IidproductoNavigation.Codigoproducto,
                        Iidbodega = p.Iidbodega,
                        Iidproducto = p.Iidproducto,
                        Iidstock = p.Iidstock,
                        Nombrestock = p.IidstockNavigation.Nombrestock
                    }).First();
                return lst;
            }
        }
        public string eliminarBodega(int id)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    if (UtilidadesController.BodegaEnUso(id) == 0)
                    {
                        var data = db.Bodega.Where(p => p.Iidbodega == id).First();
                        data.Bhabilitado = "D";
                        db.SaveChanges();
                        return "ok";
                    }
                    else
                    {
                        return "utilizada";
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
