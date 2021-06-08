using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using System;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class BodegaInventarioBL
    {
        private BodegainventarioDAL dal = new BodegainventarioDAL();
        public string guardarBodega(Bodega bodega)
        {
            return dal.GuardarBodega(bodega);
        }
        public List<Bodega> listarBodega()
        {
            return dal.ListarBodega();
        }
        public List<ListInventario> listarInventario()
        {
            return dal.ListarInventario();
        }
        public Bodega obtenerBodega(int id)
        {
            return dal.ObtenerBodegaSegunId(id);
        }
        public List<ListProducto> listarProductos()
        {
            return dal.ListarProductos();
        }
        public string modificarExistenciasSalaDeVenta(Producto producto)
        {
            return dal.ModificarExistenciasSalaDeVenta(producto);
        }
        public string editarExistenciasInventario(Int64 id, Int64 cantidad)
        {
            return dal.EditarExistenciasInventario(id, cantidad);
        }
        public string eliminarInventario(Int64 id)
        {
            return dal.EliminarInventario(id);
        }
        public List<Bodega> listarBodegaDiferenteDelParametroId(Int64 id)
        {
            return dal.ListarBodegaDiferenteDelParametroId(id);
        }
        public string moverproducto(Int64 cantidad, Int64 bodegaActual, Int64 producto, Int64 ubicacionnueva, int stock)
        {
            return dal.MoverProducto(cantidad, bodegaActual, producto, ubicacionnueva, stock);
        }
        public ListInventario obtenerInventario(int id)
        {
            return dal.ObtenerInventarioSegunId(id);
        }
        public string eliminarBodega(int id) 
        { 
            return dal.EliminarBodega(id); 
        }
    }
}
