/// <summary>
/// This method will return Style of Header Cell 
/// </summary>
/// <returns></returns>
protected ICellStyle GetHeaderStyle(HorizontalAlignment alignment)
{
    var style = (XSSFCellStyle)Workbook.CreateCellStyle();
    byte[] rgb = new byte[3] { 195, 195, 195 };
    style.SetFillForegroundColor(new XSSFColor(rgb));

    style.FillPattern = FillPattern.SolidForeground;
    style.BorderBottom = BorderStyle.Thin;
    style.BottomBorderColor = IndexedColors.Black.Index;
    style.BorderLeft = BorderStyle.Thin;
    style.LeftBorderColor = IndexedColors.Black.Index;
    style.BorderRight = BorderStyle.Thin;
    style.RightBorderColor = IndexedColors.Black.Index;
    style.BorderTop = BorderStyle.Thin;
    style.TopBorderColor = IndexedColors.Black.Index;
    style.Alignment = alignment;
    style.VerticalAlignment = VerticalAlignment.Center;
    style.WrapText = false;
    return (ICellStyle)style;
}

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


/// REMOVE
ICellStyle doubleCellFooterStyleFixedTwoDecimal = GetNormalFooterDoubleStyle("###,###,###0.00");
CreateCellStyleFont(doubleCellFooterStyleFixedTwoDecimal, 11, true); -- 272



#endregion Create styles for footer cells