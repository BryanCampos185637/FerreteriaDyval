using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdminFerreteria.Controllers
{
    public class VentaController : Controller
    {
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("venta", "index", (int)idUsuario))
                {
                    return View();
                }
                else
                {
                    return Redirect("/Home/error");
                }
            }
            else
            {
                return Redirect("/Login/index");
            }
        }
        [HttpPost]
        public int ArmardetalleVenta(Int64 iiproducto, int? descuento, int? comision, Int64 cantidad, int? subUnidad)
        {
            List<DetalleVenta> lstDetalleventa = new List<DetalleVenta>();//creamos una lista
            decimal tcomision = 0, tdescuento = 0, Totalproducto = 0;
            try
            {
                var listaSerializada = HttpContext.Session.GetString("listaFactura"); //obtenemos lo que haya en la cookie
                if (listaSerializada != null)//si la variable viene null es porque no hay ninguna lista hecha
                    lstDetalleventa = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaSerializada);//quitamos el serializado
                using (var db = new BDFERRETERIAContext())
                {
                    Producto oProducto = db.Producto.Where(p => p.Iidproducto == iiproducto).First();//capturo el producto
                    if (subUnidad == 0)//si la subUnidad viene en valor 0 es porque se le hara el calculo con el original
                    {
                        //hacemos los calculos para obtener la comision y el descuento                                                                                 
                        tcomision = Convert.ToDecimal((oProducto.Precioventa / 100) * comision);//saber la comision
                        if (comision > 0)//si la comision es mayor se obtiene el descuento del precio de venta + la comision
                        {
                            decimal precioConComision = (decimal)oProducto.Precioventa + tcomision;
                            tdescuento = Convert.ToDecimal(((precioConComision / 100) * descuento));
                        }
                        else//si no es asi solo se le descuenta al precio de venta
                        {
                            tdescuento = Convert.ToDecimal((oProducto.Precioventa / 100) * descuento);//saber el descuento
                        }
                        Totalproducto = Convert.ToDecimal(((oProducto.Precioventa + tcomision) - tdescuento) * cantidad);//obtenemos el total a pagar 
                    }
                    else//si viene mayor a 0 entonces se calculara con el subprecio
                    {
                        //hacemos los calculos para obtener la comision y el descuento                                                                                 
                        tcomision = Convert.ToDecimal((oProducto.Subprecioventa / 100) * comision);//saber la comision
                        if (comision > 0)//si la comision es mayor se obtiene el descuento del precio de venta + la comision
                        {
                            decimal precioConComision = (decimal)oProducto.Subprecioventa + tcomision;
                            tdescuento = Convert.ToDecimal(((precioConComision / 100) * descuento));
                        }
                        else//si no es asi solo se le descuenta al precio de venta
                        {
                            tdescuento = Convert.ToDecimal((oProducto.Subprecioventa / 100) * descuento);//saber el descuento
                        }
                        Totalproducto = Convert.ToDecimal(((oProducto.Subprecioventa + tcomision) - tdescuento) * cantidad);//obtenemos el total a pagar 
                    }
                    //una vez hecho los calculos creamos un objeto que agregaremos a la lista
                    DetalleVenta oDetalle = new DetalleVenta();
                    if (subUnidad == 0)//si es un sub producto le ponemos true si no false
                    {
                        oDetalle.Essubproducto = false;
                        oDetalle.preciounitario = (decimal)oProducto.Precioventa;
                        oDetalle.precioconcomision = (decimal)oProducto.Precioventa + tcomision;
                        oDetalle.unidadmedida = UtilidadesController.ObtenerNombreSubUnidad(oProducto.Iidunidadmedida);
                    }
                    else
                    {
                        oDetalle.Essubproducto = true;
                        oDetalle.preciounitario = (decimal)oProducto.Subprecioventa;
                        oDetalle.precioconcomision = (decimal)oProducto.Subprecioventa + tcomision;
                        oDetalle.subproducto = UtilidadesController.ObtenerNombreSubUnidad(subUnidad);
                    }
                    oDetalle.cantidad = (Int64)cantidad;
                    oDetalle.comision = tcomision * cantidad;
                    oDetalle.descuento = tdescuento * cantidad;
                    oDetalle.nombreproducto = oProducto.Descripcion;
                    oDetalle.total = Totalproducto;
                    oDetalle.pdescuento = (int)descuento;
                    oDetalle.pcomision = (int)comision;
                    oDetalle.iidproducto = iiproducto;
                    Int64 siguienteId = 0;
                    foreach(var item in lstDetalleventa)
                    {
                        siguienteId++;
                        item.Idlista = siguienteId;//este solo le asigna un id temporal a la lista para poder eliminar el producto de la lista
                        //si asi lo desea el usuario
                    }
                    oDetalle.Idlista = siguienteId + 1;
                    lstDetalleventa.Add(oDetalle);//agregamos el objeto a la lista
                    string listaSerializadaJson = JsonConvert.SerializeObject(lstDetalleventa);//una vez agregada serializamos a json 
                    HttpContext.Session.SetString("listaFactura", listaSerializadaJson);//guardamos la lista en la cookie
                }
                return 1;
            }
            catch (Exception e)
            {
                string error = e.Message;
                return 0;
            }
        }
        [HttpGet]
        public JsonResult listDetalleVenta()
        {
            List<DetalleVenta> lst = new List<DetalleVenta>();
            try
            {
                var listaSerializada = HttpContext.Session.GetString("listaFactura");//obtenemos el valor de la cookie
                if (listaSerializada != null)//si es diferente de null 
                    lst = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaSerializada);//quitamos el serializado
                return Json(lst);
            }
            catch (Exception e)
            {
                return Json(lst);
            }
        }
        [HttpGet]
        public int cancelarVenta()
        {
            try
            {
                HttpContext.Session.Remove("listaFactura");
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
        [HttpGet]
        public int deleteProducto(Int64 id)
        {
            try
            {
                List<DetalleVenta> lst = new List<DetalleVenta>();//creamos la variable
                string listaCookie = HttpContext.Session.GetString("listaFactura");//obtenemos la cadena
                lst = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaCookie);//quitamos el serializado
                DetalleVenta producto = lst.SingleOrDefault(p => p.Idlista.Equals(id));//obtenemos el producto de la lista
                lst.Remove(producto);//removemos el producto de la lista
                var lstSerializada = JsonConvert.SerializeObject(lst);//serializamos de nuevo la lista
                HttpContext.Session.SetString("listaFactura", lstSerializada);//y guardamos la lista
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        //accion para imprimir 
        public FileResult Factura(bool esOriginal)
        {
            Int64 id = 0;
            id = (int)HttpContext.Session.GetInt32("idFactura");
            byte[] buffer;
            switch (esOriginal)
            {
                case true:
                    buffer = UtilidadesController.facturaPDF(id);
                    return File(buffer, "application/pdf");
                case false:
                    buffer = UtilidadesController.facturaPDFprovisional(id);
                    return File(buffer, "application/pdf");
            }
        }
        [HttpGet]
        public long FacturaNo()
        {
            /// <summary>
            /// retorno el numero de la factura
            /// </summary>
            /// <returns></returns>
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var No = db.Configuracion.Where(p => p.Iidconfiguracion == 1).FirstOrDefault().Noactualfactura;
                    return No;
                }
            }
            catch (Exception e)
            {
                return 00000000;
            }
        }
        [HttpPost]
        public string confirmVenta(Factura pFactura)
        {
            try
            {
                #region quitamos serializado y guardamos el id del usuario logueado
                pFactura.Fechacreacion = DateTime.Now;
                pFactura.Iidusuario = (int)HttpContext.Session.GetInt32("UsuarioLogueado");//capturamos el id del usuario que inicio sesion
                List<DetalleVenta> lstDetalleVenta = new List<DetalleVenta>();//creamos la variable
                string listaCookie = HttpContext.Session.GetString("listaFactura");//obtenemos la cadena de la cookie
                lstDetalleVenta = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaCookie);//quitamos el serializado
                DescuentoGlobal dg = new DescuentoGlobal();
                string descuentoGlobal = HttpContext.Session.GetString("DescuentoGlobal");
                #endregion

                using (var transaction = new TransactionScope())
                {
                    do
                    {
                        using (var db = new BDFERRETERIAContext())
                        {
                            var configuracion = db.Configuracion.Where(p => p.Iidconfiguracion == 1).FirstOrDefault();//obtenemos los datos de la configuracion
                            if (configuracion != null && configuracion.Noactualfactura <= configuracion.Finfactura)
                            {
                                decimal totalDescuentoGlobal, totalCompra = 0, totalDescuento = 0, totalComision = 0, totalProductosValidosDescuentoGlobal = 0, totalProductosNoValidosDescuentoGlobal = 0;
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
                                        break;
                                        #endregion
                                }
                            }
                            else
                            {
                                return "finalFactura";
                            }
                        }
                    } while (lstDetalleVenta.Count() > 0);
                    transaction.Complete();// si todo sale bien guardamos
                }
                HttpContext.Session.Remove("listaFactura"); HttpContext.Session.Remove("DescuentoGlobal");
                return "ok";
            }
            catch (Exception e)
            {
                string msj = e.Message;//"Error en el sistema, verifica si ya configuraste el inicio de las facturas.";
                return msj;
            }
        }
        [HttpPost]
        public string aplicarDescuentoGeneral(DescuentoGlobal descuento, Usuario user)
        {
            try
            {
                HttpContext.Session.Remove("DescuentoGlobal");
                using (var db = new BDFERRETERIAContext())
                {
                    var usuario = db.Usuario.Where(p => p.Nombreusuario == user.Nombreusuario.ToUpper()
                    && p.Contraseña == UtilidadesController.encryptPassword(user.Contraseña) && p.Iidtipousuario == 1).FirstOrDefault();
                    if (usuario != null)
                    {
                        List<DetalleVenta> lst = new List<DetalleVenta>();
                        decimal totalComision = 0, totalFactura = 0, totalDescuento = 0, totalFacturaProdcutosListados = 0,
                            tdecuento = 0, totalFacturaProdcutosNoListados = 0;
                        var listaSerializada = HttpContext.Session.GetString("listaFactura");//obtenemos el valor de la cookie
                        if (listaSerializada != null)//si es diferente de null 
                            lst = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaSerializada);//quitamos el serializado
                        //recorremos la lista para obtener los totales
                        foreach (var item in lst)
                        {
                            if (!item.Essubproducto)
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
