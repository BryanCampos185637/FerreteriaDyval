using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class UnidadMedidaDAL
    {
        public static string ObtenerNombreDeSubUnidad(int? id)
        {
            //metodo que nos sirve para obtener el nombre de la subunidad
            using (var db = new BDFERRETERIAContext())
            {
                var unidad = db.Unidadmedida.Where(p => p.Iidunidadmedida.Equals(id)).FirstOrDefault();
                if (unidad != null)
                    return unidad.Nombreunidad;
                else
                    return "No tiene";
            }
        }
        public List<Unidadmedida> ListarUnidad()
        {
            using (var db = new BDFERRETERIAContext())
            {
                var lst = db.Unidadmedida.Where(p => p.Bhabilitado == "A").ToList();
                return lst;
            }
        }
        public int Guardar(Unidadmedida unidadmedida)
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
        public Unidadmedida ObtenerPorId(int id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var data = db.Unidadmedida.Where(p => p.Iidunidadmedida == id).First();
                return data;
            }
        }
        public int Eliminar(int id)
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
        public static int ExistUnidad(Unidadmedida unidad)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Unidadmedida.Where(p => p.Bhabilitado == "A" && p.Iidunidadmedida != unidad.Iidunidadmedida
                    && p.Nombreunidad == unidad.Nombreunidad).Count();
            }
        }
    }
}
