using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using System;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class CotizacionPendienteBL
    {
        CotizacionPendienteDAL dal = new CotizacionPendienteDAL();
        public List<DetalleVenta> detalleCotizacion(Int64 id)
        {
            return dal.ListarDetalleCotizacion(id);
        }
        public Cotizacion obtenerCotizacion(Int64 id)
        {
            return dal.ObtenerCotizacionPorId(id);
        }
        public List<ListCotizacion> listarCotizacion()
        {
            return dal.ListarCotizacion();
        }
        public Cotizacion obtenerNumeroCotizacion(Int64 id)
        {
            return dal.ObtenerNumeroCotizacion(id);
        }
        public bool eliminarProductoDeLaCotizacion(Int64 id, Int64 idCotizacion)
        {
            return dal.EliminarProductoDeLaCotizacion(id, idCotizacion);
        }
        public object guardarNuevoProducto(Int64 iiproducto, int? descuento, int? comision, Int64 cantidad, Int64 idCotizacion, int? Essubproducto)
        {
            return dal.GuardarNuevoProducto(iiproducto, descuento, comision, cantidad, idCotizacion, Essubproducto);
        }
    }
}
