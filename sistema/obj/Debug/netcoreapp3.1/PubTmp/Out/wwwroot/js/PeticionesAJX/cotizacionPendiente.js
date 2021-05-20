window.onload = paintTable();
$(document).ready(function ($) {
    $('#nit').inputmask('9999-999999-999-9', { placeholder: '0000-000000-000-0' });
    $('#registro').inputmask('999999-9', { placeholder: '000000-0' });
})
function paintTable() {
    $.get('/cotizacionpendiente/lstcotizacion', function (data) {
        var html = '';
        $.each(data, function (obj, item) {
            html += '<tr>'; 
            html += '<td>' + item.nocotizacion + '</td>';
            html += '<td>' + item.nombrecliente + '</td>';
            html += '<td>' + item.nombreusuario + '</td>';
            html += '<td>' + item.fechacreacion + '</td>';
            html += '<td>' + item.fechavencimiento + '</td>';
            html += '<td class="text-center">';
            html += '<a class="btn-sm btn-primary" title="Editar la lista de productos" data-toggle="modal" data-target="#modalListaProductos" href="#" onclick="verListaDeProductos(' + item.iidcotizacion + ')">Editar productos <i class="fas fa-edit"></i></a> ';
            html += '<a class="btn-sm btn-primary" title="Facturar cotizacion" data-toggle="modal" data-target="#modalFactura" href="#" onclick="ObtenerNumCotizacion(' + item.iidcotizacion + ')">Facturar <i class="fas fa-file-invoice-dollar"></i></a>';
            html += '</td>';
            html += '</tr>';
        });
        $('#tbodyCotizacion').html(html);
        $('#tableCotizacion').DataTable({
            pageLength: 5,
            lengthMenu: [5, 12, 18, 24],
            language: idiomaTabla
        });
    });
};
//agrega el numero de la cotizacion y el total a la modal 
function ObtenerNumCotizacion(id) {
    $.get('/cotizacionpendiente/ObtenerNumCotizacion?id=' + id, function (data) {
        $('#total').val('$' + data.total);
        $('#cotizacion').val(data.iidcotizacion);
        createCookie(data.iidcotizacion);
    });
}
//crea la cookie que contendra la lista
function createCookie(id) {
    $.get('/cotizacionPendiente/getDetalleCotizacionByIdCotizacion?id=' + id, function (r) {
        if (r != null)
            console.log('OK');
        else
            console.log('Error');
    });
}
//muestra los productos que estan cotizados
function verListaDeProductos(id) {
    document.getElementById('txtIIDCOTIZACION').value = id;
    $.get('/cotizacionPendiente/getDetalleCotizacionByIdCotizacion?id=' + id, function (data) {
        document.getElementById('tituloLista').innerHTML = '<h4>Editar productos de la cotizacion No. ' + data[0].nocotizacion+'</h4>';
        var html = ''; var total = 0; var comi = 0; var desc = 0;
        html += '<h5><u>Lista de productos</u></h5>';
        html += '<table class="table table-hover table-bordered table-responsive-md table-responsive-sm" id="listaCotizacion">';
        html += '<thead class="thead-dark">';
        html += '<tr>';
        html += '<th>CANT</th>';
        html += '<th>DESCRIPCION</th>';
        html += '<th>UM</th>';
        html += '<th>PRECIO UNITARIO</th>';
        html += '<th>SUBTOTAL COMI</th>';
        html += '<th>SUBTOTAL DESC</th>';
        html += '<th>SUBTOTAL</th>';
        html += '<th>OPCIONES</th>';
        html += '</tr>';
        html += '</thead>';
        html += '<tbody id="cuerpoListaProducto">';
        $.each(data, function (obj, item) {
            html += '<tr>';
            html += '<td>' + item.cantidad + '</td>';
            html += '<td>' + item.nombreproducto + '</td>';
            if (item.subproducto =='SI') {
                html += '<td>' + item.nombresubunidad + '</td>';
            } else {
                html += '<td>' + item.unidadmedida + '</td>';
            }
            html += '<td>' + item.preciounitario + '</td>';
            html += '<td>' + item.comision + '</td>';
            html += '<td>' + item.descuento + '</td>';
            html += '<td>' + item.total + '</td>';
            if (data.length > 1) {
                html += '<td class="text-center text-light">';
                html += '<a class="btn-sm btn-danger" title="Eliminar este producto" onclick="eliminarProducto(' + item.iidproducto + ',' + id + ',' + item.iidcotizacion + ')"><i class="fas fa-trash"></i></a>';
                html += '</td>';
            }
            html += '</tr>';
            comi = comi + item.comision; desc = desc + item.descuento; total = total + item.total;
        });
        html += '<tbody>';
        html += '</table>';
        $('#tablaListaProductos').html(html);
        $('#listaCotizacion').DataTable({
            pageLength: 2,
            lengthMenu: [2, 4, 6, 8],
            language: idiomaTabla
        });
        document.getElementById('tdDescuento').innerHTML = '<label>Descuento: $' + desc.toFixed(4) + '</label>';
        document.getElementById('tdComision').innerHTML = '<label>Comision: $' + comi.toFixed(4) + '</label>';
        document.getElementById('tdTotal').innerHTML = '<label>Total a pagar: $' + total.toFixed(4) + '</label>';
    });
}
/*verificamos el tipo de comprador*/
document.getElementById('tipocomprador').onchange = function () {
    var tipo = document.getElementById('tipocomprador').value;
    var inputP = document.getElementsByClassName('n');
    for (var i = 0; i < inputP.length; i++) {
        if (tipo == 'CREDITO FISCAL') {
            inputP[i].classList.add('r');
            document.getElementById('buscadorCliente').style.display = 'block';
        } else {
            inputP[i].classList.remove('r');
            inputP[i].style.borderColor = '#ccc';
            document.getElementById('buscadorCliente').style.display = 'none';
            clearData();
            document.getElementById('tipocomprador').value = 'CLIENTE FINAL';
        }
    }
}

