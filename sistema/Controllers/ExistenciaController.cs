using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AdminFerreteria.Controllers
{
    public class ExistenciaController : Controller
    {
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("Existencia", "index", (int)idUsuario))
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
        public JsonResult listProducto()
        {
            using (var db = new BDFERRETERIAContext())
            {
                #region obtener la lista de productos activos
                List<ListProducto> lst = (from product in db.Producto
                                          join stock in db.Stock on
                                          product.Iidstock equals stock.Iidstock
                                          join unidad in db.Unidadmedida on
                                          product.Iidunidadmedida equals unidad.Iidunidadmedida
                                          where product.Bhabilitado == "A"
                                          select new ListProducto
                                          {
                                              Iidproducto = product.Iidproducto,
                                              Codigoproducto = product.Codigoproducto,
                                              Descripcion = product.Descripcion,
                                              Preciocompra = product.Preciocompra,
                                              Iva = product.Iva,
                                              Ganancia = product.Ganancia,
                                              Existencias = product.Existencias,
                                              Precioventa = product.Precioventa,
                                              Subprecioventa = product.Subprecioventa == null ? -1000 : product.Subprecioventa,
                                              Subexistencia = product.Subexistencia == null ? -1000 : product.Subexistencia,
                                              Nombreunidad = unidad.Nombreunidad,
                                              Nombresubunidad = UtilidadesController.ObtenerNombreSubUnidad(product.Subunidad),
                                              Nombrestock = stock.Nombrestock,
                                              Restantes = product.Restantes == null ? -1000 : product.Restantes,
                                              Equivalencia = product.Equivalencia
                                          }).ToList();
                #endregion

                #region usando la lista anterior contaremos la cantidad de productos existentes en bodega y venta
                foreach (var item in lst)//recorremos la lista
                {
                    Int64 contador = 0;
                    var lstInventario = db.Inventario.Where(p => p.Iidproducto == item.Iidproducto && p.Bhabilitado == "A" && p.Cantidad > 0)
                        .Include(p => p.IidproductoNavigation).ToList();//llamamos las bodegas que contengan el producto
                    foreach (var j in lstInventario)//iteramos para saber la cantidad
                    {
                        contador += j.Cantidad;
                    }
                    item.Existencias += contador;//incrementamos la cantidad que ya existia en la lista
                    if (item.Subexistencia != -1000 && item.Subexistencia != null)
                    {
                        item.Subexistencia += item.Equivalencia * contador;//si hay un sub producto tambien la incrementamos
                    }
                }
                #endregion

                #region una vez sepamos cuanta cantidad hay creamos una nueva lista para mostrar solo los que tienen 10 o menos
                List<ListProducto> listaDeProductosConBajaExistencia = new List<ListProducto>();
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].Existencias <= 10)//si la existencia es menor o igual a 10 lo agregamos a la lista
                    {
                        listaDeProductosConBajaExistencia.Add(lst[i]);
                    }
                }
                #endregion

                #region creamos una cookie para almacenar la lista actual para el reporte
                string lista = JsonConvert.SerializeObject(listaDeProductosConBajaExistencia);
                HttpContext.Session.SetString("existenciasBajas", lista);
                #endregion

                return Json(listaDeProductosConBajaExistencia);
            }
        }
        public FileResult existenciasPDF()
        {
            var listaSerializada = HttpContext.Session.GetString("existenciasBajas");
            byte[] buffer = generarPDF(listaSerializada);
            return File(buffer, "application/pdf");
        }
        public byte[] generarPDF(string listaSerializada)
        {
            List<ListProducto> listaProducto = JsonConvert.DeserializeObject<List<ListProducto>>(listaSerializada);//obtenemos la lista
            string[] cabera = { "Codigo", "Descripción", "UM", "Stock", "Existencia", "Precio compra" };
            using (var memoryString = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryString);
                using (var PDF = new PdfDocument(writer))
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        Document doc = new Document(PDF, PageSize.LETTER);
                        Paragraph titulo = new Paragraph("Productos que necesitas comprar.");
                        titulo.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        titulo.SetFontSize(13);
                        doc.Add(titulo);
                        if (listaProducto != null)
                        {
                            Table tablaProductos = new Table(6).UseAllAvailableWidth();
                            Cell celda;
                            //creamos la cabecera de la tabla
                            for (int i = 0; i < cabera.Length; i++)
                            {
                                celda = new Cell().Add(new Paragraph(cabera[i]).SetFontSize(11)).SetBackgroundColor(ColorConstants.CYAN);
                                tablaProductos.AddHeaderCell(celda);
                            }
                            decimal total = 0;
                            //creamos el cuerpo de la tabla
                            foreach (var item in listaProducto)
                            {
                                celda = new Cell().Add(new Paragraph(item.Codigoproducto).SetFontSize(10));
                                tablaProductos.AddCell(celda);
                                celda = new Cell().Add(new Paragraph(item.Descripcion).SetFontSize(10));
                                tablaProductos.AddCell(celda);
                                celda = new Cell().Add(new Paragraph(item.Nombreunidad).SetFontSize(10));
                                tablaProductos.AddCell(celda);
                                celda = new Cell().Add(new Paragraph(item.Nombrestock).SetFontSize(10));
                                tablaProductos.AddCell(celda);
                                celda = new Cell().Add(new Paragraph(item.Existencias.ToString()).SetFontSize(10));
                                tablaProductos.AddCell(celda);
                                celda = new Cell().Add(new Paragraph("$" + item.Preciocompra.ToString()).SetFontSize(10));
                                tablaProductos.AddCell(celda);
                                total += (decimal)item.Preciocompra * 5;
                            }
                            int totalProductos = listaProducto.Count();
                            Table finalTabla = new Table(2).UseAllAvailableWidth();
                            celda = new Cell().Add(new Paragraph("Cantidad productos listados: " + totalProductos.ToString())
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(10))
                                .SetBackgroundColor(ColorConstants.CYAN);
                            finalTabla.AddFooterCell(celda);
                            celda = new Cell().Add(new Paragraph("Costo aproximado[5 unidades por producto]: $" + total.ToString())
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(10))
                                .SetBackgroundColor(ColorConstants.CYAN);
                            finalTabla.AddFooterCell(celda);
                            doc.Add(tablaProductos);
                            doc.Add(finalTabla);
                        }
                        doc.Close();//cerramos el documento
                    }
                    writer.Close();//y la escritura
                }
                return memoryString.ToArray();
            }
        }
    }
}
