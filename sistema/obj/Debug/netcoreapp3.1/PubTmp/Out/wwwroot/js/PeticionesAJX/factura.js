document.getElementById('btnMostrarTabla').onclick = function () {
    document.getElementById('tableData').innerHTML = '<span>Cargando...</span>';
    paintTableFactura();
}

document.getElementById('btnMostrarCotizaciones').onclick = function () {
    paintTableCotizacion();
}

function edit(id) {
    $.get('/factura/getFacturaById?id=' + id, function (data) {
        document.getElementById('titulo').innerHTML = '<h3 class="modal-title">Factura No. ' + data.nofactura + '</h3>';
        $('#nombrevendedor').val(data.nombrevendedor);
        $('#nombrecomprador').val(data.nombrecomprador);
        $('#tipocomprador').val(data.tipocomprador);
        $('#direccion').val(data.direccion);
        $('#giro').val(data.giro);
        $('#nit').val(data.nit);
        $('#comision').val(data.totalcomision);
        $('#descuento').val(data.totaldescuento);
        $('#porcentajeDescuento').val(data.porcentajedescuento);
        $('#descuentoGlobal').val(data.descuentogeneral);
        $('#total').val(data.total);
        $('#iidfactura').val(data.iidfactura);
    });
    $.get('/factura/getDetallePedidoByIidfactura?id=' + id, function (lst) {
        var html = '';
        html += '<table class="table table-hover table-bordered table-responsive-sm table-responsive-md" id = "listProductsFactura">';
        html += '<thead class="thead-dark">';
        html += '<tr>';
        html += '<th>CANTIDAD</th>';
        html += '<th>PRODUCTO</th>';
        html += '<th>UM</th>';
        html += '<th>PRECIO</th>';
        html += '<th>COMISION</th>';
        html += '<th>DESCUENTO</th>';
        html += '<th>SUBTOTAL</th>';
        html += '</tr>';
        html += '</thead>';
        html += '<tbody>';
        $.each(lst, function (obj, item) {
            html += '<tr>';
            html += '<td>' + item.cantidad + '</td>';
            html += '<td>' + item.nombreproducto + '</td>';
            if (item.subproducto == 'NO') {
                html += '<td>' + item.unidadmedida + '</td>';
            } else {
                html += '<td>' + item.nombresubunidad + '</td>';
            }
            html += '<td>' + item.preciounitario + '</td>';
            html += '<td>' + item.comision + '</td>';
            html += '<td>' + item.descuento + '</td>';
            html += '<td>' + item.total + '</td>';
            html += '</tr>';
        });
        html += '</tbody>';
        html += '</table>';
        document.getElementById('tabla').innerHTML = html;
        $('#listProductsFactura').DataTable({
            pageLength: 3,
            lengthMenu: [3, 12, 18, 24],
            language: idiomaTabla
        });
    });
}

function addIdFacturaCookie() {
    var id = document.getElementById('iidfactura').value;
    $.get('/factura/addIdFacturaCookie?id=' + id, function (r) {
        if (r) {
            messeges('success','Creando factura')
            document.getElementById('btnImprimiFactura').click();
        } else {
            messeges('error','Error imprimir la factura');
        }
    })
}

///para pintar la tabla de facturas

function paintTableFactura(link = '/factura/listFactura', headboard = ['no. factura', 'tipo comprador', 'cliente', 'vendedor', 'emitida', 'total'],
    properties = ['nofactura', 'tipocomprador', 'nombrecomprador', 'nombrevendedor', 'fechaemitida', 'total'],
    primaryKey = 'iidfactura', idModal = 'modalFactura', optionDelete = false, optionEdit = true) {
    $.get(link, function (data) {
        var html = "";
        html += '<table class="table table-hover table-bordered table-responsive-sm" id="pagination">';
        html += '<thead class="thead-dark">'
        html += '<tr class="text-center">'
        var i = 0;
        while (i < headboard.length) {
            html += '<th>' + headboard[i].toUpperCase() + '</th>'
            i++;
        }
        if (optionDelete != false || optionEdit != false) {
            html += '<th>OPCIONES</th>';
        }
        html += '</tr>'
        html += '</thead>'
        html += '<tbody>'
        for (var c = 0; c < data.length; c++) {
            html += '<tr class="text-center">';
            var objectCurret = data[c];
            for (var f = 0; f < properties.length; f++) {
                var propertyCurret = properties[f];
                html += '<td>' + objectCurret[propertyCurret] + '</td>';
            }
            if (optionDelete != false || optionEdit != false) {
                html += '<td>';
                if (optionDelete)
                    html += '<a class="btn-sm btn-danger" href="#" onclick="deleteInfo(' + objectCurret[primaryKey] + ')"><i class="fas fa-trash"></i></a> ';
                if (optionEdit)
                    html += '<a class="btn-sm btn-success" href="#" data-toggle="modal" onclick="edit(' + objectCurret[primaryKey] + ')" data-target="#' + idModal + '"><i class="fas fa-edit"></i></a>';
                html += '</td>';
            }
            html += '</tr>';
        }
        html += '</tbody>'
        html += '</table>'
        $("#tableData").html(html);
        $("#pagination").DataTable({
            pageLength: 5,
            lengthMenu: [5, 12, 18, 24],
            language: idiomaTabla
        });
    })
}
////para pintar la tabla de cotizaciones
function paintTableCotizacion(link = '/factura/listCotizacion', headboard = ['no. Cotizacion', 'comprador', 'creada por','total'],
    properties = ['nocotizacion', 'nombrecliente', 'nombreusuario', 'total'],primaryKey = 'iidcotizacion') {
    $.get(link, function (data) {
        var html = "";
        html += '<table class="table table-hover table-bordered table-responsive-sm" id="pagination">';
        html += '<thead class="thead-dark">'
        html += '<tr class="text-center">'
        var i = 0;
        while (i < headboard.length) {
            html += '<th>' + headboard[i].toUpperCase() + '</th>'
            i++;
        }
        html += '<th>OPCIONES</th>';
        html += '</tr>'
        html += '</thead>'
        html += '<tbody>'
        for (var c = 0; c < data.length; c++) {
            html += '<tr class="text-center">';
            var objectCurret = data[c];
            for (var f = 0; f < properties.length; f++) {
                var propertyCurret = properties[f];
                if (objectCurret[propertyCurret] == '-1000') {
                    html += '<td>PROVISONAL</td>';
                } else {
                    html += '<td>' + objectCurret[propertyCurret] + '</td>';
                }
            }
            html += '<td>';
            html += '<a class="btn-sm btn-primary" href="#" onclick="imprimirCotizacion(' + objectCurret[primaryKey] + ')"><i class="fas fa-edit"></i> Imprimir</a>';
            html += '</td>';
            html += '</tr>';
        }
        html += '</tbody>'
        html += '</table>'
        $("#tableData").html(html);
        $("#pagination").DataTable({
            pageLength: 5,
            lengthMenu: [5, 12, 18, 24],
            language: idiomaTabla
        });
    })
}
function imprimirCotizacion(id) {
    $.get('/factura/addIdCotizacionCookie?id=' + id, function (r) {
        if (r) {
            messeges('success', 'Creando cotizacion');
            document.getElementById('btnImprimirCotizacion').click();
        } else {
            messeges('error', 'Error imprimir la factura');
        }
    })
}