
function sendDataController(link, data, errorMessage = 'Registro ya existe',error2='El nombre usuario ya esta en uso') {
    $.ajax({
        url: link,
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (respuesta) {
            if (respuesta > 0) {
                messeges('success' ,"exito");
                document.getElementById("btnCerrar").click();
                callTable();
                clearData();
            } else if (respuesta == -1) {
                messeges('warning',errorMessage);
            } else if (respuesta == -2) {
                messeges('warning', error2);
            } else {
                messeges('error',"Error de sistema intente mas tarde");
            }
        }
    })
}

function paintTable(link, headboard, properties, primaryKey, idModal, optionDelete = true, optionEdit = true) {
    $.get(link, function (data) {
        var html = "";
        html += '<table class="table table-hover table-bordered table-responsive-md table-responsive-sm" id="pagination">';
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
                if (objectCurret[propertyCurret] != -1000) {
                    html += '<td>' + objectCurret[propertyCurret] + '</td>';
                } else {
                    html += '<td>NO TIENE</td>';
                }
            }
            if (optionDelete != false || optionEdit != false) {
                html += '<td>';
                if (optionDelete)
                    html += '<a title="Eliminar" class="btn-sm btn-danger" href="#" onclick="deleteInfo(' + objectCurret[primaryKey] + ')"><i class="fas fa-trash"></i></a> ';
                if (optionEdit)
                    html += '<a title="Editar" class="btn-sm btn-success" href="#" data-toggle="modal" onclick="edit(' + objectCurret[primaryKey] + ')" data-target="#' + idModal + '"><i class="fas fa-edit"></i></a>';
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

function validateEmpty() {
    var rpt = true;
    var inputs = document.getElementsByClassName("requerid");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].value.trim() == "") {
            rpt = false;
            inputs[i].style.borderColor = "red";
        } else {
            inputs[i].style.borderColor = "#ccc";
        }
    }
    return rpt;
}

function capturarData(frm) {
    var dataVista = document.getElementsByClassName("data");//recoje todos los que tengan la clase data
    for (var i = 0; i < dataVista.length; i++) {//itera todos los inputs
        if (dataVista[i].name != 'contraseña') {
            frm.append(dataVista[i].name, dataVista[i].value.trim().toUpperCase());// y forma el arreglo
        } else {
            frm.append(dataVista[i].name, dataVista[i].value.trim());// y forma el arreglo
        }
    }
    frm.append("bhabilitado", 'A');
}

function clearData() {
    var dataVista = document.getElementsByClassName("form-control");//recoje todos los que tengan la clase data
    for (var i = 0; i < dataVista.length; i++) {//itera todos los inputs
        dataVista[i].value = "";
        dataVista[i].style.borderColor = "#ccc";
    }
}

function colorDefault() {
    var dataVista = document.getElementsByClassName("form-control");//recoje todos los que tengan la clase data
    for (var i = 0; i < dataVista.length; i++) {//itera todos los inputs
        dataVista[i].style.borderColor = "#ccc";
    }
}

function fillCombo(link, value, text, idSelect, description = '', information = '--Selecciona una opcion--') {
    $.get(link, function (data) {
        if (data != null || data != "") {
            var html = '<option value="">' + information + '</option>';
            for (var i = 0; i < data.length; i++) {
                var objectCurret = data[i];
                if (description == '')
                    html += '<option value="' + objectCurret[value] + '">' + objectCurret[text] + '</option>';
                else
                    html += '<option value="' + objectCurret[value] + '">' + objectCurret[text] + '<- ' + objectCurret[description] + '</option>';
            }
            $('#' + idSelect).html(html);
        } else {
            var html = '<option value="">--No hay registros--</option>';
            $('#' + idSelect).html(html);
        }
    })
}

function messeges(icons = 'success', titles = 'Exito') {
    Swal.fire({
        position: 'center',
        icon: icons,
        title: titles,
        showConfirmButton: false,
        timer: 1500
    })
}

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

function getDataById(link, properties) {
    $.get(link, function (data) {
        var dataActual = data;
        for (var i = 0; i < properties.length; i++) {
            $('#' + properties[i]).val(dataActual[properties[i]]);
        }
    });
}

function deleteData(link, errorMessage = '') {
    $.get(link, function (rpt) {
        if (rpt > 0) {
            messeges('success', 'Registro eliminado')
            callTable();
        } else if (rpt == -1) {
            messeges('error', errorMessage)
        }
        else {
            messeges('error', 'Error en el sistema');
        }
    })
}