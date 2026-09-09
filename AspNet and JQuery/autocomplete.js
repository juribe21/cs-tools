/* Autocomplete */
var availableTags = "";
var CustomerAccounts = ['<%=String.Join("', '", ArrayAccountNumber) %>'];
availableTags = CustomerAccounts;

$("#inputCustomerName").autocomplete({

    source: function (request, response) {
        var results = $.ui.autocomplete.filter(availableTags, request.term);
        response(results.slice(0, 25));
    },
    select: function (event, ui) {
        var terms = split(this.value);
        terms.pop();

        terms.push((ui.item.value).replace("&", "%26"));

        this.value = (ui.item.value).replace("&", "%26")
        $("#o_Reference").val(terms);

        GetCustomerInfo("", terms);

        return false;
    }
})

$("#inputCustomerName").focus();



/* ***** Autocomplete with ASP Control ***** */ txtFrameName

var availableSapCompanies = "";
availableSapCompanies = ['<%= string.Join("', '", ArraySapCompaniesCodes) %>']; 
OR
availableTags = availableSapCompanies;

function FillTextValues() {
    // Reset to values to empty
    $("#<%= txtAccountNumber.ClientID %>").val('');

    FillTxtAccountNumber();
    CreateControlsEventsAccounts(); //  function ???
}

function FilltxtFrameNames() {
    $("#<%= txtAccountNumber.ClientID %>").autocomplete({
        source: function (request, response) {
            var results = $.ui.autocomplete.filter(availableSapCompanies, request.term);
            response(results.slice(0, 25));
        },
        select: function (event, ui) {
            var terms = split(this.value);                  
            terms.pop();                  

            terms.push((ui.item.value).replace("&", "%26"));

            // add placeholder to get the comma-and-space at the end
            this.value = (ui.item.value).replace("&", "%26")                   
            return false;
        }
    })
}

function CreateControlsEventsAccounts() {
    $("#<%= txtAccountNumber.ClientID %>").on("click", function () {
        $(this).select();
    });
}