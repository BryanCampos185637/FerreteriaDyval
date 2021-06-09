using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class ExistenciaBL
    {
        ExistenciaDAL dal = new ExistenciaDAL();
        public List<ListProducto> listarProductosActivos()
        {
            return dal.ListarProductosActivos();
        }
        public List<Inventario> obtenerInventario(long Iidproducto)
        {
            return dal.ObtenerInventario(Iidproducto);
        }
    }
}
