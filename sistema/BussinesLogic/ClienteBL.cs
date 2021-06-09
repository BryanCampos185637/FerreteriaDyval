using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using System;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class ClienteBL
    {
        ClienteDAL dal = new ClienteDAL();
        public static int existCliente(Cliente cliente)
        {
            return ClienteDAL.ExistCliente(cliente);
        }

        public List<Cliente> listar()
        {
            return dal.ListarClientes();
        }
        public int guardar(Cliente cliente)
        {
            return dal.GuardarCliente(cliente);
        }
        public Cliente obtenerPorId(Int64 id)
        {
            return dal.ObtenerClientePorId(id);
        }
        public int eliminar(Int64 id)
        {
            return dal.EliminarCliente(id);
        }
    }
}
