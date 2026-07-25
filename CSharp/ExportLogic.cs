

public class ExportCustomerTransactionsLogic : ExcelExport<ExportCustomerTransactionsResponseModel>
{
    protected override void FillSheetHeaderData(ExportCustomerTransactionsResponseModel data)
    {
        var normalHeaderStyle = GetNoFillHeaderStyle();
        CreateCellStyleFont(normalHeaderStyle, 12);
        RowIndex++;

        foreach (var rowDef in data.HeaderData.Rows)
        {
            var row = Sheet.CreateRow(RowIndex);

            foreach (var cellDef in rowDef.RowCells)
            {
                ICell cell = row.CreateCell(0);
                cell.CellStyle = normalHeaderStyle;
                cell.SetCellValue(cellDef.CellValue);
            }

            if (data.TotalColumns > 1)
            {
                var cra = new CellRangeAddress(RowIndex, RowIndex, 0, data.TotalColumns - 1);
                Sheet.AddMergedRegion(cra);
            }

            RowIndex++;
        }
    }
    protected override void FillColumnHeaderData(ExportCustomerTransactionsResponseModel data)
    {
        int cellnum = 0;

        #region Create styles for header cells

        var normalHeaderStyle = GetHeaderStyle(HorizontalAlignment.Left);
        CreateCellStyleFont(normalHeaderStyle, 11, true);
        var normalHeaderStyleTextAlignedRight = GetHeaderStyle(HorizontalAlignment.Right);
        CreateCellStyleFont(normalHeaderStyleTextAlignedRight, 11, true);

        #endregion

        //Create column header row
        var columnHeaderRow = Sheet.CreateRow(RowIndex);
        columnHeaderRow.Height = (short)Math.Round(1.75 * Unit);

        #region Add header cells to the row

        //Create column header row to show

        CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Transaction Date");
        cellnum++;

        if (!data.CustomerId.HasValue || data.CustomerId == 0)
        { }
        CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Customer Code");
        cellnum++;

        CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Customer Name");
        cellnum++;


        CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Type");
        cellnum++;

        CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Due Date");
        cellnum++;

        CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Discount Date");
        cellnum++;

        CreateCell(columnHeaderRow, cellnum, normalHeaderStyleTextAlignedRight, "Discount %");
        cellnum++;

        CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Document Number");
        cellnum++;

        CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Description");
        cellnum++;

        if (data.Currencies > 1)
        { }
        CreateCell(columnHeaderRow, cellnum, normalHeaderStyleTextAlignedRight, "Exchange Rate");
        cellnum++;


        CreateCell(columnHeaderRow, cellnum, normalHeaderStyleTextAlignedRight, "Amount");
        cellnum++;

        CreateCell(columnHeaderRow, cellnum, normalHeaderStyleTextAlignedRight, "Balance");
        cellnum++;

        #endregion Add header cells to the row
    }

