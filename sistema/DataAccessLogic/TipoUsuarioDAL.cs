using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AdminFerreteria.DataAccessLogic
{
    public class TipoUsuarioDAL
    {
        public void InsertarRolAdministrador()
        {
            using(var db = new BDFERRETERIAContext())
            {
                var tipousuario = new Tipousuario
                {
                    Fechacreacion = DateTime.Now,
                    Bhabilitado = "A",
                    Nombretipousuario = "ADMINISTRADOR",
                    Descripcion = "TIENE TODOS LOS PERMISOS"
                };
                db.Tipousuario.Add(tipousuario);
                var rpt = db.SaveChanges();
                if (rpt > 0)
                {
                    var listaPaginas = db.Pagina.Where(p => p.Bhabilitado == "A").ToList();
                    foreach(var item in listaPaginas)
                    {
                        db.Paginatipousuario.Add(new Paginatipousuario
                        {
                            Iidtipousuario = tipousuario.Iidtipousuario,
                            Iidpagina = item.Iidpagina,
                            Fechacreacion = DateTime.Now,
                            Bhabilitado = "A"
                        });
                        db.SaveChanges();
                    }
                }
            }
        }
        public Tipousuario ObtenerTipoUsuarioPorId(int id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                Tipousuario lst = db.Tipousuario.Where(p => p.Iidtipousuario == id).First();
                return lst;
            }
        }
        public List<Tipousuario> ListarTipoUsuario()
        {
            using (var db = new BDFERRETERIAContext())
            {
                List<Tipousuario> lst = db.Tipousuario.Where(p => p.Bhabilitado == "A").ToList();
                lst = lst.OrderByDescending(p => p.Iidtipousuario).ToList();
                return lst;
            }
        }
        public int Guardar(Tipousuario tipousuario, int[] idPaginas)
        {
            try
            {
                using (var bd = new BDFERRETERIAContext())
                {
                    //utilizamos una transaccion porque afectaremos dos tablas a la vez
                    using (var transaccion = new TransactionScope())
                    {
                        if (UtilidadesController.existTipoUsuario(tipousuario) == 0)//si el dato no existe en la base de datos
                        {
                            if (tipousuario.Iidtipousuario == 0)//guarda
                            {
                                tipousuario.Fechacreacion = DateTime.Now;
                                bd.Tipousuario.Add(tipousuario);
                                bd.SaveChanges();//confirmamos el guardado de tipo usaurio
                                                 //a continuacion procedemos a insertar las paginas
                                #region guardar pagina
                                var paginashabilitadas = bd.Paginatipousuario.Where(x => x.Iidtipousuario.Equals(tipousuario.Iidtipousuario)).ToList();//primero obtenemos las paginas que contengan el id del tipo usuario
                                                                                                                                                       //con un foreach vamos a recorrer los registros para deshabilitarlos
                                foreach (Paginatipousuario item in paginashabilitadas)
                                {
                                    item.Bhabilitado = "D";//cambiamos a 0 el valor
                                    bd.SaveChanges();//confirmamos que se guardara
                                }
                                //ya deshabilitada las paginas vamos a verificar si la pagina ya existe
                                foreach (int pagina in idPaginas)
                                {
                                    int existePagina = bd.Paginatipousuario.Where(p => p.Iidpagina.Equals(pagina) && p.Iidtipousuario.Equals(tipousuario.Iidtipousuario)).Count();
                                    if (existePagina == 0)//si no existe procedemos a guardar el registro
                                    {
                                        Paginatipousuario ptu = new Paginatipousuario();//instancia del modelo
                                        ptu.Iidtipousuario = tipousuario.Iidtipousuario;//almacenamos el valor del id de tipo usuario
                                        ptu.Iidpagina = pagina;//almacenamos el valor del id que viene en el array
                                        ptu.Bhabilitado = "A";//y habilitamos la pagina
                                        ptu.Fechacreacion = DateTime.Now;//le decimos la fecha que se creo
                                        bd.Paginatipousuario.Add(ptu);//preparamos para guardar
                                        bd.SaveChanges();//confirmamos el guardado
                                    }
                                    else//si el registro existe
                                    {
                                        //obtenemos el registro que contiene el id de la pagina y del tipo de usuario
                                        var paginaHabilitar = bd.Paginatipousuario.Where(p => p.Iidpagina.Equals(pagina) && p.Iidtipousuario.Equals(tipousuario.Iidtipousuario)).First();
                                        paginaHabilitar.Bhabilitado = "A";//ahora habilitamos el registro
                                        bd.SaveChanges();//confirmamos el guardado
                                    }
                                }
                                #endregion
                            }
                            else//modifica
                            {
                                var dataTipoUsuario = bd.Tipousuario.Where(p => p.Iidtipousuario.Equals(tipousuario.Iidtipousuario)).First();
                                if (dataTipoUsuario.Iidtipousuario > 1)
                                {
                                    dataTipoUsuario.Nombretipousuario = tipousuario.Nombretipousuario;
                                    dataTipoUsuario.Descripcion = tipousuario.Descripcion;
                                    bd.SaveChanges();//confirmamos el guardar de tipo usuario

                                    //a continuacion procedemos a insertar las paginas
                                    #region guardar pagina
                                    var paginashabilitadas = bd.Paginatipousuario.Where(x => x.Iidtipousuario.Equals(tipousuario.Iidtipousuario)).ToList();//primero obtenemos las paginas que contengan el id del tipo usuario
                                    foreach (Paginatipousuario item in paginashabilitadas)//con un foreach vamos a recorrer los registros para deshabilitarlos
                                    {
                                        item.Bhabilitado = "D";//cambiamos a 0 el valor
                                        bd.SaveChanges();//confirmamos que se guardara
                                    }
                                    //ya deshabilitada las paginas vamos a verificar si la pagina ya existe
                                    foreach (int pagina in idPaginas)
                                    {
                                        int existePagina = bd.Paginatipousuario.Where(p => p.Iidpagina.Equals(pagina) && p.Iidtipousuario.Equals(tipousuario.Iidtipousuario)).Count();
                                        if (existePagina == 0)//si no existe procedemos a guardar el registro
                                        {
                                            Paginatipousuario ptu = new Paginatipousuario();//instancia del modelo
                                            ptu.Iidtipousuario = tipousuario.Iidtipousuario;//almacenamos el valor del id de tipo usuario
                                            ptu.Iidpagina = pagina;//almacenamos el valor del id que viene en el array
                                            ptu.Bhabilitado = "A";//y habilitamos la pagina
                                            ptu.Fechacreacion = DateTime.Now;//le decimos la fecha que se creo
                                            bd.Paginatipousuario.Add(ptu);//preparamos para guardar
                                            bd.SaveChanges();
                                        }
                                        else//si el registro existe
                                        {
                                            //obtenemos el registro que contiene el id de la pagina y del tipo de usuario
                                            var paginaHabilitar = bd.Paginatipousuario.Where(p => p.Iidpagina.Equals(pagina) && p.Iidtipousuario.Equals(tipousuario.Iidtipousuario)).First();
                                            paginaHabilitar.Bhabilitado = "A";//ahora habilitamos el registro
                                            bd.SaveChanges();//confirmamos el guardado
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    transaccion.Complete();
                                    return 1;
                                }
                            }
                            transaccion.Complete();//aqui se guardan los cambios realizados 
                            return 1;
                        }//si existe retornamos -1
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
                return 0;
            }
        }
        public List<Pagina> ListarPaginasAsignadas(Int64 id)
        {
            List<Pagina> lst = new List<Pagina>();
            using (var bd = new BDFERRETERIAContext())
            {
                lst = (from ptu in bd.Paginatipousuario
                       where ptu.Iidtipousuario == id && ptu.Bhabilitado == "A"
                       select new Pagina
                       {
                           Iidpagina = (int)ptu.Iidpagina
                       }).ToList();
            }
            return lst;
        }
        public List<Pagina> ListarPaginasExistentes()
        {
            using (var db = new BDFERRETERIAContext())
            {
                List<Pagina> lst = db.Pagina.Where(p => p.Bhabilitado == "A").ToList();
                return lst;
            }
        }
        public int Eliminar(int id)
        {
            try
            {
                using (var bd = new BDFERRETERIAContext())
                {
                    using (var transaccion = new TransactionScope())
                    {
                        if (id > 1)
                        {
                            var data = bd.Tipousuario.Where(x => x.Iidtipousuario.Equals(id)).First();
                            data.Bhabilitado = "D";
                            bd.SaveChanges();
                            var paginashabilitadas = bd.Paginatipousuario.Where(x => x.Iidtipousuario.Equals(id)).ToList();//primero obtenemos las paginas que contengan el id del tipo usuario
                            foreach (Paginatipousuario item in paginashabilitadas)//con un foreach vamos a recorrer los registros para deshabilitarlos
                            {
                                item.Bhabilitado = "D";//cambiamos a 0 el valor
                                bd.SaveChanges();//confirmamos que se guardara
                            }
                            transaccion.Complete();//afirmamos que se guardara
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
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
