function FillLabIdParametersddl() {

    $("#ddlLabIdParameters").empty();

    $.ajax({
        type: "POST",
        url: "EntAdminHome.aspx/FillddlLabIdParameters",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $("#ddlLabIdParameters").append($("<option     />").val("select").text("Select"));
            $("#ddlLabIdParameters").val("Select");
            $.each(data.d, function () {
                $("#ddlLabIdParameters").append($("<option     />").val(this.LabId).text(this.Name));
            });

        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}