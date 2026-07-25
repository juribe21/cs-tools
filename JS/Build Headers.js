/* Data test ↓ */
var header = [
  { MatId: 1, MatName: "Name1" },
  { MatId: 2, MatName: "Name2" },
  { MatId: 3, MatName: "Name3" },
  { MatId: 4, MatName: "Name4" },
  { MatId: 5, MatName: "Name5" },
  { MatId: 6, MatName: "Name6" },
  { MatId: 7, MatName: "Name7" },
  { MatId: 8, MatName: "Name8" },
];

// Buidl header based on array
function builTh() {
  var theader = "";
  $.each(header, function (i) {
    theader += "<th>" + header[i].MatId + "</th>";
    i++;
  });

  createTableHeader(theader);
}

// concat header built with additional columns
function createTableHeader(theader) {
  $("#Alumnos thead").empty();

  $("#Alumnos thead").append(
    "<tr>" +
      "<th>NUM</th>" +
      "<th>NOMBRE DEL ALUMNO</th>" +
      "<th>NUM. CONTROL</th>" +
      theader +
      "</tr>"
  );
}
