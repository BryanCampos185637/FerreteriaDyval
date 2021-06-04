using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class FacturaController : Controller
    {
        FacturaBL facturaBl = new FacturaBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listCotizacion()
        {
            return Json(facturaBl.listarCotizaciones());   
        }
        [HttpGet]
        public JsonResult listFactura()
        {
            return Json(facturaBl.listarFacturas());
        }
        [HttpGet]
        public JsonResult getFacturaById(Int64 id)
        {
            return Json(facturaBl.detalleFactura(id));
        }
        //obtener la lista de productos
        [HttpGet]
        public JsonResult getDetallePedidoByIidfactura(Int64 id)
        {
            return Json(facturaBl.detallePedidoPorIidfactura(id));
        }
        //accion que nos permite imprimir la factura y cambia el valor de la propiedad Facturaemitida
        [HttpGet]
        public bool addIdFacturaCookie(Int64 id,bool esOriginal, decimal efectivo)
        {
            try
            {
                switch (esOriginal)//validaremos que opcion escogio
                {
                    case true://si es true se crea la original
                        return crearFacturaOriginal(id, efectivo);
                        
                    case false://si es false se crea la provisional
                        return crearFacturaProvisional(id, efectivo);
                }
            }
            catch(Exception) 
            {
                return false;//SI SUCEDE UN ERROR SE RETORNA FALSE
            }
        }
        [HttpGet]
        public bool addIdCotizacionCookie(Int64 id)
        {
            try
            {
                var seCambio = facturaBl.cambiarEstadoFacturaImpresa(id);
                if (seCambio == false)
                    return false;
                HttpContext.Session.SetInt32("idCotizacion", (int)id);//UNA VEZ MODIFICADO CREAMOS LA COOKIE QUE NOS SERVIRA PARA HACER EL DOCUMENTO
                return true;//RETORNAMOS TRUE
            }
            catch (Exception)
            {
                return false;//SI SUCEDE UN ERROR SE RETORNA FALSE
            }
        }
        #region creacion de factura
        private bool crearFacturaOriginal(long id, decimal efectivo)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
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

                    HttpContext.Session.SetInt32("idFactura", (int)id);//UNA VEZ MODIFICADO CREAMOS LA COOKIE QUE NOS SERVIRA PARA HACER EL DOCUMENTO PDF
                    transaction.Complete();
                    return true;//RETORNAMOS TRUE
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool crearFacturaProvisional(long id, decimal efectivo)
        {
            try
            {
                var rpt = facturaBl.crearFacturaProvisional(id, efectivo);
                if (rpt == false)
                    return rpt;
                HttpContext.Session.SetInt32("idFactura", (int)id);//UNA VEZ MODIFICADO CREAMOS LA COOKIE QUE NOS SERVIRA PARA HACER EL DOCUMENTO
                return true;//RETORNAMOS TRUE
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
