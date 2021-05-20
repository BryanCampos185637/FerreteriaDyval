$(function () {
    callTable();
});
function callTable() {
    paintTable('/existencia/listProducto', ['codigo', 'descripcion','stock', 'Precio compra', 'existencias', 'subexistencia', 'restan'],
        ['codigoproducto', 'descripcion','nombrestock', 'preciocompra', 'existencias', 'subexistencia', 'restantes'], 'iidproducto', 'modalEntrada', false, true);
    fillCombo('/stock/listStock', 'iidstock', 'nombrestock', 'iidstock','','--Seleccione un stock--');
}
function edit(id) {
    $.get('/producto/getProductoById?id=' + id, function (data) {
        $('#nombreproducto').val(data.descripcion);
        $('#iidproducto').val(data.iidproducto);
        $('#codigoProducto').val(data.codigoproducto);
        $('#existenciasproducto').val(data.existencias);
        $('#proveedor').val(data.proveedor);
        $('#preciocompra').val(data.preciocompra);
    });
    //cerramos el modal
    $('#tbodyProducto').html('');
    document.getElementById('btnCerrarProducto').click();
}

document.getElementById('btnActivar').onclick = function () {
    $.get('/existencia/crearListaReporte?id=' + $('#iidstock').val(), function (respuesta) {
        if (respuesta > 0) {
            messeges('success', 'Creando reporte');
            document.getElementById('btnImprimirReporte').click();
        } else {
            messeges('error','Error al crear el reporte')
        }
    })
}

$('#frmEntrada').submit(function (e) {
    e.preventDefault();
    sendData();
});

function sendData() {
    if (validateEmpty()) {
        if ($('#cantidad').val() > 0) {
            document.getElementById('cantidad').style.borderColor = '#ccc';
            var frm = new FormData();
            capturarData(frm);
            frm.append('precioCompra', $('#preciocompra').val() * 1);
            sendDataController('/entrada/saveEntrada', frm);
        } else {
            document.getElementById('cantidad').style.borderColor = 'red';
            alertify.error('La cantidad no puede ser negativo o 0');
        }
    } else {
        alertify.error('Llena los campos marcados');
    }
};