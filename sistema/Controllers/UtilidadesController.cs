using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace AdminFerreteria.Controllers
{
    public class UtilidadesController : Controller
    {
        public static string obtenerNombrePagina(string action, string controller)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Pagina.Where(p => p.Accion.Equals(action) && p.Controlador.Equals(controller) && p.Bhabilitado == "A").First().Mensaje;
            }
        }
        static int nveces = 0;
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
        public static bool seguridadCRUD(int? idUsuario)
        {
            if (idUsuario != null)
                return true;
            else
                return false;
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
        public static bool youHavePermissionToViewPage(string controller, string action, int idUsuario)
        {
            /// <summary>
            /// este metodo funciona como filtro para evitar que los usuarios accedan mediante url
            /// </summary>
            /// <param name="controller">pasamos el nombre del controlador</param>
            /// <param name="action">El nombre de la accion</param>
            /// <param name="idUsuario">y el id del usuario logueado para obtener su tipo de usuario</param>
            /// <returns></returns>
            int existe = 0;
            try
            {
                using (var bd = new BDFERRETERIAContext())
                {
                    var tipoUsuario = bd.Usuario.Where(p => p.Iidusuario == idUsuario).First();//capturamos el tipo de usuario de la cokie
                    if (tipoUsuario.Bhabilitado == "A")
                    {
                        existe = (from ptu in bd.Paginatipousuario
                                  join pagina in bd.Pagina on ptu.Iidpagina equals pagina.Iidpagina
                                  where pagina.Accion.ToLower() == action.ToLower() &&
                                  pagina.Controlador.ToLower() == controller.ToLower() &&
                                  ptu.Iidtipousuario == tipoUsuario.Iidtipousuario && ptu.Bhabilitado == "A"
                                  select new Pagina { Iidpagina = pagina.Iidpagina }).Count();
                        if (existe > 0)//si el tipo de usuario tiene relacionada esa pagina se retorna un true
                            return true;
                        else//de lo contrario un false
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
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
            SHA256Managed sha = new SHA256Managed();//instancia
            byte[] dataNoCifrada = Encoding.Default.GetBytes(pPassword);//convertimos a arreglo de byte la contraseña
            byte[] dataCifrada = sha.ComputeHash(dataNoCifrada);//pasamos el array de byte para cifrar
            var passwordEncrypt = BitConverter.ToString(dataCifrada);//convertimos a texto
            passwordEncrypt = passwordEncrypt.Replace("-", "");//reemplazamos los - por vacios
            return passwordEncrypt;//retornamos la contraseña en una cadena hexadecimal
        }
        public static byte[] facturaPDF(Int64 id)
        {
            //metodo que nos servira para crear facturas
            using (var memoryString = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryString);
                using(var facturaPDF = new PdfDocument(writer))
                {
                    using(var db = new BDFERRETERIAContext())
                    {
                        //para factura PageSize.A6
                        Document doc = new Document(facturaPDF, PageSize.A5);
                        //doc.SetMargins(5,10,5,10);//arriba, derecha, abajo, izquierda
                        #region obtenemos la data de la factura

                        Factura factura = (from f in db.Factura
                                           where f.Iidfactura == id
                                           select new Factura
                                           {
                                               Iidusuario = f.Iidusuario,
                                               Iidfactura = f.Iidfactura,
                                               Nofactura = f.Nofactura,
                                               Tipocomprador = f.Tipocomprador.ToUpper(),
                                               Nombrecliente = f.Nombrecliente.ToUpper(),
                                               Direccion = f.Direccion.ToUpper() == null ? "" : f.Direccion,
                                               Registro = f.Registro.ToUpper() == null ? "" : f.Registro,
                                               Giro = f.Giro.ToUpper() == null ? "" : f.Giro,
                                               Nit = f.Nit == null ? "" : f.Nit,
                                               Fechacreacion = f.Fechacreacion,
                                               Total = f.Total,
                                               Descuentoglobal = f.Descuentoglobal,
                                               Porcentajedescuentoglobal = f.Porcentajedescuentoglobal
                                           }).First();//obtenemos el objeto de la factura
                        List<DetalleVenta> detallePedido = (from detalle in db.Detallepedido
                                                            join producto in db.Producto on
                                                            detalle.Iidproducto equals producto.Iidproducto
                                                            join unidad in db.Unidadmedida on
                                                            producto.Iidunidadmedida equals unidad.Iidunidadmedida
                                                            where detalle.Iidfactura == id
                                                            select new DetalleVenta
                                                            {
                                                                cantidad = detalle.Cantidad,
                                                                nombreproducto = producto.Descripcion.ToUpper(),
                                                                preciounitario = (decimal)detalle.Precioactual,
                                                                total = (decimal)detalle.Subtotal,
                                                                descuento = (decimal)detalle.Descuento,
                                                                pdescuento = detalle.Porcentajedescuento,
                                                                comision = (decimal)detalle.Comision,
                                                                unidadmedida = unidad.Nombreunidad,
                                                                precioActual = (decimal)detalle.Precioactual,
                                                                Nombresubunidad = ObtenerNombreSubUnidad(producto.Subunidad),
                                                                subproducto = detalle.Essubproducto,
                                                                iva = (decimal)producto.Iva,
                                                                codigoproducto = producto.Codigoproducto,
                                                                subiva=producto.Subiva
                                                            }).ToList();//listamos el detalle del pedido
                        #endregion

                        #region detalle del pedido
                        doc.SetLeftMargin(15);
                        if (factura.Tipocomprador == "CLIENTE FINAL")
                        {
                            doc.SetTopMargin(5);
                            Paragraph paragraph = new Paragraph(factura.Nofactura+" \n \n \n \n");
                            doc.Add(paragraph.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFontSize(7));
                            #region inicio de la factura nombre cliente
                            string fechafactura = factura.Fechacreacion.ToShortDateString();
                            Paragraph fecha = new Paragraph("\n"+fechafactura);
                            fecha.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                            fecha.SetFontSize(8);
                            doc.Add(fecha);
                            //salto
                            Table datosCliente = new Table(2);
                            Cell cellDatos = new Cell().Add(new Paragraph("ESPACIO").SetFontSize(8).SetWidth(22).SetFontColor(ColorConstants.WHITE));
                            datosCliente.AddCell(cellDatos.SetBorder(Border.NO_BORDER));
                            cellDatos = new Cell().Add(new Paragraph(factura.Nombrecliente).SetFontSize(8));
                            datosCliente.AddCell(cellDatos.SetBorder(Border.NO_BORDER));
                            doc.Add(datosCliente);

                            Paragraph direccion = new Paragraph(factura.Direccion);
                            direccion.SetFontSize(8);
                            doc.Add(direccion);

                            #endregion

                            string afectado = "";
                            if (factura.Descuentoglobal > 0) { afectado = "*"; }
                            Paragraph espacio = new Paragraph("");
                            doc.Add(espacio);espacio = new Paragraph("");
                            doc.Add(espacio); espacio = new Paragraph("");
                            doc.Add(espacio);
                            espacio = new Paragraph(""); doc.Add(espacio); espacio = new Paragraph(""); doc.Add(espacio);
                            Table tablaProducto = new Table(6).UseAllAvailableWidth();
                            decimal total = 0, totaldescuento = 0;
                            //creamos el cuerpo de la tabla
                            Cell cellBody;
                            int nvecesiterado = 0;
                            foreach (var item in detallePedido)
                            {
                                total += item.total; totaldescuento += item.descuento;

                                //cant
                                cellBody = new Cell().Add(new Paragraph(item.cantidad.ToString()).SetFontSize(7).SetWidth(35));
                                tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                string descuento = "";
                                if (item.pdescuento > 0)
                                {
                                    descuento = "[D: "+ item.pdescuento.ToString() + "%]";
                                }
                                if (item.subproducto == "NO") //validamos que unidad es
                                {
                                    //descripcion
                                    cellBody = new Cell().Add(new Paragraph(item.nombreproducto+" "
                                        +item.unidadmedida + " " + descuento + " " + afectado).SetFontSize(6).SetWidth(232));
                                    tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                }
                                else
                                {
                                    //descripcion
                                    cellBody = new Cell().Add(new Paragraph(item.nombreproducto +
                                        " " + item.Nombresubunidad + " " + descuento).SetFontSize(6).SetWidth(232));
                                    tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                }
                                //precio unitario
                                decimal precio = (item.comision / item.cantidad) + item.precioActual;//obtenemos el precio unitario sumando el precio + la comision
                                //precio = Math.Round(precio, 2);
                                cellBody = new Cell().Add(new Paragraph(precio.ToString()).SetFontSize(6).SetWidth(32));
                                tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                //ventas no sujetas 
                                cellBody = new Cell().Add(new Paragraph("ESPACIO").SetFontSize(6).SetFontColor(ColorConstants.WHITE));
                                tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                //ventas exentas 
                                cellBody = new Cell().Add(new Paragraph("ESPACIO").SetFontSize(6).SetWidth(32).SetFontColor(ColorConstants.WHITE)); 
                                tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                //ventas afectadas
                                item.total = Math.Round(item.total, 2);//se redondea
                                cellBody = new Cell().Add(new Paragraph(item.total.ToString()).SetFontSize(6));
                                tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                nvecesiterado++;
                            }
                            for (int i = nvecesiterado; i < 16; i++) 
                            {
                                for (var j = 0; j < 6; j++) 
                                {
                                    cellBody = new Cell().Add(new Paragraph("ESPAC").SetFontSize(6).SetFontColor(ColorConstants.WHITE));
                                    tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                }
                            }
                            #region footer tabla
                            cellBody = new Cell().Add(new Paragraph("").SetFontSize(8).SetFontColor(ColorConstants.BLACK));
                            tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                            var user = db.Usuario.Where(p => p.Iidtipousuario == factura.Iidusuario)
                                    .Include(x => x.IidempleadoNavigation).First();
                            if (factura.Descuentoglobal > 0)
                            {
                                factura.Descuentoglobal = Math.Round(factura.Descuentoglobal, 2);
                                cellBody = new Cell(1, 5).Add(new Paragraph("Desc. de $" + factura.Descuentoglobal + " (" + factura.Porcentajedescuentoglobal + "%) productos con * afectados" +
                                    " Vendedor: " + user.IidempleadoNavigation.Nombrecompleto)
                                    .SetFontSize(7).SetFontColor(ColorConstants.BLACK));
                                tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                            }
                            else
                            {
                                factura.Descuentoglobal = Math.Round(factura.Descuentoglobal, 2);
                                cellBody = new Cell(1, 5).Add(new Paragraph("Vendedor:  " + user.IidempleadoNavigation.Nombrecompleto)
                                    .SetFontSize(7).SetFontColor(ColorConstants.BLACK));
                                tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                            }
                            //pegar el total al final
                            for (int i = 0; i < 6; i++)
                            {
                                for (int j = 0; j < 6; j++)
                                {
                                    if(i==0 && j == 5)
                                    {
                                        cellBody = new Cell().Add(new Paragraph(Math.Round(total, 2).ToString()).SetFontSize(6).SetFontColor(ColorConstants.BLACK));
                                        tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                    }
                                    else if (i == 5 && j == 5) 
                                    {
                                        factura.Total = Math.Round((decimal) factura.Total, 2);
                                        cellBody = new Cell().Add(new Paragraph(factura.Total.ToString()).SetFontSize(6).SetFontColor(ColorConstants.BLACK));
                                        tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                    }
                                    else
                                    {
                                        cellBody = new Cell().Add(new Paragraph("ESPA").SetFontSize(7).SetFontColor(ColorConstants.WHITE));
                                        tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                                    }
                                }
                            }
                            doc.Add(tablaProducto);//agrego la tabla
                            #endregion
                        }//FIN CLIENTE FINAL
                        else
                        {

                            Paragraph paragraph = new Paragraph(factura.Nofactura+" \n \n \n");
                            doc.Add(paragraph.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFontSize(7));
                            #region inicio de la factura nombre cliente
                            Table datosCliente = new Table(4);
                            Cell celdaCliente;
                            //espacios
                            celdaCliente = new Cell().Add(new Paragraph("ESP.").SetFontSize(7).SetFontColor(ColorConstants.WHITE));
                            datosCliente.AddCell(celdaCliente.SetBorder(Border.NO_BORDER));
                            //nombre cliente
                            celdaCliente = new Cell().Add(new Paragraph(factura.Nombrecliente + "\n" + "").SetFontSize(7));
                            datosCliente.AddCell(celdaCliente.SetBorder(Border.NO_BORDER));

                            //espacios
                            celdaCliente = new Cell().Add(new Paragraph("ESP.").SetFontSize(7).SetWidth(60).SetFontColor(ColorConstants.WHITE));
                            datosCliente.AddCell(celdaCliente.SetBorder(Border.NO_BORDER));

                            //fecha
                            string fechafactura = factura.Fechacreacion.ToShortDateString();
                            celdaCliente = new Cell().Add(new Paragraph(fechafactura + "\n" + "").SetFontSize(7));
                            datosCliente.AddCell(celdaCliente.SetBorder(Border.NO_BORDER));

                            //espacios
                            celdaCliente = new Cell().Add(new Paragraph("ESP.").SetFontSize(7).SetFontColor(ColorConstants.WHITE));
                            datosCliente.AddCell(celdaCliente.SetBorder(Border.NO_BORDER));

                            //direccion
                            celdaCliente = new Cell().Add(new Paragraph("\n \n" + factura.Direccion + "\n" + "").SetFontSize(7));
                            datosCliente.AddCell(celdaCliente.SetBorder(Border.NO_BORDER));

                            //espacios
                            celdaCliente = new Cell().Add(new Paragraph("ESP.").SetFontSize(7).SetFontColor(ColorConstants.WHITE));
                            datosCliente.AddCell(celdaCliente.SetBorder(Border.NO_BORDER));

                            //nrc Y giro
                            celdaCliente = new Cell().Add(new Paragraph("\n \n" + factura.Registro + "\n" + factura.Giro).SetFontSize(7));
                            datosCliente.AddCell(celdaCliente.SetBorder(Border.NO_BORDER));

                            //espacios
                            celdaCliente = new Cell().Add(new Paragraph("ESP.").SetFontSize(7).SetFontColor(ColorConstants.WHITE));
                            datosCliente.AddCell(celdaCliente.SetBorder(Border.NO_BORDER));

                            //Nit
                            celdaCliente = new Cell().Add(new Paragraph(factura.Nit).SetFontSize(7));
                            datosCliente.AddCell(celdaCliente.SetBorder(Border.NO_BORDER));

                            doc.Add(datosCliente.UseAllAvailableWidth());
                            #endregion

                            #region cuerpo de la factura
                            paragraph = new Paragraph("\n \n \n");
                            doc.Add(paragraph);
                            string[] cabecera = { "Cant", "Descripcion", "Precio\nunitario", "venta no\nsujeta", "exentas", "Gravadas" };
                            Table tablaProducto = new Table(6).UseAllAvailableWidth();
                            decimal total = 0;
                            //creamos el cuerpo de la tabla
                            Cell cellBody;
                            decimal ivaDelTotal = 0;
                            int iterador = 0;
                            foreach (var item in detallePedido)
                            {
                                iterador++;
                                cellBody = new Cell().Add(new Paragraph(item.cantidad.ToString()).SetFontSize(7).SetWidth(27));
                                tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE,1)));

                                if (item.subproducto == "NO") //validamos que unidad es
                                {

                                    cellBody = new Cell().Add(new Paragraph(item.nombreproducto + " " + item.unidadmedida).SetFontSize(7).SetWidth(238));
                                    tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));

                                    decimal precio = item.precioActual - item.iva;//obtenemos el precio sin iva
                                    precio = Math.Round(precio, 4);
                                    ivaDelTotal= item.iva * item.cantidad;
                                    cellBody = new Cell().Add(new Paragraph(precio.ToString()).SetFontSize(6));
                                    tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));
                                }
                                else
                                {
                                    cellBody = new Cell().Add(new Paragraph(item.nombreproducto + " " + item.Nombresubunidad).SetFontSize(7).SetWidth(238));
                                    tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));

                                    decimal precio = item.precioActual - (decimal)item.subiva;//obtenemos el precio sin iva
                                    precio = Math.Round(precio, 4);
                                    ivaDelTotal = (decimal)item.subiva * item.cantidad;
                                    cellBody = new Cell().Add(new Paragraph(precio.ToString()).SetFontSize(6).SetWidth(24));
                                    tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));
                                }

                                #region espacios vacios
                                cellBody = new Cell().Add(new Paragraph("ESP.").SetFontSize(7).SetFontColor(ColorConstants.WHITE).SetWidth(20));
                                tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));

                                cellBody = new Cell().Add(new Paragraph("ESP.").SetFontSize(7).SetFontColor(ColorConstants.WHITE).SetWidth(20));
                                tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));
                                #endregion
                                item.total = item.total - ivaDelTotal;
                                item.total = Math.Round(item.total, 2);
                                cellBody = new Cell().Add(new Paragraph(item.total.ToString()).SetFontSize(7));
                                tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));
                                total = total + item.total;
                            }

                            for(int i =iterador; i < 13;i++)
                            {
                                for(int c= 0; c < 6; c++)
                                {
                                    cellBody = new Cell().Add(new Paragraph("ESP.").SetFontSize(7).SetFontColor(ColorConstants.WHITE));
                                    tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));
                                }
                            }
                            cellBody = new Cell(1, 5).Add(new Paragraph("sumas").SetFontSize(7).SetFontColor(ColorConstants.WHITE)
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                            tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));

                            cellBody = new Cell().Add(new Paragraph(Math.Round(total, 4).ToString()).SetFontSize(7).SetFontColor(ColorConstants.BLACK));
                            tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));

                            //final de la tabla
                            #region final de la tabla
                            //agregamos los resultados
                            decimal ivaSumando = (total / 100) * 13;
                            decimal totalFinal = ivaSumando + total;
                            for (int i = 0; i < 6; i++)
                            {
                                for (int c = 0; c < 6; c++)
                                {
                                    if (i == 0 && c == 5)//iva
                                    {
                                        cellBody = new Cell().Add(new Paragraph(Math.Round(ivaSumando, 2).ToString()).SetFontSize(7));
                                        tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));
                                    }
                                    else if (i == 1 && c == 5)//sub total
                                    {
                                        cellBody = new Cell().Add(new Paragraph(total.ToString()).SetFontSize(7));
                                        tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));
                                    }
                                    else if (i == 5 && c == 5)//sub total + iva
                                    {
                                        factura.Total = Math.Round((decimal)factura.Total, 2);
                                        cellBody = new Cell().Add(new Paragraph(factura.Total.ToString()).SetFontSize(7));
                                        tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));
                                    }
                                    else
                                    {
                                        cellBody = new Cell().Add(new Paragraph("ESP.").SetFontSize(5).SetFontColor(ColorConstants.WHITE));
                                        tablaProducto.AddCell(cellBody.SetBorder(new SolidBorder(ColorConstants.WHITE, 1)));
                                    }
                                }
                            }
                            #endregion

                            doc.Add(tablaProducto);//agrego la tabla
                            #endregion
                        }
                        #endregion

                        doc.Close();//cerramos el documento
                        writer.Close();//y la escritura
                    }
                }
                return memoryString.ToArray();
            }
        }
        public static byte[] crearCotizacion(Int64 id)
        {
            string[] cabecera = { "CANT.", "DESCRIPCION", "PRECIO\n UNITARIO", "SUBTOTAL" };
            using (var memoryString = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryString);
                using (var facturaPDF = new PdfDocument(writer))
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        //para factura PageSize.A6
                        Document doc = new Document(facturaPDF, PageSize.LETTER);
                        #region obtenemos la data de la cotizacion

                        Cotizacion cotizacion = (from f in db.Cotizacion
                                                 where f.Iidcotizacion == id
                                                 select new Cotizacion
                                                 {
                                                     Iidcotizacion = f.Iidcotizacion,
                                                     Nocotizacion = f.Nocotizacion,
                                                     Fechacreacion = f.Fechacreacion,
                                                     Fechavencimiento = f.Fechavencimiento,
                                                     Iidusuario = f.Iidusuario,
                                                     Total = f.Total
                                                 }).First();//obtenemos el objeto de la factura
                        List<DetalleVenta> detalleCotizacion = (from detalle in db.Detallecotizacion
                                                                join producto in db.Producto on
                                                                detalle.Iidproducto equals producto.Iidproducto
                                                                join unidad in db.Unidadmedida on
                                                                producto.Iidunidadmedida equals unidad.Iidunidadmedida
                                                                where detalle.Iidcotizacion == id
                                                                select new DetalleVenta
                                                                {
                                                                    codigoproducto = producto.Codigoproducto,
                                                                    cantidad = detalle.Cantidad,
                                                                    nombreproducto = producto.Descripcion.ToUpper(),
                                                                    preciounitario = (decimal)detalle.Precioactual,
                                                                    total = (decimal)detalle.Subtotal,
                                                                    descuento = (decimal)detalle.Descuento,
                                                                    comision = (decimal)detalle.Comision,
                                                                    unidadmedida = unidad.Nombreunidad,
                                                                    precioActual = (decimal)detalle.Precioactual
                                                                }).ToList();//listamos el detalle del pedido
                        #endregion

                        #region creamos el inicio de la cotizacion
                        Paragraph titulo = new Paragraph("Materiales Eléctricos DYVAL & Ferreteria La Terminal").SetFontSize(16)
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontColor(ColorConstants.WHITE);
                        doc.Add(titulo);
                        //salto
                        Paragraph paragraph1 = new Paragraph("DISTRIBUIDOR DE MATERIAL ELECTRICO DE ALTA Y BAJA TENCION. \nSIEMPRE CON LOS MEJORES PRECIOS.");
                        paragraph1.SetFontSize(10).SetFontColor(ColorConstants.WHITE);
                        paragraph1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        doc.Add(paragraph1);
                        Paragraph direccion = new Paragraph("Calle a Nahulingo, Local 6, Urb. Las Victorias, Lote 3, Plaza San Andres I, Prolongacion 6º CI. Ote. Sonsonate. Cel.: 7647-5162");
                        direccion.SetFontSize(8).SetFontColor(ColorConstants.WHITE);
                        direccion.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        doc.Add(direccion);
                        #endregion

                        #region los datos del vendedor

                        string fecha = cotizacion.Fechacreacion.ToString("dd/MM/yyyy");
                        string fechaVencimiento = cotizacion.Fechavencimiento.ToString("dd/MM/yyyy");
                        string nombreVendedor = db.Usuario.Where(p => p.Iidusuario == cotizacion.Iidusuario)
                           .Include(x => x.IidempleadoNavigation).First().IidempleadoNavigation.Nombrecompleto;
                        if (cotizacion.Nocotizacion == "-1000")
                        {
                            Paragraph dtoCliente = new Paragraph("Factura provisional Emitida el " + fecha + " Creado por: " + nombreVendedor);
                            dtoCliente.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                            dtoCliente.SetFontColor(ColorConstants.WHITE);
                            dtoCliente.SetFontSize(10);
                            doc.Add(dtoCliente);
                        }
                        else
                        {
                            Paragraph dtoCliente = new Paragraph("Cotizacion No. " + cotizacion.Nocotizacion.ToString() + " Emitida el " + fecha +
                                " Creado por: " + nombreVendedor);
                            dtoCliente.SetFontColor(ColorConstants.WHITE);
                            dtoCliente.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                            dtoCliente.SetFontSize(10);
                            doc.Add(dtoCliente);
                        }

                        #endregion

                        #region detalle de la cotizacion
                        Paragraph paragraph = new Paragraph("");
                        doc.Add(paragraph);
                        paragraph = new Paragraph("");
                        doc.Add(paragraph);
                        Table tablaProducto = new Table(4).UseAllAvailableWidth();
                        Cell titulotabla = new Cell(1, 4).SetBorder(Border.NO_BORDER).Add(new Paragraph("Con deseo de servirles, nos permitimos ofrecerle" +
                            " la mejor oferta en materiales electricos y construccion").SetFontColor(ColorConstants.WHITE)
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(9)); ;
                        titulotabla.SetBackgroundColor(ColorConstants.WHITE);
                        tablaProducto.AddHeaderCell(titulotabla);
                        Cell celda;
                        //agregamos los encabezados
                        for (int i = 0; i < cabecera.Length; i++)
                        {
                            celda = new Cell();
                            //celda.SetBorder(Border.NO_BORDER);
                            celda.Add(new Paragraph(cabecera[i]).SetFontColor(ColorConstants.WHITE)).SetFontSize(9).SetBorder(Border.NO_BORDER);
                            tablaProducto.AddHeaderCell(celda);//agregamos la celda
                        }
                        decimal total = 0;
                        //creamos el cuerpo de la tabla
                        Cell cellBody;
                        int nvecesIterado = 0;
                        foreach (var item in detalleCotizacion)
                        {
                            nvecesIterado++;
                            cellBody = new Cell().SetBorder(Border.NO_BORDER).Add(new Paragraph(item.cantidad.ToString()).SetFontSize(9).SetWidth(31));
                            tablaProducto.AddCell(cellBody);

                            cellBody = new Cell().SetBorder(Border.NO_BORDER).Add(new Paragraph(item.nombreproducto).SetFontSize(9).SetWidth(272));
                            tablaProducto.AddCell(cellBody);
                            //obtenemos el precio unitario sumando el precio + la comision
                            decimal precio = ((item.precioActual * item.cantidad) + item.comision)/item.cantidad; precio = Math.Round(precio, 4);
                            cellBody = new Cell().SetBorder(Border.NO_BORDER).Add(new Paragraph(precio.ToString()).SetFontSize(9).SetWidth(41));
                            tablaProducto.AddCell(cellBody);

                            decimal subtotal = precio * item.cantidad; subtotal = Math.Round(subtotal, 2);
                            cellBody = new Cell().SetBorder(Border.NO_BORDER).Add(new Paragraph(subtotal.ToString()).SetFontSize(9).SetWidth(41));
                            tablaProducto.AddCell(cellBody);
                            total = total + subtotal;
                        }
                        float[] anchos = { 31, 272, 41, 41 };
                        for(int i=nvecesIterado; i < 25; i++)
                        {
                            for (int column = 0; column < 4; column++) 
                            {
                                cellBody = new Cell().Add(new Paragraph("ESP").SetFontColor(ColorConstants.WHITE).SetFontSize(9)
                                    .SetWidth(anchos[column]));
                                tablaProducto.AddCell(cellBody.SetBorder(Border.NO_BORDER));
                            }
                        }
                        #region footer tabla
                        cellBody = new Cell().SetBorder(Border.NO_BORDER).Add(new Paragraph("ESPACIO").SetWidth(31).SetFontSize(12).SetFontColor(ColorConstants.WHITE));
                        tablaProducto.AddFooterCell(cellBody);
                        cellBody = new Cell().SetBorder(Border.NO_BORDER).Add(new Paragraph("validá hasta " + fechaVencimiento).SetFontSize(12).SetFontColor(ColorConstants.BLACK));
                        tablaProducto.AddFooterCell(cellBody);
                        cellBody = new Cell().SetBorder(Border.NO_BORDER).Add(new Paragraph(total.ToString()).SetFontSize(12).SetFontColor(ColorConstants.BLACK));
                        tablaProducto.AddFooterCell(cellBody);
                        cellBody = new Cell().SetBorder(Border.NO_BORDER).Add(new Paragraph("ESPACIO").SetWidth(31).SetFontSize(12).SetFontColor(ColorConstants.WHITE));
                        tablaProducto.AddFooterCell(cellBody);
                        #endregion

                        doc.Add(tablaProducto);//agrego la tabla
                        #endregion

                        doc.Close();//cerramos el documento
                        writer.Close();//y la escritura
                        //si es un comprobante eliminamos el registro
                        if(cotizacion.Nocotizacion == "-1000")
                        {
                            using(TransactionScope transaction= new TransactionScope())
                            {
                                var lstDetalle = db.Detallecotizacion.Where(p => p.Iidcotizacion == cotizacion.Iidcotizacion).ToList();//obtenemos la lista de productos
                                foreach(var item in lstDetalle)
                                {
                                    db.Detallecotizacion.Remove(item);//y empezamos a eliminar
                                    db.SaveChanges();
                                }
                                db.Cotizacion.Remove(cotizacion);//para finalizar eliminamos la cotizacion
                                db.SaveChanges();
                                transaction.Complete();
                            }
                        }
                    }
                }
                return memoryString.ToArray();
            }
        }
        public static byte[] crearReporteVenta(List<ListFactura> lstFactura)
        {
            //metodo que nos servira para crear facturas
            string[] cabecera = { "CANT.", "DESCRIPCION","UM", "P.U", "desc.","p.desc","comi","p.comi","subtotal" };
            using (var memoryString = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryString);
                using (var PDF = new PdfDocument(writer))
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        //para factura PageSize.A6
                        Document doc = new Document(PDF, PageSize.LEGAL);
                        Paragraph titulo = new Paragraph("Ferreteria La Terminal - Reporte de ventas").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(20);
                        doc.Add(titulo);
                        Table tablaDetalle = new Table(9).UseAllAvailableWidth();
                        Cell celda;
                        decimal totalVenta = 0, totalEfectivo = 0, totalCambio = 0; Int64 totalProductos = 0, totalFacturas = 0, totalDescuentoGlobal = 0;
                        foreach(var lstF in lstFactura)//recorremos las facturas
                        {
                            totalEfectivo += lstF.efectivo; totalCambio += lstF.cambio;
                            totalDescuentoGlobal = (long)(totalDescuentoGlobal + lstF.descuentogeneral);
                            totalFacturas++;//contador de facturas
                            if(lstF.tipocomprador=="CLIENTE FINAL")
                            {
                                celda = new Cell(1, 9).Add(new Paragraph("Factura No. " + lstF.nofactura + " emitida el " + lstF.fechaemitida).SetFontSize(11)
                                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBackgroundColor(ColorConstants.YELLOW));
                                tablaDetalle.AddCell(celda);
                            }
                            else
                            {
                                celda = new Cell(1, 9).Add(new Paragraph("Comprobante fiscal No. " + lstF.nofactura + " emitida el " + lstF.fechaemitida).SetFontSize(11)
                                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBackgroundColor(ColorConstants.YELLOW));
                                tablaDetalle.AddCell(celda);
                            }
                            celda = new Cell(1, 4).Add(new Paragraph("Vendido por: " + lstF.nombrevendedor).SetFontSize(8));
                            tablaDetalle.AddCell(celda);
                            celda = new Cell(1, 5).Add(new Paragraph("Comprado por: " + lstF.nombrecomprador).SetFontSize(8));
                            tablaDetalle.AddCell(celda);

                            
                            for (int i = 0; i < cabecera.Length; i++)
                            {
                                celda = new Cell().Add(new Paragraph(cabecera[i].ToUpper()).SetFontSize(8));
                                tablaDetalle.AddCell(celda);
                            }
                            Int64 id = lstF.iidfactura;
                            List<DetalleVenta> lstVenta = obtenerListaFactura(id);//obtenemos la lista
                            foreach(var lstV in lstVenta)//recorremos el listado de productos
                            {
                                celda = new Cell().Add(new Paragraph(lstV.cantidad.ToString()).SetFontSize(8));
                                tablaDetalle.AddCell(celda);

                                if (lstV.subproducto == "NO")
                                {
                                    if (lstF.descuentogeneral > 0)
                                    {
                                        celda = new Cell().Add(new Paragraph(lstV.nombreproducto + " *").SetFontSize(8));
                                        tablaDetalle.AddCell(celda);

                                        celda = new Cell().Add(new Paragraph(lstV.unidadmedida).SetFontSize(8));
                                        tablaDetalle.AddCell(celda);
                                    }
                                    else
                                    {
                                        celda = new Cell().Add(new Paragraph(lstV.nombreproducto).SetFontSize(8));
                                        tablaDetalle.AddCell(celda);

                                        celda = new Cell().Add(new Paragraph(lstV.unidadmedida).SetFontSize(8));
                                        tablaDetalle.AddCell(celda);
                                    }
                                }
                                else
                                {
                                    celda = new Cell().Add(new Paragraph(lstV.nombreproducto).SetFontSize(8));
                                    tablaDetalle.AddCell(celda);

                                    celda = new Cell().Add(new Paragraph(lstV.Nombresubunidad).SetFontSize(8));
                                    tablaDetalle.AddCell(celda);
                                    
                                }
                                celda = new Cell().Add(new Paragraph(lstV.preciounitario.ToString()).SetFontSize(8));
                                tablaDetalle.AddCell(celda);

                                celda = new Cell().Add(new Paragraph(lstV.descuento.ToString()).SetFontSize(8));
                                tablaDetalle.AddCell(celda);

                                celda = new Cell().Add(new Paragraph(lstV.pdescuento.ToString()+"%").SetFontSize(8));
                                tablaDetalle.AddCell(celda);

                                celda = new Cell().Add(new Paragraph(lstV.comision.ToString()).SetFontSize(8));
                                tablaDetalle.AddCell(celda);

                                celda = new Cell().Add(new Paragraph(lstV.pcomision.ToString() + "%").SetFontSize(8));
                                tablaDetalle.AddCell(celda);

                                celda = new Cell().Add(new Paragraph(lstV.total.ToString()).SetFontSize(8));
                                tablaDetalle.AddCell(celda);
                                totalProductos = totalProductos + lstV.cantidad;
                            }

                            celda = new Cell(1, 3).Add(new Paragraph("Efectivo: " + lstF.efectivo).SetFontSize(8)
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                            tablaDetalle.AddCell(celda);
                            celda = new Cell(1, 3).Add(new Paragraph("Cambio: " + lstF.cambio).SetFontSize(8)
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                            tablaDetalle.AddCell(celda);
                            celda = new Cell(1, 3).Add(new Paragraph("Total: " + lstF.total).SetFontSize(8)
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                            tablaDetalle.AddCell(celda);

                            totalVenta = totalVenta + lstF.total;//vamos sumando el total
                            celda = new Cell(1,9).Add(new Paragraph("SE APLICO UN DESCUENTO DEL "+lstF.porcentajedescuento+ "% DESCUENTO GLOBAL DE $"+
                                lstF.descuentogeneral).SetFontSize(8)
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                            tablaDetalle.AddCell(celda);
                        }
                        doc.Add(tablaDetalle);
                        doc.Add(new Paragraph(""));
                        Table datosRelevantes = new Table(6);
                        datosRelevantes.AddCell(new Cell(1, 6).Add(new Paragraph("DATOS GENERALES")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBackgroundColor(ColorConstants.CYAN)));
                        datosRelevantes.AddCell(new Cell().Add(new Paragraph("Facturas emitidas: " + totalFacturas.ToString())));
                        datosRelevantes.AddCell(new Cell().Add(new Paragraph("Productos vendidos: " + totalProductos.ToString())));
                        datosRelevantes.AddCell(new Cell().Add(new Paragraph("Venta total: $" + totalVenta.ToString())));
                        datosRelevantes.AddCell(new Cell().Add(new Paragraph("Descuento a facturas: $" + totalDescuentoGlobal.ToString())));
                        datosRelevantes.AddCell(new Cell().Add(new Paragraph("Efectivo entrante: $" + totalEfectivo.ToString())));
                        datosRelevantes.AddCell(new Cell().Add(new Paragraph("Total cambio: $" + totalCambio.ToString())));
                        doc.Add(datosRelevantes);

                        doc.Close();//cerramos el documento
                        writer.Close();//y la escritura
                    }
                }
                return memoryString.ToArray();
            }
        }
        public static List<DetalleVenta>obtenerListaFactura(Int64 id)
        {
            using(var db = new BDFERRETERIAContext())
            {
                var lista = (from detalle in db.Detallepedido
                             join producto in db.Producto on
                             detalle.Iidproducto equals producto.Iidproducto
                             join unidad in db.Unidadmedida on
                             producto.Iidunidadmedida equals unidad.Iidunidadmedida
                             where detalle.Iidfactura == id
                             select new DetalleVenta
                             {
                                 cantidad = detalle.Cantidad,
                                 nombreproducto = producto.Descripcion.ToUpper(),
                                 preciounitario = (decimal)detalle.Precioactual,
                                 total = (decimal)detalle.Subtotal,
                                 descuento = (decimal)detalle.Descuento,
                                 pdescuento = detalle.Porcentajedescuento,
                                 comision = (decimal)detalle.Comision,
                                 unidadmedida = unidad.Nombreunidad,
                                 precioActual = (decimal)detalle.Precioactual,
                                 Nombresubunidad = ObtenerNombreSubUnidad(producto.Subunidad),
                                 subproducto = detalle.Essubproducto,
                                 iva = (decimal)producto.Iva,
                                 subiva = (decimal)producto.Subiva
                             }).ToList();//listamos el detalle del pedido
                return lista;
            }
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
            //metodo que nos servira para crear facturas
            using (var memoryString = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryString);
                using (var facturaPDF = new PdfDocument(writer))
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        //para factura PageSize.A6
                        Document doc = new Document(facturaPDF, PageSize.A5);
                        //doc.SetMargins(5,10,5,10);//arriba, derecha, abajo, izquierda
                        #region obtenemos la data de la factura

                        Factura factura = (from f in db.Factura
                                           where f.Iidfactura == id
                                           select new Factura
                                           {
                                               Iidusuario = f.Iidusuario,
                                               Iidfactura = f.Iidfactura,
                                               Nofactura = f.Nofactura,
                                               Tipocomprador = f.Tipocomprador.ToUpper(),
                                               Nombrecliente = f.Nombrecliente.ToUpper(),
                                               Direccion = f.Direccion.ToUpper() == null ? "" : f.Direccion,
                                               Registro = f.Registro.ToUpper() == null ? "" : f.Registro,
                                               Giro = f.Giro.ToUpper() == null ? "" : f.Giro,
                                               Nit = f.Nit == null ? "" : f.Nit,
                                               Fechacreacion = f.Fechacreacion,
                                               Total = f.Total,
                                               Descuentoglobal = f.Descuentoglobal,
                                               Porcentajedescuentoglobal = f.Porcentajedescuentoglobal,
                                           }).First();//obtenemos el objeto de la factura
                        List<DetalleVenta> detallePedido = (from detalle in db.Detallepedido
                                                            join producto in db.Producto on
                                                            detalle.Iidproducto equals producto.Iidproducto
                                                            join unidad in db.Unidadmedida on
                                                            producto.Iidunidadmedida equals unidad.Iidunidadmedida
                                                            where detalle.Iidfactura == id
                                                            select new DetalleVenta
                                                            {
                                                                cantidad = detalle.Cantidad,
                                                                nombreproducto = producto.Descripcion.ToUpper(),
                                                                preciounitario = (decimal)detalle.Precioactual,
                                                                total = (decimal)detalle.Subtotal,
                                                                descuento = (decimal)detalle.Descuento,
                                                                pdescuento = detalle.Porcentajedescuento,
                                                                comision = (decimal)detalle.Comision,
                                                                unidadmedida = unidad.Nombreunidad,
                                                                precioActual = (decimal)detalle.Precioactual,
                                                                Nombresubunidad = ObtenerNombreSubUnidad(producto.Subunidad),
                                                                subproducto = detalle.Essubproducto,
                                                                iva = (decimal)producto.Iva,
                                                                codigoproducto = producto.Codigoproducto,
                                                                subiva = producto.Subiva
                                                            }).ToList();//listamos el detalle del pedido
                        #endregion

                        #region titulo
                        Table informacionPrincipal = new Table(2).UseAllAvailableWidth();
                        Cell celda;
                        celda = new Cell().Add(new Paragraph("FERRETERIA\nLA TERMINAL")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        informacionPrincipal.AddCell(celda.SetWidth(170));
                        celda = new Cell().Add(new Paragraph("FACTURA PROVISIONAL\nNo " + factura.Nofactura)
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        informacionPrincipal.AddCell(celda);
                        doc.Add(informacionPrincipal);
                        doc.Add(new Paragraph(""));
                        #endregion

                        #region datos cliente y vendedor
                        Table datosCLienteVendedor = new Table(2).UseAllAvailableWidth();
                        var data = db.Usuario.Where(p => p.Iidusuario == factura.Iidusuario).Include(x => x.IidempleadoNavigation).First();
                        datosCLienteVendedor.AddCell(celda = new Cell().Add(new Paragraph("Vendedor: "+data.IidempleadoNavigation.Nombrecompleto).SetFontSize(7)));
                        datosCLienteVendedor.AddCell(celda = new Cell().Add(new Paragraph("Comprador: " + factura.Nombrecliente).SetFontSize(7)));
                        doc.Add(datosCLienteVendedor);
                        doc.Add(new Paragraph(""));
                        #endregion

                        #region cuerpo de la factura
                        string afectado = "";
                        if (factura.Descuentoglobal > 0) { afectado = "*"; }
                        Table tablaProducto = new Table(6).UseAllAvailableWidth();
                        decimal total = 0, totaldescuento = 0;
                        //creamos el cuerpo de la tabla
                        Cell cellBody;


                        string[] aray = { "CANT", "DESCRIPCION", "PRECIO UNITARIO", "v. NO SUJETAS", "Ventas EXENTAS", "Ventas afectadas" };
                        for (int i = 0; i < aray.Length; i++)
                        {
                            tablaProducto.AddHeaderCell(cellBody = new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                                .Add(new Paragraph(aray[i].ToUpper()).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(8)));
                        }
                        int nvecesiterado = 0;
                        foreach (var item in detallePedido)
                        {
                            total += item.total; totaldescuento += item.descuento;

                            //cant
                            cellBody = new Cell().Add(new Paragraph(item.cantidad.ToString()).SetFontSize(7));
                            tablaProducto.AddCell(cellBody);
                            string descuento = "";
                            if (item.pdescuento > 0)
                            {
                                descuento = "[Desc: " + item.pdescuento.ToString() + "%]";
                            }
                            if (item.subproducto == "NO") //validamos que unidad es
                            {
                                //descripcion
                                cellBody = new Cell().Add(new Paragraph(item.nombreproducto + " "
                                    + item.unidadmedida + " " + descuento + " " + afectado).SetFontSize(7));
                                tablaProducto.AddCell(cellBody.SetWidth(140));
                            }
                            else
                            {
                                //descripcion
                                cellBody = new Cell().Add(new Paragraph(item.nombreproducto +
                                    " " + item.Nombresubunidad + " " + descuento).SetFontSize(7));
                                tablaProducto.AddCell(cellBody.SetWidth(140));
                            }
                            //precio unitario
                            decimal precio = (item.comision / item.cantidad) + item.precioActual;//obtenemos el precio unitario sumando el precio + la comision
                            precio = Math.Round(precio, 2);
                            cellBody = new Cell().Add(new Paragraph(precio.ToString()).SetFontSize(7));
                            tablaProducto.AddCell(cellBody);
                            //ventas no sujetas 
                            cellBody = new Cell().Add(new Paragraph("ESPACIO").SetFontSize(7).SetFontColor(ColorConstants.WHITE));
                            tablaProducto.AddCell(cellBody);
                            //ventas exentas 
                            cellBody = new Cell().Add(new Paragraph("ESPACIO").SetFontSize(7).SetFontColor(ColorConstants.WHITE));
                            tablaProducto.AddCell(cellBody);
                            //ventas afectadas
                            item.total = Math.Round(item.total, 2);//se redondea
                            cellBody = new Cell().Add(new Paragraph(item.total.ToString()).SetFontSize(7));
                            tablaProducto.AddCell(cellBody);
                            nvecesiterado++;
                        }
                        for (int i = nvecesiterado; i < 16; i++)
                        {
                            for (var j = 0; j < 6; j++)
                            {
                                cellBody = new Cell().Add(new Paragraph("ESPAC").SetFontSize(7).SetFontColor(ColorConstants.WHITE));
                                tablaProducto.AddCell(cellBody);
                            }
                        }

                        var user = db.Usuario.Where(p => p.Iidtipousuario == factura.Iidusuario)
                                .Include(x => x.IidempleadoNavigation).First();
                        if (factura.Descuentoglobal > 0)
                        {
                            factura.Descuentoglobal = Math.Round(factura.Descuentoglobal, 2);
                            cellBody = new Cell(1, 6).Add(new Paragraph("Desc. de $" + factura.Descuentoglobal + " (" + factura.Porcentajedescuentoglobal + "%) productos con * afectados")
                                .SetFontSize(7).SetFontColor(ColorConstants.BLACK));
                            tablaProducto.AddCell(cellBody);
                        }
                        else
                        {
                            factura.Descuentoglobal = Math.Round(factura.Descuentoglobal, 2);
                            cellBody = new Cell(1, 6).Add(new Paragraph("Desc. de $" + factura.Descuentoglobal + " (" + factura.Porcentajedescuentoglobal + "%) productos con * afectados")
                                .SetFontSize(7).SetFontColor(ColorConstants.WHITE));
                            tablaProducto.AddCell(cellBody);
                        }
                        #endregion

                        #region footer tabla

                        cellBody = new Cell(1,4).Add(new Paragraph("Autorizado por:").SetFontSize(8).SetFontColor(ColorConstants.BLACK));
                        tablaProducto.AddCell(cellBody);
                        cellBody = new Cell().Add(new Paragraph("SUMAS: ").SetFontSize(8).SetFontColor(ColorConstants.BLACK));
                        tablaProducto.AddCell(cellBody);
                        cellBody = new Cell().Add(new Paragraph("$"+Math.Round(total, 2).ToString()).SetFontSize(8).SetFontColor(ColorConstants.BLACK));
                        tablaProducto.AddCell(cellBody);


                        cellBody = new Cell(1, 4).Add(new Paragraph("Firma:").SetFontSize(8).SetFontColor(ColorConstants.BLACK));
                        tablaProducto.AddCell(cellBody);
                        cellBody = new Cell().Add(new Paragraph("TOTAL: ").SetFontSize(8).SetFontColor(ColorConstants.BLACK));
                        tablaProducto.AddCell(cellBody);
                        factura.Total = Math.Round((decimal)factura.Total, 2);
                        cellBody = new Cell().Add(new Paragraph("$"+factura.Total.ToString()).SetFontSize(8).SetFontColor(ColorConstants.BLACK));
                        tablaProducto.AddCell(cellBody);
                        doc.Add(tablaProducto);//agrego la tabla
                        #endregion
                        doc.Close();//cerramos el documento
                        writer.Close();//y la escritura
                    }
                }
                return memoryString.ToArray();
            }
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

        public static byte[] crearReporteInventario<T>(List<T> lst, string[] cabecera, string[] propiedades)
        {
            using(var ms = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using(ExcelPackage ep = new ExcelPackage())
                {
                    ep.Workbook.Worksheets.Add("Hoja 1");//agrego una hoja
                    #region primera linea
                    var colorFromHex = System.Drawing.ColorTranslator.FromHtml("#89b8ee");//creo un color para el fondo
                    var contenido = ep.Workbook.Worksheets[0];
                    contenido.Cells[2, 1].Value = "PRODUCTO ORIGINAL";
                    using (var rango = contenido.Cells[2, 1, 2, 8])
                    {
                        rango.Merge = true;//combina celdas
                        rango.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//centra el texto
                        //agrega color 
                        rango.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rango.Style.Fill.BackgroundColor.SetColor(colorFromHex);
                    }
                    contenido.Cells[2, 9].Value = "SUB PRODUCTO";
                    using (var rango = contenido.Cells[2, 9, 2, 11])
                    {
                        rango.Merge = true;//combina celdas
                        rango.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//centra el texto
                        //agrega color 
                        rango.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rango.Style.Fill.BackgroundColor.SetColor(colorFromHex);
                    }
                    #endregion
                    #region cabecera
                    for (int i = 0; i < cabecera.Length; i++)
                    {
                        contenido.Cells[3, i + 1].Value = cabecera[i];
                        contenido.Cells[3, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        contenido.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(colorFromHex);
                        
                    }
                    using (var rango = contenido.Cells[2, 1, 3, 11])
                    {
                        rango.Style.Font.Bold = true;//ponerlo en negritas
                        rango.Style.Font.Size = 12;//ajusta tamaño de fuente
                    }
                    #endregion
                    #region cuerpo de la tabla
                    int fila = 4, columna;
                    if (lst.Count > 0)
                    {
                        foreach (var dataActual in lst)
                        {
                            columna = 1;
                            foreach (var propiedad in propiedades)
                            {
                                var texto = dataActual.GetType().GetProperty(propiedad).GetValue(dataActual);//obtenemos el valor de la propiedad
                                if (texto != null) { contenido.Cells[fila, columna].Value = texto; }//validamos si no es null
                                else { contenido.Cells[fila, columna].Value = "----"; }//si es null

                                contenido.Cells[fila, columna].Style.Font.Size = 11;//ajusta tamaño de fuente
                                columna++;
                            }
                            fila++;
                        }
                    }
                    using (var rango = contenido.Cells[2, 1, fila-1, 11])
                    {
                        //agregaremos bordes
                        rango.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rango.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rango.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rango.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rango.AutoFitColumns();//ajusta el ancho automaticamente
                    }
                    #endregion
                    ep.SaveAs(ms);
                }
                return ms.ToArray();
            }
        }

        public static List<ListReporteInventario> crearListaReporteSala(Inventario inventario, string nombrestock)
        {
            List<ListReporteInventario> lst = new List<ListReporteInventario>();
            using(var db = new BDFERRETERIAContext())
            {
                if (nombrestock != null) 
                {
                    lst = db.Producto.Where(p => p.Bhabilitado == "A" && p.IidstockNavigation.Nombrestock.Contains(nombrestock))
                        .Include(x => x.IidstockNavigation).Include(x => x.IidstockNavigation)
                    .Select(p => new ListReporteInventario
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
                }
                else
                {
                    lst = db.Producto.Where(p => p.Bhabilitado == "A").Include(x => x.IidstockNavigation).Include(x => x.IidstockNavigation)
                    .Select(p => new ListReporteInventario
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
                }
            }
            return lst;
        }

        public static List<ListReporteInventario> crearlistaReporteBodega(Inventario inventario, string nombrestock)
        {
            List<ListReporteInventario> lst = new List<ListReporteInventario>();
            using(var db = new BDFERRETERIAContext())
            {
                if (inventario.Iidbodega > 0 && nombrestock != null)  //si ambos traen parametro entonces es una lista especifica
                {
                    lst = db.Inventario.Where(p => p.Bhabilitado == "A" && p.IidstockNavigation.Nombrestock.Contains(nombrestock)
                    && p.Iidbodega == inventario.Iidbodega).Include(x => x.IidstockNavigation).Include(x => x.IidstockNavigation)
                    .Include(x => x.IidproductoNavigation).Select(p => new ListReporteInventario
                    {
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
                else if (inventario.Iidbodega > 0 && nombrestock == null)  //si solo es bodega entonces se listan todos los stock
                {
                    lst = db.Inventario.Where(p => p.Bhabilitado == "A" && p.Iidbodega == inventario.Iidbodega)
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
                        Subcantidad = (decimal)(p.Cantidad * p.IidproductoNavigation.Equivalencia),
                        Subprecio = p.IidproductoNavigation.Subprecioventa.ToString(),
                        Codigoproducto = p.IidproductoNavigation.Codigoproducto
                    }).ToList();
                }
            }
            return lst;
        }

        #region prueba agregar n cantidad de productos
        public static int create20Productos()
        {
            using (var db = new BDFERRETERIAContext())
            {
                try
                {
                    int idforanea = 1;
                    for (int i = 1; i < 31; i++)
                    {
                        Producto producto = new Producto();
                        producto.Iidunidadmedida = idforanea;
                        producto.Codigoproducto = "COD0" + i.ToString();
                        producto.Descripcion = "PRODUCTO No. " + i.ToString();
                        producto.Existencias = 0;
                        producto.Preciocompra = (decimal)7.9000 + (decimal)0.3489;
                        producto.Iva = (decimal)1.1913 + (decimal)0.3489;
                        producto.Porcentajeganancia = 16;
                        producto.Ganancia = (decimal)1.2640 + (decimal)0.3489;
                        producto.Precioventa = (decimal)10.3553 + (decimal)0.3489;
                        producto.Fechacreacion = DateTime.Now;
                        producto.Bhabilitado = "A";
                        producto.Iidstock = idforanea;
                        if (i % 3 == 0)
                        {
                            producto.Subunidad = idforanea;
                            producto.Equivalencia = 10;
                            producto.Subpreciounitario = (decimal)0.7900 + (decimal)0.3489;
                            producto.Subporcentaje = 16;
                            producto.Subganancia = (decimal?)0.1264 + (decimal)0.3489;
                            producto.Subiva = (decimal?)0.1191 + (decimal)0.3489;
                            producto.Subprecioventa = (decimal?)1.0355 + (decimal)0.3489;
                            producto.Subexistencia = 0;
                            producto.Restantes = producto.Equivalencia;
                        }
                        db.Producto.Add(producto);
                        if (i % 50 == 0) { db.SaveChanges(); }
                        idforanea++;
                        if (idforanea > 5) { idforanea = 1; }
                    }
                    db.SaveChanges();
                    return 1;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }
        #endregion
    }
}
