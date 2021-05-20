using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DAL
{
    public class UnidadMedidaDAL
    {
        public List<Unidadmedida> listar()
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = db.Unidadmedida.Where(p => p.Bhabilitado == "A").ToList();
                return lst;
            }
        }
        public int guardar(Unidadmedida unidadmedida)
        {
            unidadmedida.Fechacreacion = DateTime.Now;
            try
            {
                int nveces = UtilidadesController.existUnidad(unidadmedida);
                if (nveces == 0)
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        if (unidadmedida.Iidunidadmedida == 0)
                        {
                            db.Unidadmedida.Add(unidadmedida);
                        }
                        else
                        {
                            Unidadmedida unidad = db.Unidadmedida.Where(p => p.Iidunidadmedida.Equals(unidadmedida.Iidunidadmedida)).First();
                            unidad.Nombreunidad = unidadmedida.Nombreunidad;
                        }
                        db.SaveChanges();
                        return 1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public Unidadmedida obtener(int id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var data = db.Unidadmedida.Where(p => p.Iidunidadmedida == id).First();
                return data;
            }
        }
        public int eliminar(int id)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var data = db.Unidadmedida.Where(p => p.Iidunidadmedida == id).First();
                    data.Bhabilitado = "D";
                    db.SaveChanges();
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