    protected override void FillData(ExportCustomerTransactionsResponseModel data)
    {
        RowIndex++;
        List<int> displayColumns = Showcolumns(data.ColumnsToHide, data.TotalColumns);

        #region Create styles for data cells
        ICellStyle normalStyleTextAlignmentLeft = GetNormalStyle(HorizontalAlignment.Left);
        CreateCellStyleFont(normalStyleTextAlignmentLeft);

        ICellStyle normalStyleTextAlignmentRight = GetNormalStyle(HorizontalAlignment.Right);
        CreateCellStyleFont(normalStyleTextAlignmentRight);

        ICellStyle doubleCellStyleTwoDecimal = GetNormalDoubleStyle("###,###,####.##");
        CreateCellStyleFont(doubleCellStyleTwoDecimal);

        ICellStyle doubleCellStyleFixedTwoDecimal = GetNormalDoubleStyle("###,###,###0.00");
        CreateCellStyleFont(doubleCellStyleFixedTwoDecimal);

        ICellStyle doubleCellStyleThreeDecimal = GetNormalDoubleStyle("###,###,####.###");
        CreateCellStyleFont(doubleCellStyleThreeDecimal);

        ICellStyle doubleCellStyleFixedThreeDecimal = GetNormalDoubleStyle("###,###,###0.000");
        CreateCellStyleFont(doubleCellStyleFixedThreeDecimal);

        ICellStyle doubleCellStyleFourDecimal = GetNormalDoubleStyle("###,###,####.####");
        CreateCellStyleFont(doubleCellStyleFourDecimal);

        ICellStyle doubleCellStyleFixedFourDecimal = GetNormalDoubleStyle("###,###,###0.0000");
        CreateCellStyleFont(doubleCellStyleFixedFourDecimal);

        ICellStyle doubleCellStyleFiveDecimal = GetNormalDoubleStyle("###,###,####.#####");
        CreateCellStyleFont(doubleCellStyleFiveDecimal);
        ICellStyle doubleCellStyleFixedFiveDecimal = GetNormalDoubleStyle("###,###,###0.00000");
        CreateCellStyleFont(doubleCellStyleFixedFiveDecimal);

        ICellStyle intRegularCellStyleTextAlignmentRight = GetNormalIntStyle(HorizontalAlignment.Right);
        CreateCellStyleFont(intRegularCellStyleTextAlignmentRight);
        ICellStyle intRegularCellStyleTextAlignmentLeft = GetNormalIntStyle(HorizontalAlignment.Left);
        CreateCellStyleFont(intRegularCellStyleTextAlignmentLeft);

        ICellStyle intCommaSeparatedCellStyleTextAlignmentRight = GetNormalIntStyle(HorizontalAlignment.Right, "###,###,###");
        CreateCellStyleFont(intCommaSeparatedCellStyleTextAlignmentRight);
        ICellStyle intCommaSeparatedCellStyleTextAlignmentLeft = GetNormalIntStyle(HorizontalAlignment.Left, "###,###,###");
        CreateCellStyleFont(intCommaSeparatedCellStyleTextAlignmentLeft);

        #endregion

        foreach (var item in data.DetailData.DetailRows)
        {
            //Create Detail row
            var detailRow = Sheet.CreateRow(RowIndex);
            int cellnum = 0;

            foreach (int val in displayColumns)
            {
                switch (val)
                {
                    case 0:
                        CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.TransactionDate.ToString("M/d/yyyy"));
                        cellnum++;
                        break;
                    case 1:
                        if (!data.CustomerId.HasValue || data.CustomerId == 0)
                        { }
                        CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.CustomerCode);
                        cellnum++;

                        break;
                    case 2:
                        if (!data.CustomerId.HasValue || data.CustomerId == 0)
                        { }
                        CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.CustomerName);
                        cellnum++;

                        break;
                    case 3:
                        CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.Type);
                        cellnum++;
                        break;
                    case 4:
                        CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.DueDate.HasValue ? item.DueDate.Value.ToString("M/d/yyyy") : string.Empty);
                        cellnum++;
                        break;
                    case 5:
                        CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.DiscountDate.HasValue ? item.DiscountDate.Value.ToString("M/d/yyyy") : string.Empty);
                        cellnum++;
                        break;
                    case 6:
                        CreateCell(detailRow, cellnum, doubleCellStyleFixedTwoDecimal, item.DiscountPercentage.HasValue ? item.DiscountPercentage : string.Empty);
                        cellnum++;
                        break;
                    case 7:
                        CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.DocumentNumber);
                        cellnum++;
                        break;
                    case 8:
                        CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.Description);
                        cellnum++;
                        break;
                    case 9:
                        if (data.Currencies > 1)
                        {
                            CreateCell(detailRow, cellnum, doubleCellStyleFixedTwoDecimal, item.ExchangeRate.HasValue ? item.ExchangeRate : string.Empty);
                            cellnum++;
                        }
                        break;
                    case 10:
                        CreateCell(detailRow, cellnum, doubleCellStyleFixedTwoDecimal, item.Amount.HasValue ? item.Amount : string.Empty);
                        cellnum++;
                        break;
                    case 11:
                        CreateCell(detailRow, cellnum, doubleCellStyleFixedTwoDecimal, item.Balance.HasValue ? item.Balance : string.Empty);
                        cellnum++;
                        break;
                }
            }
            RowIndex++;
        }

    }

    protected override void FillFooterData(ExportCustomerTransactionsResponseModel data)
    {
        int cellnum = -1;

        //Create footer row
        var columnFooterRow = Sheet.CreateRow(RowIndex++);
        columnFooterRow.Height = (short)Math.Round(1.75 * Unit);

        #region Create styles for footer cells

        ICellStyle normalHeaderStyle = GetHeaderStyle(HorizontalAlignment.Left);
        CreateCellStyleFont(normalHeaderStyle, 11, true);

        ICellStyle normalHeaderStyleTextAlignedRight = GetHeaderStyle(HorizontalAlignment.Right);
        CreateCellStyleFont(normalHeaderStyleTextAlignedRight, 11, true);

        ICellStyle doubleCellStyleFixedTwoDecimal = GetHeaderDoubleStyle("###,###,###0.00");
        CreateCellStyleFont(doubleCellStyleFixedTwoDecimal, 11, true);

        #endregion Create styles for footer cells


        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

        if (!data.CustomerId.HasValue || data.CustomerId == 0)
        { }
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);


        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

        if (data.Currencies > 1)
        { }
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);


        cellnum++;
        var amount = data.DetailData.DetailRows.Sum(x => (x.Amount));
        CreateCell(columnFooterRow, cellnum, normalHeaderStyleTextAlignedRight, $"{amount:###,###,##0.00}");

        cellnum++;
        var balance = data.DetailData.DetailRows.Sum(x => (x.Balance));
        CreateCell(columnFooterRow, cellnum, normalHeaderStyleTextAlignedRight, $"{balance:###,###,##0.00}");

        ResizeColumns(data.TotalColumns);
    }


}
