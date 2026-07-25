protected override void FillFooterData(ExportSalesOrderCashPaymentsDataResponseModel data)
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


    ///      **** NEW ****
    ICellStyle doubleCellStyleFixedTwoDecimal = GetHeaderDoubleStyle("###,###,###0.00");
    CreateCellStyleFont(doubleCellStyleFixedTwoDecimal, 11, true);

    #endregion Create styles for footer cells

    for (int i = 0; i < data.TotalColumns - 1; i++)
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    cellnum++;
    var amount = data.DetailData.DetailRows.Sum(x => (x.Amount));
    CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, amount);

    ResizeColumns(data.TotalColumns);
}

/*
 foreach solution JU 
    Nota: foreach val variable es el que control el flujo 
*/
protected override void FillFooterData(ExportSalesQuoteLinesResponseModel data)
{
    try
    {
        _cellNum = 0;
        List<int> displaycolumns = Showcolumns(data.ColumnsToHide, totalColumns);

        #region Create styles for footer cells

        ICellStyle normalHeaderStyle = GetHeaderStyle(HorizontalAlignment.Left);
        CreateCellStyleFont(normalHeaderStyle, 11, true);
        ICellStyle normalHeaderStyleTextAlignedRight = GetHeaderStyle(HorizontalAlignment.Right);
        CreateCellStyleFont(normalHeaderStyleTextAlignedRight, 11, true);

        ICellStyle normalHeaderStyleTextAlignedCenter = GetHeaderStyle(HorizontalAlignment.Center);
        CreateCellStyleFont(normalHeaderStyleTextAlignedCenter, 11, true);


        ICellStyle intRegularCellStyleTextAlignmentRight = GetHeaderIntStyle(HorizontalAlignment.Right);
        CreateCellStyleFont(intRegularCellStyleTextAlignmentRight, 11, true);
        ICellStyle intRegularCellStyleTextAlignmentLeft = GetHeaderIntStyle(HorizontalAlignment.Left);
        CreateCellStyleFont(intRegularCellStyleTextAlignmentLeft, 11, true);


        #endregion

        //Create footer row
        var columnFooterRow = Sheet.CreateRow(RowIndex);
        columnFooterRow.Height = (short)Math.Round(1.75 * Unit);

        int[] columnsToSum = new int[] { 28, 29 };
        foreach (int val in displaycolumns)
        {
            if (!columnsToSum.Contains(val) && val != 27)
            {
                CreateCell(columnFooterRow, _cellNum, normalHeaderStyle, EmptyValue, 1);
                _cellNum++;
                continue;
            }
            if (val == 27)
            {
                CreateCell(columnFooterRow, _cellNum, normalHeaderStyleTextAlignedRight, string.Empty, 1);
                if (displaycolumns.Contains(28) || displaycolumns.Contains(29))
                {
                    CreateCell(columnFooterRow, _cellNum, normalHeaderStyleTextAlignedRight, "Total", 1);
                    _cellNum++;
                }
            }
            if (val == 28)
            {
                var totalExtendedPrice1 = data.DetailData.DetailRows.Sum(x => x.ExtendedPrice);
                CreateCell(columnFooterRow, _cellNum, normalHeaderStyleTextAlignedCenter, string.Format("{0:###,###,###0.00}", totalExtendedPrice1));
                _cellNum++;
            }
            if (val == 29)
            {
                var totalExtendedCost1 = data.DetailData.DetailRows.Sum(x => x.ExtendedCost);
                CreateCell(columnFooterRow, _cellNum, normalHeaderStyleTextAlignedCenter, string.Format("{0:###,###,###0.00}", totalExtendedCost1));
                _cellNum++;
            }
            //if(!displaycolumns.Contains(28) || !displaycolumns.Contains(29))
            //{
            //    CreateCell(columnFooterRow, _cellNum, normalHeaderStyleTextAlignedRight, string.Empty, 1);
            //}
        }

    }
    catch (Exception ex)
    {
        string error = ex.Message;
    }

    ResizeColumns(data.TotalColumns);
}

/* *********************************************************** */

