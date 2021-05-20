using System;
using System.Collections.Generic;
using System.Linq;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AdminFerreteria.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("reporte", "index", (int)idUsuario))
                {
                    BDFERRETERIAContext db = new BDFERRETERIAContext();
                    var empleado = db.Empleado.Where(p => p.Iidempleado == 1).First();
                    DateTime fecha = Convert.ToDateTime(empleado.Fechacreacion);
                    ViewBag.fechaInicioSistema = fecha.ToString("yyyy-MM-dd");//el primer usuario que se registro fue la primer funcion que se hizo en el sistema
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
        //crea la lista de reportes tomando las fechas establecidas por el usuario
        public int createListReportVenta(string desde, string hasta)
        {
            try
            {
                List<ListFactura> lstFactura = new List<ListFactura>();
                using (var db = new BDFERRETERIAContext())
                {
                    if (desde != null && hasta != null)// si los dos parametros vienen llenos
                    {
                        lstFactura = (from factura in db.Factura
                                      join usuario in db.Usuario on
                                      factura.Iidusuario equals usuario.Iidusuario
                                      join empleado in db.Empleado on
                                      usuario.Iidempleado equals empleado.Iidempleado
                                      where factura.Bhabilitado == "A"
                                      && factura.Fechacreacion >= Convert.ToDateTime(desde)
                                      && factura.Fechacreacion <= Convert.ToDateTime(hasta)
                                      select new ListFactura
                                      {
                                          iidfactura = factura.Iidfactura,
                                          iidusuario = factura.Iidusuario,
                                          nombrevendedor = empleado.Nombrecompleto,
                                          tipocomprador = factura.Tipocomprador,
                                          nombrecomprador = factura.Nombrecliente,
                                          direccion = factura.Direccion,
                                          registro = factura.Registro,
                                          giro = factura.Giro,
                                          nit = factura.Nit,
                                          nofactura = factura.Nofactura,
                                          total = (decimal)factura.Total,
                                          fechaemitida = factura.Fechacreacion.ToShortDateString(),
                                          porcentajedescuento=factura.Porcentajedescuentoglobal,
                                          descuentogeneral=factura.Descuentoglobal,
                                          efectivo=(decimal)factura.Efectivo,
                                          cambio=(decimal)factura.Cambio
                                      }).ToList();
                    }
                    else if (desde != null && hasta == null)//reporte de dia especifico
                    {
                        lstFactura = (from factura in db.Factura
                                      join usuario in db.Usuario on
                                      factura.Iidusuario equals usuario.Iidusuario
                                      join empleado in db.Empleado on
                                      usuario.Iidempleado equals empleado.Iidempleado
                                      where factura.Bhabilitado == "A"
                                      && factura.Fechacreacion >= Convert.ToDateTime(desde)
                                      && factura.Fechacreacion <= Convert.ToDateTime(desde)
                                      select new ListFactura
                                      {
                                          iidfactura = factura.Iidfactura,
                                          iidusuario = factura.Iidusuario,
                                          nombrevendedor = empleado.Nombrecompleto,
                                          tipocomprador = factura.Tipocomprador,
                                          nombrecomprador = factura.Nombrecliente,
                                          direccion = factura.Direccion,
                                          registro = factura.Registro,
                                          giro = factura.Giro,
                                          nit = factura.Nit,
                                          nofactura = factura.Nofactura,
                                          total = (decimal)factura.Total,
                                          fechaemitida = factura.Fechacreacion.ToShortDateString(),
                                          porcentajedescuento = factura.Porcentajedescuentoglobal,
                                          descuentogeneral = factura.Descuentoglobal,
                                          efectivo = (decimal)factura.Efectivo,
                                          cambio = (decimal)factura.Cambio
                                      }).ToList();
                    }
                    else//reporte general
                    {
                        lstFactura = (from factura in db.Factura
                                      join usuario in db.Usuario on
                                      factura.Iidusuario equals usuario.Iidusuario
                                      join empleado in db.Empleado on
                                      usuario.Iidempleado equals empleado.Iidempleado
                                      where factura.Bhabilitado == "A"
                                      select new ListFactura
                                      {
                                          iidfactura = factura.Iidfactura,
                                          iidusuario = factura.Iidusuario,
                                          nombrevendedor = empleado.Nombrecompleto,
                                          tipocomprador = factura.Tipocomprador,
                                          nombrecomprador = factura.Nombrecliente,
                                          direccion = factura.Direccion,
                                          registro = factura.Registro,
                                          giro = factura.Giro,
                                          nit = factura.Nit,
                                          nofactura = factura.Nofactura,
                                          total= (decimal)factura.Total,
                                          fechaemitida = factura.Fechacreacion.ToShortDateString(),
                                          porcentajedescuento = factura.Porcentajedescuentoglobal,
                                          descuentogeneral = factura.Descuentoglobal,
                                          efectivo = (decimal)factura.Efectivo,
                                          cambio = (decimal)factura.Cambio
                                      }).ToList();
                    }
                }
                string listaSerializada = JsonConvert.SerializeObject(lstFactura);
                HttpContext.Session.SetString("lstFactura", listaSerializada);
                return 1; 
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        //accion para generar reporte
        public FileResult ReporteVentas()
        {
            try
            {
                string lstSerializada = HttpContext.Session.GetString("lstFactura");
                List<ListFactura> lstFactura = JsonConvert.DeserializeObject<List<ListFactura>>(lstSerializada);
                byte[] buffer = UtilidadesController.crearReporteVenta(lstFactura);//obtenemos el reporte
                HttpContext.Session.Remove("lstFactura");//removemos el id que esta en la cookie
                return File(buffer, "application/pdf");
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public FileResult ReporteInventario()
        {
            byte[] buffer = null;
            try
            {
                string lstSerializada = HttpContext.Session.GetString("ReporteInventario");//obtenemos la lista de la cookie
                List<ListReporteInventario> listReporteInventarios = JsonConvert.DeserializeObject<List<ListReporteInventario>>(lstSerializada);
                string[] cabecera = {"COD", "DESCRIPCIÓN","PROVEEDOR", "BODEGA", "STOCK", "UM", "CANT", "PRECIO","UM", "CANT", "PRECIO" };
                string[] propiedades = { "Codigoproducto", "Nombreproducto","Proveedor", "Nombrebodega", "Nombrestock", "Nombreunidad", "Cantidad", "Precio", "Nombresubunidad", "Subcantidad", "Subprecio" };
                buffer = UtilidadesController.crearReporteInventario(listReporteInventarios,cabecera,propiedades);//obtenemos el reporte
                HttpContext.Session.Remove("ReporteInventario");//removemos la lista que esta en la cookie
                return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception e)
            {
                return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
        [HttpPost]
        public string crearCookieReporteInventario(Inventario inventario, string nombrestock)
        {
            List<ListReporteInventario> lst = new List<ListReporteInventario>();
            try
            {
                if (inventario.Iidbodega == -1)//si el id de la bodega viene en -1 quiere decir que haremos una lista de la sala de ventas
                {
                    lst = UtilidadesController.crearListaReporteSala(inventario, nombrestock);
                }
                else if (inventario.Iidbodega > 0)
                {
                    lst = UtilidadesController.crearlistaReporteBodega(inventario,nombrestock);
                }
                else
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        lst = db.Producto.Where(p => p.Bhabilitado == "A").Include(x => x.IidstockNavigation)
                            .Include(x => x.IidstockNavigation).Select(p => new ListReporteInventario
                            {
                                Nombrebodega = "Sala de venta".ToUpper(),
                                Nombreproducto = p.Descripcion,
                                Nombreunidad = p.IidunidadmedidaNavigation.Nombreunidad,
                                Precio = p.Precioventa.ToString(),
                                Nombrestock = p.IidstockNavigation.Nombrestock,
                                Cantidad = (long)p.Existencias,
                                Nombresubunidad = db.Unidadmedida.Where(y => y.Iidunidadmedida == p.Subunidad).FirstOrDefault().Nombreunidad,
                                Subcantidad = (decimal)p.Subexistencia,
                                Subprecio = p.Subprecioventa.ToString(),
                                Codigoproducto = p.Codigoproducto
                            }).ToList();
                        var listaBodegas = db.Inventario.Where(p => p.Bhabilitado == "A")
                            .Include(x => x.IidstockNavigation).Include(x => x.IidbodegaNavigation)
                            .Include(x => x.IidproductoNavigation).Select(p => new ListReporteInventario
                            {
                                Nombrebodega = p.IidbodegaNavigation.Nombrebodega,
                                Nombreproducto = p.IidproductoNavigation.Descripcion,
                                Nombreunidad = db.Unidadmedida.Where(y => y.Iidunidadmedida == p.IidproductoNavigation.Iidunidadmedida).FirstOrDefault().Nombreunidad,
                                Precio = p.IidproductoNavigation.Precioventa.ToString(),
                                Nombrestock = p.IidstockNavigation.Nombrestock,
                                Cantidad = (long)p.IidproductoNavigation.Existencias,
                                Nombresubunidad = db.Unidadmedida.Where(y => y.Iidunidadmedida == p.IidproductoNavigation.Subunidad).FirstOrDefault().Nombreunidad,
                                Subcantidad = (decimal)p.IidproductoNavigation.Subexistencia,
                                Subprecio = p.IidproductoNavigation.Subprecioventa.ToString(),
                                Codigoproducto = p.IidproductoNavigation.Codigoproducto
                            }).ToList();
                        lst.AddRange(listaBodegas);
                    }
                }
                //ordenamos la lista por el nombre del stock
                lst = lst.OrderBy(p => p.Nombrestock).ToList();
                //serializamos la lista y la guardamos en una cookie
                HttpContext.Session.SetString("ReporteInventario", JsonConvert.SerializeObject(lst));
                return "ok";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
