using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class ProductoDAL
    {
        public static int ExistProducto(Producto producto)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Producto.Where(p => p.Bhabilitado == "A" && p.Iidproducto != producto.Iidproducto
                    && p.Codigoproducto == producto.Codigoproducto).Count();
            }
        }
        public List<ListProducto> FiltrarPorCodigo(string Codigo)
        {
            using (var db = new BDFERRETERIAContext())
            {
                #region filtrar por codigo
                var lst = (from product in db.Producto
                           join stock in db.Stock on
                           product.Iidstock equals stock.Iidstock
                           join unidad in db.Unidadmedida on
                           product.Iidunidadmedida equals unidad.Iidunidadmedida
                           where product.Bhabilitado == "A" && product.Codigoproducto.Equals(Codigo.ToUpper())
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
                               Subiva = product.Subiva == null ? -1000 : product.Subiva,
                               Nombresubunidad = UtilidadesController.ObtenerNombreSubUnidad(product.Subunidad),
                               Nombrestock = stock.Nombrestock,
                               Restantes = product.Restantes == null ? -1000 : product.Restantes,
                               Equivalencia = product.Equivalencia
                           }).ToList();
                return lst;
                #endregion
            }
        }
        public List<ListProducto> FiltrarPorNombre(string Nombre)
        {
            using (var db = new BDFERRETERIAContext())
            {
                #region filtrar por nombre
                var lst = (from product in db.Producto
                           join stock in db.Stock on
                           product.Iidstock equals stock.Iidstock
                           join unidad in db.Unidadmedida on
                           product.Iidunidadmedida equals unidad.Iidunidadmedida
                           where product.Bhabilitado == "A" && product.Descripcion.Contains(Nombre.ToUpper())
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
                               Subiva = product.Subiva == null ? -1000 : product.Subiva,
                               Nombresubunidad = UtilidadesController.ObtenerNombreSubUnidad(product.Subunidad),
                               Nombrestock = stock.Nombrestock,
                               Restantes = product.Restantes == null ? -1000 : product.Restantes,
                               Equivalencia = product.Equivalencia
                           }).Take(500).ToList();
                return lst;
                #endregion
            }
        }
        public List<ListProducto> ListarProductosActivos()
        {
            using (var db = new BDFERRETERIAContext())
            {
                #region sin filtro
                var lista = (from product in db.Producto
                             join stock in db.Stock on
                             product.Iidstock equals stock.Iidstock
                             join unidad in db.Unidadmedida on
                             product.Iidunidadmedida equals unidad.Iidunidadmedida
                             orderby product.Iidproducto descending
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
                                 Subiva = product.Subiva == null ? -1000 : product.Subiva,
                                 Nombresubunidad = UtilidadesController.ObtenerNombreSubUnidad(product.Subunidad),
                                 Nombrestock = stock.Nombrestock,
                                 Restantes = product.Restantes == null ? -1000 : product.Restantes,
                                 Equivalencia = product.Equivalencia
                             }).Take(225).ToList();
                return lista;
                #endregion
            }
        }
        public int GuardarProducto(Producto producto)
        {
            try
            {
                if (UtilidadesController.existProducto(producto) == 0)
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        if (producto.Iidproducto == 0)
                        {
                            producto.Restantes = producto.Equivalencia;
                            if (producto.Subunidad != null) { producto.Subexistencia = 0; }
                            producto.Fechacreacion = DateTime.Now;
                            producto.Existencias = 0;
                            db.Producto.Add(producto);
                        }
                        else
                        {
                            Producto oProducto = db.Producto.Where(p => p.Iidproducto == producto.Iidproducto).First();
                            oProducto.Codigoproducto = producto.Codigoproducto;
                            oProducto.Descripcion = producto.Descripcion;
                            oProducto.Preciocompra = producto.Preciocompra;
                            oProducto.Iva = producto.Iva;
                            oProducto.Ganancia = producto.Ganancia;
                            oProducto.Precioventa = producto.Precioventa;
                            oProducto.Porcentajeganancia = producto.Porcentajeganancia;
                            oProducto.Iidunidadmedida = producto.Iidunidadmedida;
                            oProducto.Iidstock = producto.Iidstock;
                            /*los datos secundarios del producto*/
                            if (producto.Subunidad != null && producto.Subunidad > 0)
                            {
                                oProducto.Subunidad = producto.Subunidad;
                                oProducto.Equivalencia = producto.Equivalencia;
                                oProducto.Subpreciounitario = producto.Subpreciounitario;
                                oProducto.Subiva = producto.Subiva;
                                oProducto.Subporcentaje = producto.Subporcentaje;
                                oProducto.Subganancia = producto.Subganancia;
                                oProducto.Subprecioventa = producto.Subprecioventa;
                                if (oProducto.Restantes == null)
                                    oProducto.Restantes = producto.Equivalencia;
                            }
                            else
                            {
                                oProducto.Subunidad = null;
                            }
                        }
                        db.SaveChanges();
                        return 1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public Producto ObtenerProducto(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = db.Producto.Where(p => p.Iidproducto == id).FirstOrDefault();
                return lst;
            }
        }
        public int EliminarProducto(Int64 id)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    Producto producto = db.Producto.Where(p => p.Iidproducto == id).First();
                    producto.Bhabilitado = "D";
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public string ObtenerNombreUnidad(int id)
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
        public void Insertar100productos()
        {
            using (var db = new BDFERRETERIAContext())
            {
                for (var i = 1; i <= 100; i++)
                {
                    db.Producto.Add(new Producto
                    {
                        Iidstock = 1,
                        Iidunidadmedida = 1,
                        Descripcion = "CA-38 PALA DÚPLEX MANGO FIBRA DE VIDRIO, HOJA REMACHADA, MANGOS FIBRA DE VIDRIO 44" + i.ToString(),
                        Codigoproducto = "cod_" + (i * 89).ToString(),
                        Existencias = 0,
                        Preciocompra = 10,
                        Ganancia = 1,
                        Iva = 10,
                        Porcentajeganancia = 2,
                        Precioventa = 20,
                        Bhabilitado = "A"
                    });
                    if (i % 25 == 0)
                        db.SaveChanges();
                }
                db.SaveChanges();
            }
        }
        public int CantidadDeProductosExistente()
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Producto.Where(p => p.Bhabilitado == "A").Count();
            }
        }
        public int CambiarExistenciaGeneral(long cantidad)
        {
            using (var db = new BDFERRETERIAContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var productos = db.Producto.Where(p => p.Bhabilitado == "A").ToList();
                        foreach (var producto in productos)
                        {
                            producto.Existencias = cantidad;
                            if (producto.Subunidad != null)
                            {
                                producto.Subexistencia = (cantidad * producto.Equivalencia);
                                producto.Restantes = producto.Equivalencia;
                            }
                            db.Entry(producto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        db.SaveChanges();
                        transaction.Commit();
                        return 1;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
    }
}
