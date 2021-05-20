using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AdminFerreteria.DAL;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class FacturaController : Controller
    {
        FacturaDAL dal = new FacturaDAL();
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("factura", "index", (int)idUsuario))
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
        [HttpGet]
        public JsonResult listCotizacion()
        {
            return Json(dal.listarCotizaciones());   
        }
        [HttpGet]
        public JsonResult listFactura()
        {
            return Json(dal.listarFacturas());
        }

        [HttpGet]
        public JsonResult getFacturaById(Int64 id)
        {
            return Json(dal.detalleFactura(id));
        }
        //obtener la lista de productos
        [HttpGet]
        public JsonResult getDetallePedidoByIidfactura(Int64 id)
        {
            return Json(dal.detallePedidoPorIidfactura(id));
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
                        using (var transaction = new TransactionScope())
                        {
                            using (var db = new BDFERRETERIAContext())
                            {
                                var data = db.Factura.Where(p => p.Iidfactura == id).First();//OBTENEMOS LA DATA
                                List<Detallepedido> listaProductos = db.Detallepedido.Where(p => p.Iidfactura == data.Iidfactura).ToList();//obtenemos la lista del pedido
                                foreach (var item in listaProductos)
                                {
                                    #region armamos el objeto producto para descontar la cantidad
                                    Producto oProducto = db.Producto.Where(p => p.Iidproducto.Equals(item.Iidproducto)).First();//buscamos el producto en la bd
                                    if (item.Essubproducto == "SI")//si es un subproducto le descontaremos a la sub existencia
                                    {
                                        oProducto.Subexistencia = oProducto.Subexistencia - item.Cantidad;//descontamos el total de productos
                                        if (item.Cantidad > oProducto.Equivalencia)
                                        {
                                            int contador = Convert.ToInt32(item.Cantidad), residuos = 0;
                                            int cantidadDeProductosADescontar = 0; int equivalencia = Convert.ToInt32(oProducto.Equivalencia);
                                            while (equivalencia < contador)
                                            {
                                                cantidadDeProductosADescontar++;
                                                contador = contador - equivalencia;
                                                residuos = contador;
                                            }
                                            oProducto.Existencias = oProducto.Existencias - cantidadDeProductosADescontar;
                                            oProducto.Restantes = oProducto.Restantes - residuos;
                                            if (oProducto.Restantes < 0)
                                            {
                                                oProducto.Existencias = oProducto.Existencias - 1;
                                                oProducto.Subexistencia = equivalencia - residuos;
                                            }
                                            else if (oProducto.Restantes == 0)//si es igual a 0
                                            {
                                                oProducto.Restantes = oProducto.Equivalencia;//volvemos a la eqivalencia los restantes
                                                oProducto.Existencias = oProducto.Existencias - 1;//y restamos uno de las existencias
                                            }
                                            db.SaveChanges();//guardamos producto
                                        }
                                        else
                                        {
                                            oProducto.Restantes = oProducto.Restantes - item.Cantidad;//restamos de los restantes 
                                            if (oProducto.Restantes <= 0)//si el resultado es menor o igual a 0 
                                            {
                                                if (oProducto.Restantes == 0)//si es igual a 0
                                                {
                                                    oProducto.Restantes = oProducto.Equivalencia;//volvemos a la eqivalencia los restantes
                                                    oProducto.Existencias = oProducto.Existencias - 1;//y restamos uno de las existencias
                                                }
                                                else if (oProducto.Restantes < 0)//si los restantes son menores a 0
                                                {
                                                    var restos = oProducto.Restantes * -1;//volvemos a positivos el valor
                                                    oProducto.Existencias = oProducto.Existencias - 1;//restamos uno de las existencias
                                                    oProducto.Restantes = oProducto.Equivalencia;//volvemos a la eqivalencia los restantes
                                                                                                 //ahora el valor de restos es lo que se desconto de la otra pieza
                                                    oProducto.Restantes = oProducto.Restantes - restos;//tenemos que descontarselo a la nueva pieza
                                                }
                                            }
                                            db.SaveChanges();//guardamos producto
                                        }
                                        db.SaveChanges();
                                    }
                                    else//si es el original a las existencias
                                    {
                                        oProducto.Existencias = oProducto.Existencias - item.Cantidad;//descontamos el total de productos
                                        var descuentoSubExistencias = oProducto.Equivalencia * item.Cantidad;
                                        oProducto.Subexistencia = oProducto.Subexistencia - descuentoSubExistencias;
                                        db.SaveChanges();
                                    }
                                    #endregion
                                }
                                data.Efectivo = efectivo;
                                data.Cambio = efectivo-data.Total;
                                data.Original = "SI";
                                data.Facturaemitida = "SI";//MODIFICAMOS SU PROPIEDAD Y LO GUARDAMOS
                                db.SaveChanges();//solo si de descontaron guardamos los cambios
                                HttpContext.Session.SetInt32("idFactura", (int)id);//UNA VEZ MODIFICADO CREAMOS LA COOKIE QUE NOS SERVIRA PARA HACER EL DOCUMENTO PDF
                            }
                            transaction.Complete();
                            return true;//RETORNAMOS TRUE
                        }

                    case false://si es false se crea la provisional
                        using(var db = new BDFERRETERIAContext())
                        {
                            var data = db.Factura.Where(p => p.Iidfactura == id).First();//OBTENEMOS LA DATA
                            data.Efectivo = efectivo;
                            data.Cambio = efectivo - data.Total;
                            data.Original = "NO";
                            data.Facturaemitida = "SI";//MODIFICAMOS SU PROPIEDAD Y LO GUARDAMOS
                            db.SaveChanges();//solo si de descontaron guardamos los cambios
                            HttpContext.Session.SetInt32("idFactura", (int)id);//UNA VEZ MODIFICADO CREAMOS LA COOKIE QUE NOS SERVIRA PARA HACER EL DOCUMENTO
                            return true;//RETORNAMOS TRUE
                        }
                }
            }
            catch(Exception e) 
            {
                return false;//SI SUCEDE UN ERROR SE RETORNA FALSE
            }
        }
        [HttpGet]
        public bool addIdCotizacionCookie(Int64 id)
        {
            try
            {
                using (BDFERRETERIAContext bd = new BDFERRETERIAContext())
                {
                    var data = bd.Cotizacion.Where(p => p.Iidcotizacion == id).First();//OBTENEMOS LA DATA
                    data.Cotizacionemitida = "SI";//MODIFICAMOS SU PROPIEDAD Y LO GUARDAMOS
                    bd.SaveChanges();
                }
                HttpContext.Session.SetInt32("idCotizacion", (int)id);//UNA VEZ MODIFICADO CREAMOS LA COOKIE QUE NOS SERVIRA PARA HACER EL DOCUMENTO
                return true;//RETORNAMOS TRUE
            }
            catch (Exception e)
            {
                return false;//SI SUCEDE UN ERROR SE RETORNA FALSE
            }
        }
    }
}
