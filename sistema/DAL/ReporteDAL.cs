using AdminFerreteria.Models;
using AdminFerreteria.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DAL
{
    public class ReporteDAL
    {
        public static List<ListFactura>CrearListaReporteVenta(string desde, string hasta)
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
        public static List<ListReporteInventario> ObtenerProveedoresReporteInventario(BDFERRETERIAContext db, List<ListReporteInventario> lst)
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
    }
}