private int columnOK(int[] hidecolums)
{
    int[] hiddablecolumns = new int[] { 22, 25, 27 };
    int i = 2;
    foreach (int item in hidecolums)
    {
        if (!hidecolums.Contains(hiddablecolumns[i]))
        {
            return hiddablecolumns[i];
        }
        i--;

        if (i < 0)
        {
            return 23;
        }
    }

    return 23;

    //if (hidecolums.Any(x => hiddablecolumns.Any(y => y == x)))
    //{
    //    // Sacarlo a un metodo externo foreach
    //    // si ColumnsToHide contiene la columna, que busque en la siguiente hacia atras
    //    // 27, then 25, then 23, 
    //}
}

/* *********************************************************** */

int[] columnsHidde = new int[] { 21, 22, 24, 25, 26, 27 };
if (data.ColumnsToHide.Intersect(columnsHidde).Any())
{
    continue;
}

if (!data.ColumnsToHide.Contains(val) && val == 27)
{

}

/* ***********************Compare Lists ******************************** */
/* ******************************************************************* */
bool hasMatch = myStrings.Any(x => parameters.Any(y => y.source == x));
/* ******************************************************************* */



/* *********************************************************** */

/// <summary>
/// ExportTagsLogic
/// Fill footer row
/// </summary>
/// <param name="data"></param>
protected override void FillFooterData(ExportTagsDataResponseModel data)
{
    int cellnum = -1;
    decimal footerTotalCost = 0m;

    #region Create styles for footer cells

    ICellStyle normalHeaderStyle = GetHeaderStyle(HorizontalAlignment.Left);
    CreateCellStyleFont(normalHeaderStyle, 11, true);
    ICellStyle normalHeaderStyleTextAlignedRight = GetHeaderStyle(HorizontalAlignment.Right);
    CreateCellStyleFont(normalHeaderStyleTextAlignedRight, 11, true);


    ICellStyle intRegularCellStyleTextAlignmentRight = GetHeaderIntStyle(HorizontalAlignment.Right);
    CreateCellStyleFont(intRegularCellStyleTextAlignmentRight, 11, true);
    ICellStyle intRegularCellStyleTextAlignmentLeft = GetHeaderIntStyle(HorizontalAlignment.Left);
    CreateCellStyleFont(intRegularCellStyleTextAlignmentLeft, 11, true);

    ICellStyle intCommaSeparatedCellStyleTextAlignmentRight = GetHeaderIntStyle(HorizontalAlignment.Right,
        "###,###,###");
    CreateCellStyleFont(intCommaSeparatedCellStyleTextAlignmentRight, 11, true);
    ICellStyle intCommaSeparatedCellStyleTextAlignmentLeft = GetHeaderIntStyle(HorizontalAlignment.Left, "###,###,###");
    CreateCellStyleFont(intCommaSeparatedCellStyleTextAlignmentLeft, 11, true);

    ICellStyle doubleCellStyleTwoDecimal = GetHeaderDoubleStyle("###,###,####.##");
    CreateCellStyleFont(doubleCellStyleTwoDecimal, 11, true);

    ICellStyle doubleCellStyleFixedTwoDecimal = GetHeaderDoubleStyle("###,###,###0.00");
    CreateCellStyleFont(doubleCellStyleFixedTwoDecimal, 11, true);

    ICellStyle doubleCellStyleThreeDecimal = GetHeaderDoubleStyle("###,###,####.###");
    CreateCellStyleFont(doubleCellStyleThreeDecimal, 11, true);

    ICellStyle doubleCellStyleFixedThreeDecimal = GetHeaderDoubleStyle("###,###,###0.000");
    CreateCellStyleFont(doubleCellStyleFixedThreeDecimal, 11, true);


    ICellStyle doubleCellStyleFourDecimal = GetHeaderDoubleStyle("###,###,####.####");
    CreateCellStyleFont(doubleCellStyleFourDecimal, 11, true);
    ICellStyle doubleCellStyleFixedFourDecimal = GetHeaderDoubleStyle("###,###,###0.0000");
    CreateCellStyleFont(doubleCellStyleFixedFourDecimal, 11, true);

    #endregion

    //Create footer row
    var columnFooterRow = Sheet.CreateRow(RowIndex++);
    columnFooterRow.Height = (short)Math.Round(1.75 * Unit);

    #region Add cells to footer row

    #region CellToHide15

    if (!data.ColumnsToHide.Any(x => x.Equals("0")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("1")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("2")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("3")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("4")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("5")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("6")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }
    if (!data.ColumnsToHide.Any(x => x.Equals("7")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("8")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("9")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("10")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("11")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("12")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("13")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("14")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }
    #endregion CellToHide15

    #region CellToHide15_37
    if (!data.ColumnsToHide.Any(x => x.Equals("15")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("16")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("17")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("18")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("19")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("20")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("21")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }

    if (!data.ColumnsToHide.Any(x => x.Equals("22")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("23")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }

    if (!data.ColumnsToHide.Any(x => x.Equals("24")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("25")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("26")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("27")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("28")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("29")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("30")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("31")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("32")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("33")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }
    if (!data.ColumnsToHide.Any(x => x.Equals("34")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }

    if (!data.ColumnsToHide.Any(x => x.Equals("35")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("36")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("37")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
    }

    #endregion CellToHide15_37

    if (!data.ColumnsToHide.Any(x => x.Equals("38")))
    {
        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyleTextAlignedRight, "Total", 1);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("39")))
    {
        cellnum++;
        var totalTheoWt = data.DetailData.DetailRows.Sum(x => (x.TheoreticalWeight));
        CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedThreeDecimal, totalTheoWt);
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("40")))
    {
        cellnum++;
        var totalScaleWt = data.DetailData.DetailRows.Sum(x => (x.ScaleWeight));
        CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedThreeDecimal, totalScaleWt);
    }

    if (data.CanViewTagCostFlag)
    {
        var totalMaterialCost = data.DetailData.DetailRows.Sum(x => (x.MaterialCost ?? 0));
        footerTotalCost += totalMaterialCost;

        if (!data.ColumnsToHide.Any(x => x.Equals("41")))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalMaterialCost);
        }

        var totalInboundCost = data.DetailData.DetailRows.Sum(x => (x.InboundFreightCost ?? 0));
        footerTotalCost += totalInboundCost;

        if (!data.ColumnsToHide.Any(x => x.Equals("42")))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalInboundCost);
        }

        var totalCost3 = data.DetailData.DetailRows.Sum(x => (x.Cost3 ?? 0));
        footerTotalCost += totalCost3;

        if (!data.ColumnsToHide.Any(x => x.Equals("43")))
        {
            if (!string.IsNullOrEmpty(data.InventoryInitData.TagCost3Label))
            {
                cellnum++;
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalCost3);
            }
        }

        var totalCost4 = data.DetailData.DetailRows.Sum(x => (x.Cost4 ?? 0));
        footerTotalCost += totalCost4;

        if (!data.ColumnsToHide.Any(x => x.Equals("44")))
        {
            if (!string.IsNullOrEmpty(data.InventoryInitData.TagCost4Label))
            {
                cellnum++;
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalCost4);
            }
        }

        var totalCost5 = data.DetailData.DetailRows.Sum(x => (x.Cost5 ?? 0));
        footerTotalCost += totalCost5;

        if (!data.ColumnsToHide.Any(x => x.Equals("45")))
        {
            if (!string.IsNullOrEmpty(data.InventoryInitData.TagCost5Label))
            {
                cellnum++;
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalCost5);
            }
        }

        var totalCost6 = data.DetailData.DetailRows.Sum(x => (x.Cost6 ?? 0));
        footerTotalCost += totalCost6;

        if (!data.ColumnsToHide.Any(x => x.Equals("46")))
        {
            if (!string.IsNullOrEmpty(data.InventoryInitData.TagCost6Label))
            {
                cellnum++;
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalCost6);
            }
        }

        var totalCost7 = data.DetailData.DetailRows.Sum(x => (x.Cost7 ?? 0));
        footerTotalCost += totalCost7;

        if (!data.ColumnsToHide.Any(x => x.Equals("47")))
        {
            if (!string.IsNullOrEmpty(data.InventoryInitData.TagCost7Label))
            {
                cellnum++;
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalCost7);
            }
        }

        var totalCost8 = data.DetailData.DetailRows.Sum(x => (x.Cost8 ?? 0));
        footerTotalCost += totalCost8;

        if (!data.ColumnsToHide.Any(x => x.Equals("48")))
        {
            if (!string.IsNullOrEmpty(data.InventoryInitData.TagCost8Label))
            {
                cellnum++;
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalCost8);
            }
        }

        var totalCost9 = data.DetailData.DetailRows.Sum(x => (x.Cost9 ?? 0));
        footerTotalCost += totalCost9;

        if (!data.ColumnsToHide.Any(x => x.Equals("49")))
        {
            if (!string.IsNullOrEmpty(data.InventoryInitData.TagCost9Label))
            {
                cellnum++;
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalCost9);
            }
        }

        var totalCost10 = data.DetailData.DetailRows.Sum(x => (x.Cost10 ?? 0));
        footerTotalCost += totalCost10;

        if (!data.ColumnsToHide.Any(x => x.Equals("50")))
        {
            if (!string.IsNullOrEmpty(data.InventoryInitData.TagCost10Label))
            {
                cellnum++;
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalCost10);
            }
        }

        if (!data.ColumnsToHide.Any(x => x.Equals("51")))
        {
            //Footer Total
            cellnum++;
            CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, footerTotalCost);
        }
    }

    // UDFs
    #region UDFs
    if (!data.ColumnsToHide.Any(x => x.Equals("52")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF1Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("53")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF2Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("54")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF3Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("55")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF4Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("56")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF5Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("57")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF6Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("58")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF7Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("59")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF8Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("60")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF9Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("61")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF10Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("62")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchaseDetailUDF1Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("63")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchaseDetailUDF2Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("64")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchaseDetailUDF3Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("65")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchaseDetailUDF4Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("66")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchaseDetailUDF5Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("67")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchaseDetailUDF6Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("68")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchaseDetailUDF7Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("69")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchaseDetailUDF8Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("70")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchaseDetailUDF9Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("71")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchaseDetailUDF10Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("72")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchasingUDF1Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("73")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchasingUDF2Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("74")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchasingUDF3Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("75")))
    {
        if (!string.IsNullOrEmpty(data.PurchasingInitData.PurchasingUDF4Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    #endregion UDFs

    #region Item UDF
    if (!data.ColumnsToHide.Any(x => x.Equals("76")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.ItemUDF1Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("77")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.ItemUDF2Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("78")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.ItemUDF3Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    if (!data.ColumnsToHide.Any(x => x.Equals("79")))
    {
        if (!string.IsNullOrEmpty(data.InventoryInitData.ItemUDF4Label))
        {
            cellnum++;
            CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
        }
    }

    // Wareouse #10323
    if (!data.ColumnsToHide.Any(x => x.Equals("80")))
    {

        cellnum++;
        CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);

    }

    #endregion Item UDF


    ResizeColumns(data.TotalColumns);

}


/// <summary>
/// ExportSalesQuoteLinesLogic
/// Fill footer row Ej 2
/// </summary>
/// <param name="data"></param>
protected override void FillFooterData(ExportSalesQuoteLinesResponseModel data)
{
    try
    {
        _cellNum = 0; // recorre sobre las celdas
        List<int> displaycolumns = Showcolumns(data.ColumnsToHide, data.TotalColumns);

        #region Create styles for footer cells

        ICellStyle normalHeaderStyle = GetHeaderStyle(HorizontalAlignment.Left);
        CreateCellStyleFont(normalHeaderStyle, 11, true);
        ICellStyle normalHeaderStyleTextAlignedRight = GetHeaderStyle(HorizontalAlignment.Right);
        CreateCellStyleFont(normalHeaderStyleTextAlignedRight, 11, true);

        ICellStyle normalHeaderStyleTextAlignedCenter = GetHeaderStyle(HorizontalAlignment.Center);
        CreateCellStyleFont(normalHeaderStyleTextAlignedCenter, 11, true);


        ICellStyle intRegularCellStyleTextAlignmentRight = GetHeaderIntStyle(HorizontalAlignment.Right);
        CreateCellStyleFont(intRegularCellStyleTextAlignmentRight, 11, true);
        ICellStyle intRegularCellStyleTextAlignmentLeft = GetHeaderIntStyle(HorizontalAlignment.Left);
        CreateCellStyleFont(intRegularCellStyleTextAlignmentLeft, 11, true);


        #endregion

        //Create footer row
        var columnFooterRow = Sheet.CreateRow(RowIndex);
        columnFooterRow.Height = (short)Math.Round(1.75 * Unit);

        #region Add cells to footer row                

        CreateCell(columnFooterRow, _cellNum, normalHeaderStyleTextAlignedRight, "Total Extended Price");
        _cellNum++;
        var totalExtendedPrice = data.DetailData.DetailRows.Sum(x => x.ExtendedPrice);
        CreateCell(columnFooterRow, _cellNum, normalHeaderStyleTextAlignedCenter, string.Format("{0:###,###,###0.00}", totalExtendedPrice));

        RowIndex++;// salta File
        columnFooterRow = Sheet.CreateRow(RowIndex);
        _cellNum = 0;
        CreateCell(columnFooterRow, _cellNum, normalHeaderStyleTextAlignedRight, "Total Extended Cost");
        _cellNum++;
        var totalExtendedCost = data.DetailData.DetailRows.Sum(x => x.ExtendedCost);
        CreateCell(columnFooterRow, _cellNum, normalHeaderStyleTextAlignedCenter, string.Format("{0:###,###,###0.00}", totalExtendedCost));

        #endregion Add cells to footer row
    }
    catch (Exception ex)
    {
        string error = ex.Message;
    }

    ResizeColumns(data.TotalColumns);
}


/// <summary>
/// ExportSalesOrderLinesLogic
/// Fill footer row -- foreach Solution
/// </summary>
/// <param name="data"></param>
protected override void FillFooterData(ExportSalesOrderLinesDataResponseModel data)
{
    int cellnum = 0;
    decimal footerTotalCost = 0m;
    List<int> displaycolumns = Showcolumns(data.ColumnsToHide, totalFileColumns);

    #region Create styles for footer cells

    ICellStyle normalHeaderStyle = GetHeaderStyle(HorizontalAlignment.Left);
    CreateCellStyleFont(normalHeaderStyle, 11, true);
    ICellStyle normalHeaderStyleTextAlignedRight = GetHeaderStyle(HorizontalAlignment.Right);
    CreateCellStyleFont(normalHeaderStyleTextAlignedRight, 11, true);


    ICellStyle intRegularCellStyleTextAlignmentRight = GetHeaderIntStyle(HorizontalAlignment.Right);
    CreateCellStyleFont(intRegularCellStyleTextAlignmentRight, 11, true);
    ICellStyle intRegularCellStyleTextAlignmentLeft = GetHeaderIntStyle(HorizontalAlignment.Left);
    CreateCellStyleFont(intRegularCellStyleTextAlignmentLeft, 11, true);

    ICellStyle intCommaSeparatedCellStyleTextAlignmentRight = GetHeaderIntStyle(HorizontalAlignment.Right,
        "###,###,###");
    CreateCellStyleFont(intCommaSeparatedCellStyleTextAlignmentRight, 11, true);
    ICellStyle intCommaSeparatedCellStyleTextAlignmentLeft = GetHeaderIntStyle(HorizontalAlignment.Left, "###,###,###");
    CreateCellStyleFont(intCommaSeparatedCellStyleTextAlignmentLeft, 11, true);

    ICellStyle doubleCellStyleTwoDecimal = GetHeaderDoubleStyle("###,###,####.##");
    CreateCellStyleFont(doubleCellStyleTwoDecimal, 11, true);

    ICellStyle doubleCellStyleFixedTwoDecimal = GetHeaderDoubleStyle("###,###,###0.00");
    CreateCellStyleFont(doubleCellStyleFixedTwoDecimal, 11, true);

    ICellStyle doubleCellStyleThreeDecimal = GetHeaderDoubleStyle("###,###,####.###");
    CreateCellStyleFont(doubleCellStyleThreeDecimal, 11, true);

    ICellStyle doubleCellStyleFixedThreeDecimal = GetHeaderDoubleStyle("###,###,###0.000");
    CreateCellStyleFont(doubleCellStyleFixedThreeDecimal, 11, true);


    ICellStyle doubleCellStyleFourDecimal = GetHeaderDoubleStyle("###,###,####.####");
    CreateCellStyleFont(doubleCellStyleFourDecimal, 11, true);
    ICellStyle doubleCellStyleFixedFourDecimal = GetHeaderDoubleStyle("###,###,###0.0000");
    CreateCellStyleFont(doubleCellStyleFixedFourDecimal, 11, true);

    #endregion

    //Create footer row
    var columnFooterRow = Sheet.CreateRow(RowIndex++);
    columnFooterRow.Height = (short)Math.Round(1.75 * Unit);

    #region Add cells to footer row

    foreach (int val in displaycolumns)
    {
        switch (val)
        {
            case 0:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 1:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 2:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 3:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 4:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 5:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 6:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 7:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 8:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 9:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 10:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 11:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 12:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 13:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 14:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 15:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 16:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 17:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 18:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 19:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 20:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 21:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                if (!displaycolumns.Contains(22))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 22:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;

                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 23:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyleTextAlignedRight, "Total", 1);
                cellnum++;
                break;
            case 24:
                var totalOrderWeight = data.DetailData.DetailRows.Sum(x => (x.OrderWeight));
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedThreeDecimal, totalOrderWeight);
                cellnum++;

                if (!displaycolumns.Contains(25))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 25:
                var totalOpenWeight = data.DetailData.DetailRows.Sum(x => (x.OpenWeight));
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedThreeDecimal, totalOpenWeight);
                cellnum++;
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 26:
                var orderExtendedAmount = data.DetailData.DetailRows.Sum(x => (x.UnitPrice));
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, orderExtendedAmount);
                cellnum++;

                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 27:
                var totalExtendedPriceOrderQuantity = data.DetailData.DetailRows.Sum(x => (x.ExtendedPriceOrderQuantity));
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalExtendedPriceOrderQuantity);
                cellnum++;
                break;
            case 28:
                var totalExtendedPriceOpenQuantity = data.DetailData.DetailRows.Sum(x => (x.ExtendedPriceOpenQuantity));
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalExtendedPriceOpenQuantity);
                cellnum++;
                break;
            case 29:
                var totalExtendedCostOrderQuantity = data.DetailData.DetailRows.Sum(x => (x.ExtendedCostOrderQuantity));
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalExtendedCostOrderQuantity);
                cellnum++;
                break;
            case 30:
                var totalExtendedCostOpenQuantity = data.DetailData.DetailRows.Sum(x => (x.ExtendedCostOpenQuantity));
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalExtendedCostOpenQuantity);
                cellnum++;
                break;
            case 31:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 32:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesDetailUDF1Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 33:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesDetailUDF2Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 34:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesDetailUDF3Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 35:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesDetailUDF4Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 36:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesDetailUDF5Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 37:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesDetailUDF6Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 38:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesDetailUDF7Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 39:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesDetailUDF8Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 40:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesDetailUDF9Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 41:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesDetailUDF10Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 42:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 43:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 44:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 45:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 46:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesUDF1Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 47:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesUDF2Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 48:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesUDF3Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 49:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesUDF4Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 50:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesUDF5Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;

                }
                break;
            case 51:
                if (!string.IsNullOrEmpty(data.SalesInitData.SalesUDF6Label))
                {
                    CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                    cellnum++;
                }
                break;
            case 52:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 53:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 54:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
            case 55:
                CreateCell(columnFooterRow, cellnum, normalHeaderStyle, EmptyValue, 1);
                cellnum++;
                break;
        }
    }

    #endregion

    ResizeColumns(data.TotalColumns);
}