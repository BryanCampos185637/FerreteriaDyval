using AdminFerreteria.Models;
using AdminFerreteria.ViewMovels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdminFerreteria.DataAccessLogic
{
    public class BitacoraSistemaDAL
    {
        public async Task InsertarBitacoraSistema(Bitacorasistema bitacorasistema)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    db.Bitacorasistema.Add(bitacorasistema);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ListBitacoraSistema> PaginarListaBitacora(int pagina, string filtro)
        {
            try
            {
                using (var context = new BDFERRETERIAContext())
                {
                    int totalActivos = await context.Bitacorasistema
                        .Include(P => P.IidusuarioNavigation.IidempleadoNavigation)
                        .Where(p => p.IidusuarioNavigation.IidempleadoNavigation.Nombrecompleto.Contains(filtro) && p.Iidusuario != 7)
                        .CountAsync();
                    int totalPaginas = (int)Math.Ceiling((double)totalActivos / 8);
                    if (pagina > totalPaginas) { pagina = totalPaginas; }
                    var list = await context.Bitacorasistema
                                   .Include(p => p.IidusuarioNavigation.IidempleadoNavigation)
                                   .Include(p => p.IidusuarioNavigation.IidtipousuarioNavigation)
                                   .Where(p => p.IidusuarioNavigation.IidempleadoNavigation.Nombrecompleto.Contains(filtro) && p.Iidusuario != 7)
                                   .OrderByDescending(p => p.Fechaactividad)
                                   .Skip((pagina - 1) * 8)
                                   .Take(8).ToListAsync();
                    return new ListBitacoraSistema
                    {
                        LstBitacora = list,
                        PaginaActual = pagina,
                        TotalRegistros = totalActivos,
                        RegistroPorPagina = 8,
                        TotalPaginas = totalPaginas,
                        Filtro = filtro
                    };
                }
            }
            catch (Exception e)
            {
                string d = e.Message;
                return null;
            }
        }
    }
}
