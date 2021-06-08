using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.ViewModels;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class FacturaEmitidaBL
    {
        FacturaEmitidaDAL dal = new FacturaEmitidaDAL();
        public List<ListFactura> buscarFacturaPorFechas(string fecha)
        {
            return dal.BuscarFacturaPorFechas(fecha);
        }
        public List<ListFactura> buscarFacturasEnEspera()
        {
            return dal.BuscarFacturasEnEspera();
        }
    }
}
