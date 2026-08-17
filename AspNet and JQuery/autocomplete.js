/* Autocomplete */

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