using System;
using System.Collections.Generic;
using System.Linq;
using AdminFerreteria.Helper.HelperEncriptar;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminFerreteria.Helper.HelperReportes;
using AdminFerreteria.BussinesLogic;

namespace AdminFerreteria.Controllers
{
    public class UtilidadesController : Controller
    {
        static int nveces = 0;
        public static string obtenerNombrePagina(string action, string controller)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Pagina.Where(p => p.Accion.Equals(action) && p.Controlador.Equals(controller) && p.Bhabilitado == "A").First().Mensaje;
            }
        }
        public static Bitacoraentrada crearBitacora(Int64 idBodega, Int64 idProducto, Int64 cantidad,
            Int64 idstock, decimal subcantidad, Int64 identrada)
        {
            Bitacoraentrada bitacoraentrada = new Bitacoraentrada();
            bitacoraentrada.Iidentrada = identrada;
            bitacoraentrada.Iidbodega = (int)idBodega;
            bitacoraentrada.Iidstock = (int)idstock;
            bitacoraentrada.Iidproducto = idProducto;
            bitacoraentrada.Cantidad = cantidad;
            bitacoraentrada.Subcantidad = subcantidad;
            return bitacoraentrada;
        }
        public static int yaExiteEsteInventario(int id,int idproducto)
        {
            using (var db = new BDFERRETERIAContext())
            {
                nveces = db.Inventario.Where(p => p.Iidbodega == id && p.Iidproducto == idproducto && p.Bhabilitado == "A").Count();
                return nveces;
            }
        }
        public static int BodegaEnUso(int id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                nveces = db.Inventario.Where(p => p.Iidbodega == id && p.Bhabilitado == "A" && p.Cantidad > 0).Count();
                return nveces;
            }
        }
        public static string ObtenerNombreSubUnidad(int? id)
        {
            //metodo que nos sirve para obtener el nombre de la subunidad
            using (var db = new BDFERRETERIAContext())
            {
                var unidad = db.Unidadmedida.Where(p => p.Iidunidadmedida.Equals(id)).FirstOrDefault();
                if (unidad != null)
                    return unidad.Nombreunidad;
                else
                    return "No tiene";
            }
        }
        public static int existEmpleado(Empleado oEmpleado)
        {
            using(var db = new BDFERRETERIAContext())
            {
                nveces = db.Empleado.Where(p => p.Iidempleado != oEmpleado.Iidempleado && 
                    p.Dui.ToLower() == oEmpleado.Dui.ToLower() &&
                    p.Bhabilitado=="A").Count();
                return nveces;
            }
        }
        public static int existUsuario(Usuario oUsuario)
        {
            using (var db = new BDFERRETERIAContext())
            {
                nveces = db.Usuario.Where(p => p.Iidusuario != oUsuario.Iidusuario &&
                    p.Nombreusuario.ToLower() == oUsuario.Nombreusuario.ToLower() &&
                    p.Bhabilitado == "A").Count();
                return nveces;
            }
        }
        public static int existStock(Stock stock)
        {
            using (var db = new BDFERRETERIAContext())
            {
                nveces = db.Stock.Where(p => p.Iidstock != stock.Iidstock &&
                    p.Nombrestock==stock.Nombrestock && p.Bhabilitado == "A").Count();
                return nveces;
            }
        }
        public static int existCliente(Cliente cliente)
        {
            using (var db = new BDFERRETERIAContext())
            {
                nveces = db.Cliente.Where(p => p.Iidcliente != cliente.Iidcliente &&
                    p.Nombrecompleto == cliente.Nombrecompleto && p.Registro==cliente.Registro &&
                    p.Nit==cliente.Nit && p.Bhabilitado == "A").Count();
                return nveces;
            }
        }
        public static int existTipoUsuario(Tipousuario tipousuario)
        {
            using (var db = new BDFERRETERIAContext())
            {
                nveces = db.Tipousuario.Where(p => p.Iidtipousuario != tipousuario.Iidtipousuario &&
                    p.Nombretipousuario.ToLower() == tipousuario.Nombretipousuario.ToLower() &&
                    p.Bhabilitado == "A").Count();
                return nveces;
            }
        }
        public static int existProducto(Producto producto)
        {
            using (var db = new BDFERRETERIAContext())
            {
                nveces = db.Producto.Where(p=>p.Bhabilitado=="A" && p.Iidproducto!=producto.Iidproducto
                    && p.Codigoproducto==producto.Codigoproducto).Count();
                return nveces;
            }
        }
        public static int existDetalleCotizacion(long idCotizacion,long idProducto)
        {
            using (var db = new BDFERRETERIAContext())
            {
                nveces = db.Detallecotizacion.Where(p =>p.Iidproducto == idProducto
                    && p.Iidcotizacion == idCotizacion).Count();
                return nveces;
            }
        }
        public static int existUnidad(Unidadmedida unidad)
        {
            using (var db = new BDFERRETERIAContext())
            {
                nveces = db.Unidadmedida.Where(p => p.Bhabilitado == "A" && p.Iidunidadmedida != unidad.Iidunidadmedida
                    && p.Nombreunidad == unidad.Nombreunidad).Count();
                return nveces;
            }
        }
        public static string encryptPassword(string pPassword)
        {
            return EncriptarStringSHA256.EncriptarTexto(pPassword);
        }
        public static byte[] facturaPDF(Int64 id)
        {
            //metodo que nos servira para crear facturas
            return ReportesSistema.GenerarfacturaPDF(id);
        }
        public static byte[] crearCotizacion(Int64 id)
        {
            return ReportesSistema.GenerarCotizacionPDF(id);
        }
        public static byte[] crearReporteVenta(List<ListFactura> lstFactura)
        {
            return ReportesSistema.GenerarReporteVentaPDF(lstFactura);
        }
        
        public static string crearNoDocumento(string noactual, int nodigitos)
        {
            string codigo = "";
            codigo = noactual;//agregamos el numero actual que esta en la configuracion
            int i = 0;
            while (i <= (nodigitos - 1))
            {
                codigo = "0" + codigo;
                i = codigo.Length;
            }
            return codigo;
        }
        public static Detallepedido crearDetallePedido(List<DetalleVenta> lstDetalleVenta, Int64 idFactura)
        {
            //instancia del modelo detalle pedido
            Detallepedido oDetallepedido = new Detallepedido();
            //armamos el objeto a guardar
            oDetallepedido.Iidfactura = idFactura;
            oDetallepedido.Iidproducto = lstDetalleVenta[0].iidproducto;
            oDetallepedido.Precioactual = lstDetalleVenta[0].preciounitario;
            oDetallepedido.Cantidad = lstDetalleVenta[0].cantidad;
            oDetallepedido.Porcentajedescuento = lstDetalleVenta[0].pdescuento;
            oDetallepedido.Descuento = lstDetalleVenta[0].descuento;
            oDetallepedido.Porcentajecomision = lstDetalleVenta[0].pcomision;
            oDetallepedido.Comision = lstDetalleVenta[0].comision;
            oDetallepedido.Subtotal = lstDetalleVenta[0].total;
            oDetallepedido.Fechacreacion = DateTime.Now;
            oDetallepedido.Bhabilitado = "A";
            if (lstDetalleVenta[0].Essubproducto)//SI ES UN SUB PRODUCTO PONEMOS SI
                oDetallepedido.Essubproducto = "SI";
            else//SI NO GUARDAMOS NO
                oDetallepedido.Essubproducto = "NO";
            return oDetallepedido;
        }
        public static Factura crearFactura(Factura factura)
        {
            try
            {
                Factura pfactura = new Factura();//creamos un objeto nuevo
                pfactura.Iidusuario = factura.Iidusuario;
                pfactura.Tipocomprador = factura.Tipocomprador;
                pfactura.Nombrecliente = factura.Nombrecliente;
                pfactura.Direccion = factura.Direccion;
                pfactura.Registro = factura.Registro;
                pfactura.Giro = factura.Giro;
                pfactura.Nit = factura.Nit;
                pfactura.Total = factura.Total;
                pfactura.Bhabilitado = "A";
                pfactura.Fechacreacion = DateTime.Now;
                pfactura.Facturaemitida = "NO";
                pfactura.Nofactura = factura.Nofactura;
                pfactura.Totalcomision = factura.Totalcomision;
                pfactura.Totaldescuento = factura.Totaldescuento;
                pfactura.Porcentajedescuentoglobal = factura.Porcentajedescuentoglobal;
                pfactura.Descuentoglobal = factura.Descuentoglobal;
                return pfactura;
            }
            catch(Exception e) 
            {
                return null;
            }
        }
        public static Cliente crearClienteCreditoFiscal(Factura pFactura)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombrecompleto = pFactura.Nombrecliente;
                cliente.Direccion = pFactura.Direccion;
                cliente.Registro = pFactura.Registro;
                cliente.Giro = pFactura.Giro;
                cliente.Nit = pFactura.Nit;
                cliente.Fechacreacion = DateTime.Now;
                cliente.Bhabilitado = "A";
                return cliente;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static Cotizacion crearCotizacion(string nombre,int idUsuarioLogueado,int tipodocumento,string noactual,int nodigitos)
        {
            try
            {
                Cotizacion cotizacion = new Cotizacion();
                cotizacion.Bhabilitado = "A";
                cotizacion.Nombrecliente = nombre.ToUpper();
                cotizacion.Cotizacionfacturada = "N";
                cotizacion.Fechacreacion = DateTime.Now;
                cotizacion.Cotizacionemitida = "NO";
                cotizacion.Iidusuario = idUsuarioLogueado;
                if (tipodocumento == 1)//si es una cotizacion real se guarda la configuracion
                {
                    cotizacion.Nocotizacion = crearNoDocumento(noactual, nodigitos);
                }
                else
                {
                    cotizacion.Nocotizacion = "-1000";//si no se le agrega un numero negativo
                }
                cotizacion.Fechavencimiento = DateTime.Now.AddDays(30);
                return cotizacion;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static Detallecotizacion crearDetalleCotizacion(List<DetalleVenta>item,Cotizacion cotizacion)
        {
            try
            {
                Detallecotizacion oDetalle = new Detallecotizacion();
                //armamos el objeto a guardar
                oDetalle.Iidcotizacion = cotizacion.Iidcotizacion;
                oDetalle.Iidproducto = item[0].iidproducto;
                oDetalle.Precioactual = item[0].preciounitario;
                oDetalle.Cantidad = item[0].cantidad;
                oDetalle.Porcentajedescuento = item[0].pdescuento;
                oDetalle.Descuento = item[0].descuento;
                oDetalle.Porcentajecomision = item[0].pcomision;
                oDetalle.Comision = item[0].comision;
                oDetalle.Subtotal = item[0].total;
                oDetalle.Fechavencimiento = DateTime.Now;
                oDetalle.Bhabilitado = "A";
                if (item[0].Essubproducto)//SI ES UN SUB PRODUCTO PONEMOS SI
                    oDetalle.Essubproducto = "SI";
                else//SINO GUARDAMOS NO
                    oDetalle.Essubproducto = "NO";
                return oDetalle;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static byte[] facturaPDFprovisional(Int64 id)
        {
            return ReportesSistema.GenerarfacturaProvisionalPDF(id);
        }

        public static Inventario crearObjetInventario(int idBodega, int idProducto, int cantidad, int stock)
        {
            Inventario inventario = new Inventario();
            inventario.Iidbodega = idBodega;
            inventario.Iidproducto = idProducto;
            inventario.Cantidad = cantidad;
            inventario.Iidstock = stock;
            inventario.Bhabilitado = "A";
            return inventario;
        }

        public static byte[] crearReporteInventario<T>(string tipo, List<T> lst, string[] cabecera, string[] propiedades)
        {
            if(tipo=="excel")
                return ReportesSistema.GenerarReporteInventarioExcel(lst, cabecera, propiedades);
            else
                return ReportesSistema.GenerarReporteInventarioPDF(lst, cabecera, propiedades);
        }

        public static List<ListReporteInventario> crearListaReporteSala(Inventario inventario, string nombrestock)
        {
            List<ListReporteInventario> lst = new List<ListReporteInventario>();
            using(var db = new BDFERRETERIAContext())
            {
                #region obtenemos el listado
                if (nombrestock != null)
                {
                    lst = db.Producto.Where(p => p.Bhabilitado == "A" && p.IidstockNavigation.Nombrestock.Contains(nombrestock))
                        .Include(x => x.IidstockNavigation).Include(x => x.IidstockNavigation)
                    .Select(p => new ListReporteInventario
                    {
                        Nombrebodega = "Sala de venta".ToUpper(),
                        Nombreproducto = p.Descripcion,
                        Iidproducto = p.Iidproducto,
                        Nombreunidad = p.IidunidadmedidaNavigation.Nombreunidad,
                        Precio = p.Precioventa.ToString(),
                        Nombrestock = p.IidstockNavigation.Nombrestock,
                        Cantidad = (long)p.Existencias,
                        Nombresubunidad = db.Unidadmedida.Where(y => y.Iidunidadmedida == p.Subunidad).FirstOrDefault().Nombreunidad,
                        Subcantidad = (decimal)p.Subexistencia,
                        Subprecio = p.Subprecioventa.ToString(),
                        Codigoproducto = p.Codigoproducto
                    }).ToList();
                }
                else
                {
                    lst = db.Producto.Where(p => p.Bhabilitado == "A").Include(x => x.IidstockNavigation).Include(x => x.IidstockNavigation)
                    .Select(p => new ListReporteInventario
                    {
                        Nombrebodega = "Sala de venta".ToUpper(),
                        Nombreproducto = p.Descripcion,
                        Nombreunidad = p.IidunidadmedidaNavigation.Nombreunidad,
                        Iidproducto = p.Iidproducto,
                        Precio = p.Precioventa.ToString(),
                        Nombrestock = p.IidstockNavigation.Nombrestock,
                        Cantidad = (long)p.Existencias,
                        Nombresubunidad = db.Unidadmedida.Where(y => y.Iidunidadmedida == p.Subunidad).FirstOrDefault().Nombreunidad,
                        Subcantidad = (decimal)p.Subexistencia,
                        Subprecio = p.Subprecioventa.ToString(),
                        Codigoproducto = p.Codigoproducto
                    }).ToList();
                }
                #endregion

                lst = ReporteBL.ObtenerProveedoresReporteInventario(db, lst);
            }
            return lst;
        }

        public static List<ListReporteInventario> crearlistaReporteBodega(Inventario inventario, string nombrestock)
        {
            List<ListReporteInventario> lst = new List<ListReporteInventario>();
            using(var db = new BDFERRETERIAContext())
            {
                #region obtenemos el listado
                if (inventario.Iidbodega > 0 && nombrestock != null)  //si ambos traen parametro entonces es una lista especifica
                {
                    lst = db.Inventario.Where(p => p.Bhabilitado == "A" && p.IidstockNavigation.Nombrestock.Contains(nombrestock)
                    && p.Iidbodega == inventario.Iidbodega).Include(x => x.IidstockNavigation).Include(x => x.IidstockNavigation)
                    .Include(x => x.IidproductoNavigation).Select(p => new ListReporteInventario
                    {
                        Nombrebodega = p.IidbodegaNavigation.Nombrebodega,
                        Nombreproducto = p.IidproductoNavigation.Descripcion,
                        Iidproducto = p.IidproductoNavigation.Iidproducto,
                        Nombreunidad = db.Unidadmedida.Where(y => y.Iidunidadmedida == p.IidproductoNavigation.Iidunidadmedida).FirstOrDefault().Nombreunidad,
                        Precio = p.IidproductoNavigation.Precioventa.ToString(),
                        Nombrestock = p.IidstockNavigation.Nombrestock,
                        Cantidad = (long)p.IidproductoNavigation.Existencias,
                        Nombresubunidad = db.Unidadmedida.Where(y => y.Iidunidadmedida == p.IidproductoNavigation.Subunidad).FirstOrDefault().Nombreunidad,
                        Subcantidad = (decimal)(p.Cantidad * p.IidproductoNavigation.Equivalencia),
                        Subprecio = p.IidproductoNavigation.Subprecioventa.ToString(),
                        Codigoproducto = p.IidproductoNavigation.Codigoproducto
                    }).ToList();
                }
                else if (inventario.Iidbodega > 0 && nombrestock == null)  //si solo es bodega entonces se listan todos los stock
                {
                    lst = db.Inventario.Where(p => p.Bhabilitado == "A" && p.Iidbodega == inventario.Iidbodega)
                    .Include(x => x.IidstockNavigation).Include(x => x.IidbodegaNavigation)
                    .Include(x => x.IidproductoNavigation).Select(p => new ListReporteInventario
                    {
                        Iidproducto = p.IidproductoNavigation.Iidproducto,
                        Nombrebodega = p.IidbodegaNavigation.Nombrebodega,
                        Nombreproducto = p.IidproductoNavigation.Descripcion,
                        Nombreunidad = db.Unidadmedida.Where(y => y.Iidunidadmedida == p.IidproductoNavigation.Iidunidadmedida).FirstOrDefault().Nombreunidad,
                        Precio = p.IidproductoNavigation.Precioventa.ToString(),
                        Nombrestock = p.IidstockNavigation.Nombrestock,
                        Cantidad = (long)p.IidproductoNavigation.Existencias,
                        Nombresubunidad = db.Unidadmedida.Where(y => y.Iidunidadmedida == p.IidproductoNavigation.Subunidad).FirstOrDefault().Nombreunidad,
                        Subcantidad = (decimal)(p.Cantidad * p.IidproductoNavigation.Equivalencia),
                        Subprecio = p.IidproductoNavigation.Subprecioventa.ToString(),
                        Codigoproducto = p.IidproductoNavigation.Codigoproducto
                    }).ToList();
                }
                #endregion

                lst = ReporteBL.ObtenerProveedoresReporteInventario(db, lst);
            }
            return lst;
        }
    }
}
