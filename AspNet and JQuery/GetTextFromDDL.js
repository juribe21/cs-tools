/* Get text from aspNet element */

function SearchButton_Click() {
    var selectedValue = $('#ddlSapCompany').find(":selected").text();
    searchSelectedValue(selectedValue);
}

function SearchButton_Click() {
    var concepValue = $('#ddlSapCompany').find(":selected").val();
    searchSelectedValue(concepValue);
}

