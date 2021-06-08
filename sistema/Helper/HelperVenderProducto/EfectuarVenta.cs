using System;
using System.Collections.Generic;
using System.Transactions;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Helper.HelperVenderProducto
{
    public class EfectuarVenta: Controller
    {
        
        public bool crearFacturaOriginal(long id, decimal efectivo)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    FacturaBL facturaBl = new FacturaBL();
                    #region obtener la data
                    var dataFactura = facturaBl.obtenerFacturaPorId(id);//OBTENEMOS LA DATA
                    List<Detallepedido> listaProductos = facturaBl.obtenerListadoDetallePedidoPorIdFactura(dataFactura.Iidfactura);//obtenemos la lista del pedido
                    #endregion

                    #region descontar producto a vender
                    var rptFactura = facturaBl.venderProducto(listaProductos);
                    if (rptFactura == false)
                        return rptFactura;
                    #endregion

                    #region modificar estado de la factura
                    var rptModificarFactura = facturaBl.modificarEstadoFactura(dataFactura, efectivo);
                    if (rptModificarFactura == false)
                        return rptModificarFactura;
                    #endregion

                    transaction.Complete();
                    return true;//RETORNAMOS TRUE
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool crearFacturaProvisional(long id, decimal efectivo)
        {
            try
            {
                FacturaBL facturaBl = new FacturaBL();
                var rpt = facturaBl.crearFacturaProvisional(id, efectivo);
                if (rpt == false)
                    return rpt;
                return true;//RETORNAMOS TRUE
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
