
window.onload = callTable()
function callTable() {
    paintTable('/producto/listProducto', ['codigo', 'descripcion', 'Precio compra', 'iva', 'utilidad', 'precio', 'existencias', 'subprecio', 'subexistencia','fracciones'],
        ['codigoproducto', 'descripcion', 'preciocompra', 'iva', 'ganancia', 'precioventa', 'existencias', 'subprecioventa', 'subexistencia', 'restantes'],
        'iidproducto', 'modalProducto', true, true);
    fillCombo('/unidadmedida/listUnidad', 'iidunidadmedida', 'nombreunidad', 'iidunidadmedida');
    fillCombo('/unidadmedida/listUnidad', 'iidunidadmedida', 'nombreunidad', 'subiidunidadmedida');
    fillCombo('/stock/listStock', 'iidstock', 'nombrestock', 'iidstock');
};

$('#frmProducto').submit(function (e) {
    e.preventDefault();
    sendData();
});

function deleteDataOfTheForm() {
    $('#iidproducto').val('');
    $('#codigoproducto').val('');
    $('#iidunidadmedida').val('');
    $('#iidstock').val('');
    $('#descripcion').val('');
    $('#proveedor').val('');
    $('#preciocompra').val(0);
    $('#iva').val('0.0000');
    $('#Precioiva').val('0.0000');
    $('#gananciaObtenida').val(1);
    $('#ganancia').val('0.0000');
    $('#precioventa').val('0.0000');
    $('#equivalencia').val(0);
    $('#subiidunidadmedida').val('');
    $('#subpreciocompra').val('0.0000');
    $('#subiva').val(1);
    $('#subPrecioiva').val('0.0000');
    $('#subgananciaObtenida').val(1);
    $('#subganancia').val('0.0000');
    $('#subPrecioiva').val('0.0000');
    $('#subprecioventa').val('0.0000');
}

function edit(id) {
    deleteDataOfTheForm();
    colorDefault();
    $.get('/producto/getProductoById?id=' + id, function (data) {
        $('#iidproducto').val(data.iidproducto);
        $('#codigoproducto').val(data.codigoproducto);
        $('#descripcion').val(data.descripcion);
        $('#proveedor').val(data.proveedor);
        $('#iva').val(data.iva);
        $('#preciocompra').val(data.preciocompra);
        $('#ganancia').val(data.ganancia);
        $('#precioventa').val(data.precioventa);
        $('#gananciaObtenida').val(data.porcentajeganancia);
        $('#iidunidadmedida').val(data.iidunidadmedida);
        $('#iidstock').val(data.iidstock);
        var precioCOnIva = parseFloat(data.preciocompra) + parseFloat(data.iva);
        $('#Precioiva').val(precioCOnIva.toFixed(4));
        /*obtenemos los datos secundarios*/
        $('#subiidunidadmedida').val(data.subunidad);
        $('#equivalencia').val(data.equivalencia);
        $('#subpreciocompra').val(data.subpreciounitario);
        $('#subgananciaObtenida').val(data.subporcentaje);
        $('#subiva').val(data.subiva);
        var subprecioConIva = parseFloat(data.subpreciounitario) + parseFloat(data.subiva);
        if (subprecioConIva != null) {
            $('#subPrecioiva').val(subprecioConIva.toFixed(4));
        }
        $('#subganancia').val(data.subganancia);
        $('#subprecioventa').val(data.subprecioventa);
    })
};

function calculatePrice() {
    var precioCompra = document.getElementById('preciocompra').value;
    var ganancia = document.getElementById('gananciaObtenida').value;
    if (!isNaN(precioCompra)) {
        if (parseFloat(precioCompra) > 0) {
            if (parseFloat(ganancia) > 0 && parseFloat(ganancia) <= 100) {
                //calculo del iva
                var precioIva = (parseFloat(precioCompra) / 100) * 13;
                document.getElementById('iva').value = precioIva.toFixed(4);
                //pintamos el precio
                var precioConIva = (parseFloat(precioIva) + parseFloat(precioCompra));
                document.getElementById('Precioiva').value = precioConIva.toFixed(4);//mostramos el precio con iva
                //calculo de la ganacia
                var precioGanacia = ((parseFloat(precioCompra) + precioIva) / 100) * ganancia;
                document.getElementById('ganancia').value = precioGanacia.toFixed(4);
                //precio final 
                var precioFinal = parseFloat(precioCompra) + parseFloat(precioIva) + parseFloat(precioGanacia);
                document.getElementById('precioventa').value = precioFinal.toFixed(4);
            } else {
                messeges('warning', 'Ganancia fuera de rango');
            }
        } else {
            messeges('warning', 'Precio compra no pueden ser menor a 0.');
        }
    } else {
        messeges('warning', 'Precio compra debe ser numerico.');
    }
    if ($('#subiidunidadmedida').val() != null || $('#subiidunidadmedida').val() > 0) {
        calculateSubPrice();
    }
}

function calculateSubPrice() {
    var precioCOmpraOriginal = document.getElementById('preciocompra').value;//capturo el precio original
    var equivalencia = document.getElementById('equivalencia').value;//capturo la equivalencia
    var subprecio = parseFloat(precioCOmpraOriginal) / parseFloat(equivalencia); $('#subpreciocompra').val(subprecio.toFixed(4));//divido el precio original entre la equivalencia
    var precioCompra = document.getElementById('subpreciocompra').value;
    var ganancia = document.getElementById('subgananciaObtenida').value;
    if (!isNaN(precioCompra)) {
        if (parseFloat(precioCompra) > 0) {
            if (parseFloat(ganancia) > 0 && parseFloat(ganancia) <= 100) {
                //calculo del iva
                var precioIva = (parseFloat(precioCompra) / 100) * 13;
                document.getElementById('subiva').value = precioIva.toFixed(4);
                //pintamos el precio
                var precioConIva = (parseFloat(precioIva) + parseFloat(precioCompra));
                document.getElementById('subPrecioiva').value = precioConIva.toFixed(4);//mostramos el precio con iva
                //calculo de la ganacia
                var precioGanacia = ((parseFloat(precioCompra) + precioIva) / 100) * ganancia;
                document.getElementById('subganancia').value = precioGanacia.toFixed(4);
                //precio final 
                var precioFinal = parseFloat(precioCompra) + parseFloat(precioIva) + parseFloat(precioGanacia);
                document.getElementById('subprecioventa').value = precioFinal.toFixed(4);
            } else {
                messeges('warning', 'Ganancia fuera de rango');
            }
        } else {
            messeges('warning', 'Precio compra no pueden ser menor a 0.');
        }
    } else {
        messeges('warning', 'Precio compra debe ser numerico.');
    }

}
function deleteInfo(id) {
    messegeConfirm('/producto/deleteProducto?id=' + id, 'Eliminar producto',
        '¿Estas seguro que deseas eliminar el producto?','Si! eliminar')
};

function sendData() {
    if (validateEmpty()) {
        var frm = new FormData();
        capturarData(frm);
        sendDataController('/producto/saveProducto', frm, 'El codigo ingresado ya esta asociado a un producto');
        deleteDataOfTheForm();
    } else {
        messeges('warning','Llena los campos marcados');
    }
    $('#gananciaObtenida').val(1);
    document.getElementById('gananciaObtenida').value = 1;
};