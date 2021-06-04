using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using AdminFerreteria.Request;
using System;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class EntradaBL
    {
        EntradaDAL dal = new EntradaDAL();
        public List<ListEntrada> listarEntrada()
        {
            return dal.ListarEntrada();
        }
        public List<Bitacoraentrada> obtenerListaBitacora(Int64 id)
        {
            return dal.ObtenerListaBitacora(id);
        }
        public List<Stock> listarStock()
        {
            return dal.ListarStock();
        }
        public int eliminarEntrada(Int64 id)
        {
            return dal.EliminarEntrada(id);
        }
        public int guardarEntrada(Entrada entrada, Int64[] bodegas, Int64[] cantidades, Int64 ventas, Int64[] stock, decimal precioCompra)
        {
            return dal.GuardarEntrada(entrada, bodegas, cantidades, ventas, stock, precioCompra);
        }
    }
}
