using NPOI.SS.UserModel;
using System;
using System.Linq;
using Bayern.CapstoneService.DAL.ExcelUtility.ResponseModels;
using NPOI.SS.Util;
using System.Collections.Generic;

/// *************** #13646 New ExportCashRequirements Method *************** ///

namespace Bayern.CapstoneService.DAL.ExcelUtility.ExportLogic
{
    public class ExportCashRequirementsLogic : ExcelExport<ExportCashRequirementsDataResponseModel>
    {

        protected override void FillSheetHeaderData(ExportCashRequirementsDataResponseModel data)
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

        protected override void FillColumnHeaderData(ExportCashRequirementsDataResponseModel data)
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

            CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Vendor Code");
            cellnum++;

            CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Vendor Name");
            cellnum++;

            CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Due Date");
            cellnum++;

            CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Discount Date");
            cellnum++;

            CreateCell(columnHeaderRow, cellnum, normalHeaderStyleTextAlignedRight, "Discount %");
            cellnum++;

            CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Transaction Type");
            cellnum++;

            CreateCell(columnHeaderRow, cellnum, normalHeaderStyle, "Document #");
            cellnum++;

            CreateCell(columnHeaderRow, cellnum, normalHeaderStyleTextAlignedRight, "Amount");
            cellnum++;

            #endregion Add header cells to the row
        }

        protected override void FillData(ExportCashRequirementsDataResponseModel data)
        {
            RowIndex++;
            List<int> displayColumns = Showcolumns(data.ColumnsToHide, data.TotalColumns);

            #region Create styles for data cells
            ICellStyle normalHeaderStyle = GetHeaderStyle(HorizontalAlignment.Left);
            CreateCellStyleFont(normalHeaderStyle, 11, true);

            ICellStyle normalStyleTextAlignmentLeft = GetNormalStyle(HorizontalAlignment.Left);
            CreateCellStyleFont(normalStyleTextAlignmentLeft);

            var normalHeaderStyleTextAlignedRight = GetHeaderStyle(HorizontalAlignment.Right);
            CreateCellStyleFont(normalHeaderStyleTextAlignedRight, 11, true);

            ICellStyle doubleCellStyleTwoDecimal = GetNormalDoubleStyle("###,###,####.##");
            CreateCellStyleFont(doubleCellStyleTwoDecimal);

            ICellStyle doubleCellStyleFixedTwoDecimal = GetNormalDoubleStyle("$ ###,###,###0.00");
            CreateCellStyleFont(doubleCellStyleFixedTwoDecimal);

            ICellStyle doubleCellStyleFixedTwoDecimalSubToal = GetHeaderDoubleStyle("$ ###,###,###0.00");
            CreateCellStyleFont(doubleCellStyleFixedTwoDecimalSubToal, 11, true);

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

            var totalVendors = (from dr in data.DetailData.DetailRows select dr.VendorId).Distinct().ToList();

            foreach (var vendor in totalVendors)
            {
                int cellnum = 0;
                IRow detailRow = null;

                var vendorDetailRows = (from dr in data.DetailData.DetailRows where dr.VendorId == vendor select dr).ToList();

                foreach (var item in vendorDetailRows)
                {
                    detailRow = Sheet.CreateRow(RowIndex);
                    cellnum = 0;

                    //Create Detail row
                    foreach (int val in displayColumns)
                    {
                        switch (val)
                        {

                            case 0:
                                CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.VendorCode);
                                cellnum++;
                                break;
                            case 1:
                                CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.VendorName);
                                cellnum++;
                                break;
                            case 2:
                                CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.DueDate.HasValue ? item.DueDate.Value.ToString("M/d/yyyy") : string.Empty);
                                cellnum++;
                                break;
                            case 3:
                                CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.DiscountDate.HasValue ? item.DiscountDate.Value.ToString("M/d/yyyy") : "N/A");
                                cellnum++;
                                break;
                            case 4:
                                if (item.DiscountDate.HasValue && item.DiscountPercentage.HasValue)
                                {
                                    CreateCell(detailRow, cellnum, doubleCellStyleFixedTwoDecimal, item.DiscountPercentage.Value);
                                }
                                else if (item.DiscountDate.HasValue && !item.DiscountPercentage.HasValue)
                                {
                                    CreateCell(detailRow, cellnum, doubleCellStyleFixedTwoDecimal, string.Empty);
                                }
                                else if (!item.DiscountDate.HasValue && !item.DiscountPercentage.HasValue)
                                {
                                    CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, "N/A");
                                }
                                cellnum++;
                                break;
                            case 5:
                                CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.TransactionTypeDescription);
                                cellnum++;
                                break;
                            case 6:
                                CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.DocumentNumber);
                                cellnum++;
                                break;
                            case 7:
                                CreateCell(detailRow, cellnum, doubleCellStyleFixedTwoDecimal, item.Amount.HasValue ? +item.Amount : 0M);
                                cellnum++;
                                break;
                        }
                    }
                    RowIndex++;
                }

                /// Create SubTotal Section
                cellnum = -1;
                var columnFooterRow = Sheet.CreateRow(RowIndex++);
                for (int i = 0; i < data.TotalColumns - 2; i++)
                {
                    cellnum++;
                    CreateCell(columnFooterRow, cellnum, normalStyleTextAlignmentLeft, EmptyValue, 1);
                }

                cellnum++;
                CreateCell(columnFooterRow, cellnum, normalHeaderStyleTextAlignedRight, "Vendor Total:", 1);

                cellnum++;
                var vendorAmount = vendorDetailRows.Sum(x => (x.Amount));
                CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimalSubToal, vendorAmount);

                /// Row separation between vendors
                RowIndex++;
                CreateCell(columnFooterRow, 0, normalStyleTextAlignmentLeft, EmptyValue, 1);
            }
        }

        protected override void FillFooterData(ExportCashRequirementsDataResponseModel data)
        {
            int cellnum = -1;

            //Create footer row
            RowIndex++;
            var columnFooterRow = Sheet.CreateRow(RowIndex);
            columnFooterRow.Height = (short)Math.Round(1.75 * Unit);

            #region Create styles for footer cells

            ICellStyle normalStyleTextAlignmentLeft = GetNormalStyle(HorizontalAlignment.Left);
            CreateCellStyleFont(normalStyleTextAlignmentLeft);

            ICellStyle normalHeaderStyle = GetHeaderStyle(HorizontalAlignment.Left);
            CreateCellStyleFont(normalHeaderStyle, 11, true);

            ICellStyle normalHeaderStyleTextAlignedRight = GetHeaderStyle(HorizontalAlignment.Right);
            CreateCellStyleFont(normalHeaderStyleTextAlignedRight, 11, true);

            ICellStyle doubleCellStyleFixedTwoDecimal = GetHeaderDoubleStyle("###,###,###0.00");
            CreateCellStyleFont(doubleCellStyleFixedTwoDecimal, 11, true);


            #endregion Create styles for footer cells


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
            CreateCell(columnFooterRow, cellnum, normalHeaderStyleTextAlignedRight, "Total All Vendors:", 1);

            cellnum++;
            var totalAllVendors = data.DetailData.DetailRows.Sum(x => (x.Amount));
            CreateCell(columnFooterRow, cellnum, doubleCellStyleFixedTwoDecimal, totalAllVendors);

            ResizeColumns(data.TotalColumns);
        }
    }
}
