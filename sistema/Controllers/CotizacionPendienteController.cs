using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperBitacora;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Helper.HelperSession;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class CotizacionPendienteController : Controller
    {
        private decimal totalProductosNoValidosDescuentoGlobal;
        private decimal totalProductosValidosDescuentoGlobal;
        private decimal totalDescuentoGlobal;
        CotizacionPendienteBL cotizacionPendienteBl = new CotizacionPendienteBL();

        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult lstcotizacion()
        {
            return Json(cotizacionPendienteBl.listarCotizacion());
        }
        [HttpGet]
        public JsonResult getDetalleCotizacionByIdCotizacion(Int64 id)
        {
            var lst = cotizacionPendienteBl.detalleCotizacion(id);
            string listaSerializada = JsonConvert.SerializeObject(lst);
            HttpContext.Session.SetString("lstDetalleCotizacion", listaSerializada);
            return Json(lst);
        }
        [HttpGet]
        public JsonResult ObtenerNumCotizacion(Int64 id)
        {
            return Json(cotizacionPendienteBl.obtenerNumeroCotizacion(id));
        }
        [HttpPost]
        public string confirmVenta(Factura pFactura, Int64 iidcliente, Int64 iidCotizacion)
        {
            try
            {
                using(var transaction = new TransactionScope())
                {
                    var fechaActual = DateTime.Now;
                    var cotizacion = cotizacionPendienteBl.obtenerCotizacion(iidCotizacion);//capturamos la cotizacion
                    if (cotizacion.Fechavencimiento.ToShortDateString() == fechaActual.ToShortDateString() ||
                        cotizacion.Fechavencimiento < fechaActual)//validamos que la fecha de vencimiento no sea la actual
                    {
                        return "vencida";
                    }
                    else//si aun es valida se guarda la factura
                    {
                        #region creacion de variables y se quita la serializacion
                        List<DetalleVenta> lstDetalleVenta = new List<DetalleVenta>();//creamos la variable
                        string listaCookie = HttpContext.Session.GetString("lstDetalleCotizacion");//obtenemos la cadena
                        lstDetalleVenta = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaCookie);//quitamos el serializado
                        pFactura.Iidusuario = (int)HttpContext.Session.GetInt32("UsuarioLogueado");//capturamos el id del usuario que inicio sesion
                        DescuentoGlobal dg = new DescuentoGlobal();
                        string descuentoGlobal = HttpContext.Session.GetString("DescuentoGlobal");
                        #endregion

                        #region guardar factura o comprobante
                        do
                        {
                            using (var db = new BDFERRETERIAContext())
                            {
                                var configuracion = db.Configuracion.Where(p => p.Iidconfiguracion == 1).FirstOrDefault();//obtenemos los datos de la configuracion
                                if (configuracion != null && configuracion.Noactualfactura <= configuracion.Finfactura)
                                {
                                    decimal totalCompra = 0, totalDescuento = 0, totalComision = 0;
                                    switch (pFactura.Tipocomprador)
                                    {
                                        #region si es un credito fiscal
                                        case "CREDITO FISCAL":

                                            #region crear la factura en bd
                                            pFactura.Nofactura = UtilidadesController.crearNoDocumento(configuracion.Noactualfactura.ToString(),
                                                configuracion.Nodigitosfactura);
                                            var facturaCredito = UtilidadesController.crearFactura(pFactura);
                                            db.Factura.Add(facturaCredito);
                                            db.SaveChanges();
                                            #endregion

                                            #region utilizamos la lista de los productos para crear el detalle de pedido en la bd

                                            for (int j = 0; j < 12; j++)  //recorremos la lista para almcaenar en la bd
                                            {
                                                if (lstDetalleVenta.Count() > 0)
                                                {
                                                    #region reunimos la informacion para aplicar descuento a factura
                                                    if (lstDetalleVenta[0].Essubproducto)
                                                        totalProductosNoValidosDescuentoGlobal += lstDetalleVenta[0].total;
                                                    else
                                                        totalProductosValidosDescuentoGlobal += lstDetalleVenta[0].total;
                                                    #endregion

                                                    #region guardamos el detalle del pedido
                                                    var oDetallepedido = UtilidadesController.crearDetallePedido(lstDetalleVenta, facturaCredito.Iidfactura);
                                                    db.Detallepedido.Add(oDetallepedido);
                                                    db.SaveChanges();
                                                    #endregion

                                                    totalCompra += lstDetalleVenta[0].total; totalDescuento += lstDetalleVenta[0].descuento; totalComision += lstDetalleVenta[0].comision;
                                                    lstDetalleVenta.Remove(lstDetalleVenta[0]);//eliminamos el producto de la lista porque ya se guardo en la bd
                                                }
                                                else
                                                {
                                                    j = 15;
                                                }
                                            }
                                            #region descuento global
                                            if (descuentoGlobal != null) { dg = JsonConvert.DeserializeObject<DescuentoGlobal>(descuentoGlobal); }
                                            if (dg == null || dg.descuentogeneral == 0)
                                            {
                                                facturaCredito.Total = totalCompra;//le agregamos el total de la compra a la factura
                                                facturaCredito.Totalcomision = totalComision;
                                                facturaCredito.Totaldescuento = totalDescuento;
                                                facturaCredito.Porcentajedescuentoglobal = 0;
                                                facturaCredito.Descuentoglobal = 0;
                                                db.SaveChanges();//y guardamos
                                            }
                                            else
                                            {
                                                decimal comisionActual = totalComision - totalDescuento;//tenemos que saber cuento le quedara de comision
                                                totalDescuentoGlobal = (totalProductosValidosDescuentoGlobal / 100) * dg.porcentajedescuento;//calculamos el descuento global
                                                if (comisionActual <= 0)//si la comision llega a 0
                                                {
                                                    facturaCredito.Total = (totalProductosValidosDescuentoGlobal - totalDescuentoGlobal) + totalProductosNoValidosDescuentoGlobal;
                                                    facturaCredito.Totalcomision = 0;
                                                    facturaCredito.Totaldescuento = totalDescuento;
                                                    facturaCredito.Porcentajedescuentoglobal = dg.porcentajedescuento;
                                                    facturaCredito.Descuentoglobal = totalDescuentoGlobal;
                                                    db.SaveChanges();//y guardamos
                                                }
                                                else//si tiene comision
                                                {
                                                    facturaCredito.Total = (totalProductosValidosDescuentoGlobal - totalDescuentoGlobal) + totalProductosNoValidosDescuentoGlobal;
                                                    decimal descuentoComision = comisionActual - totalDescuentoGlobal;//calculamos cuanto se le descontara a la comision
                                                    facturaCredito.Totaldescuento = descuentoComision;//guardamos el resultado del descuento
                                                    facturaCredito.Totaldescuento = totalDescuento;
                                                    facturaCredito.Porcentajedescuentoglobal = dg.porcentajedescuento;
                                                    facturaCredito.Descuentoglobal = totalDescuentoGlobal;
                                                    db.SaveChanges();//y guardamos
                                                }
                                            }
                                            #endregion

                                            #region guardar cliente fiscal siempre y cuando no se repita
                                            var cliente = UtilidadesController.crearClienteCreditoFiscal(pFactura);
                                            if (UtilidadesController.existCliente(cliente) == 0)//si no existe podemos guardar
                                            {
                                                db.Cliente.Add(cliente);
                                                db.SaveChanges();
                                            }
                                            #endregion
                                            configuracion.Noactualcreditofiscal = configuracion.Noactualcreditofiscal + 1;
                                            db.SaveChanges();//una vez guardado el comprobante incrementamos el contador
                                            #endregion
                                            LogicaBitacoraSistema.InsertarBitacoraSistema(
                                            "Facturo la cotización numero " + new CotizacionPendienteBL().obtenerNumeroCotizacion(iidCotizacion).Nocotizacion + " como factura de credito fiscal numero "+
                                            pFactura.Nofactura,
                                            Cookies.obtenerObjetoSesion
                                            (
                                                HttpContext.Session,
                                                "UsuarioLogueado"
                                            )
                                         );
                                            break;
                                        #endregion

                                        #region si es un cliente final
                                        case "CLIENTE FINAL":
                                            #region crear la factura en bd
                                            pFactura.Nofactura = UtilidadesController.crearNoDocumento(configuracion.Noactualfactura.ToString(),
                                                configuracion.Nodigitosfactura);
                                            var facturaFinal = UtilidadesController.crearFactura(pFactura);
                                            db.Factura.Add(facturaFinal);
                                            db.SaveChanges();
                                            #endregion

                                            #region utilizamos la lista de los productos para crear el detalle de pedido en la bd
                                            for (int j = 0; j < 16; j++)  //recorremos la lista para almcaenar en la bd
                                            {
                                                if (lstDetalleVenta.Count() > 0)
                                                {
                                                    #region reunimos la informacion para aplicar descuento a factura
                                                    if (lstDetalleVenta[0].Essubproducto)
                                                        totalProductosNoValidosDescuentoGlobal += lstDetalleVenta[0].total;
                                                    else
                                                        totalProductosValidosDescuentoGlobal += lstDetalleVenta[0].total;
                                                    #endregion

                                                    #region guardamos el detalle del pedido
                                                    var oDetallepedido = UtilidadesController.crearDetallePedido(lstDetalleVenta, facturaFinal.Iidfactura);
                                                    db.Detallepedido.Add(oDetallepedido);
                                                    db.SaveChanges();
                                                    #endregion

                                                    totalCompra += lstDetalleVenta[0].total; totalDescuento += lstDetalleVenta[0].descuento; totalComision += lstDetalleVenta[0].comision;
                                                    lstDetalleVenta.Remove(lstDetalleVenta[0]);//eliminamos el producto de la lista porque ya se guardo en la bd
                                                }
                                                else
                                                {
                                                    j = 15;
                                                }
                                            }
                                            #endregion

                                            #region descuento global
                                            if (descuentoGlobal != null) { dg = JsonConvert.DeserializeObject<DescuentoGlobal>(descuentoGlobal); }
                                            if (dg == null || dg.descuentogeneral == 0)
                                            {
                                                facturaFinal.Total = totalCompra;//le agregamos el total de la compra a la factura
                                                facturaFinal.Totalcomision = totalComision;
                                                facturaFinal.Totaldescuento = totalDescuento;
                                                facturaFinal.Porcentajedescuentoglobal = 0;
                                                facturaFinal.Descuentoglobal = 0;
                                                db.SaveChanges();//y guardamos
                                            }
                                            else
                                            {
                                                decimal comisionActual = totalComision - totalDescuento;//tenemos que saber cuento le quedara de comision
                                                totalDescuentoGlobal = (totalProductosValidosDescuentoGlobal / 100) * dg.porcentajedescuento;//calculamos el descuento global
                                                if (comisionActual <= 0)//si la comision llega a 0
                                                {
                                                    facturaFinal.Total = (totalProductosValidosDescuentoGlobal - totalDescuentoGlobal) + totalProductosNoValidosDescuentoGlobal;
                                                    facturaFinal.Totalcomision = 0;
                                                    facturaFinal.Totaldescuento = totalDescuento;
                                                    facturaFinal.Porcentajedescuentoglobal = dg.porcentajedescuento;
                                                    facturaFinal.Descuentoglobal = totalDescuentoGlobal;
                                                    db.SaveChanges();//y guardamos
                                                }
                                                else//si tiene comision
                                                {
                                                    facturaFinal.Total = (totalProductosValidosDescuentoGlobal - totalDescuentoGlobal) + totalProductosNoValidosDescuentoGlobal;
                                                    decimal descuentoComision = comisionActual - totalDescuentoGlobal;//calculamos cuanto se le descontara a la comision
                                                    facturaFinal.Totaldescuento = descuentoComision;//guardamos el resultado del descuento
                                                    facturaFinal.Totaldescuento = totalDescuento;
                                                    facturaFinal.Porcentajedescuentoglobal = dg.porcentajedescuento;
                                                    facturaFinal.Descuentoglobal = totalDescuentoGlobal;
                                                    db.SaveChanges();//y guardamos
                                                }
                                            }
                                            #endregion

                                            configuracion.Noactualfactura = configuracion.Noactualfactura + 1;
                                            db.SaveChanges();//una vez guardada la factura incrementamos el contador

                                            LogicaBitacoraSistema.InsertarBitacoraSistema(
                                            "Facturo la cotización numero " + new CotizacionPendienteBL().obtenerNumeroCotizacion(iidCotizacion).Nocotizacion + " como factura de cliente final numero "+ pFactura.Nofactura,
                                            Cookies.obtenerObjetoSesion
                                            (
                                                HttpContext.Session,
                                                "UsuarioLogueado"
                                            )
                                         );
                                            break;
                                            #endregion
                                    }
                                    if (lstDetalleVenta.Count() == 0)
                                    {
                                        var data = db.Cotizacion.Where(p => p.Iidcotizacion == cotizacion.Iidcotizacion).FirstOrDefault();
                                        data.Cotizacionfacturada = "S";
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    return "finalFactura";
                                }
                            }
                        } while (lstDetalleVenta.Count() > 0);
                        #endregion
                    }
                    transaction.Complete();
                    return "ok";
                }
            }
            catch (Exception e)
            {
                string msj = e.Message;
                return msj;
            }
        }
        [HttpGet]
        public bool eliminarProductoDeLaLista(Int64 id,Int64 idCotizacion)
        {
            try
            {
                return cotizacionPendienteBl.eliminarProductoDeLaCotizacion(id, idCotizacion);
            }
            catch(Exception e)
            {
                return false;
            }
        }
        [HttpPost]
        public int guardarNuevoProducto(Int64 iiproducto, int? descuento, int? comision, Int64 cantidad, Int64 idCotizacion,int? Essubproducto)
        {
            return (int)cotizacionPendienteBl.guardarNuevoProducto(iiproducto, descuento, comision, cantidad, idCotizacion, Essubproducto);
        }
        [HttpPost]
        public string aplicarDescuentoGeneral(DescuentoGlobal descuento, Usuario user,Int64 idCotizacion)
        {
            try
            {
                HttpContext.Session.Remove("DescuentoGlobal");
                using (var db = new BDFERRETERIAContext())
                {
                    var usuario = db.Usuario.Where(p => p.Nombreusuario == user.Nombreusuario.ToUpper() &&
                                                   p.Contraseña == UtilidadesController.encryptPassword(user.Contraseña) && p.Iidtipousuario == 1).FirstOrDefault();
                    if (usuario != null)
                    {
                        List<DetalleVenta> lst = new List<DetalleVenta>();
                        decimal totalComision = 0, totalFactura = 0, totalDescuento = 0, totalFacturaProdcutosListados = 0,
                            tdecuento = 0, totalFacturaProdcutosNoListados = 0;
                        var listaSerializada = HttpContext.Session.GetString("lstDetalleCotizacion");//obtenemos el valor de la cookie
                        if (listaSerializada != null)//si es diferente de null 
                            lst = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaSerializada);//quitamos el serializado
                        //recorremos la lista para obtener los totales
                        foreach (var item in lst)
                        {
                            if (item.subproducto == "NO")
                            {
                                totalFacturaProdcutosListados = totalFacturaProdcutosListados + item.total;
                            }
                            else
                            {
                                totalFacturaProdcutosNoListados = totalFacturaProdcutosNoListados + item.total;
                            }
                            totalComision = totalComision + item.comision;
                            totalFactura = totalFactura + item.total;
                            tdecuento = tdecuento + item.descuento;
                        }
                        //hacemos el calculo del descuento
                        DescuentoGlobal descuentoGlobal = new DescuentoGlobal();
                        descuentoGlobal.porcentajedescuento = descuento.porcentajedescuento;//guardamos el porcentaje que se aplico
                        totalDescuento = (totalFacturaProdcutosListados / 100) * descuento.porcentajedescuento;//calculamos el descuento
                        descuentoGlobal.descuentogeneral = totalDescuento;
                        decimal comisionActual = totalComision - tdecuento;
                        if (comisionActual == 0)//si no tiene comision
                        {
                            descuentoGlobal.totalcomision = comisionActual;
                            descuentoGlobal.totalfactura = (totalFacturaProdcutosListados - totalDescuento) + totalFacturaProdcutosNoListados;
                        }
                        else//si tiene comision
                        {
                            decimal descuentoComision = comisionActual - totalDescuento;//calculamos cuanto se le descontara a la comision
                            descuentoGlobal.totalcomision = descuentoComision;//guardamos el resultado del descuento
                            descuentoGlobal.totalfactura = (totalFacturaProdcutosListados - totalDescuento) + totalFacturaProdcutosNoListados;
                        }
                        //crearemos una variable de sesion que almacene esta informacion temporalmente
                        string dataSerializada = JsonConvert.SerializeObject(descuentoGlobal);//serializamos el objeto a json
                        HttpContext.Session.SetString("DescuentoGlobal", dataSerializada);
                        return descuentoGlobal.totalfactura.ToString();
                    }
                    else
                    {
                        return "-1";
                    }
                }
            }
            catch (Exception e)
            {
                return "0";
            }
        }
        
    }
}
