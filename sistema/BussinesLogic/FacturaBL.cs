using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using System;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class FacturaBL
    {
        FacturaDAL dal = new FacturaDAL();
        public static List<DetalleVenta> obtenerListaFacturaParaReporte(Int64 id)
        {
            return FacturaDAL.ObtenerListaFacturaParaReporte(id);
        }
        public List<ListCotizacion> listarCotizaciones()
        {
            return dal.ListarCotizaciones();
        }
        public List<ListFactura> listarFacturas()
        {
            return dal.ListarFacturas();
        }
        public ListFactura detalleFactura(Int64 id)
        {
            return dal.DetalleFactura(id);
        }
        public List<DetalleVenta> detallePedidoPorIidfactura(Int64 id)
        {
            return dal.DetallePedidoPorIidfactura(id);
        }
        public bool crearFacturaProvisional(long id, decimal efectivo)
        {
            return dal.CrearFacturaProvisional(id, efectivo);
        }
        public Factura obtenerFacturaPorId(long id)
        {
            return dal.ObtenerFacturaPorId(id);
        }
        public List<Detallepedido> obtenerListadoDetallePedidoPorIdFactura(long id)
        {
            return dal.ObtenerListadoDetallePedidoPorIdFactura(id);
        }
        public bool venderProducto(List<Detallepedido> listaProductos)
        {
            return dal.VenderProducto(listaProductos);
        }
        public bool modificarEstadoFactura(Factura dataFactura, decimal efectivo)
        {
            return dal.ModificarEstadoFactura(dataFactura, efectivo);
        }
        public bool cambiarEstadoFacturaImpresa(long id)
        {
            return dal.CambiarEstadoFacturaImpresa(id);
        }
    }
}
