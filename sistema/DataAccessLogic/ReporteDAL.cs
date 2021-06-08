using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class ReporteDAL
    {
        public static List<ListFactura>CrearListadoReporteVenta(string desde, string hasta)
        {
            List<ListFactura> lstFactura = new List<ListFactura>();
            using (var db = new BDFERRETERIAContext())
            {
                if (desde != null && hasta != null)// si los dos parametros vienen llenos
                {
                    lstFactura = (from factura in db.Factura
                                  join usuario in db.Usuario on
                                  factura.Iidusuario equals usuario.Iidusuario
                                  join empleado in db.Empleado on
                                  usuario.Iidempleado equals empleado.Iidempleado
                                  where factura.Bhabilitado == "A"
                                  && factura.Fechacreacion >= Convert.ToDateTime(desde)
                                  && factura.Fechacreacion <= Convert.ToDateTime(hasta)
                                  select new ListFactura
                                  {
                                      iidfactura = factura.Iidfactura,
                                      iidusuario = factura.Iidusuario,
                                      nombrevendedor = empleado.Nombrecompleto,
                                      tipocomprador = factura.Tipocomprador,
                                      nombrecomprador = factura.Nombrecliente,
                                      direccion = factura.Direccion,
                                      registro = factura.Registro,
                                      giro = factura.Giro,
                                      nit = factura.Nit,
                                      nofactura = factura.Nofactura,
                                      total = (decimal)factura.Total,
                                      fechaemitida = factura.Fechacreacion.ToShortDateString(),
                                      porcentajedescuento = factura.Porcentajedescuentoglobal,
                                      descuentogeneral = factura.Descuentoglobal,
                                      efectivo = (decimal)factura.Efectivo,
                                      cambio = (decimal)factura.Cambio
                                  }).ToList();
                }
                else if (desde != null && hasta == null)//reporte de dia especifico
                {
                    lstFactura = (from factura in db.Factura
                                  join usuario in db.Usuario on
                                  factura.Iidusuario equals usuario.Iidusuario
                                  join empleado in db.Empleado on
                                  usuario.Iidempleado equals empleado.Iidempleado
                                  where factura.Bhabilitado == "A"
                                  && factura.Fechacreacion >= Convert.ToDateTime(desde)
                                  && factura.Fechacreacion <= Convert.ToDateTime(desde)
                                  select new ListFactura
                                  {
                                      iidfactura = factura.Iidfactura,
                                      iidusuario = factura.Iidusuario,
                                      nombrevendedor = empleado.Nombrecompleto,
                                      tipocomprador = factura.Tipocomprador,
                                      nombrecomprador = factura.Nombrecliente,
                                      direccion = factura.Direccion,
                                      registro = factura.Registro,
                                      giro = factura.Giro,
                                      nit = factura.Nit,
                                      nofactura = factura.Nofactura,
                                      total = (decimal)factura.Total,
                                      fechaemitida = factura.Fechacreacion.ToShortDateString(),
                                      porcentajedescuento = factura.Porcentajedescuentoglobal,
                                      descuentogeneral = factura.Descuentoglobal,
                                      efectivo = (decimal)factura.Efectivo,
                                      cambio = (decimal)factura.Cambio
                                  }).ToList();
                }
                else//reporte general
                {
                    lstFactura = (from factura in db.Factura
                                  join usuario in db.Usuario on
                                  factura.Iidusuario equals usuario.Iidusuario
                                  join empleado in db.Empleado on
                                  usuario.Iidempleado equals empleado.Iidempleado
                                  where factura.Bhabilitado == "A"
                                  select new ListFactura
                                  {
                                      iidfactura = factura.Iidfactura,
                                      iidusuario = factura.Iidusuario,
                                      nombrevendedor = empleado.Nombrecompleto,
                                      tipocomprador = factura.Tipocomprador,
                                      nombrecomprador = factura.Nombrecliente,
                                      direccion = factura.Direccion,
                                      registro = factura.Registro,
                                      giro = factura.Giro,
                                      nit = factura.Nit,
                                      nofactura = factura.Nofactura,
                                      total = (decimal)factura.Total,
                                      fechaemitida = factura.Fechacreacion.ToShortDateString(),
                                      porcentajedescuento = factura.Porcentajedescuentoglobal,
                                      descuentogeneral = factura.Descuentoglobal,
                                      efectivo = (decimal)factura.Efectivo,
                                      cambio = (decimal)factura.Cambio
                                  }).ToList();
                }
            }
            return lstFactura;
        }
        public static List<ListReporteInventario> ObtenerProveedorReporteInventario(BDFERRETERIAContext db, List<ListReporteInventario> lst)
        {
            #region recorremos la lista para obtener los proveedores de los productos
            if (lst.Count > 0)
            {
                int totalRgistros = lst.Count, filaActual=0;
                foreach (var item in lst)
                {
                    filaActual++;
                    string arregloProveedores = "";
                    var idProducto = item.Iidproducto;//capturamos el id del producto
                    var ListaEntradas = db.Entrada.Where(p => p.Iidproducto.Equals(idProducto)).ToList();//buscamos el la base de datos si se ha hecho alguna entrada
                    if (ListaEntradas.Count > 0)
                    {
                        foreach (var lstEntrada in ListaEntradas)
                        {
                            var proveedorActual = lstEntrada.Proveedor;
                            if (proveedorActual != null && !arregloProveedores.Contains(proveedorActual))
                            {
                                if (filaActual == totalRgistros)
                                    arregloProveedores += proveedorActual + ".";
                                else
                                    arregloProveedores += proveedorActual + ", ";
                            }
                        }
                        item.Proveedor = arregloProveedores;
                    }
                }
            }
            return lst;
            #endregion
        }
        public static List<DetalleVenta> ObtenerListadoDetalleCotizacion(BDFERRETERIAContext db, long id)
        {
            return (from detalle in db.Detallecotizacion
             join producto in db.Producto on
             detalle.Iidproducto equals producto.Iidproducto
             join unidad in db.Unidadmedida on
             producto.Iidunidadmedida equals unidad.Iidunidadmedida
             where detalle.Iidcotizacion == id
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
                 Nombresubunidad = UtilidadesController.ObtenerNombreSubUnidad(producto.Subunidad),
                 subproducto = detalle.Essubproducto,
                 iva = (decimal)producto.Iva,
                 codigoproducto = producto.Codigoproducto,
                 subiva = producto.Subiva
             }).ToList();//listamos el detalle del pedido
        }
        public static Cotizacion ObtenerDetalleCotizacion(BDFERRETERIAContext db, long id)
        {
            return (from f in db.Cotizacion
             where f.Iidcotizacion == id
             select new Cotizacion
             {
                 Iidcotizacion = f.Iidcotizacion,
                 Nocotizacion = f.Nocotizacion,
                 Fechacreacion = f.Fechacreacion,
                 Fechavencimiento = f.Fechavencimiento,
                 Iidusuario = f.Iidusuario,
                 Total = f.Total
             }).First();//obtenemos el objeto de la factura
        }
        public static List<DetalleVenta> ObtenerListadoDetalleFactura(BDFERRETERIAContext db, long id)
        {
            return (from detalle in db.Detallepedido
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
                        codigoproducto = producto.Codigoproducto,
                        subiva = producto.Subiva
                    }).ToList();//listamos el detalle del pedido
        }
        public static Factura ObtenerDetalleFactura(BDFERRETERIAContext db, long id)
        {
            return (from f in db.Factura
                    where f.Iidfactura == id
                    select new Factura
                    {
                        Iidusuario = f.Iidusuario,
                        Iidfactura = f.Iidfactura,
                        Nofactura = f.Nofactura,
                        Tipocomprador = f.Tipocomprador.ToUpper(),
                        Nombrecliente = f.Nombrecliente.ToUpper(),
                        Direccion = f.Direccion.ToUpper() == null ? "" : f.Direccion,
                        Registro = f.Registro.ToUpper() == null ? "" : f.Registro,
                        Giro = f.Giro.ToUpper() == null ? "" : f.Giro,
                        Nit = f.Nit == null ? "" : f.Nit,
                        Fechacreacion = f.Fechacreacion,
                        Total = f.Total,
                        Descuentoglobal = f.Descuentoglobal,
                        Porcentajedescuentoglobal = f.Porcentajedescuentoglobal
                    }).First();//obtenemos el objeto de la factura
        }
    }
}
