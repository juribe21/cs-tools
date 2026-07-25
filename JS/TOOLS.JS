/* To check/uncheck a checkbox, use the attribute checked and alter that. With jQuery you can do: */

$("#myCheckbox").attr("checked", true); // Checks it
$("#myCheckbox").attr("checked", false); // Unchecks it

/*.attr() is deprecated for properties; use the new .prop() function instead as: */

$("#myCheckbox").prop("checked", true); // Checks it
$("#myCheckbox").prop("checked", false); // Unchecks it

/* ----------------------------------------------------------------------------------------------- */

/* find extension method */
var selectedOption = $('#ddlCountry').find('option[value= "0"]').text(); 

/* ----------------------------------------------------------------------------------------------- */
/* css */
$("#<%= lblMsgShipToUpdated.ClientID %>").css('color', '#0D5692');
$("#btnToUpdated").css('color', '#0D5692');

/* ----------------------------------------------------------------------------------------------- */


/* ----------------------------------------------------------------------------------------------- */
    $.ajax({
        type: "POST",
        url: "AddressBookDetail.aspx/LoadCountries",
        //data: JSON.stringify({ country: shipInformation.ShipToCountry }),
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        success: function (response) {

            var jsonData = response.d;

            var lista = "";
            lista = "<option value=-1>- SELECT -</option>"
            var idx = -1;
            for (i in jsonData) {
                //lista += "<option value='" + idx + "'>" + jsonData[i] + "</option>";
                lista += "<option value='" + jsonData[i].Id + "'>" + jsonData[i].Country + "</option>";
                idx++;
            }
            $("#ddlCountry").html(lista);
        },
        error: function (response) {
            alert(r.d);
        }
    });
}