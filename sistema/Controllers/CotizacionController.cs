using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CotizacionController : Controller
    {
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        //accion que nos devuelve la lista de productos cotizados
        [HttpGet]
        public JsonResult listDetalleCotizacion()
        {
            List<DetalleVenta> lst = new List<DetalleVenta>();
            try
            {
                var listaSerializada = HttpContext.Session.GetString("listaCotizacion");//obtenemos el valor de la cookie
                if (listaSerializada != null)//si es diferente de null 
                    lst = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaSerializada);//quitamos el serializado
                return Json(lst);
            }
            catch (Exception e)
            {
                return Json(lst);
            }
        }
        //agrega un nuevo producto a la lista de cotizacion
        [HttpPost]
        public int ArmardetalleCotizacion(Int64 iiproducto, int? descuento, int? comision, Int64 cantidad,int? subUnidad)
        {
            List<DetalleVenta> lstDetalleventa = new List<DetalleVenta>();//creamos una lista
            decimal tcomision = 0, tdescuento = 0, Totalproducto = 0;
            try
            {
                var listaSerializada = HttpContext.Session.GetString("listaCotizacion"); //obtenemos lo que haya en la cokie
                if (listaSerializada != null)//si la variable viene null es porque no hay ninguna lista hecha
                    lstDetalleventa = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaSerializada);//quitamos el serializado
                using (var db = new BDFERRETERIAContext())
                {
                    Producto oProducto = db.Producto.Where(p => p.Iidproducto == iiproducto).First();//capturo el producto
                                                                                                     //hacemos los calculos para obtener la comision y el descuento                                                                                 
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
                    else//si viene mayor a 0 entonces se le hara calculo con el subprecio
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
                    foreach (var item in lstDetalleventa)
                    {
                        siguienteId++;
                        item.Idlista = siguienteId;
                    }
                    oDetalle.Idlista = siguienteId + 1;
                    lstDetalleventa.Add(oDetalle);//agregamos el objeto a la lista
                    string listaSerializadaJson = JsonConvert.SerializeObject(lstDetalleventa);//una vez agregada serializamos a json 
                    HttpContext.Session.SetString("listaCotizacion", listaSerializadaJson);//guardamos la lista en la cookie
                }
                return 1;
            }
            catch (Exception e)
            {
                string error = e.Message;
                return 0;
            }
        }
        //eimina un producto de la lista de cotizacion
        [HttpGet]
        public int deleteProducto(Int64 id)
        {
            try
            {
                List<DetalleVenta> lst = new List<DetalleVenta>();//creamos la variable
                string listaCookie = HttpContext.Session.GetString("listaCotizacion");//obtenemos la cadena
                lst = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaCookie);//quitamos el serializado
                DetalleVenta producto = lst.SingleOrDefault(p => p.Idlista.Equals(id));//obtenemos el producto de la lista
                lst.Remove(producto);//removemos el producto de la lista
                var lstSerializada = JsonConvert.SerializeObject(lst);//serializamos de nuevo la lista
                HttpContext.Session.SetString("listaCotizacion", lstSerializada);//y guardamos la lista
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        [HttpGet]
        public int cancelarCotizacion()
        {
            try
            {
                HttpContext.Session.Remove("listaCotizacion");
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        [HttpPost]
        public int confirmarCotizacion(string nombre, int tipodocumento)
        {
            CotizacionBL dal = new CotizacionBL();
            string documento = "";
            try
            {
                #region quitamos el serializado a la lista
                List<DetalleVenta> lstDetalleVenta = new List<DetalleVenta>();//creamos la variable
                string listaCookie = HttpContext.Session.GetString("listaCotizacion");//obtenemos la cadena
                lstDetalleVenta = JsonConvert.DeserializeObject<List<DetalleVenta>>(listaCookie);//quitamos el serializado
                #endregion
                int rpt = dal.GuardarCotizacion(nombre, tipodocumento, lstDetalleVenta, (int)HttpContext.Session.GetInt32("UsuarioLogueado"));
                if (rpt == 1)
                {
                    if (tipodocumento == 1)
                        documento = "Cotización";
                    else
                        documento = "Factura provisional";
                    LogicaBitacoraSistema.InsertarBitacoraSistema(
                        "Creo una "+documento+" para el cliente "+nombre, Cookies.obtenerObjetoSesion(
                            HttpContext.Session,"UsuarioLogueado"
                       )
                    );
                    HttpContext.Session.Remove("listaCotizacion");
                    return rpt;
                }
                else
                    return rpt;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        //accion para imprimir 
        public FileResult Cotizacion()
        {
            Int64 id = 0;
            id = (int)HttpContext.Session.GetInt32("idCotizacion");
            byte[] buffer = UtilidadesController.crearCotizacion(id);
            //HttpContext.Session.Remove("idCotizacion");//removemos el id que esta en la cookie
            return File(buffer, "application/pdf");
        }
    }
}
