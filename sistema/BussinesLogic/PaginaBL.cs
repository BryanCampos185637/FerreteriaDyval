using AdminFerreteria.DataAccessLogic;

namespace AdminFerreteria.BussinesLogic
{
    public class PaginaBL
    {
        PaginaDAL dal = new PaginaDAL();
        public int insertarPaginas()
        {
            return dal.InsertarPaginas();
        }
        public bool existePaginaEnSistema()
        {
            return dal.ExistenPaginasRegistradasEnSistema();
        }
    }
}
