<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Global/EnterprisePageTemplate.master"
    CodeBehind="EntAdminHome.aspx.vb" Inherits=".EntAdminHome" %>

    <%@ Register Assembly="ComponentArt.Charting.WebChart" Namespace="ComponentArt.Charting" TagPrefix="cc1" %>
        <asp:Content ID="Content1" ContentPlaceHolderID="ChildContent1" runat="server">
            <script src="../../Scripts/js_ConnectShip_v2.js"></script>
            <style>
                .customers {
                    font-family: Arial, Helvetica, sans-serif;
                    border-collapse: collapse;
                    width: 100%;
                }

                .customers td,
                .customers th {
                    border: 1px solid #ddd;
                    padding: 2px;
                }

                .customers tr:nth-child(even) {
                    background-color: #f2f2f2;
                }

                .customers tr:hover {
                    background-color: #ddd;
                }

                .customers th {
                    padding-top: 5px;
                    padding-bottom: 5px;
                    text-align: left;
                    background-color: #105494;
                    color: white;
                }
            </style>
            <script type="text/javascript">
                function ResetValues() {
                    $("#RadioCustomerAll").prop("checked", true);
                    $("#CustomerInput").val('');
                    $(".Customer").hide();

                    ChangeRadioStates();
                    CreateControlsEvents();
                }

                function ChangeRadioStates() {

                    $("#RadioSapCustomer").attr('checked', 'checked');


                    RadioSelected();
                    $("#CustomerInput").val(Value);
                }


                function RadioSelected() {
                    FilterType = $('input[name="CustomerRadio"]:checked').val()


                    availableTags = CustomerSapValues;
                    $("#CustomerLabel").text('Customer Account:');


                    InputFocus();
                    FillTextBox();
                }


                function InputFocus() {
                    $(".Customer").show();
                    $("#CustomerInput").val('');
                    //$("#CustomerInput").click();
                }

                function FillTextBox() {
                    $("#CustomerInput").autocomplete({
                        source: function (request, response) {
                            console.log('source')
                            var results = $.ui.autocomplete.filter(availableTags, request.term);
                            response(results.slice(0, 25));
                        },
                        select: function (event, ui) {
                            console.log('select')
                            var terms = split(this.value);
                            //alert(terms);
                            // remove the current input
                            terms.pop();
                            // add the selected item
                            //(ui.item.value).replace("&", "%26");
                            //alert((ui.item.value).replace("&", "%26"));
                            //terms.push((ui.item.value).replace("&", "%26"));

                            terms.push((ui.item.value).replace("&", "%26"));

                            // add placeholder to get the comma-and-space at the end
                            this.value = (ui.item.value).replace("&", "%26")
                            //alert(this.value);
                            //SendNewMessage()
                            return false;
                        }
                    })
                }

                function split(val) {
                    return val.split(/,\s*/);
                }

                function extractLast(term) {
                    return split(term).pop();
                }

                function CreateControlsEvents() {
                    // select all the text when you click on the element
                    $("#CustomerInput").on("click", function () {
                        $(this).select();
                    });

                    // customerRadio change function
                    $("input[name=CustomerRadio]").change(function () {
                        RadioSelected();
                    });
                }



                var CustomerSapValues = "";

                $(document).ready(function () {

                    fillRxPortalProcessDDL();

                    CustomerSapValues = ['<%=String.Join("', '", ArrayCustomerSap) %>'];
                    ResetValues();

                });
            </script>
            <asp:Table runat="server" BorderWidth="0" Width="100%" CellPadding="0" CellSpacing="0">
                <asp:TableRow Visible="false">
                    <asp:TableCell>
                        <input type="radio" name="CustomerRadio" id="RadioCustomerAll" value="ALL" /> ALL
                    </asp:TableCell>
                    <asp:TableCell>
                        <input type="radio" name="CustomerRadio" id="RadioCustomerGroup" value="CG" /> Customer Group
                    </asp:TableCell>
                    <asp:TableCell>
                        <input type="radio" name="CustomerRadio" id="RadioCustomerName" value="CA" /> Customer Name
                    </asp:TableCell>
                    <asp:TableCell>
                        <input type="radio" name="CustomerRadio" id="RadioSapCustomer" value="SA" checked="checked" />
                        Customer Account
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow BackColor="#738FBF">
                    <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" CssClass="header" Width="5px">
                        &nbsp;
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" CssClass="HeaderSub1">
                        RxPortal Enterprise Adminstration
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" CssClass="header">
                        &nbsp;
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" CssClass="HeaderSub1">
                        <asp:TextBox ID="RxSearchBox" BorderWidth="1" BorderStyle="Inset" Height="14px" runat="server">
                        </asp:TextBox>
                        <asp:RegularExpressionValidator ID="Validator1" ASPClass="RegularExpressionValidator"
                            ControlToValidate="RxSearchBox" ValidationExpression="[0-9]{2,}" runat="server">
                        </asp:RegularExpressionValidator>
                        <asp:Button ID="SearchButton" OnClick="SearchButton_Click" Text="Find Rx" CssClass="ButtonText"
                            runat="server" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" CssClass="header" Width="5px">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <!-- SAP Messages -->
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4">
                        <div style="position:fixed;margin-left:500px;margin-top:15px">
                            <table class="OrderDetail"
                                style="width: 400px; border: 1px solid; border-collapse: collapse">
                                <tr style="background-color: #1E7B1E; color: white; height: 20px">
                                    <th><b>SAP Messages</b></th>
                                </tr>
                                <tr id="tr_RxProcess"
                                    style="background-color: #B1CDB1; height: 35px; text-align: center">
                                    <td style="margin-top:-5px"><span class="Text3">RxPortal Process:</span>
                                        <select id="ddlPortalProcess" name="ddlPortalProcess"
                                            style="width: 250px; color: black; font-size: 12px; height: 20px">
                                        </select>
                                        <br />
                                        <input type="button" value="Lookup" onclick="goToDetailPage()"
                                            class="ButtonText" style="margin-top: 10px" />
                                    </td>
                                </tr>
                                <tr id="tr_RxNumber"
                                    style="background-color: #B1CDB1; height: 35px; text-align: center; display: none">
                                    <td><span class="Text3">Rx Number:</span>
                                        <input type="text" id="inputRxNumber" />
                                        <input type="button" value="Lookup" onclick="goToDetailPage()"
                                            class="ButtonText" />
                                    </td>
                                </tr>
                                <tr id="tr_Process" style="background-color: #B1CDB1; height: 35px; text-align: center">
                                    <td>
                                        <input type="radio" id="rbProcess" value="RxProcess" checked="checked"
                                            onclick="CheckUncheck('Process')" />RxPortal Process
                                        <input type="radio" id="rbRxNumber" value="RxNumber"
                                            onclick="CheckUncheck('RxNumber')" />Rx Number


                                    </td>

                                </tr>
                            </table>
                        </div>
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Table runat="server">


                            <asp:TableRow>

                                <asp:TableCell>
                                    <asp:GridView ID="GridView1" runat="server" Width="350" CellPadding="2"
                                        CssClass="WipGrid-View"
                                        EmptyDataText="<BR><BR>There are No Active Jobs In This Lab"
                                        AutoGenerateColumns="False" AllowPaging="true" PageSize="25">
                                        <PagerSettings Mode="Numeric" NextPageText="Next" PreviousPageText="Back"
                                            Position="Bottom" PageButtonCount="20" />
                                        <PagerStyle CssClass="pager" />
                                        <RowStyle CssClass="normal" />
                                        <HeaderStyle CssClass="header" />
                                        <EmptyDataRowStyle HorizontalAlign="center" CssClass="Text3" />
                                        <AlternatingRowStyle CssClass="alternate" />
                                        <Columns>
                                            <asp:BoundField ReadOnly="true" HeaderText="Shipping Carrier"
                                                DataField="CarrierName" ItemStyle-HorizontalAlign="left">
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Start Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartTime"
                                                        Text='<%# GlobalFunctionsClass.FormatTimeString(Eval("CarrierStartTime")) %>'
                                                        runat="server" ItemStyle-HorizontalAlign="center"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cutoff Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCutoffTime"
                                                        Text='<%# GlobalFunctionsClass.FormatTimeString(Eval("CarrierCutoffTime")) %>'
                                                        runat="server" ItemStyle-HorizontalAlign="center"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:TableCell>


                                <asp:TableCell HorizontalAlign="Center">
                                    <asp:Table runat="server" CellPadding="4" CellSpacing="0" Width="340px"
                                        CssClass="OrderDetail" BorderWidth="1" BorderStyle="Solid"
                                        BorderColor="#0D5692">
                                        <asp:TableRow BackColor="#0D5692" BorderWidth="1" BorderStyle="Solid"
                                            BorderColor="#0D5692">
                                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle"
                                                ColumnSpan="4" ForeColor="White" Font-Bold="true">
                                                RxPortal Lab Parameters
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BackColor="#ffffff">
                                            <asp:TableCell HorizontalAlign="Center" ColumnSpan="4">
                                                <asp:Table runat="server" BorderWidth="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell runat="server" ID="TableCell1"
                                                            HorizontalAlign="Left" VerticalAlign="Middle"
                                                            CssClass="Text3">
                                                            LabId:
                                                        </asp:TableCell>
                                                        <asp:TableCell VerticalAlign="Middle">
                                                            <input type="text" class="Customer" id="labIdInput"
                                                                name="labIdInput" />
                                                            <input type="button" value="Lookup" class="ButtonText"
                                                                onclick="goToLabIdConfigMaintenance()" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>


                            </asp:TableRow>
                            <asp:TableRow>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Center">
                                    <asp:Table runat="server" CellPadding="4" CellSpacing="0" Width="340px"
                                        CssClass="OrderDetail" BorderWidth="1" BorderStyle="Solid"
                                        BorderColor="#0D5692">
                                        <asp:TableRow BackColor="#0D5692" BorderWidth="1" BorderStyle="Solid"
                                            BorderColor="#0D5692">
                                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle"
                                                ColumnSpan="4" ForeColor="White" Font-Bold="true">
                                                RxPortal Lab Parameters
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BackColor="#ffffff">
                                            <asp:TableCell HorizontalAlign="Center" ColumnSpan="4">
                                                <asp:Table runat="server" BorderWidth="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell runat="server" ID="TableCell1"
                                                            HorizontalAlign="Left" VerticalAlign="Middle"
                                                            CssClass="Text3">
                                                            LabId:
                                                        </asp:TableCell>
                                                        <asp:TableCell VerticalAlign="Middle">
                                                            <input type="text" class="Customer" id="labIdInput"
                                                                name="labIdInput" />
                                                            <input type="button" value="Lookup" class="ButtonText"
                                                                onclick="goToLabIdConfigMaintenance()" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Center">
                                    <asp:Table runat="server" CellPadding="4" CellSpacing="0" Width="340px"
                                        CssClass="OrderDetail" BorderWidth="1" BorderStyle="Solid"
                                        BorderColor="#0D5692">
                                        <asp:TableRow BackColor="#0D5692" BorderWidth="1" BorderStyle="Solid"
                                            BorderColor="#0D5692">
                                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle"
                                                ColumnSpan="4" ForeColor="White" Font-Bold="true">
                                                RxPortal Enterprise Config
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow BackColor="#ffffff">
                                            <asp:TableCell HorizontalAlign="Center" ColumnSpan="4">
                                                <asp:Table runat="server" BorderWidth="0">
                                                    <asp:TableRow>
                                                        <%--<asp:TableCell runat="server" ID="TableCell2"
                                                            HorizontalAlign="Left" VerticalAlign="Middle"
                                                            CssClass="Text3">
                                                            RxPortal:
                                            </asp:TableCell>--%>
                                            <asp:TableCell VerticalAlign="Middle">
                                                <%--<input type="text" class="Customer" id="RxPortalInput"
                                                    name="labIdInput" />--%>
                                                <input type="button" value="Load RxPortal Config" class="ButtonText"
                                                    onclick="goToRxPortalEnterpriseConfig()" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                </asp:TableRow>
                <asp:TableRow>
                </asp:TableRow>
                <asp:TableRow>
                </asp:TableRow>
                <asp:TableRow>

                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Table runat="server" CellPadding="4" CellSpacing="0" Width="340px" CssClass="OrderDetail"
                            BorderWidth="1" BorderStyle="Solid" BorderColor="#0D5692">
                            <asp:TableRow BackColor="#1E7B1E" BorderWidth="1" BorderStyle="Solid" BorderColor="#1E7B1E">
                                <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" ColumnSpan="4"
                                    ForeColor="White" Font-Bold="true">
                                    Customer Account Lookup
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow BackColor="#B1CDB1">
                                <asp:TableCell HorizontalAlign="Center" ColumnSpan="4">
                                    <asp:Table runat="server" BorderWidth="0">
                                        <asp:TableRow>
                                            <asp:TableCell runat="server" ID="TableCell3" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="Text3">
                                                Account:
                                            </asp:TableCell>
                                            <asp:TableCell VerticalAlign="Middle">
                                                <input type="text" class="Customer" id="CustomerInput"
                                                    name="CustomerInput" />

                                                <input type="button" value="Lookup" class="ButtonText"
                                                    onclick="goToAccountMaintenance()" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            </asp:TableCell>
            </asp:TableRow>

            </asp:Table>




            <script>
                function goToAccountMaintenance() {
                    var customerID = $("#CustomerInput").val();
                    if (!customerID) {
                        $('#CustomerInput').focus();
                        return;
                    } else {
                        location.href = "../Enterprise/Customers/AccountMaintenance.aspx?SAPCustomerID=" + customerID;
                    }
                }

                function goToLabIdConfigMaintenance() {
                    var labId = $("#labIdInput").val();
                    if (!labId) {
                        $("#labIdInput").css("border-style", "solid");
                        $("#labIdInput").css("border-color", "#FFFFFF");
                        $('#labIdInput').focus();
                        return;
                    } else {
                        location.href = "../LabData/RxlabIdConfig.aspx?labId=" + labId;
                    }
                }

                function goToRxPortalEnterpriseConfig() {
                    location.href = "../LabData/RxPortalConfig.aspx";
                }

                function goToDetailPage() {
                    var Option = "";
                    var RxNumber = $("#inputRxNumber").val();
                    var RxProcess = $("#ddlPortalProcess").val();
                    if ($('#rbProcess').is(':checked')) {
                        Option = "Process";
                    } else {
                        Option = "RxNumber";
                    }
                    location.href = "../Enterprise/SapMessageDetail.aspx?Option=" + Option + "&RxNumber=" + RxNumber + "&RxProcess=" + RxProcess;

                }

                function CheckUncheck(option) {
                    if (option == "Process") {
                        $("#tr_RxNumber").hide();
                        $("#tr_RxProcess").show();

                        $("#rbProcess").attr("checked", true);
                        $("#rbRxNumber").attr("checked", false);

                        fillRxPortalProcessDDL();

                    } else {
                        $("#inputRxNumber").val("");
                        $("#tr_RxNumber").show();
                        $("#tr_RxProcess").hide();

                        $("#rbProcess").attr("checked", false);
                        $("#rbRxNumber").attr("checked", true);
                    }
                }
            </script>
            
        </asp:Content>