<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Global/SystemPageTemplate.master" CodeBehind="CzvReduceProducts.aspx.vb" Inherits=".CzvReduceProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildContent1" runat="server">

    <script type="text/jscript" src="<%=Page.ResolveUrl("~/Scripts/jquery.js")%>"></script>
    <script type="text/jscript" src="<%=Page.ResolveUrl("~/Scripts/jquery-ui.js")%>"></script>

    <script type="text/javascript">

        $(document).ready(function () {
        });

    </script>

    <asp:Table runat="server" BorderWidth="0" Width="100%" CellPadding="0" CellSpacing="0">
        <asp:TableRow BackColor="#738FBF">
            <asp:TableCell>
                <asp:Table runat="server" BorderWidth="0" Width="80" CellPadding="0" CellSpacing="0">
                    <asp:TableRow>
                        <asp:TableCell>
                        &nbsp;&nbsp;
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Height="25px" CssClass="header">
                            <asp:ImageButton ID="ExcelImageButton" CommandArgument="Excel" CommandName="FileType" OnCommand="ExportToOffice" runat="server" ImageUrl="~/images/Excel2.gif" />
                        </asp:TableCell>
                        <asp:TableCell>
                        &nbsp;
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Height="25px" CssClass="header">
                            <asp:ImageButton ID="WordImageButton" CommandArgument="Word" CommandName="FileType" OnCommand="ExportToOffice" runat="server" ImageUrl="~/images/Word2.gif" />
                        </asp:TableCell>
                        <asp:TableCell>
                        &nbsp;
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="25px" CssClass="header">
                            <asp:ImageButton ID="PdfImageButton" OnClick="ExportToPDF" runat="server" ImageUrl="~/images/Reader2.gif" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" CssClass="HeaderSub1">
           <%-- <asp:Label ID="lblLabID" runat="server"></asp:Label>--%>
            CZV Reduce Product Portfolio
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" CssClass="header">
            &nbsp;
            </asp:TableCell>

            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" CssClass="HeaderSub1">

                <asp:DropDownList ID="ddlCriteria" runat="server" OnTextChanged="btnSelectedCriteria" CssClass="select2-dropdown" AutoPostBack="true"></asp:DropDownList>
                <asp:DropDownList ID="ddlReduceProductoCriteria" Width="120px" runat="server" CssClass="select2-dropdown" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>

            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" CssClass="header">
            &nbsp;
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5">

                <div runat="server" id="DivScroll">
                    <asp:GridView ID="GridView1" runat="server" Width="185%" Height="150%" CellPadding="2" CssClass="WipGrid-View"
                        AutoGenerateColumns="false" AllowPaging="true" PageSize="25" AllowSorting="False"
                        OnPageIndexChanging="gridView_PageIndexChanging">
                        <PagerSettings Mode="Numeric" NextPageText="Next" PreviousPageText="Back" Position="Bottom" PageButtonCount="20" />
                        <PagerStyle CssClass="pagination" HorizontalAlign="Center" />
                        <EmptyDataRowStyle HorizontalAlign="center" CssClass="Text3" />
                        <RowStyle CssClass="normal" />
                        <HeaderStyle CssClass="header" />
                        <AlternatingRowStyle CssClass="alternate" />
                        <Columns>
                            <asp:BoundField ReadOnly="true" HeaderText="GLAART" DataField="GLAART"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Edi Code" DataField="EdiCode" SortExpression="EdiCode" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Commercial Desc" DataField="CommercialDesc" SortExpression="CommercialDesc" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Commercial Name" DataField="CommercialName" SortExpression="CommercialName" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Commercial Design" DataField="CommercialDesign" SortExpression="CommercialDesign" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                            <asp:BoundField ReadOnly="true" HeaderText="Region Country" DataField="RegionCountry" SortExpression="RegionCountry" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Lens Design" DataField="LensDesign" SortExpression="LensDesign" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Lens Material" DataField="LensMaterial" SortExpression="LensMaterial" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Lens Color" DataField="LensColor" SortExpression="LensColor" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Focal Type" DataField="FocalType" SortExpression="FocalType" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                            <asp:BoundField ReadOnly="true" HeaderText="Portfolio" DataField="Portfolio" SortExpression="Portfolio" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Product Family" DataField="ProductFamily" SortExpression="ProductFamily" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Variant" DataField="Variant" SortExpression="Variant" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Variant Color" DataField="VariantColor" SortExpression="VariantColor" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Material Index" DataField="MaterialIndex" SortExpression="MaterialIndex" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                            <asp:BoundField ReadOnly="true" HeaderText="Material Group" DataField="MaterialGroup" SortExpression="MaterialGroup" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Stock Lens" DataField="StockLens" SortExpression="StockLens" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField ReadOnly="true" HeaderText="Sport Lens" DataField="SportLens" SortExpression="SportLens" ItemStyle-HorizontalAlign="Left"></asp:BoundField>

                        </Columns>
                    </asp:GridView>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>
