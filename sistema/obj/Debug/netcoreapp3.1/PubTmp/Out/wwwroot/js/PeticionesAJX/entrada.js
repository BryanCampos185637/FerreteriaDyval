
window.onload = callTable()
function callTable() {
    paintTable('/Entrada/listEntrada', ['producto', 'existencia pasada', 'f. expedicion ccf', 'f. inicio credito', 'f. final', 'entrada'],
        ['descripcionproducto', 'existencias', 'fechaexpedicionccf', 'fechainiciocredito', 'fechavencimiento', 'entrada'],
        'iidentrada', '', false, false);
};

function abrirModalProducto() {
    $.get('/producto/listProducto', function (data) {
        var html = '';
        html += '<table class="table table-hover table-bordered table-responsive-sm" id="tableproduct">'
        html += '<thead class="thead-dark">'
        html += '<tr>'
        html += '<th>CODIGO</th>'
        html += '<th>PRODUCTO</th>'
        html += '<th>OPERACIONES</th>'
        html += '</tr>'
        html += '</thead>'
        html += '<tbody>'
        $.each(data, function (objeto, propiedad) {
            html += '<tr>';
            html += '<td>' + propiedad.codigoproducto + '</td>';
            html += '<td>' + propiedad.descripcion + '</td>';
            html += '<td>';
            html += '<a class="btn-sm btn-primary" href="#" onclick="getDataProductoById(' + propiedad.iidproducto + ')">Selecciona <i class="fas fa-check"></i></a> ';
            html += '</td>';
            html += '</tr>';
        });
        html += '</tbody>'
        html += '</table>'
        $('#tablaProducto').html(html);
        $("#tableproduct").DataTable({
            pageLength: 4,
            lengthMenu: [4, 8, 12, 15],
            language: idiomaTabla
        });
    });
}

function getDataProductoById(id) {
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

//function edit(id) {
//    alertify.confirm('Opcion bloqueada', function (e) {
//        if (e) {
//            alertify.success('Gracias por su comprension.');
//        } else {
//            alertify.success('Gracias por su comprension.');
//        }
//    })
//};

//function deleteInfo(id) {
//    alertify.confirm('Estas seguro de eliminar este registro?', function (e) {
//        if (e) {
//            deleteData('/entrada/deleteEntrada?id=' + id, 'Solo se pueden eliminar registro de este dia.');
//            //alertify.success('Gracias por su comprencion');
//        }
//    })
//};

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
            messeges('warning','La cantidad no puede ser negativo o 0');
        }
    } else {
        messeges('warning','Llena los campos marcados');
    }
};