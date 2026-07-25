/* Ge valur from td of table based on id="" */

$("#Alumnos tr td").each(function () {
  var calPrepaId = $(this).attr("id");
  if (calPrepaId >= 0) {
    calificacionesIds.push(calPrepaId);
  }
});
