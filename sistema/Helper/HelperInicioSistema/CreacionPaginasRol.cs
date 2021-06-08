using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Controllers;
using System;

namespace AdminFerreteria.Helper.HelperInicioSistema
{
    public class CreacionPaginasRol
    {
        public static void iniciarSistema()
        {
            try
            {
                var pagina = new PaginaBL();
                var tipo = new TipoUsuarioBL();
                if (!pagina.existePaginaEnSistema())
                {
                    var rptPagina = pagina.insertarPaginas();
                    if (rptPagina > 0)
                        tipo.insertarRolAdministrador();
                }
            }
            catch (Exception e)
            {
                HomeController home = new HomeController();
                home.ErrorInicio("Error:" + e.Message);
            }
        }
    }
}
