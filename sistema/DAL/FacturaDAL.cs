using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using AdminFerreteria.DAL;

namespace AdminFerreteria.DAL
{
    public class FacturaDAL
    {
        public static List<DetalleVenta> obtenerListaFacturaParaReporte(Int64 id)
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
                                 Nombresubunidad = UnidadMedidaDAL.ObtenerNombreSubUnidad(producto.Subunidad),
                                 subproducto = detalle.Essubproducto,
                                 iva = (decimal)producto.Iva,
                                 subiva = (decimal)producto.Subiva
                             }).ToList();//listamos el detalle del pedido
                return lista;
            }
        }

        public List<ListCotizacion> listarCotizaciones()
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
        public List<ListFactura> listarFacturas()
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
        public ListFactura detalleFactura(Int64 id)
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
        public List<DetalleVenta> detallePedidoPorIidfactura(Int64 id)
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
