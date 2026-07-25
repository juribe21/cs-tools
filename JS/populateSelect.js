 function callLabIdWebService() {
     $("#SapCode").attr('checked', false);
     selectedOption = "labId"
     $.ajax({
         type: "POST",
         async: false,
         url: "RxLabConfig.aspx/LabID_CheckedChanged",
         //data: JSON.stringify({ param1: "value1", param2: 123 }),
         data: '{param1: "' + "service" + '", param2: "' + "123" + '" }',
         contentType: "application/json; charset=utf-8",
         dataType: "JSON",
         success: function (response) {
             
             var jsonData = response;
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
             alert("Error callWebService ");
         }
     });
 }


  function SearchButton_Click() {
     var selectedValue = $('#ddlSapCompany').find(":selected").text();
     searchSelectedValue(selectedValue);
 }