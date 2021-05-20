using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DAL
{
    public class ClienteDAL
    {
        public List<Cliente> listar()
        {
            using (var db = new BDFERRETERIAContext())
            {
                List<Cliente> lst = db.Cliente.Where(p => p.Bhabilitado == "A").ToList();
                return lst;
            }
        }
        public int guardar(Cliente cliente)
        {
            try
            {
                int nveces = UtilidadesController.existCliente(cliente);
                if (nveces == 0)
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        if (cliente.Iidcliente == 0)
                        {
                            db.Cliente.Add(cliente);
                        }
                        else
                        {
                            var data = db.Cliente.Where(p => p.Iidcliente.Equals(cliente.Iidcliente)).First();
                            data.Nombrecompleto = cliente.Nombrecompleto;
                            data.Direccion = cliente.Direccion;
                            data.Registro = cliente.Registro;
                            data.Giro = cliente.Giro;
                            data.Nit = cliente.Nit;
                        }
                        db.SaveChanges();
                    }
                    return 1;
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
        public Cliente obtenerPorId(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                Cliente lst = db.Cliente.Where(p => p.Iidcliente == id).First();
                return lst;
            }
        }
        public int eliminar(Int64 id)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    Cliente lst = db.Cliente.Where(p => p.Iidcliente == id).First();
                    lst.Bhabilitado = "D";
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
