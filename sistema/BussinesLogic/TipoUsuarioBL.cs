using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using System;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class TipoUsuarioBL
    {
        TipoUsuarioDAL dal = new TipoUsuarioDAL();
        public static int existTipoUsuario(Tipousuario tipousuario)
        {
            return TipoUsuarioDAL.ExistTipoUsuario(tipousuario);
        }
        public void insertarRolAdministrador()
        {
            dal.InsertarRolAdministrador();
        }
        public Tipousuario ObtenerTipoUsuario(int id)
        {
            return dal.ObtenerTipoUsuarioPorId(id);
        }
        public List<Tipousuario> Listar()
        {
            return dal.ListarTipoUsuario();
        }
        public int guardar(Tipousuario tipousuario, int[] idPaginas)
        {
            return dal.Guardar(tipousuario, idPaginas);
        }
        public List<Pagina> listarPaginasAsignadas(Int64 id)
        {
            return dal.ListarPaginasAsignadas(id);
        }
        public List<Pagina> listarPaginasExistentes()
        {
            return dal.ListarPaginasExistentes();
        }
        public int eliminar(int id)
        {
            return dal.Eliminar(id);
        }
    }
}
