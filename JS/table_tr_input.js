/* Obtener los valores de los solo los textos que estan dentro del table */
$("#Alumnos tr input[type=text]").each(function () {
  var calificacion = $(this).val();
  if (calificacion !== "") {
    //var cal = Folios[i] + "," + calificacion + "," + aprovada;
    var cal = Folios[i] + "," + calificacion;
    calificaciones.push(cal);
    i++;
  } else {
    var calNA = Folios[i] + "," + "--";
    calVacias.push(calNA);
    i++;
  }
});
