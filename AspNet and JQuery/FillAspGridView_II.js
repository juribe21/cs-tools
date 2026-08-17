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
    
    function OnSuccess(r) {
        //Parse the XML and extract the records.
        var jdata = r.d; // $($.jsonData(r.d)).find("Table");

        //Reference GridView Table.
        var table = $("[id*=GridView1]");

        //Reference the Dummy Row.
        var row = table.find("tr:last-child").clone(true);

        //Remove the Dummy Row.   
        $("tr", table).not($("tr:first-child", table)).remove();

        //Loop through the XML and add Rows to the Table.
       $.each(customers, function () {
            var customer = $(this);
            $("td", row).eq(0).html($(this).find("CustomerID").text());
            $("td", row).eq(1).html($(this).find("ContactName").text());
            $("td", row).eq(2).html($(this).find("Country").text());
            table.append(row);
            row = table.find("tr:last-child").clone(true);
        });
        
    }
