
// #12285 Modify GenerateNewPackingList Method
// Line 753
public string ExportSalesOrderLines(ExportSalesOrderLinesInput input, string filePath, int userId)
{
    try
    {
        string fileName = string.Empty;
        // Excel Filename and path.
        fileName = string.Format("ExportSalesOrderLines_{0}.xlsx", DateTime.Now.ToString("MMddyyyhhmssfff"));
        string filefullPath = string.Concat(filePath, fileName);
        var totalColumnsFile = 56;
        var exportSalesOrderLinesDataResponseModel = new ExportSalesOrderLinesDataResponseModel();

        List<ExportSalesOrderLinesResult> salesOrderLinesList = new List<ExportSalesOrderLinesResult>();

        using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
        {
            ExportUserPreferenceseHelper.StoreColumnsToHideUserPreference(this.Context, userId, System.Reflection.MethodBase.GetCurrentMethod().Name, input.ColumnsToHide);
            int[] ctha = Utilities.ValidateColumnsToHide(input.ColumnsToHide, totalColumnsFile);

            var salesInitEntityAccessor = new SalesInitEntityAccessor(context);
            var salesInit = salesInitEntityAccessor.GetSalesInit();
            totalColumnsFile -= (GetExportSalesLinesOrderOptionalExtraColumnsToHideCount(salesInit, ctha) + ctha.Length);
            exportSalesOrderLinesDataResponseModel.SalesInitData = salesInit;
            exportSalesOrderLinesDataResponseModel.TotalColumns = totalColumnsFile;
            exportSalesOrderLinesDataResponseModel.ColumnsToHide = ctha;


            var rowIndex = 0;

            #region Column Headers

            var reportTitle = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = ImportExportConstants.ExportSalesOrderLines,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            var rowReportTitle = new HeaderRowDefinition();
            rowReportTitle.RowIndex = rowIndex++;
            rowReportTitle.RowCells.Add(reportTitle);
            exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowReportTitle);


            string reportExportDate = DateTime.Now.ToString("M/d/yyyy h:mm tt");
            var rowReporExportDateTime = new HeaderRowDefinition();
            var reportExportDateTime = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = "Export Date/Time: " + reportExportDate,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            rowReporExportDateTime.RowIndex = rowIndex++;
            rowReporExportDateTime.RowCells.Add(reportExportDateTime);
            exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowReporExportDateTime);

            string status = "Include: ";
            if (input.IncludeType == 1)
                status += "Only Closed Lines";
            else if (input.IncludeType == 2)
                status += "Only Open Lines";
            else
                status += "All Lines";

            var rowReporStatus = new HeaderRowDefinition();
            var reportStatus = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = status,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            rowReporStatus.RowIndex = rowIndex++;
            rowReporStatus.RowCells.Add(reportStatus);
            exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowReporStatus);

            if (input.BranchId > 0)
            {
                string branchName = context.BranchEntities.Where(x => x.BranchId == input.BranchId).Select(x => x.Name).FirstOrDefault();
                var branchNameCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Branch: {branchName}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowBranchName = new HeaderRowDefinition();
                rowIndex++;
                rowBranchName.RowIndex = rowIndex;
                rowBranchName.RowCells.Add(branchNameCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowBranchName);
            }

            if (input.WarehouseId > 0)
            {
                string warehouseName = context.WarehouseEntities.Where(x => x.WarehouseId == input.WarehouseId).Select(x => x.Name).FirstOrDefault();
                var warehouseNameCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Warehouse: {warehouseName}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowWarehouseName = new HeaderRowDefinition();
                rowIndex++;
                rowWarehouseName.RowIndex = rowIndex;
                rowWarehouseName.RowCells.Add(warehouseNameCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowWarehouseName);
            }

            if (input.SalesOrderHeaderId > 0)
            {
                var salesOrderHeaderCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Sales Order #: {input.SalesOrderHeaderId}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowSalesOrderHeaderId = new HeaderRowDefinition();
                rowIndex++;
                rowSalesOrderHeaderId.RowIndex = rowIndex;
                rowSalesOrderHeaderId.RowCells.Add(salesOrderHeaderCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowSalesOrderHeaderId);
            }

            if (input.CreatedLoginUserId > 0)
            {
                var user = (from LU in context.LoginUserEntities where LU.LoginUserId == input.CreatedLoginUserId select new { LU.Name }).DefaultIfEmpty().First();
                string userName = user.Name;

                var userNameCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Entered-By User: {userName}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUserName = new HeaderRowDefinition();
                rowIndex++;
                rowUserName.RowIndex = rowIndex;
                rowUserName.RowCells.Add(userNameCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUserName);
            }

            if (input.PrimarySalesRepId > 0)
            {
                string primarySalesRepName = context.SalesRepEntities.Where(x => x.RepId == input.PrimarySalesRepId).Select(x => x.Name).FirstOrDefault();
                var primarySalesRepCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Primary Rep: {primarySalesRepName}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowPrimarySalesRep = new HeaderRowDefinition();
                rowIndex++;
                rowPrimarySalesRep.RowIndex = rowIndex;
                rowPrimarySalesRep.RowCells.Add(primarySalesRepCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowPrimarySalesRep);
            }

            if (input.PreparedBySalesRepId > 0)
            {
                string preparedBySalesRepName = context.SalesRepEntities.Where(x => x.RepId == input.PreparedBySalesRepId).Select(x => x.Name).FirstOrDefault();
                var preparedBySalesRepCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Prepared-By Rep: {preparedBySalesRepName}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowPreparedBySalesRep = new HeaderRowDefinition();
                rowIndex++;
                rowPreparedBySalesRep.RowIndex = rowIndex;
                rowPreparedBySalesRep.RowCells.Add(preparedBySalesRepCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowPreparedBySalesRep);
            }

            if (input.InsideSalesRepGroupId > 0)
            {
                string insideSalesRepGroupName = context.SalesRepGroupEntities.Where(x => x.GroupId == input.InsideSalesRepGroupId).Select(x => x.Description).FirstOrDefault();
                var insideSalesRepGroupCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Assigned Inside Rep Group: {insideSalesRepGroupName}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowInsideSalesRepGroup = new HeaderRowDefinition();
                rowIndex++;
                rowInsideSalesRepGroup.RowIndex = rowIndex;
                rowInsideSalesRepGroup.RowCells.Add(insideSalesRepGroupCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowInsideSalesRepGroup);
            }

            if (input.InsideSalesRepId > 0)
            {
                string insideSalesRepName = context.SalesRepEntities.Where(x => x.RepId == input.InsideSalesRepId).Select(x => x.Name).FirstOrDefault();
                var insideSalesRepCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Assigned Inside Rep: {insideSalesRepName}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowInsideSalesRep = new HeaderRowDefinition();
                rowIndex++;
                rowInsideSalesRep.RowIndex = rowIndex;
                rowInsideSalesRep.RowCells.Add(insideSalesRepCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowInsideSalesRep);
            }

            if (input.OutsideSalesRepGroupId > 0)
            {
                string outsideSalesRepGroupName = context.SalesRepGroupEntities.Where(x => x.GroupId == input.OutsideSalesRepGroupId).Select(x => x.Description).FirstOrDefault();
                var preparedBySalesRepCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Assigned Outside Rep Group: {outsideSalesRepGroupName}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowPreparedBySalesRep = new HeaderRowDefinition();
                rowIndex++;
                rowPreparedBySalesRep.RowIndex = rowIndex;
                rowPreparedBySalesRep.RowCells.Add(preparedBySalesRepCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowPreparedBySalesRep);
            }

            if (input.OutsideSalesRepId > 0)
            {
                string outsideSalesRepName = context.SalesRepEntities.Where(x => x.RepId == input.OutsideSalesRepId).Select(x => x.Name).FirstOrDefault();
                var outsideSalesRepCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Assigned Outside Rep: {outsideSalesRepName}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowOutsideSalesRep = new HeaderRowDefinition();
                rowIndex++;
                rowOutsideSalesRep.RowIndex = rowIndex;
                rowOutsideSalesRep.RowCells.Add(outsideSalesRepCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowOutsideSalesRep);
            }


            if (input.OrderDateLow != null || input.OrderDateHigh != null)
            {
                rowIndex++;
                var reportDate = string.Empty;
                if (input.OrderDateLow != null && input.OrderDateLow == input.OrderDateHigh)
                    reportDate = $"Order Date: {input.OrderDateLow.Value.ToString("M/d/yyyy")}";
                if (input.OrderDateLow != null && input.OrderDateHigh != null && input.OrderDateLow < input.OrderDateHigh)
                    reportDate = $"Order Date: {input.OrderDateLow.Value.ToString("M/d/yyyy")} thru {input.OrderDateHigh.Value.ToString("M/d/yyyy")}";
                if (input.OrderDateLow != null && input.OrderDateHigh == null)
                    reportDate = $"Order Date: On or after {input.OrderDateLow.Value.ToString("M/d/yyyy")}";
                if (input.OrderDateLow == null && input.OrderDateHigh != null)
                    reportDate = $"Order Date: On or before {input.OrderDateHigh.Value.ToString("M/d/yyyy")}";

                var reportDateTime = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = reportDate,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowReporDateTime = new HeaderRowDefinition();

                rowReporDateTime.RowIndex = rowIndex;
                rowReporDateTime.RowCells.Add(reportDateTime);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowReporDateTime);
            }

            if (input.TargetedDateLow != null || input.TargetedDateHigh != null)
            {
                rowIndex++;
                var reportDate = string.Empty;
                if (input.TargetedDateLow != null && input.TargetedDateLow == input.TargetedDateHigh)
                {
                    if (salesInit.SalesOrderTargetedDateType == 1)
                    {
                        reportDate =
                            $"Targeted Delivery/Pick-Up Date: {input.TargetedDateLow.Value.ToString("M/d/yyyy")}";
                    }
                    else
                    {
                        reportDate =
                            $"Targeted Shipment/Pick-Up Date: {input.TargetedDateLow.Value.ToString("M/d/yyyy")}";
                    }
                }

                if (input.TargetedDateLow != null && input.TargetedDateHigh != null &&
                    input.TargetedDateLow < input.TargetedDateHigh)
                {
                    if (salesInit.SalesOrderTargetedDateType == 1)
                    {
                        reportDate =
                            $"Targeted Delivery/Pick-Up Date: {input.TargetedDateLow.Value.ToString("M/d/yyyy")} thru {input.TargetedDateHigh.Value.ToString("M/d/yyyy")}";
                    }
                    else
                    {
                        reportDate =
                            $"Targeted Shipment/Pick-Up Date: {input.TargetedDateLow.Value.ToString("M/d/yyyy")} thru {input.TargetedDateHigh.Value.ToString("M/d/yyyy")}";
                    }
                }

                if (input.TargetedDateLow != null && input.TargetedDateHigh == null)
                {
                    if (salesInit.SalesOrderTargetedDateType == 1)
                    {
                        reportDate =
                            $"Targeted Delivery/Pick-Up Date: On or after {input.TargetedDateLow.Value.ToString("M/d/yyyy")}";
                    }
                    else
                    {
                        reportDate =
                            $"Targeted Shipment/Pick-Up Date: On or after {input.TargetedDateLow.Value.ToString("M/d/yyyy")}";
                    }
                }

                if (input.TargetedDateLow == null && input.TargetedDateHigh != null)
                {
                    if (salesInit.SalesOrderTargetedDateType == 1)
                    {
                        reportDate =
                            $"Targeted Delivery/Pick-Up Date: On or before {input.TargetedDateHigh.Value.ToString("M/d/yyyy")}";
                    }
                    else
                    {
                        reportDate =
                            $"Targeted Shipment/Pick-Up Date: On or before {input.TargetedDateHigh.Value.ToString("M/d/yyyy")}";
                    }
                }


                var reportDateTime = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = reportDate,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowReporDateTime = new HeaderRowDefinition();

                rowReporDateTime.RowIndex = rowIndex;
                rowReporDateTime.RowCells.Add(reportDateTime);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowReporDateTime);
            }

            if (input.CustomerGroupId > 0)
            {
                rowIndex++;
                string customerGroupDescription = context.CustomerGroupEntities.Where(x => x.GroupId == input.CustomerGroupId).Select(x => x.Description).FirstOrDefault();
                var customerGroupCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Customer Group: {customerGroupDescription}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowCustomerGroup = new HeaderRowDefinition();
                rowCustomerGroup.RowIndex = rowIndex;
                rowCustomerGroup.RowCells.Add(customerGroupCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowCustomerGroup);
            }

            if (input.CustomerId > 0)
            {
                rowIndex++;
                string customerCode = context.CustomerEntities.Where(x => x.CustomerId == input.CustomerId).Select(x => x.CustomerCode).FirstOrDefault();
                var customerNameCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Customer Code: {customerCode}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowCustomerName = new HeaderRowDefinition();
                rowCustomerName.RowIndex = rowIndex;
                rowCustomerName.RowCells.Add(customerNameCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowCustomerName);
            }

            if (input.CategoryGroupId > 0)
            {
                rowIndex++;
                string categoryGroupDescription = context.CategoryGroupEntities.Where(x => x.GroupId == input.CategoryGroupId).Select(x => x.Description).FirstOrDefault();
                var categoryGroupCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Category Group: {categoryGroupDescription}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowCategoryGroup = new HeaderRowDefinition();
                rowCategoryGroup.RowIndex = rowIndex;
                rowCategoryGroup.RowCells.Add(categoryGroupCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowCategoryGroup);
            }

            if (input.CategoryId > 0)
            {
                rowIndex++;
                string categoryDescription = context.CategoryEntities.Where(x => x.CategoryId == input.CategoryId).Select(x => x.Description).FirstOrDefault();
                var categoryCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Category: {categoryDescription}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowCategory = new HeaderRowDefinition();
                rowCategory.RowIndex = rowIndex;
                rowCategory.RowCells.Add(categoryCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowCategory);
            }

            if (input.ItemId > 0)
            {
                rowIndex++;
                string itemCode = context.ItemEntities.Where(x => x.ItemId == input.ItemId).Select(x => x.ItemCode).FirstOrDefault();
                var categoryCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Item Code: {itemCode}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowCategory = new HeaderRowDefinition();
                rowCategory.RowIndex = rowIndex;
                rowCategory.RowCells.Add(categoryCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowCategory);
            }

            if (!string.IsNullOrEmpty(input.SalesOrderDetailUDF1))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF1Label}: {input.SalesOrderDetailUDF1}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.SalesOrderDetailUDF2))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF2Label}: {input.SalesOrderDetailUDF2}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.SalesOrderDetailUDF3))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF3Label}: {input.SalesOrderDetailUDF3}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.SalesOrderDetailUDF4))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF4Label}: {input.SalesOrderDetailUDF4}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.SalesOrderDetailUDF5))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF5Label}: {input.SalesOrderDetailUDF5}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.SalesOrderDetailUDF6))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF6Label}: {input.SalesOrderDetailUDF6}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.SalesOrderDetailUDF7))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF7Label}: {input.SalesOrderDetailUDF7}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.SalesOrderDetailUDF8))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF8Label}: {input.SalesOrderDetailUDF8}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.SalesOrderDetailUDF9))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF9Label}: {input.SalesOrderDetailUDF9}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.SalesOrderDetailUDF10))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF10Label}: {input.SalesOrderDetailUDF10}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            var reportHeaderRowSeparation = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = string.Empty,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            var rowSeparator = new HeaderRowDefinition();
            rowSeparator.RowIndex = rowIndex++;
            rowSeparator.RowCells.Add(reportHeaderRowSeparation);
            exportSalesOrderLinesDataResponseModel.HeaderData.Rows.Add(rowSeparator);

            #endregion

            #region GetData
            int? includeType = input.IncludeType;
            int? branchId = ((input.BranchId ?? 0) == 0) ? null : input.BranchId;
            int? warehouseId = ((input.WarehouseId ?? 0) == 0) ? null : input.WarehouseId;
            int? salesOrderHeaderId = ((input.SalesOrderHeaderId ?? 0) == 0) ? null : input.SalesOrderHeaderId;
            int? insideSalesRepGroupId = ((input.InsideSalesRepGroupId ?? 0) == 0) ? null : input.InsideSalesRepGroupId;
            int? insideSalesRepId = ((input.InsideSalesRepId ?? 0) == 0) ? null : input.InsideSalesRepId;
            int? outsideSalesRepGroupId = ((input.OutsideSalesRepGroupId ?? 0) == 0) ? null : input.OutsideSalesRepGroupId;
            int? outsideSalesRepId = ((input.OutsideSalesRepId ?? 0) == 0) ? null : input.OutsideSalesRepId;
            int? primarySalesRepId = ((input.PrimarySalesRepId ?? 0) == 0) ? null : input.PrimarySalesRepId;
            int? createdLoginUserId = ((input.CreatedLoginUserId ?? 0) == 0) ? null : input.CreatedLoginUserId;
            int? preparedBySalesRepId = ((input.PreparedBySalesRepId ?? 0) == 0) ? null : input.PreparedBySalesRepId;
            int? customerGroupId = ((input.CustomerGroupId ?? 0) == 0) ? null : input.CustomerGroupId;
            int? customerId = ((input.CustomerId ?? 0) == 0) ? null : input.CustomerId;
            int? categoryGroupId = ((input.CategoryGroupId ?? 0) == 0) ? null : input.CategoryGroupId;
            int? categoryId = ((input.CategoryId ?? 0) == 0) ? null : input.CategoryId;
            int? itemId = ((input.ItemId ?? 0) == 0) ? null : input.ItemId;
            string salesOrderDetailUDF1 = input.SalesOrderDetailUDF1 == string.Empty ? null : input.SalesOrderDetailUDF1;
            string salesOrderDetailUDF2 = input.SalesOrderDetailUDF2 == string.Empty ? null : input.SalesOrderDetailUDF2;
            string salesOrderDetailUDF3 = input.SalesOrderDetailUDF3 == string.Empty ? null : input.SalesOrderDetailUDF3;
            string salesOrderDetailUDF4 = input.SalesOrderDetailUDF4 == string.Empty ? null : input.SalesOrderDetailUDF4;
            string salesOrderDetailUDF5 = input.SalesOrderDetailUDF5 == string.Empty ? null : input.SalesOrderDetailUDF5;
            string salesOrderDetailUDF6 = input.SalesOrderDetailUDF6 == string.Empty ? null : input.SalesOrderDetailUDF6;
            string salesOrderDetailUDF7 = input.SalesOrderDetailUDF7 == string.Empty ? null : input.SalesOrderDetailUDF7;
            string salesOrderDetailUDF8 = input.SalesOrderDetailUDF8 == string.Empty ? null : input.SalesOrderDetailUDF8;
            string salesOrderDetailUDF9 = input.SalesOrderDetailUDF9 == string.Empty ? null : input.SalesOrderDetailUDF9;
            string salesOrderDetailUDF10 = input.SalesOrderDetailUDF10 == string.Empty ? null : input.SalesOrderDetailUDF10;

            DateTime? _targettedLowDate = input.TargetedDateLow.HasValue ? input.TargetedDateLow.Value.Date : (DateTime?)null;
            DateTime? _targettedHighDate = input.TargetedDateHigh.HasValue ? input.TargetedDateHigh.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

            DateTime? _orderLowDate = input.OrderDateLow.HasValue ? input.OrderDateLow.Value.Date : (DateTime?)null;
            DateTime? _OrderHighDate = input.OrderDateHigh.HasValue ? input.OrderDateHigh.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

            var exportSalesOrderLinesList = context.ExportSalesOrderLines(
                warehouseId: warehouseId,
                includeType: includeType,
                branchId: branchId,
                insideSalesRepGroupId: insideSalesRepGroupId,
                insideSalesRepId: insideSalesRepId,
                outsideSalesRepGroupId: outsideSalesRepGroupId,
                outsideSalesRepId: outsideSalesRepId,
                orderDateLow: _orderLowDate,
                orderDateHigh: _OrderHighDate,
                targetedDateLow: _targettedLowDate,
                targetedDateHigh: _targettedHighDate,
                customerGroupId: customerGroupId,
                customerId: customerId,
                primarySalesRepId: primarySalesRepId,
                createdLoginUserId: createdLoginUserId,
                preparedBySalesRepId: preparedBySalesRepId,
                categoryGroupId: categoryGroupId,
                categoryId: categoryId,
                itemId: itemId,
                salesOrderDetailUDF1: salesOrderDetailUDF1,
                salesOrderDetailUDF2: salesOrderDetailUDF2,
                salesOrderDetailUDF3: salesOrderDetailUDF3,
                salesOrderDetailUDF4: salesOrderDetailUDF4,
                salesOrderDetailUDF5: salesOrderDetailUDF5,
                salesOrderDetailUDF6: salesOrderDetailUDF6,
                salesOrderDetailUDF7: salesOrderDetailUDF7,
                salesOrderDetailUDF8: salesOrderDetailUDF8,
                salesOrderDetailUDF9: salesOrderDetailUDF9,
                salesOrderDetailUDF10: salesOrderDetailUDF10,
                salesOrderHeaderId: salesOrderHeaderId
                ).ToList();
            #endregion GetData
            if (!exportSalesOrderLinesList.Any())
                return string.Empty;

            // Step	3	-	For	Each	record	found	in	Step	2	do	the	following.
            if (exportSalesOrderLinesList.Count > 0)
            {
                foreach (var salesOrderLine in exportSalesOrderLinesList)
                {
                    decimal? openExtendedAmount = 0m;
                    decimal? openExtendedCost = 0;
                    decimal? openWeight = 0;

                    decimal? orderQuantity = salesOrderLine.OrderQuantity;
                    decimal? shippedQuantity = salesOrderLine.ShippedQuantity;
                    decimal openQuantity = (orderQuantity - shippedQuantity) ?? 0m;
                    var orderExtendedAmount = salesOrderLine.UnitPriceExtended;
                    openExtendedAmount = orderExtendedAmount - salesOrderLine.ExtendedPrice;
                    var orderWeight = salesOrderLine.TotalLineWeight;
                    var orderExtendedCost = salesOrderLine.UnitCostExtended;
                    decimal? marginPercentage = (orderExtendedAmount - orderExtendedCost);

                    // This to avoid aritmetic exception divideByZero.
                    if (orderExtendedAmount > 0)
                    {
                        marginPercentage = marginPercentage / orderExtendedAmount;
                    }

                    if (openQuantity < 0 || openExtendedAmount < 0)
                    {
                        openQuantity = 0;
                        openExtendedAmount = 0;
                        openExtendedCost = 0;
                        openWeight = 0;

                        if (input.IncludeType == 2)
                        {
                            // 	Do	not	include	this	record	in	the	excel	sheet. 
                            continue;
                        }
                    }
                    else
                    {
                        openExtendedCost = openExtendedAmount - (marginPercentage * openExtendedAmount);
                        openExtendedCost = Math.Round(openExtendedCost ?? 0, 2, MidpointRounding.AwayFromZero);


                        if (salesOrderLine.OrderQuantityUOM == UOMConstants.LB ||
                            salesOrderLine.OrderQuantityUOM == UOMConstants.KG)
                        {

                            openWeight = openQuantity;
                        }
                        else
                        {
                            openWeight = (openQuantity / (orderQuantity > 0 ? orderQuantity : 1)) * orderWeight;
                            openWeight = Math.Round(openWeight ?? 0, 3, MidpointRounding.AwayFromZero);
                        }
                    }

                    salesOrderLine.OrderWeight = salesOrderLine.TotalLineWeight;
                    salesOrderLine.OpenQuantity = openQuantity;
                    salesOrderLine.OpenWeight = openWeight ?? 0.0m;
                    salesOrderLine.UnitPrice = salesOrderLine.UnitPrice;
                    salesOrderLine.ExtendedPriceOrderQuantity = orderExtendedAmount;
                    salesOrderLine.ExtendedPriceOpenQuantity = openExtendedAmount ?? 0.0m;
                    salesOrderLine.ExtendedCostOrderQuantity = orderExtendedCost;
                    salesOrderLine.ExtendedCostOpenQuantity = openExtendedCost ?? 0;
                    salesOrderLine.MarginPercentage = marginPercentage.HasValue ? (marginPercentage.Value * 100) : 0;

                    salesOrderLinesList.Add(salesOrderLine);
                }
            }
            else
            {
                return string.Empty;
            }
        }


        exportSalesOrderLinesDataResponseModel.DetailData.DetailRows.AddRange(salesOrderLinesList);

        var workbook = (new ExportSalesOrderLinesLogic()).ExportToExcel(
            exportSalesOrderLinesDataResponseModel.TotalColumns - 1, ImportExportConstants.ExportSalesOrderLines,
            exportSalesOrderLinesDataResponseModel);

        Shared.Utilities.OutPutDestinationPathValidator.ValidateDestinationPath(filePath);

        using (var fileStream = new FileStream(filefullPath, FileMode.Create, FileAccess.Write))
        {
            workbook.Write(fileStream);
            return fileName;
        }
    }
    catch (OutOfMemoryException ome)
    {
        throw new CapstoneException(BusinessLogicException.ExportToExcelFailed, "Too many rows returned. Please narrow the filter criteria.");
    }

}


// Step 3b
CustomerBranchInsideSalesRepEntityAccessor
    - CheckSalesRepForCustomerAndBranch() //Line 171


