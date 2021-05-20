﻿
$(function () {
    fillCombo('/stock/listStock', 'iidstock', 'nombrestock', 'cbxStock', false);
    fillCombo('/bodegainventario/listBodega', 'iidbodega', 'nombrebodega', 'cbxBodega', true);
})
$('#frmReporte').submit(function (e) {
        e.preventDefault();
        createReport();
})
function createReport() {
    $.get('/reporte/createListReportVenta?desde=' + $('#desde').val() + '&hasta=' + $('#hasta').val(), function (resp) {
        if (resp > 0) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Creando reporte...',
                showConfirmButton: false,
                timer: 1500
            })
            document.getElementById('btnReporte').click();
        } else {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Error al crear el reporte.',
                showConfirmButton: false,
                timer: 1500
            })
        }
    })
}

$('#frmReporteInventario').submit(function (e) {
    e.preventDefault();
    createReporteInventario();
})

function createReporteInventario() {
    var frm = new FormData();
    frm.append('Iidbodega', $('#cbxBodega').val());
    frm.append('nombrestock', $('#cbxStock').val());
    $.ajax({
        url: '/reporte/crearCookieReporteInventario',
        type: 'POST',
        contentType: false,
        processData: false,
        data: frm,
        success: function (resp) {
            if (resp == 'ok') {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Creando reporte...',
                    showConfirmButton: false,
                    timer: 2000
                })
                document.getElementById('btnReporteInventario').click();
            } else {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'Error al crear el reporte.<br>' + resp,
                    showConfirmButton: false,
                    timer: 2000
                })
            }
        }
    })
}

function fillCombo(link, value, text, idSelect, esBodega) {
    $.get(link, function (data) {
        if (data != null || data != "") {
            var html = '<option value="">--seleccione una opcion--</option>';
            if (esBodega) { html += '<option value="-1">SALA DE VENTA</option>'; }
            for (var i = 0; i < data.length; i++) {
                var objectCurret = data[i];
                html += '<option value="' + objectCurret[value] + '">' + objectCurret[text] + '</option>';
            }
            $('#' + idSelect).html(html);
        } else {
            var html = '<option value="">--No hay registros--</option>';
            $('#' + idSelect).html(html);
        }
    })
}