using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class ExistenciaDAL
    {
        public List<ListProducto> ListarProductosActivos()
        {
            using (var db = new BDFERRETERIAContext())
            {
                #region obtener la lista de productos activos
                List<ListProducto> lst = (from prod in db.Producto
                                          join stock in db.Stock on
                                          prod.Iidstock equals stock.Iidstock
                                          join unidad in db.Unidadmedida on
                                          prod.Iidunidadmedida equals unidad.Iidunidadmedida
                                          where prod.Bhabilitado == "A"
                                          select new ListProducto
                                          {
                                              Iidproducto = prod.Iidproducto,
                                              Codigoproducto = prod.Codigoproducto,
                                              Descripcion = prod.Descripcion,
                                              Preciocompra = prod.Preciocompra,
                                              Iva = prod.Iva,
                                              Ganancia = prod.Ganancia,
                                              Existencias = prod.Existencias,
                                              Precioventa = prod.Precioventa,
                                              Subprecioventa = prod.Subprecioventa == null ? -1000 : prod.Subprecioventa,
                                              Subexistencia = prod.Subexistencia == null ? -1000 : prod.Subexistencia,
                                              Nombreunidad = unidad.Nombreunidad,
                                              Nombresubunidad = UnidadMedidaDAL.ObtenerNombreDeSubUnidad(prod.Subunidad),
                                              Nombrestock = stock.Nombrestock,
                                              Restantes = prod.Restantes == null ? -1000 : prod.Restantes,
                                              Equivalencia = prod.Equivalencia
                                          }).ToList();
                return lst;
                #endregion
            }
        }
        public List<Inventario> ObtenerInventario(long Iidproducto)
        {
            using(var db = new BDFERRETERIAContext())
            {
                return db.Inventario.Where(p => p.Iidproducto == Iidproducto && p.Bhabilitado == "A" && p.Cantidad > 0)
                        .Include(p => p.IidproductoNavigation).ToList();
            }
        }
    }
}
