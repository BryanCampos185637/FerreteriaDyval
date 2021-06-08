using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.ViewModels;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class CotizacionBL
    {
        CotizacionDAL dal = new CotizacionDAL();
        public int GuardarCotizacion(string nombre, int tipodocumento, List<DetalleVenta> lstDetalleVenta, int sesion)
        {
            return dal.GuardarCotizaciones(nombre, tipodocumento, lstDetalleVenta, sesion);
        }
    }
}
