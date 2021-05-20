using System;
using System.Linq;
using System.Transactions;
using AdminFerreteria.DAL;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class EntradaController : Controller
    {
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("entrada", "index", (int)idUsuario))
                {
                    return View();
                }
                else
                {
                    return Redirect("/Home/error");
                }
            }
            else
            {
                return Redirect("/Login/index");
            }
        }
        EntradaDAL dal = new EntradaDAL();
        [HttpGet]
        public JsonResult listEntrada()
        {
            return Json(dal.listarEntrada());
        }
        [HttpPost]
        public int saveEntrada(Entrada entrada, Int64[] bodegas, Int64[] cantidades, Int64 ventas, Int64[] stock, decimal precioCompra)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    using (var transaction = new TransactionScope())
                    {
                        if (entrada.Iidentrada == 0)
                        {
                            entrada.Fechacreacion = DateTime.Now;
                            entrada.Preciocompra = precioCompra;
                            db.Entrada.Add(entrada);
                            db.SaveChanges();
                            Producto producto = db.Producto.Where(p => p.Iidproducto == entrada.Iidproducto).First();
                            if (producto.Preciocompra != precioCompra)//si el precio de compra es diferente al actual entonces modificaremos
                            {
                                producto.Preciocompra = precioCompra;
                                db.SaveChanges();
                            }
                            if (ventas > 0)//si ventas es mayor a 0 es porque se agregaran productos a la sala de venta
                            {
                                //vamos a modificar el producto ya que entraron productos
                                Int64 existenciasAumentadas = (Int64)producto.Existencias + ventas;
                                producto.Existencias = existenciasAumentadas;
                                decimal? subexistencia = 0;
                                if (producto.Subunidad != null)//si tiene una subunidad se le saca el porcentaje
                                {
                                    subexistencia = ventas * producto.Equivalencia;//obtenemos cuantas partes entraron del producto original
                                    if (producto.Subexistencia == null)// si es null solo agregamos el nuevo datos
                                    {
                                        producto.Subexistencia = subexistencia;
                                    }
                                    else//si ya hay un registro entonces sumamos lo que hay con lo nuevo
                                    {
                                        producto.Subexistencia = producto.Subexistencia + subexistencia;//y se las sumamos
                                    }
                                }
                                #region creamos la bitacora de este producto
                                //-1 para mi significara que es la sala de venta
                                var bitacoraProducto = UtilidadesController.crearBitacora(-1, producto.Iidproducto,
                                    existenciasAumentadas, -1, (decimal)subexistencia, entrada.Iidentrada);
                                db.Bitacoraentrada.Add(bitacoraProducto);
                                db.SaveChanges();
                                #endregion
                            }
                            //empezamos aguardar los datos de la entrada
                            for (int i = 0; i < bodegas.Length; i++) 
                            {
                                var nveces = UtilidadesController.yaExiteEsteInventario((int)bodegas[i], (int)entrada.Iidproducto);
                                if (nveces > 0) //si ya existe este registro entonces solo aumentamos la cantidad en esa bodega
                                {
                                    var data = db.Inventario.Where(p => p.Iidbodega == (int)bodegas[i] && 
                                        p.Iidproducto == (int)entrada.Iidproducto && p.Bhabilitado == "A").First();
                                    data.Cantidad += cantidades[i];
                                    data.Bhabilitado = "A";
                                    db.SaveChanges();
                                }
                                else//si no tenemos que crear el registro
                                {
                                    Inventario inventario = UtilidadesController.crearObjetInventario((int)bodegas[i], 
                                        (int)entrada.Iidproducto, (int)cantidades[i], (int)stock[i]);
                                    db.Inventario.Add(inventario);
                                    db.SaveChanges();
                                }
                                #region creamos la bitacora de la entrada hacia las bodegas
                                //-1 para mi significara que es la sala de venta
                                var bitacora = UtilidadesController.crearBitacora((int)bodegas[i],(int)entrada.Iidproducto,
                                   (int)cantidades[i], (int)stock[i], 0, entrada.Iidentrada);
                                db.Bitacoraentrada.Add(bitacora);
                                db.SaveChanges();
                                #endregion
                            }
                            db.SaveChanges();
                        }
                        transaction.Complete();
                        return 1;
                    }
                }
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        [HttpGet]
        public int deleteEntrada(Int64 id)
        {
            return dal.eliminarEntrada(id);
        }
        [HttpGet]
        public JsonResult listarStock()
        {
            return Json(dal.listarStock());
        }
    }
}
