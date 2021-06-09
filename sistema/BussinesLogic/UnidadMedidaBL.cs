using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class UnidadMedidaBL
    {
        UnidadMedidaDAL dal = new UnidadMedidaDAL();
        public static string ObtenerNombreSubUnidad(int? id)
        {
            return UnidadMedidaDAL.ObtenerNombreDeSubUnidad(id);
        }
        public List<Unidadmedida> listar()
        {
            return dal.ListarUnidad();
        }
        public int guardar(Unidadmedida unidadmedida)
        {
            return dal.Guardar(unidadmedida);
        }
        public Unidadmedida obtener(int id)
        {
            return dal.ObtenerPorId(id);
        }
        public int eliminar(int id)
        {
            return dal.Eliminar(id);
        }
        public static int existUnidad(Unidadmedida unidad)
        {
            return UnidadMedidaDAL.ExistUnidad(unidad);
        }
    }
}
