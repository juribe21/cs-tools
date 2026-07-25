function cargarMateriasHeader() {
  $.ajax({
    url: UrlGuardaEdicionAlumnosCalificaciones,
    type: "POST",
    //data: JSON.stringify({ "cal": calificaciones, "hImt": headerIMT }),
    data: JSON.stringify({ cal: calificaciones, calsDgeti: calsDgeti }),
    dataType: "JSON",
    contentType: "application/json; charset=utf-8",
    success: function (data) {
      if (data.Guardado) {
        $.msgGrowl({
          type: "success",
          title: data.title,
          text: data.mensaje,
          onClose: function () {
            headerIMT = null;
            calificaciones = null;
            location.reload();
          },
          lifetime: 3500,
        });
      } else {
        $.msgGrowl({
          type: "error",
          title: data.title,
          text: data.mensaje,
        });
      }
    },
  });
}
