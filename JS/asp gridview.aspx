<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

<asp:GridView ID="CustomersGridView" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="CustomerID" HeaderText="ID" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="City" HeaderText="City" />
    </Columns>
</asp:GridView>

<script type="text/javascript">
    $(document).ready(function () {
        $.ajax({
            type: "POST",
            url: "MyPage.aspx/GetCustomerData", // Replace MyPage with your actual page name
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var customers = JSON.parse(response.d); // Parse the JSON string
                var gridView = $("#<%= CustomersGridView.ClientID %>");
                var tbody = gridView.find("tbody");
                tbody.empty(); // Clear existing rows

                $.each(customers, function (index, customer) {
                    var row = "<tr>" +
                        "<td>" + customer.CustomerID + "</td>" +
                        "<td>" + customer.Name + "</td>" +
                        "<td>" + customer.City + "</td>" +
                        "</tr>";
                    tbody.append(row);
                });
            },
            error: function (xhr, status, error) {
                console.error("AJAX Error:", error);
            }
        });
    });
</script>
