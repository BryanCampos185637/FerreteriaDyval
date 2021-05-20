$('#frmReporte').submit(function (e) {
        e.preventDefault();
        createReport();
    })
    function createReport() {
        $.get('/reporte/createListReportVenta?desde=' + $('#desde').val() + '&hasta='+$('#hasta').val(), function (resp) {
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