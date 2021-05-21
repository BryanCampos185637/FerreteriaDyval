using AdminFerreteria.DAL;
using AdminFerreteria.Models;

namespace AdminFerreteria.Helper.HelperBitacora
{
    public class LogicaBitacoraSistema
    {
        public static void InsertarBitacoraBL(string mensaje, int? UsuarioLogueado)
        {
            BitacoraSistemaDAL bitacora = new BitacoraSistemaDAL();
            bitacora.insertarBitacora(new Bitacorasistema
            {
                Iidusuario = (int)UsuarioLogueado,
                Descripcionbitacora = mensaje,
                Fechaactividad = System.DateTime.Now
            });
        }
    }
}