//valida que los campos no esten vacioa
function validateEmpty() {
    var rpt = true;
    var input = document.getElementsByClassName('r');
    for (var i = 0; i < input.length; i++) {
        if (input[i].value.trim() == '') {
            input[i].style.borderColor = 'red'; rpt = false;
        } else {
            input[i].style.borderColor = '#ccc';
        }
    }
    return rpt;
}
/*funcion que nos permite crear la factura y el pedido */
function saveFactura() {
    if ($('#tipocomprador').val() != '') {
        document.getElementById('tipocomprador').style.borderColor = '#ccc';
        if (validateEmpty()) {
            var frm = new FormData();
            frm.append('Tipocomprador', $('#tipocomprador').val().toUpperCase());
            frm.append('Nombrecliente', $('#nombrecliente').val().toUpperCase());
            frm.append('Direccion', $('#direccion').val().toUpperCase());
            frm.append('Registro', $('#registro').val().toUpperCase());
            frm.append('Giro', $('#giro').val().toUpperCase());
            frm.append('Nit', $('#nit').val());
            frm.append('iidcliente', $('#iidcliente').val());
            frm.append('Bhabilitado', 'A');
            frm.append('iidCotizacion', $('#cotizacion').val());
            $.ajax({
                url: '/cotizacionPendiente/confirmVenta',
                type: 'POST',
                contentType: false,
                processData: false,
                data: frm,
                success: function (e) {
                    if (e == 'ok') {
                        Swal.fire({
                            title: 'Exito',
                            text: 'Venta creada',
                            icon: 'success',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                location.href = '/cotizacionPendiente/index';
                            } else {
                                location.href = '/cotizacionPendiente/index';
                            }
                        })
                    } else if (e == 'finalFactura') {
                        messeges('error', 'Se ha superado el limite de facturas establecido en el sistema');
                    } else if (e == 'finalCredito') {
                        messeges('error', 'Se ha superado el limite de comprobantes de credito fiscal establecido en el sistema');
                    } else {
                        messeges('error',e);
                    }
                }
            });
        } else {
            messeges('error','Complete los campos marcados');
        }//validacion de campos vacios
    } else {
        messeges('error','Selecciona un tipo de comprador');
        document.getElementById('tipocomprador').style.borderColor = 'red';
    }//validacion de tipo comprador
}

