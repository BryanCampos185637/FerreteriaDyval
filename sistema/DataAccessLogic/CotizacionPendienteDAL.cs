using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class CotizacionPendienteDAL
    {
        public List<DetalleVenta> ListarDetalleCotizacion(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                List<DetalleVenta> detalleCotizacion = (from cotizacion in db.Cotizacion
                                                        join dt in db.Detallecotizacion on
                                                        cotizacion.Iidcotizacion equals dt.Iidcotizacion
                                                        join producto in db.Producto on
                                                        dt.Iidproducto equals producto.Iidproducto
                                                        join unida in db.Unidadmedida on
                                                        producto.Iidunidadmedida equals unida.Iidunidadmedida
                                                        where dt.Bhabilitado == "A" && dt.Iidcotizacion == id
                                                        select new DetalleVenta
                                                        {
                                                            iidcotizacion = cotizacion.Iidcotizacion,
                                                            iidproducto = producto.Iidproducto,
                                                            preciounitario = (decimal)dt.Precioactual,
                                                            cantidad = dt.Cantidad,
                                                            descuento = (decimal)dt.Descuento,
                                                            pdescuento = dt.Porcentajedescuento,
                                                            pcomision = dt.Porcentajecomision,
                                                            comision = (decimal)dt.Comision,
                                                            nombreproducto = producto.Descripcion,
                                                            total = (decimal)dt.Subtotal,
                                                            unidadmedida = unida.Nombreunidad,
                                                            nocotizacion = cotizacion.Nocotizacion,
                                                            Nombresubunidad = UtilidadesController.ObtenerNombreSubUnidad(producto.Subunidad),
                                                            subproducto = dt.Essubproducto
                                                        }).ToList();

                return detalleCotizacion;
            }
        }
        public Cotizacion ObtenerCotizacionPorId(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var cotizacion = db.Cotizacion.Where(p => p.Iidcotizacion == id).FirstOrDefault();//configuraciones del sistema
                return cotizacion;
            }
        }
        public List<ListCotizacion> ListarCotizacion()
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = (from cotizacion in db.Cotizacion
                           join usuario in db.Usuario on
                           cotizacion.Iidusuario equals usuario.Iidusuario
                           join empleado in db.Empleado on
                           usuario.Iidempleado equals empleado.Iidempleado
                           where cotizacion.Bhabilitado == "A" &&
                           cotizacion.Cotizacionfacturada == "N" &&
                           cotizacion.Cotizacionemitida == "SI" &&
                           cotizacion.Fechavencimiento > DateTime.Now
                           select new ListCotizacion
                           {
                               iidcotizacion = cotizacion.Iidcotizacion,
                               iidusuario = cotizacion.Iidusuario,
                               nocotizacion = cotizacion.Nocotizacion,
                               fechacreacion = cotizacion.Fechacreacion.ToShortDateString(),
                               fechavencimiento = cotizacion.Fechavencimiento.ToShortDateString(),
                               cotizacionfacturada = cotizacion.Cotizacionfacturada,
                               total = (decimal)cotizacion.Total,
                               nombreusuario = empleado.Nombrecompleto,
                               nombrecliente = cotizacion.Nombrecliente
                           }).ToList();
                return lst;
            }
        }
        public Cotizacion ObtenerNumeroCotizacion(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var cotizacion = db.Cotizacion.Where(p => p.Iidcotizacion == id).FirstOrDefault();
                return cotizacion;
            }
        }
        public bool EliminarProductoDeLaCotizacion(Int64 id, Int64 idCotizacion)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var data = db.Detallecotizacion.Where(p => p.Iidproducto.Equals(id) && p.Iidcotizacion.Equals(idCotizacion)).FirstOrDefault();//obtenemos la data del producto
                    db.Detallecotizacion.Remove(data);//eliminamos el producto de la lista
                    db.SaveChanges();//guardamos los cambios
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public object GuardarNuevoProducto(Int64 iiproducto, int? descuento, int? comision, Int64 cantidad, Int64 idCotizacion, int? Essubproducto)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    decimal tcomision = 0, tdescuento = 0, Totalproducto = 0;
                    var item = db.Producto.Where(p => p.Iidproducto.Equals(iiproducto)).FirstOrDefault();
                    //calculos del precio comision y descuento
                    if (Essubproducto == 0 || Essubproducto == null)//si la subUnidad viene en valor 0 es porque se le hara el calculo con el original
                    {
                        //hacemos los calculos para obtener la comision y el descuento                                                                                 
                        tcomision = Convert.ToDecimal((item.Precioventa / 100) * comision);//saber la comision
                        if (comision > 0)//si la comision es mayor se obtiene el descuento del precio de venta + la comision
                        {
                            decimal precioConComision = (decimal)item.Precioventa + tcomision;
                            tdescuento = Convert.ToDecimal(((precioConComision / 100) * descuento));
                        }
                        else//si no es asi solo se le descuenta al precio de venta
                        {
                            tdescuento = Convert.ToDecimal((item.Precioventa / 100) * descuento);//saber el descuento
                        }
                        Totalproducto = Convert.ToDecimal(((item.Precioventa + tcomision) - tdescuento) * cantidad);//obtenemos el total a pagar 
                    }
                    else//si viene mayor a 0 entonces se le hara calculo con el subprecio
                    {
                        //hacemos los calculos para obtener la comision y el descuento                                                                                 
                        tcomision = Convert.ToDecimal((item.Subprecioventa / 100) * comision);//saber la comision
                        if (comision > 0)//si la comision es mayor se obtiene el descuento del precio de venta + la comision
                        {
                            decimal precioConComision = (decimal)item.Subprecioventa + tcomision;
                            tdescuento = Convert.ToDecimal(((precioConComision / 100) * descuento));
                        }
                        else//si no es asi solo se le descuenta al precio de venta
                        {
                            tdescuento = Convert.ToDecimal((item.Subprecioventa / 100) * descuento);//saber el descuento
                        }
                        Totalproducto = Convert.ToDecimal(((item.Subprecioventa + tcomision) - tdescuento) * cantidad);//obtenemos el total a pagar 
                    }
                    Detallecotizacion oDetalle = new Detallecotizacion();
                    //armamos el objeto a guardar
                    oDetalle.Iidcotizacion = idCotizacion;
                    oDetalle.Iidproducto = item.Iidproducto;
                    oDetalle.Precioactual = item.Precioventa;
                    oDetalle.Cantidad = cantidad;
                    oDetalle.Porcentajedescuento = (int)descuento;
                    oDetalle.Descuento = tdescuento;
                    oDetalle.Porcentajecomision = (int)comision;
                    oDetalle.Comision = tcomision;
                    oDetalle.Subtotal = Totalproducto;
                    oDetalle.Fechavencimiento = DateTime.Now;
                    oDetalle.Bhabilitado = "A";
                    if (Essubproducto > 0 && Essubproducto != null)//SI ES UN SUB PRODUCTO PONEMOS SI
                        oDetalle.Essubproducto = "SI";
                    else//SINO GUARDAMOS NO
                        oDetalle.Essubproducto = "NO";
                    db.Detallecotizacion.Add(oDetalle);
                    db.SaveChanges();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
