/* Activar based on class name */
/*  "<td> <input type='text' class='cals' onkeyup='javascript: leaveEnter();' id='txtCalificacion' style='width:40px; align-items:center;' value='" + data[i].Calificacion + "' /> </td>" +*/

$(".cals").each(function () {
  $(".cals").attr("readonly", false);
  $(".cals").show();
});
