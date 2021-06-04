using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;

namespace AdminFerreteria.BussinesLogic
{
    public class ConfiguracionBL
    {
        ConfiguracionDAL dal = new ConfiguracionDAL();
        public Configuracion obtenerConfiguracionSistema()
        {
            return dal.ObtenerConfiguracionSistema();
        }
        public int ActualizarConfiguracion(Configuracion configuracion, string usuario, string contra)
        {
            return dal.ActualizarConfiguracionSistema(configuracion, usuario, contra);
        }
    }
}
