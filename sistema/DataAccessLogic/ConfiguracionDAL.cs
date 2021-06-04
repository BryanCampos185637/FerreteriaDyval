using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using System;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class ConfiguracionDAL
    {
        public Configuracion ObtenerConfiguracionSistema()
        {
            using (var db = new BDFERRETERIAContext())
            {
                var data = db.Configuracion.Where(p => p.Iidconfiguracion == 1).FirstOrDefault();
                return data;
            }
        }
        public int ActualizarConfiguracionSistema(Configuracion configuracion, string usuario, string contra)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    contra = UtilidadesController.encryptPassword(contra);
                    Usuario user = db.Usuario.Where(p => p.Nombreusuario == usuario && p.Contraseña == contra).FirstOrDefault();
                    if (user != null && user.Iidtipousuario == 1)
                    {
                        var data = db.Configuracion.Where(p => p.Iidconfiguracion == 1).First();
                        data.Iniciocotizacion = configuracion.Iniciocotizacion;
                        data.Iniciofactura = configuracion.Iniciofactura;
                        data.Iniciocreditofiscal = configuracion.Iniciocreditofiscal;
                        data.Fincotizacion = configuracion.Fincotizacion;
                        data.Fincreditofiscal = configuracion.Fincreditofiscal;
                        data.Finfactura = configuracion.Finfactura;
                        data.Noactualcotizacion = configuracion.Noactualcotizacion;
                        data.Noactualfactura = configuracion.Noactualfactura;
                        data.Noactualcreditofiscal = configuracion.Noactualcreditofiscal;
                        data.Nodigitoscotizacion = configuracion.Nodigitoscotizacion;
                        data.Nodigitoscreditofiscal = configuracion.Nodigitoscreditofiscal;
                        data.Nodigitosfactura = configuracion.Nodigitosfactura;
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
