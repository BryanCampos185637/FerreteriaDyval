using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class ReporteBL
    {
        public static List<ListFactura> CrearListaReporteVenta(string desde, string hasta)
        {
            return ReporteDAL.CrearListadoReporteVenta(desde, hasta);
        }
        public static List<ListReporteInventario> ObtenerProveedoresReporteInventario(BDFERRETERIAContext db, List<ListReporteInventario> lst)
        {
            return ReporteDAL.ObtenerProveedorReporteInventario(db, lst);
        }
        public static List<DetalleVenta> ObtenerListaDetalleCotizacion(BDFERRETERIAContext db, long id)
        {
            return ReporteDAL.ObtenerListadoDetalleCotizacion(db, id);
        }
        public static Cotizacion obtenerDetalleCotizacion(BDFERRETERIAContext db, long id)
        {
            return ReporteDAL.ObtenerDetalleCotizacion(db, id);
        }
        public static List<DetalleVenta> ObtenerListaDetalleFactura(BDFERRETERIAContext db, long id)
        {
            return ReporteDAL.ObtenerListadoDetalleFactura(db, id);
        }
        public static Factura obtenerDetalleFactura(BDFERRETERIAContext db, long id)
        {
            return ReporteDAL.ObtenerDetalleFactura(db, id);
        }
    }
}
