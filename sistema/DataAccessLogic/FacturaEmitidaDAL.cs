using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class FacturaEmitidaDAL
    {
        public List<ListFactura> BuscarFacturaPorFechas(string fecha = "")
        {
            using (var db = new BDFERRETERIAContext())
            {
                if (fecha != null)
                {
                    string fechaConFormato = Convert.ToDateTime(fecha).ToString("yyyy-MM");
                    var lst = (from factura in db.Factura
                               join user in db.Usuario on
                               factura.Iidusuario equals user.Iidusuario
                               join empleado in db.Empleado on
                               user.Iidempleado equals empleado.Iidempleado
                               where factura.Bhabilitado == "A"
                               && factura.Fechacreacion.ToString().Contains(fechaConFormato)
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
                else
                {
                    var lst = (from factura in db.Factura
                               join user in db.Usuario on
                               factura.Iidusuario equals user.Iidusuario
                               join empleado in db.Empleado on
                               user.Iidempleado equals empleado.Iidempleado
                               where factura.Bhabilitado == "A"
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
        }
        public List<ListFactura> BuscarFacturasEnEspera()
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = (from factura in db.Factura
                           join user in db.Usuario on
                           factura.Iidusuario equals user.Iidusuario
                           join empleado in db.Empleado on
                           user.Iidempleado equals empleado.Iidempleado
                           where factura.Bhabilitado == "A" && factura.Original == "NO"
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
    }
}
