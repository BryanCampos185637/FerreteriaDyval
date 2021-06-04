using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;

namespace AdminFerreteria.BussinesLogic
{
    public class LoginBL
    {
        LoginDAL dal = new LoginDAL();
        public int Login(Usuario user)
        {
            return dal.LogIn(user);
        }
        public Usuario obtenerDataUsuarioLog(Usuario user)
        {
            return dal.ObtenerDataUsuarioLog(user);
        }
        public int RegistrarUsuarioNuevo(Empleado empleado, string nombreusuario, string contraseña)
        {
            return dal.RegistrarUserNuevo(empleado, nombreusuario, contraseña);
        }
        public object listarPaginasMenu(int idTipoUsuario)
        {
            return dal.ListarPaginasMenu(idTipoUsuario);
        }
    }
}
