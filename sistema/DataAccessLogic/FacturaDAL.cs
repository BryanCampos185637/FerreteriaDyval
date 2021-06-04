using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class FacturaDAL
    {
        public bool CambiarEstadoFacturaImpresa(long id)
        {
            try
            {
                using (BDFERRETERIAContext bd = new BDFERRETERIAContext())
                {
                    var data = bd.Cotizacion.Where(p => p.Iidcotizacion == id).First();//OBTENEMOS LA DATA
                    data.Cotizacionemitida = "SI";//MODIFICAMOS SU PROPIEDAD Y LO GUARDAMOS
                    bd.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ModificarEstadoFactura(Factura dataFactura, decimal efectivo)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    dataFactura.Efectivo = efectivo;
                    dataFactura.Cambio = efectivo - dataFactura.Total;
                    dataFactura.Original = "SI";
                    dataFactura.Facturaemitida = "SI";//MODIFICAMOS SU PROPIEDAD Y LO GUARDAMOS
                    db.SaveChanges();//solo si de descontaron guardamos los cambios
                };
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool VenderProducto(List<Detallepedido> listaProductos)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    foreach (var item in listaProductos)
                    {
                        #region armamos el objeto producto para descontar la cantidad
                        Producto oProducto = db.Producto.Where(p => p.Iidproducto.Equals(item.Iidproducto)).First();//buscamos el producto en la bd
                        if (item.Essubproducto == "SI")//si es un subproducto le descontaremos a la sub existencia
                        {
                            oProducto.Subexistencia = oProducto.Subexistencia - item.Cantidad;//descontamos el total de productos
                            if (item.Cantidad > oProducto.Equivalencia)
                            {
                                int contador = Convert.ToInt32(item.Cantidad), residuos = 0;
                                int cantidadDeProductosADescontar = 0; int equivalencia = Convert.ToInt32(oProducto.Equivalencia);
                                while (equivalencia < contador)
                                {
                                    cantidadDeProductosADescontar++;
                                    contador = contador - equivalencia;
                                    residuos = contador;
                                }
                                oProducto.Existencias = oProducto.Existencias - cantidadDeProductosADescontar;
                                oProducto.Restantes = oProducto.Restantes - residuos;
                                if (oProducto.Restantes < 0)
                                {
                                    oProducto.Existencias = oProducto.Existencias - 1;
                                    oProducto.Subexistencia = equivalencia - residuos;
                                }
                                else if (oProducto.Restantes == 0)//si es igual a 0
                                {
                                    oProducto.Restantes = oProducto.Equivalencia;//volvemos a la eqivalencia los restantes
                                    oProducto.Existencias = oProducto.Existencias - 1;//y restamos uno de las existencias
                                }
                                db.SaveChanges();//guardamos producto
                            }
                            else
                            {
                                oProducto.Restantes = oProducto.Restantes - item.Cantidad;//restamos de los restantes 
                                if (oProducto.Restantes <= 0)//si el resultado es menor o igual a 0 
                                {
                                    if (oProducto.Restantes == 0)//si es igual a 0
                                    {
                                        oProducto.Restantes = oProducto.Equivalencia;//volvemos a la eqivalencia los restantes
                                        oProducto.Existencias = oProducto.Existencias - 1;//y restamos uno de las existencias
                                    }
                                    else if (oProducto.Restantes < 0)//si los restantes son menores a 0
                                    {
                                        var restos = oProducto.Restantes * -1;//volvemos a positivos el valor
                                        oProducto.Existencias = oProducto.Existencias - 1;//restamos uno de las existencias
                                        oProducto.Restantes = oProducto.Equivalencia;//volvemos a la eqivalencia los restantes
                                                                                     //ahora el valor de restos es lo que se desconto de la otra pieza
                                        oProducto.Restantes = oProducto.Restantes - restos;//tenemos que descontarselo a la nueva pieza
                                    }
                                }
                                db.SaveChanges();//guardamos producto
                            }
                            db.SaveChanges();
                        }
                        else//si es el original a las existencias
                        {
                            oProducto.Existencias = oProducto.Existencias - item.Cantidad;//descontamos el total de productos
                            var descuentoSubExistencias = oProducto.Equivalencia * item.Cantidad;
                            oProducto.Subexistencia = oProducto.Subexistencia - descuentoSubExistencias;
                            db.SaveChanges();
                        }
                        #endregion
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public List<Detallepedido>ObtenerListadoDetallePedidoPorIdFactura(long id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Detallepedido.Where(p => p.Iidfactura == id).ToList();
            }
        }
        public Factura ObtenerFacturaPorId(long id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Factura.Where(p => p.Iidfactura == id).First();//OBTENEMOS LA DATA
            }
        }
        public bool CrearFacturaProvisional(long id, decimal efectivo)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var data = db.Factura.Where(p => p.Iidfactura == id).First();//OBTENEMOS LA DATA
                    data.Efectivo = efectivo;
                    data.Cambio = efectivo - data.Total;
                    data.Original = "NO";
                    data.Facturaemitida = "SI";//MODIFICAMOS SU PROPIEDAD Y LO GUARDAMOS
                    db.SaveChanges();//solo si de descontaron guardamos los cambios
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<DetalleVenta> ObtenerListaFacturaParaReporte(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lista = (from detalle in db.Detallepedido
                             join producto in db.Producto on
                             detalle.Iidproducto equals producto.Iidproducto
                             join unidad in db.Unidadmedida on
                             producto.Iidunidadmedida equals unidad.Iidunidadmedida
                             where detalle.Iidfactura == id
                             select new DetalleVenta
                             {
                                 cantidad = detalle.Cantidad,
                                 nombreproducto = producto.Descripcion.ToUpper(),
                                 preciounitario = (decimal)detalle.Precioactual,
                                 total = (decimal)detalle.Subtotal,
                                 descuento = (decimal)detalle.Descuento,
                                 pdescuento = detalle.Porcentajedescuento,
                                 comision = (decimal)detalle.Comision,
                                 unidadmedida = unidad.Nombreunidad,
                                 precioActual = (decimal)detalle.Precioactual,
                                 Nombresubunidad = UnidadMedidaDAL.ObtenerNombreDeSubUnidad(producto.Subunidad),
                                 subproducto = detalle.Essubproducto,
                                 iva = (decimal)producto.Iva,
                                 subiva = (decimal)producto.Subiva
                             }).ToList();//listamos el detalle del pedido
                return lista;
            }
        }
        public List<ListCotizacion> ListarCotizaciones()
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = (from cotizacion in db.Cotizacion
                           join user in db.Usuario on
                           cotizacion.Iidusuario equals user.Iidusuario
                           join empleado in db.Empleado on
                           user.Iidempleado equals empleado.Iidempleado
                           where cotizacion.Bhabilitado == "A" && cotizacion.Cotizacionemitida == "NO"
                           select new ListCotizacion
                           {
                               iidcotizacion = cotizacion.Iidcotizacion,
                               nocotizacion = cotizacion.Nocotizacion,
                               nombrecliente = cotizacion.Nombrecliente,
                               nombreusuario = empleado.Nombrecompleto,
                               total = (decimal)cotizacion.Total
                           }).ToList();
                return lst;
            }
        }
        public List<ListFactura> ListarFacturas()
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var lst = (from factura in db.Factura
                               join user in db.Usuario on
                               factura.Iidusuario equals user.Iidusuario
                               join empleado in db.Empleado on
                               user.Iidempleado equals empleado.Iidempleado
                               where factura.Bhabilitado == "A" && factura.Facturaemitida == "NO"
                               && factura.Original == null
                               select new ListFactura
                               {
                                   iidfactura = factura.Iidfactura,
                                   tipocomprador = factura.Tipocomprador,
                                   nombrecomprador = factura.Nombrecliente,
                                   nombrevendedor = empleado.Nombrecompleto,
                                   fechaemitida = factura.Fechacreacion.ToShortDateString(),
                                   total = (decimal)factura.Total,
                                   nofactura = factura.Nofactura
                               }).ToList();
                    return lst;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ListFactura DetalleFactura(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var data = (from factura in db.Factura
                            join user in db.Usuario on
                            factura.Iidusuario equals user.Iidusuario
                            join empleado in db.Empleado on
                            user.Iidempleado equals empleado.Iidempleado
                            where factura.Iidfactura == id
                            select new ListFactura
                            {
                                iidfactura = factura.Iidfactura,
                                tipocomprador = factura.Tipocomprador,
                                nombrecomprador = factura.Nombrecliente,
                                nombrevendedor = empleado.Nombrecompleto,
                                fechaemitida = factura.Fechacreacion.ToShortDateString(),
                                total = (decimal)factura.Total,
                                direccion = factura.Direccion == null ? "" : factura.Direccion,
                                giro = factura.Giro == null ? "" : factura.Giro,
                                nit = factura.Nit == null ? "" : factura.Nit,
                                iidusuario = factura.Iidusuario,
                                nofactura = factura.Nofactura,
                                totalcomision = factura.Totalcomision,
                                totaldescuento = factura.Totaldescuento,
                                porcentajedescuento = factura.Porcentajedescuentoglobal,
                                descuentogeneral = factura.Descuentoglobal,
                                efectivo = (decimal)factura.Efectivo,
                                cambio = (decimal)factura.Cambio,
                                original = factura.Original
                            }).First();
                return data;
            }
        }
        public List<DetalleVenta> DetallePedidoPorIidfactura(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                List<DetalleVenta> lst = (from detalle in db.Detallepedido
                                          join producto in db.Producto on
                                          detalle.Iidproducto equals producto.Iidproducto
                                          join unidad in db.Unidadmedida on
                                          producto.Iidunidadmedida equals unidad.Iidunidadmedida
                                          where detalle.Iidfactura == id
                                          select new DetalleVenta
                                          {
                                              cantidad = detalle.Cantidad,
                                              nombreproducto = producto.Descripcion,
                                              preciounitario = (decimal)detalle.Precioactual,
                                              total = (decimal)detalle.Subtotal,
                                              descuento = (decimal)detalle.Descuento,
                                              comision = (decimal)detalle.Comision,
                                              unidadmedida = unidad.Nombreunidad,
                                              Nombresubunidad = UtilidadesController.ObtenerNombreSubUnidad(producto.Subunidad),
                                              subproducto = detalle.Essubproducto
                                          }).ToList();
                return lst;
            }
        }
    }
}
