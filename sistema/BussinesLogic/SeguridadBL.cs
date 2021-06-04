using AdminFerreteria.DataAccessLogic;

namespace AdminFerreteria.BussinesLogic
{
    public class SeguridadBL
    {
        public static int tieneAutorizacion(string controller, string action, int idUsuario)
        {
            return SeguridadDAL.TieneAutorizacion(controller, action, idUsuario);
        }
    }
}
