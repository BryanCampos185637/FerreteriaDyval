﻿using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DAL
{
    public class ProductoDAL
    {
        public List<ListProducto> buscarProductos(string Codigo, string Nombre)
        {
            using (var db = new BDFERRETERIAContext())
            {
                if (Codigo != null)
                {
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
                }
                else if (Nombre != null)
                {
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
                }
                else
                {
                    #region entity
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
                                 }).Take(500).ToList();
                    #endregion
                    return lista;
                }
            }
        }
        public int guardarProducto(Producto producto)
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
        public Producto obtenerProducto(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = db.Producto.Where(p => p.Iidproducto == id).FirstOrDefault();
                return lst;
            }
        }
        public int eliminarProducto(Int64 id)
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
        public string obtenerNombreUnidad(int id)
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
    }
}