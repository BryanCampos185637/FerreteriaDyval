using AdminFerreteria.Models;
namespace AdminFerreteria.Helper.HelperCalculoPrecio
{
    public class CalcularPrecioProducto
    {

        public static Producto calcular(Producto obj)
        {
            #region calculo producto original
            var utilidadDelProducto = ((obj.Preciocompra / 100) * obj.Porcentajeganancia);
            var utilidadMasPrecioCompra = utilidadDelProducto + obj.Preciocompra;
            var ivaDelProducto = utilidadMasPrecioCompra * (decimal)0.13;
            var precioVenta = utilidadMasPrecioCompra + ivaDelProducto;

            obj.Precioventa = precioVenta;
            obj.Iva = ivaDelProducto;
            obj.Ganancia = utilidadDelProducto;
            #endregion

            #region calculo sub producto 
            if (obj.Subunidad > 0)
            {
                var subPrecioCompra = obj.Preciocompra / obj.Equivalencia;
                var utilidadSubProducto = ((subPrecioCompra / 100) * obj.Subporcentaje);
                var utilidadMasSubPrecioCompra = utilidadSubProducto + subPrecioCompra;
                var ivaSubPrecio = utilidadMasSubPrecioCompra * (decimal)0.13;
                var subPrecioVenta = ivaSubPrecio + utilidadMasSubPrecioCompra;

                obj.Subpreciounitario = subPrecioCompra;
                obj.Subprecioventa = subPrecioVenta;
                obj.Subiva = ivaSubPrecio;
                obj.Subganancia = utilidadSubProducto;
            }
            else
            {
                obj.Subunidad = null;
                obj.Subpreciounitario = 0;
                obj.Subprecioventa = 0;
                obj.Subiva = 0;
                obj.Subganancia = 0;
            }
            #endregion

            return obj;
        }
    }
}
