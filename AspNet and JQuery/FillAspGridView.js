

function searchSelectedValue(selectedValue) {     
    var rowClass = 'normal'       
    $.ajax({
        type: "POST",
        async: false,
        url: "RxLabConfig.aspx/SearchButton_Click",
        //data: JSON.stringify({ param1: "value1", param2: 123 }),
        data: '{value: "' + selectedValue + '", selectedOption: "' + selectedOption + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            var jdata = response.d; /* Always check response */
            $("#<%=GridView1.ClientID%> tr:has(td)").remove(); // Remove rows
            
            $.each(jdata, function (i) {
                $('#<%= GridView1.ClientID %> tbody').append( // Adding new filtered rows        
                    "<tr class=" + rowClass + ">" +
                        '<td>' + jdata[i].RegionCountry + '</td>' +
                        '<td>' + jdata[i].ColorCode + '</td>' +
                        '<td>' + jdata[i].ColorDesc + '</td>' +
                        '<td>' + jdata[i].Treatment + '</td>' +
                        '<td>' + jdata[i].LensColor + '</td>' +
                        '<td>' + jdata[i].MaterialClass + '</td>' +
                        '<td>' + jdata[i].LensColorID + '</td>' +
                        '<td>' + jdata[i].LensColorDesc + '</td>' +                            
                        '<td>' + jdata[i].LensColorClass + '</td>' +
                    "</tr>"
                );
                i++;
                if (i % 2 > 0) {
                rowClass = 'alternate'
                }
                else {
                rowClass = 'normal'
                }
            });

        },
        error: function (xhr, status, error) {
            alert("Error on WebService");
        }
    });
}