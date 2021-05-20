using AdminFerreteria.Models;
using AdminFerreteria.ViewMovels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AdminFerreteria.DAL
{
    public class BitacoraSistemaDAL
    {
        public void insertarBitacora(Bitacorasistema bitacorasistema)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    db.Bitacorasistema.Add(bitacorasistema);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ListBitacoraSistema paginar(int pagina, string filtro)
        {
            try
            {
                using (var context = new BDFERRETERIAContext())
                {
                    int totalActivos = context.Bitacorasistema
                        .Include(P=>P.IidusuarioNavigation.IidempleadoNavigation)
                        .Where(p => p.IidusuarioNavigation.IidempleadoNavigation.Nombrecompleto.Contains(filtro))
                        .Count();
                    int totalPaginas = (int)Math.Ceiling((double)totalActivos / 8);
                    if (pagina > totalPaginas) { pagina = totalPaginas; }
                    var list = context.Bitacorasistema
                                   .Include(p => p.IidusuarioNavigation.IidempleadoNavigation)
                                   .Include(p=>p.IidusuarioNavigation.IidtipousuarioNavigation)
                                   .Where(p => p.IidusuarioNavigation.IidempleadoNavigation.Nombrecompleto.Contains(filtro))
                                   .OrderByDescending(p => p.Fechaactividad)
                                   .Skip((pagina - 1) * 8)
                                   .Take(8).ToList();
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
