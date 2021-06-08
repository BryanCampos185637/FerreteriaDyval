using System;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Helper.HelperVenderProducto;
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
                bool respuesta = false;
                var helper = new EfectuarVenta();
                if (esOriginal)
                {
                    respuesta = helper.crearFacturaOriginal(id, efectivo);
                    if (respuesta)
                        HttpContext.Session.SetInt32("idFactura", (int)id);//UNA VEZ MODIFICADO CREAMOS LA COOKIE QUE NOS SERVIRA PARA HACER EL DOCUMENTO
                }
                else
                {
                    respuesta = helper.crearFacturaProvisional(id, efectivo);
                    if (respuesta)
                        HttpContext.Session.SetInt32("idFactura", (int)id);//UNA VEZ MODIFICADO CREAMOS LA COOKIE QUE NOS SERVIRA PARA HACER EL DOCUMENTO
                }
                return respuesta;
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
    }
}
