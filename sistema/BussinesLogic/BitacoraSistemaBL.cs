using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using AdminFerreteria.ViewMovels;

namespace AdminFerreteria.BussinesLogic
{
    public class BitacoraSistemaBL
    {
        BitacoraSistemaDAL dal = new BitacoraSistemaDAL();
        public void insertarBitacora(Bitacorasistema bitacorasistema)
        {
            dal.InsertarBitacoraSistema(bitacorasistema);
        }
        public ListBitacoraSistema paginar(int pagina, string filtro) 
        {
            return dal.PaginarListaBitacora(pagina, filtro);
        }
    }
}
