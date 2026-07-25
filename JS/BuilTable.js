function cargaListaGrupoDgeti () {

    $.ajax({
        url: UrlCargaListaGrupoDgeti,
        type: "POST",
        data: JSON.stringify({ "grupoId": grupoDgeti, "gradoid": semestre  }),
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jdata = data._listaEva;
            var ndms = 0;
            if (data.Regreso) {
                $.msgGrowl({
                    type: 'success',
                    title: 'Reporte EVA Semestral',
                    text: 'Cargando calificaciones y datos de Reporte',
                    lifetime: 1200,
                });
                var i = 0;
                $("#Alumnos tbody").empty();
                $.each(jdata, function (i) {

                    $("#Alumnos tbody").append(
                        "<tr id='" + jdata[i].Num + "'>" +
                        //"<td style='display:none;' scope='row'>" + jdata[i].NoFolio + "</td>" +
                        "<td scope='row'>" + jdata[i].Num + "</td>" +
                        "<td scope='row'>" + jdata[i].Nombre.toUpperCase() + "</td>" +
                        "<td scope='row'>" + jdata[i].NoControl.toUpperCase() + "</td>" +
                        "<td scope='row'>" + jdata[i].Materia1.toFixed(ndms) + "</td>" +
                        "<td scope='row'>" + jdata[i].Materia2.toFixed(ndms) + "</td>" +
                        "<td scope='row'>" + jdata[i].Materia3.toFixed(ndms) + "</td>" +
                        "<td scope='row'>" + jdata[i].Materia4.toFixed(ndms) + "</td>" +
                        "<td scope='row'>" + jdata[i].Materia5.toFixed(ndms) + "</td>" +
                        "<td scope='row'>" + jdata[i].Materia6.toFixed(ndms) + "</td>" +
                        "<td scope='row'>" + jdata[i].Materia7.toFixed(ndms) + "</td>" +
                        "<td scope='row'>" + jdata[i].Materia8.toFixed(ndms) + "</td>" +
                        "</tr>");
                    i++;
                })
                /* ------------------ */
            }
            else {
                $.msgGrowl({
                    type: 'info',
                    title: 'Sistema IMT DGETI',
                    onClose: function () { $("#Alumnos tbody").empty(); },
                    text: "Grupo seleccionado NO tiene grupo DGETI asignado",
                    lifetime: 3500,
                });
            }


        },
    })
}
