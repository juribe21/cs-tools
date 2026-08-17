 /*
    Call web service and fill Select
 */
 
 function callSapCodeWebService() {
     $("#LabID").attr('checked', false);
     selectedOption = "sap"
     $.ajax({
         type: "POST",
         async: false,
         url: "RxLabConfig.aspx/SapCode_CheckedChanged",
         data: JSON.stringify({ param1: "value1", param2: 123 }),
         //data: '{param1: "' + "service" + '", param2: "' + "123" + '" }',
         contentType: "application/json; charset=utf-8",
         dataType: "JSON",
         success: function (response) {

             var jsonData = response;

             /* fill Select */
             var lista = "";
             lista = "<option value=0>SELECT</option>"
             var idx = 0;
             for (i in jsonData.d) {
                 lista += "<option value='" + idx + "'>" + jsonData.d[i] + "</option>";
                 idx++;
             }
             $("#ddlSapCompany").html(lista);

         },
         error: function (xhr, status, error) {
             alert("Error on WebService ");
         }
     });
 }

/* html elements */
// Create an HTML element [radio, button, select] and on event onclick call JS function
 /* 
    <input type="radio" id="SapCode" onclick="callSapCodeWebService()" style="margin-right:10px;" />Sap Code       
*/

/* SELECT */
/*
    <select id="ddlSapCompany" style="width:90px; border-radius:3px;" onchange="SearchButton_Click()" ></select>
*/

function callSapCodeWebService() {
     
     $.ajax({
         type: "POST",
         async: false,
         url: "RxLabConfig.aspx/SapCode_CheckedChanged",
         data: JSON.stringify({ param1: "value1", param2: 123 }),         
         contentType: "application/json; charset=utf-8",
         dataType: "JSON",
         success: function (response) {

             var jsonData = response.d;
             /* fill textbox */            
            //  $("#ddlSapCompany").html(lista);

         },
         error: function (xhr, status, error) {
             alert("Error on WebService ");
         }
     });
 }


     function loadSelectedValueII(selectedOption) {
        $.ajax({
            type: "POST",
            url: "CzvReduceProducts.aspx/LoadReduceProductMasterList",
            data: JSON.stringify({ value: selectedOption, selectedOption: selectedCriteria }), 
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",
            success: OnSuccess,
            failure: function (r) {
                alert(r.d);
            },
            error: function (response) {
                alert(r.d);
            }
        });
    }


     function GetCountryCode() {
     var countryName = $("#<%= txtCountry.ClientID %>").val(); // GET Value
     $.ajax({
         type: "POST",
         url: "AddressBookDetail.aspx/LoadCountryCode",
         data: JSON.stringify({ countryName: countryName }),
         contentType: "application/json; charset=utf-8",
         dataType: "JSON",
         success: function (response) {

             var jsonData = response.d;
             $("#<%= txtShipToCountryCode.ClientID %>").val(jsonData); // SET Value
             /* fill textbox */
             //  $("#ddlSapCompany").html(lista);

         },
         error: function (xhr, status, error) {
             alert("Error on WebService ");
         }
     });
 }


 function SaveButtonSaveAddress() {

    $("#<%= lblMsgShipToUpdated.ClientID %>").text(""); // clear

    if (ValidateFields(false)) {

        $.ajax({
            type: "POST",
            url: "AddressBookDetail.aspx/SaveAddress",
            data: JSON.stringify({ shipInformation: shipInformation }),
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",
            success: function (response) {

                var jsonData = response.d;
                if (jsonData.IsError) {

                    $("#<%= lblMsgShipToUpdated.ClientID %>").css('color', 'red'); // css
                    $("#<%= lblMsgShipToUpdated.ClientID %>").text(jsonData.lblMsgShipToUpdated); // set text
                    ValidateFields(jsonData.PhoneFormat, jsonData.EmailFormat);                   
                }
                else {
                    $("#<%= lblAccountHeader.ClientID %>").text(jsonData.lblAccountHeader);
                    $("#<%= lblMsgShipToUpdated.ClientID %>").css('color', '#0D5692');
                    $("#<%= lblMsgShipToUpdated.ClientID %>").text(jsonData.lblMsgShipToUpdated);
                    $("#btnCancel").attr("value", "Back");
                    //disableDiv();
                }
            },
            error: function (response) {
                alert(r.d);
            }
        });
    }
    else {
        return;
    }
}