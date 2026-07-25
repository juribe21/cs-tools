/* 
    Build table based on List
    Method buildCalificaciones(data, i) construye fila de calificaciones
    se pasa la data y el contador
*/

function cargaListaGrupoDgeti() {
  if (semestre == 0 || grupoDgeti == 0) {
    return;
  }
  $.ajax({
    url: UrlCargaListaGrupoDgeti,
    type: "POST",
    data: JSON.stringify({ grupoId: grupoDgeti, gradoid: semestre }),
    dataType: "JSON",
    contentType: "application/json; charset=utf-8",
    success: function (data) {
      var jdata = data.listas;
      if (data.Regreso) {
        $.msgGrowl({
          type: "success",
          title: "Reporte EVA Semestral",
          text: "Cargando calificaciones y datos de Reporte",
          lifetime: 1200,
        });
        var i = 0;
        $("#Alumnos tbody").empty();
        $.each(jdata, function (i) {
          $("#Alumnos tbody").append(
            "<tr id='" +
              jdata[i].Num +
              "'>" +
              //"<td style='display:none;' scope='row'>" + jdata[i].NoFolio + "</td>" +
              "<td scope='row'>" +
              jdata[i].Num +
              "</td>" +
              "<td scope='row'>" +
              jdata[i].Nombre.toUpperCase() +
              "</td>" +
              "<td scope='row'>" +
              jdata[i].NoControl.toUpperCase() +
              "</td>" +
              buildCalificaciones(data, i) +
              "</tr>"
          );
          i++;
        });
      } else {
        $.msgGrowl({
          type: "info",
          title: "Sistema IMT DGETI",
          onClose: function () {
            $("#Alumnos tbody").empty();
          },
          text: "Grupo seleccionado NO tiene grupo DGETI asignado",
          lifetime: 3500,
        });
      }
    },
  });
}

function buildCalificaciones(data, ii) {
  var calificaciones = "";
  var calsLength = data[ii].Claves.length;
  var cals = data[ii].Claves;
  for (var i = ii; i <= ii; i++) {
    for (var j = 0; j < calsLength; j++) {
      calificaciones += "<td scope='row'>" + cals[j].Calificacion + "</td>";
    }
  }
  return calificaciones;
}
