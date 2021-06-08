using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AdminFerreteria.DataAccessLogic
{
    public class CotizacionDAL
    {
        public int GuardarCotizaciones(string nombre, int tipodocumento, List<DetalleVenta> lstDetalleVenta,int sesion)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    do
                    {
                        using (var db = new BDFERRETERIAContext())
                        {
                            var configuracion = db.Configuracion.Where(p => p.Iidconfiguracion == 1).FirstOrDefault();
                            //validamos que aun queda num de cotizacion
                            if (configuracion != null && configuracion.Noactualcotizacion <= configuracion.Fincotizacion)
                            {
                                #region creamos la cotizacion
                                var cotizacion = UtilidadesController.crearCotizacion(nombre, sesion,
                                    tipodocumento, configuracion.Noactualcotizacion.ToString(), configuracion.Nodigitoscotizacion);
                                db.Cotizacion.Add(cotizacion);
                                db.SaveChanges();
                                #endregion

                                #region utilizamos la lista de los productos para crear el detalle de la cotizacion en la bd
                                decimal totalCotizacion = 0;
                                for (int i = 0; i < 26; i++)
                                {
                                    if (lstDetalleVenta.Count() > 0)
                                    {
                                        Detallecotizacion oDetalle = UtilidadesController.crearDetalleCotizacion(lstDetalleVenta, cotizacion);
                                        db.Detallecotizacion.Add(oDetalle);
                                        db.SaveChanges();
                                        totalCotizacion += lstDetalleVenta[0].total;
                                        lstDetalleVenta.Remove(lstDetalleVenta[0]);
                                    }
                                    else
                                    {
                                        i = 26;
                                    }
                                }
                                cotizacion.Total = totalCotizacion;
                                db.SaveChanges();//y guardamos
                                if (tipodocumento == 1)// solo si es cotizacion se hace el descuento en el sistema
                                {
                                    configuracion.Noactualcotizacion = configuracion.Noactualcotizacion + 1;
                                    db.SaveChanges();//una vez guardada la factura incrementamos el contador
                                }
                                #endregion
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    } while (lstDetalleVenta.Count() > 0);
                    transaction.Complete();//completamos la transaccion
                    return 1;
                }
            }
            catch (Exception e)
            {
                string msj = e.Message;
                return 0;
            }
        }
    }
}
