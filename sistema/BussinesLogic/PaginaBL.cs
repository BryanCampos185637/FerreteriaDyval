using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class PaginaBL
    {
        PaginaDAL dal = new PaginaDAL();
        public static string obtenerNombrePagina(string action, string controller)
        {
            return PaginaDAL.ObtenerNombrePagina(action, controller);
        }
        public int insertarPaginas()
        {
            return dal.InsertarPaginas();
        }
        public bool existePaginaEnSistema()
        {
            return dal.ExistenPaginasRegistradasEnSistema();
        }
        public List<Pagina> listarPaginas()
        {
            return dal.ListarPaginas();
        }
        public Pagina obtenerPaginaPorId(int id)
        {
            return dal.ObtenerPaginaPorId(id);
        }
        public int guardar(Pagina pagina)
        {
            return dal.Guardar(pagina);
        }
        public string eliminar(int id) 
        {
            return dal.Eliminar(id);
        }
    }
}