function clearData() {
    var dataVista = document.getElementsByClassName("form-control");//recoje todos los que tengan la clase data
    for (var i = 0; i < dataVista.length; i++) {//itera todos los inputs
        if (dataVista[i].name != 'total') {
            dataVista[i].value = "";
            dataVista[i].style.borderColor = "#ccc";
        }
    }
}
//elimina el producto de la lista de cotizacion
function eliminarProducto(id, idProducto, idCotizacion) {
    Swal.fire({
        title: 'Estas por eliminar el producto',
        text: '¿Estas seguro que deseas eliminar este producto?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si! eliminar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get('/cotizacionPendiente/eliminarProductoDeLaLista?id=' + id + '&idCotizacion=' + idCotizacion, function (data) {
                if (data) {
                    messeges('success', 'Producto eliminado de la lista');
                    verListaDeProductos(idProducto);
                } else {
                    messeges('error', 'Error al eliminar el producto');
                }
            });
        }
    })
}
//muestra la lista de cliente fiscal
function abrirModalCliente() {
    $.get('/cliente/listCliente', function (data) {
        var html = '';
        html += '<table class="table table-hover table-responsive-md table-responsive-sm" id="tablecliente">';
        html += '<thead class="thead-dark">';
        html += '<tr>';
        html += '<th>NOMBRE</th>';
        html += '<th>REGISTRO</th>';
        html += '<th>NIT</th>';
        html += '<th>OPERACIONES</th>';
        html += '</tr>';
        html += '</thead>';
        html += '<tbody>';
        $.each(data, function (objeto, propiedad) {
            html += '<tr>';
            html += '<td>' + propiedad.nombrecompleto + '</td>';
            html += '<td>' + propiedad.registro + '</td>';
            html += '<td>' + propiedad.nit + '</td>';
            html += '<td>';
            html += '<a class="btn-sm btn-primary" href="#" onclick="getCliente(' + propiedad.iidcliente + ')">seleccionar <i class="fas fa-check"></i></a> ';
            html += '</td>';
            html += '</tr>';
        });
        html += '</tbody>';
        html += '</table>';
        $('#tablaClienteFiscal').html(html);
        $("#tablecliente").DataTable({
            pageLength: 5,
            lengthMenu: [5, 10, 15, 20],
            language: idiomaTabla
        });
    });
}
//completa el formulario del cliente 
function getCliente(id) {
    $.get('/cliente/getClienteById?id=' + id, function (data) {
        $('#iidcliente').val(data.iidcliente);
        $('#nombrecliente').val(data.nombrecompleto);
        $('#direccion').val(data.direccion);
        $('#registro').val(data.registro);
        $('#giro').val(data.giro);
        $('#nit').val(data.nit);
    });
    document.getElementById('btnCerrarModalCliente').click();//cerramos el modal
}
/*funcion que pinta la tabla en la modal de productos */
function abrirModalProducto() {
    $.get('/producto/listProducto', function (data) {
        var html = '';
        html += '<table class="table table-hover table-bordered table-responsive-md table-responsive-sm" id="tableproduct">';
        html += '<thead class="thead-dark">';
        html += '<tr>';
        html += '<th>CODIGO</th>';
        html += '<th>PRODUCTO</th>';
        html += '<th>STOCK</th>';
        html += '<th>EXISTENCIAS</th>';
        html += '<th>PRECIO</th>';
        html += '<th>SUBEXISTENCIAS</th>';
        html += '<th>SUBPRECIO</th>';
        html += '<th class="text-center">OPCIONES</th>';
        html += '</tr>';
        html += '</thead>';
        html += '<tbody>';
        $.each(data, function (objeto, propiedad) {
            var existencia = propiedad.existencias * 1;
            var subexistencia = propiedad.subexistencia * 1;
            var subprecio = propiedad.subprecioventa * 1;
            html += '<tr>';
            html += '<td>' + propiedad.codigoproducto + '</td>';
            html += '<td>' + propiedad.descripcion + '</td>';
            html += '<td>' + propiedad.nombrestock + '</td>';
            html += '<td>' + propiedad.existencias + '</td>';
            html += '<td>' + propiedad.precioventa + '</td>';
            if (subexistencia > 0 && subexistencia != null)//si subexistencias es diferente de null
                html += '<td>' + subexistencia + '</td>';
            else//si es null
                html += '<td>NO TIENE</td>';

            if (subprecio > 0 && subprecio != null)//si subprecio es diferente de null
                html += '<td>' + subprecio + '</td>';
            else//si es null
                html += '<td>NO TIENE</td>';
            /*botonera de opciones*/
            html += '<td class="text-center">';
            if (existencia > 1) {//si es mayor a 1 se puede vender
                html += '<a title="Selecciona el producto con el precio original" class="btn-sm btn-primary" href="#" onclick="getProducto(' + propiedad.iidproducto + ')">' + propiedad.nombreunidad + ' <i class="fas fa-check"></i></a> ';
                if (subexistencia > 0 && subexistencia != null) {//si la sub existencia existe podemos vender original y sub producto
                    html += '<a title="Selecciona el producto con el subprecio" class="btn-sm btn-success" href="#" onclick="getSubProducto(' + propiedad.iidproducto + ')">' + propiedad.nombresubunidad + ' <i class="fas fa-check"></i></a> ';
                }
            }
            else
                html += '<a title="Este producto no cuenta con existencias" class="btn-sm btn-danger" href="#" onclick="mensajeError()">Bloqueado <i class="fas fa-check"></i></a> ';

            html += '</td>';
            html += '</tr>';
        });
        html += '</tbody>';
        html += '</table>';
        $('#tablaProducto').html(html);
        $("#tableproduct").DataTable({
            pageLength: 5,
            lengthMenu: [5, 10, 15, 20],
            language: idiomaTabla
        });
    });
}

