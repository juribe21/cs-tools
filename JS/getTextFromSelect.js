 function SearchButton_Click() {
     var selectedValue = $('#ddlSapCompany').find(":selected").text();
     searchSelectedValue(selectedValue);
 }