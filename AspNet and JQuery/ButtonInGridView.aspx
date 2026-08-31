<!-- [1] Add → EnableEventValidation="false" on the top of the page -->
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Global/QcPageTemplate.master" EnableEventValidation="false" CodeBehind="CsOrderCret.aspx.vb" Inherits=".CsOrderCret" %>

<!-- [2] template of the button -->
<Columns>
    <asp:TemplateField ShowHeader="False">
        <ItemTemplate>
            <asp:Button ID="btnRxNumberDetail" OnClick="btnRxNumberDetails" Text="Credit Order" CssClass="ButtonText"
                runat="server" />
        </ItemTemplate>
    </asp:TemplateField>

    <asp:BoundField ReadOnly="true" DataField="OrderService" HeaderText="Order Service"></asp:BoundField>
    <asp:BoundField ReadOnly="true" DataField="OrderTrackingID" HeaderText="Rx Number"></asp:BoundField>
</Columns>

<!-- [3] code behind -->
 Protected Sub btnRxNumberDetails(ByVal sender As Object, ByVal e As EventArgs)
     'Get the button that raised the event
     Dim btn As Button = CType(sender, Button)

     'Get the row that contains this button
     Dim grv As GridViewRow = CType(btn.NamingContainer, GridViewRow)

     Dim rxNumber As String = grv.Cells(2).Text
     Response.Redirect("~/CustomerService/Credits/RxOrderCredit.aspx?OrderID=" & rxNumber)
 End Sub