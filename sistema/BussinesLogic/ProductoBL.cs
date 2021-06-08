using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using System;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class ProductoBL
    {
        ProductoDAL dal = new ProductoDAL();
        public List<ListProducto> buscarProductos(string Codigo, string Nombre)
        {
            if (Codigo != null)
                return dal.FiltrarPorCodigo(Codigo);
            else if (Nombre != null)
                return dal.FiltrarPorNombre(Nombre);
            else
                return dal.ListarProductosActivos();
        }
        public int guardarProducto(Producto producto)
        {
            return dal.GuardarProducto(producto);
        }
        public Producto obtenerProducto(Int64 id)
        {
            return dal.ObtenerProducto(id);
        }
        public int eliminarProducto(Int64 id)
        {
            return dal.EliminarProducto(id);
        }
        public string obtenerNombreUnidad(int id)
        {
            return dal.ObtenerNombreUnidad(id);
        }
        public int cantidadDeProductos()
        {
            return dal.CantidadDeProductosExistente();
        }
    }
}
