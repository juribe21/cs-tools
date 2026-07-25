function getSelectID() {
  $("#selGrupoPrepa").change(function () {
    calsDgeti.GrupoId = $(this).children(":selected").attr("id");
  });
}

function seleccionaGrupo() {
  var e = document.getElementById("selGrupoPrepa");
  calsDgeti.FolioGrupoId = e.options[e.selectedIndex].value;
  // ↓ ↓
  calsDgeti.GrupoId = $("#selGrupoPrepa option:selected").attr("id"); // ← ←
}

// Option 2
var conceptName = $('#aioConceptName').find(":selected").val();