/*obtiene el producto original*/
function getProducto(id) {
    /**
    * llena el formulario donde se agrega el descuento cantidad y comision
    * @param {any} id//se solicita el id del producto
    */
    $.get('/producto/getProductoById?id=' + id, function (data) {
        $('#txtExistencias').val(data.existencias);
        $('#txtPrecioUnitario').val(data.precioventa);
        $('#iidproducto').val(data.iidproducto);
        $('#subunidad').val(0);
        $.get('/producto/ObtenerNombreUnidad?id=' + data.iidunidadmedida, function (nombreunidad) {
            $('#txtProducto').val('[' + nombreunidad + '] ' + data.descripcion);
        });
        calculateDiscount();//calculamos
    });
    document.getElementById('btnCerrarProducto').click();//cerramos el modal
}
/*obtiene el producto original*/
function getSubProducto(id) {
    $.get('/producto/getProductoById?id=' + id, function (data) {
        $.get('/producto/ObtenerNombreUnidad?id=' + data.subunidad, function (nombreunidad) {
            $('#txtProducto').val('[' + nombreunidad + '] ' + data.descripcion);
        });
        $('#txtExistencias').val(data.subexistencia);
        $('#txtPrecioUnitario').val(data.subprecioventa);
        $('#iidproducto').val(data.iidproducto);
        $('#subunidad').val(data.subunidad);
        calculateDiscount();//calculamos
    });
    document.getElementById('btnCerrarProducto').click();//cerramos el modal
}
//hace el calculo de la comision y el descuento
function calculateDiscount() {
    var txtDescuento = document.getElementById('txtDescuento').value * 1;
    var txtComision = document.getElementById('txtComision').value * 1;
    var precioUnitario = document.getElementById('txtPrecioUnitario').value * 1;
    var txtCantidad = document.getElementById('txtCantidad').value * 1;
    var tcomision = 0; var tdescuento = 0; var comision = 0; var descuento = 0;
    //calculos
    if (txtDescuento != "" || txtComision != "" || txtCantidad != "") {
        //obtener comision
        tcomision = ((parseFloat(precioUnitario) / 100) * txtComision);//se obtiene la comision unitaria
        comision = tcomision * txtCantidad;//se multiplica por la cantidad de producto que se llevara
        document.getElementById('txtTcomision').value = comision.toFixed(4);//se muestra el total
        //obtener descuento
        if (tcomision > 0) {//si se le a aplicado comision
            var precioUnitarioConComision = precioUnitario + tcomision;//se suma el precio unitario + el total de la comison
            tdescuento = ((precioUnitarioConComision / 100) * txtDescuento);//se le saca el descuento al resultado anterior
            descuento = tdescuento * txtCantidad;//se multiplica por la cantidad de producto que se llevara
            document.getElementById('txtTdescuento').value = descuento.toFixed(4);//se muestra el total
        } else {//si no tiene comision
            tdescuento = ((parseFloat(precioUnitario) / 100) * txtDescuento);//se le saca el descuento solo al precio unitario
            descuento = tdescuento * txtCantidad;//se multiplica por la cantidad de producto que se llevara
            document.getElementById('txtTdescuento').value = descuento.toFixed(4);//se muestra el total
        }
        //dar el total
        var totalPrecioUnitario = precioUnitario * txtCantidad;//se obtiene el total de la compra 
        var totalVenta = (totalPrecioUnitario + comision) - descuento;//se suman el total de la comision mas el precio unitario y a eso se le descuenta el total de descuento
        document.getElementById('txtTotal').value = totalVenta.toFixed(4);//se muestra el total
    }
}
/*funcion para agregar un producto a la lista de venta*/
function addList() {
    if ($('#iidproducto').val() != "") {
        var cantidad = $('#txtCantidad').val() * 1;
        var existencias = $('#txtExistencias').val() * 1;
        var comision = $('#txtComision').val();
        var descuento = $('#txtDescuento').val();
        var subUnidad = $('#subunidad').val();
        var idCotizacion = $('#txtIIDCOTIZACION').val();
        if (cantidad > 0) {
            if (cantidad < existencias) {
                var frm = new FormData();
                frm.append('Essubproducto', subUnidad);
                frm.append('iiproducto', $('#iidproducto').val());
                frm.append('cantidad', $('#txtCantidad').val());
                frm.append('idCotizacion', idCotizacion);
                if (comision != '')
                    frm.append('comision', comision);
                else
                    frm.append('comision', 0);
                if (descuento != '')
                    frm.append('descuento', descuento);
                else
                    frm.append('descuento', 0);
                $.ajax({
                    url: '/cotizacionPendiente/guardarNuevoProducto',
                    type: 'POST',
                    contentType: false,
                    processData: false,
                    data: frm,
                    success: function (r) {
                        if (r > 0) {
                            messeges('success', 'Producto agregado');
                            verListaDeProductos(idCotizacion);
                            var input = document.getElementsByClassName('form-control');
                            for (var i = 0; i < input.length; i++) {
                                input[i].value = '0';
                            }
                        } else if (r == -1) {
                            messeges('warning', 'Este producto ya esta en la lista.');
                        } else {
                            messeges('error', 'Error en el sistema')
                        }
                    }
                })
            } else {
                messeges('warning', 'Las existencias no pueden quedar a 0');
            }//fin validacio cantidad <= existencias && existencias > 0
        } else {
            messeges('warning', 'Cantidad no puede ser 0.');
        }//fin validacio cantidad > 0
    } else {
        messeges('error', 'No haz seleccionado un producto');
    }//fin validacio $('#iidproducto').val() != ""
}
//mensajes de la vista
function messeges(icons = 'success', titles = 'Exito') {
    Swal.fire({
        position: 'center',
        icon: icons,
        title: titles,
        showConfirmButton: false,
        timer: 1500
    })
}
//mensaje
function messegeConfirm(url, mensaje = 'Elminiar', subtitulo = '¿Estas seguro que deseas eliminar el registro?', tituloBoton = 'Si, eliminar!') {
    Swal.fire({
        title: mensaje,
        text: subtitulo,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: tituloBoton
    }).then((result) => {
        if (result.isConfirmed) {
            deleteData(url);
        }
    })
}