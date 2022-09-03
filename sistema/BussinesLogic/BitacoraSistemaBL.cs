using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using AdminFerreteria.ViewMovels;
using System.Threading.Tasks;

namespace AdminFerreteria.BussinesLogic
{
    public class BitacoraSistemaBL
    {
        BitacoraSistemaDAL dal = new BitacoraSistemaDAL();
        public async Task insertarBitacora(Bitacorasistema bitacorasistema)
        {
            await dal.InsertarBitacoraSistema(bitacorasistema);
        }
        public async Task<ListBitacoraSistema> paginar(int pagina, string filtro) 
        {
            return await dal.PaginarListaBitacora(pagina, filtro);
        }
    }
}
