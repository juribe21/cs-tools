public void ExportSalesQuoteLines(SalesQuoteLines input, string filePath, int userId, RunningReport runningReport)
{
    try
    {
        string fileName = string.Empty;
        // Excel Filename and path.
        fileName = string.Format("ExportSalesQuoteLines{0}.xlsx", DateTime.Now.ToString("MMddyyyhhmssfff"));
        string filefullPath = string.Concat(filePath, fileName);
        var totalColumnsFile = 52;
        var exportSalesQuoteLinesDataResponseModel = new ExportSalesQuoteLinesResponseModel();

        List<SalesQuoteLinesResult> salesQuoteLinesResult = new List<SalesQuoteLinesResult>();
        SalesQuoteLinesResult saleQuote = null;

        using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
        {
            ExportUserPreferenceseHelper.StoreColumnsToHideUserPreference(this.Context, userId, System.Reflection.MethodBase.GetCurrentMethod().Name, input.ColumnsToHide);

            #region Check UDF to Hide

            var salesInitEntityAccessor = new SalesInitEntityAccessor(context);
            var salesInit = salesInitEntityAccessor.GetSalesInit();
            if (salesInit != null)
            {

                if (string.IsNullOrEmpty(salesInit.SalesDetailUDF1Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "28" : ",28";
                if (string.IsNullOrEmpty(salesInit.SalesDetailUDF2Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "29" : ",29";
                if (string.IsNullOrEmpty(salesInit.SalesDetailUDF3Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "30" : ",30";
                if (string.IsNullOrEmpty(salesInit.SalesDetailUDF4Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "31" : ",31";
                if (string.IsNullOrEmpty(salesInit.SalesDetailUDF5Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "32" : ",32";
                if (string.IsNullOrEmpty(salesInit.SalesDetailUDF6Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "33" : ",33";
                if (string.IsNullOrEmpty(salesInit.SalesDetailUDF7Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "34" : ",34";
                if (string.IsNullOrEmpty(salesInit.SalesDetailUDF8Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "35" : ",35";
                if (string.IsNullOrEmpty(salesInit.SalesDetailUDF9Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "36" : ",36";
                if (string.IsNullOrEmpty(salesInit.SalesDetailUDF10Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "37" : ",37";

                if (string.IsNullOrEmpty(salesInit.SalesUDF1Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "42" : ",42";
                if (string.IsNullOrEmpty(salesInit.SalesUDF2Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "43" : ",43";
                if (string.IsNullOrEmpty(salesInit.SalesUDF3Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "44" : ",44";
                if (string.IsNullOrEmpty(salesInit.SalesUDF4Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "45" : ",45";
                if (string.IsNullOrEmpty(salesInit.SalesUDF5Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "46" : ",46";
                if (string.IsNullOrEmpty(salesInit.SalesUDF6Label))
                    input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "47" : ",47";
            }

            #endregion

            int[] ctha = Utilities.ValidateColumnsToHide(input.ColumnsToHide, totalColumnsFile);

            totalColumnsFile -= (GetExportSalesaColumnsToHide.GetExportSalesLinesOrderOptionalExtraColumnsToHideCount(salesInit, ctha) + ctha.Length);
            exportSalesQuoteLinesDataResponseModel.SalesInitData = salesInit;
            exportSalesQuoteLinesDataResponseModel.TotalColumns = totalColumnsFile;
            exportSalesQuoteLinesDataResponseModel.ColumnsToHide = ctha;
            exportSalesQuoteLinesDataResponseModel.cusInit = Context.CustomerInitEntities.FirstOrDefault();

            var rowIndex = 0;

            #region Column Headers

            #region Title
            var reportTitle = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = ImportExportConstants.ExportSalesQuoteLines,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            var rowReportTitle = new HeaderRowDefinition();
            rowReportTitle.RowIndex = rowIndex++;
            rowReportTitle.RowCells.Add(reportTitle);
            exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowReportTitle);
            #endregion Title

            #region ReportDate
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
            exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowReporExportDateTime);
            #endregion ReportDate

            #region CreditcardAccount
            exportCreditCardTransactionsResponseModel.Currencies = context.CurrencyEntities.Count();

            if (exportCreditCardTransactionsResponseModel.Currencies > 1)
            {
                var reportCreditcardAccount = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = "Credit card Account: " + context.CreditCardAccountEntities.Where(x => x.CreditCardAccountId == input.CreditCardAccountId).FirstOrDefault().Name,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowCreditcardAccount = new HeaderRowDefinition();
                rowCreditcardAccount.RowIndex = rowIndex + 1;
                rowCreditcardAccount.RowCells.Add(reportCreditcardAccount);
                exportCreditCardTransactionsResponseModel.HeaderData.Rows.Add(rowCreditcardAccount);
            }


            #endregion CreditcardAccount

            #region Status
            string status = "Include: ";
            if (input.IncludeType == 1)
                status += "All quotes";
            else if (input.IncludeType == 2)
                status += "	Only quotes that have been converted to an order";
            else
                status += "Only quotes that have not been converted to an order";
            var rowReporStatus = new HeaderRowDefinition();
            var reportStatus = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = status,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            rowReporStatus.RowIndex = rowIndex++;
            rowReporStatus.RowCells.Add(reportStatus);
            exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowReporStatus);
            #endregion Status

            #region Currency
            exportVendorTransactionAgingResponseModel.Currencies = context.CurrencyEntities.Count();

            if (exportVendorTransactionAgingResponseModel.Currencies > 1)
            {
                var reportCurrency = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = "Currency: " + context.CurrencyEntities.Where(x => x.CurrencyId == input.CurrencyId).FirstOrDefault().Description,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowCurrency = new HeaderRowDefinition();
                rowCurrency.RowIndex = rowIndex + 1;
                rowCurrency.RowCells.Add(reportCurrency);
                exportVendorTransactionAgingResponseModel.HeaderData.Rows.Add(rowCurrency);

            }
            #endregion Currency

            #region QuoteStatus
            string quotestatus = "Quote Status: ";
            if (input.IncludeExpiredQuotesFlag)
            {
                quotestatus += "All Quotes";
            }
            else
            {
                quotestatus += "Not Expired";
            }
            var rowReporQuoteStatus = new HeaderRowDefinition();
            var reportQuoteStatus = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = quotestatus,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            rowReporQuoteStatus.RowIndex = rowIndex++;
            rowReporQuoteStatus.RowCells.Add(reportQuoteStatus);
            exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowReporQuoteStatus);
            #endregion QuoteStatus

            #region BranchId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowBranchName);
            }
            #endregion BranchId

            #region WarehouseId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowWarehouseName);
            }
            #endregion WarehouseId

            #region CreatedLoginUserId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUserName);
            }
            #endregion CreatedLoginUserId

            #region PrimarySalesRepId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowPrimarySalesRep);
            }
            #endregion PrimarySalesRepId

            #region InsideSalesRepGroupId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowInsideSalesRepGroup);
            }
            #endregion InsideSalesRepGroupId

            #region InsideSalesRepId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowInsideSalesRep);
            }
            #endregion InsideSalesRepId

            #region OutsideSalesRepGroupId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowPreparedBySalesRep);
            }
            #endregion OutsideSalesRepGroupId

            #region OutsideSalesRepId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowOutsideSalesRep);
            }
            #endregion OutsideSalesRepId

            #region PreparedBySalesRepId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowPreparedBySalesRep);
            }
            #endregion PreparedBySalesRepId

            #region QuoteDate
            if (input.QuoteDateLow != null && input.QuoteDateHigh != null)
            {
                rowIndex++;
                var reportQuoteDate = string.Empty;
                if (input.QuoteDateLow == input.QuoteDateHigh)
                {
                    reportQuoteDate = $"Quote Date: {input.QuoteDateLow.Value.ToString("M/d/yyyy")}";
                }
                if (input.QuoteDateLow != null && input.QuoteDateHigh != null && input.QuoteDateLow < input.QuoteDateHigh)
                {
                    reportQuoteDate = $"Quote Date: {input.QuoteDateLow.Value.ToString("M/d/yyyy")} thru {input.QuoteDateHigh.Value.ToString("M/d/yyyy")}";
                }
                if (input.QuoteDateLow != null && input.QuoteDateHigh == null)
                {
                    reportQuoteDate = $"Quote Date: On or after {input.QuoteDateLow.Value.ToString("M/d/yyyy")}";
                }
                if (input.QuoteDateLow == null && input.QuoteDateHigh != null)
                {
                    reportQuoteDate = $"Quote Date: On or before {input.QuoteDateHigh.Value.ToString("M/d/yyyy")}";
                }

                var reportHeaderQuoteDate = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = reportQuoteDate,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowReporDateTime = new HeaderRowDefinition();

                rowReporDateTime.RowIndex = rowIndex;
                rowReporDateTime.RowCells.Add(reportHeaderQuoteDate);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowReporDateTime);
            }
            #endregion QuoteDate

            #region AnticipatedDate // CHECK IF DATES ARE NULLABLES
            if (input.AnticipatedDateLow != null && input.AnticipatedDateHigh != null)
            {
                rowIndex++;
                var reportAnticipateDate = "Anticipated Ready Date: ";
                if (input.AnticipatedDateLow == input.AnticipatedDateHigh)
                {
                    reportAnticipateDate += $"{input.AnticipatedDateLow.Value.ToString("M/d/yyyy")}";
                }
                if (input.AnticipatedDateLow != null && input.AnticipatedDateHigh != null && input.AnticipatedDateLow < input.AnticipatedDateHigh)
                {
                    reportAnticipateDate += $"{input.AnticipatedDateLow.Value.ToString("M/d/yyyy")} thru {input.AnticipatedDateHigh.Value.ToString("M/d/yyyy")}";
                }
                if (input.AnticipatedDateLow != null && input.AnticipatedDateHigh == null)
                {
                    reportAnticipateDate += $"On or after {input.AnticipatedDateLow.Value.ToString("M/d/yyyy")}";
                }
                if (input.AnticipatedDateLow == null && input.AnticipatedDateHigh != null)
                {
                    reportAnticipateDate += $"On or before {input.AnticipatedDateHigh.Value.ToString("M/d/yyyy")}";
                }

                var reportHeaderAnticipatedDate = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = reportAnticipateDate,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowReporDateTime = new HeaderRowDefinition();

                rowReporDateTime.RowIndex = rowIndex;
                rowReporDateTime.RowCells.Add(reportHeaderAnticipatedDate);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowReporDateTime);
            }
            #endregion AnticipatedDate

            #region TransactionDate CHECK IF DATES ARE NULLABLES
            if (input.LowTransactionDate != null || input.HighTransactionDate != null)
            {
                rowIndex++;
                var reportAnticipateDate = "Transaction Date: ";
                if (input.LowTransactionDate == input.HighTransactionDate)
                {
                    reportAnticipateDate += $"{input.LowTransactionDate.Value.ToString("M/d/yyyy")}";
                }
                if (input.LowTransactionDate != null && input.HighTransactionDate != null && input.LowTransactionDate < input.HighTransactionDate)
                {
                    reportAnticipateDate += $"{input.LowTransactionDate.Value.ToString("M/d/yyyy")} thru {input.HighTransactionDate.Value.ToString("M/d/yyyy")}";
                }
                if (input.LowTransactionDate != null && input.HighTransactionDate == null)
                {
                    reportAnticipateDate += $"On or after {input.LowTransactionDate.Value.ToString("M/d/yyyy")}";
                }
                if (input.LowTransactionDate == null && input.HighTransactionDate != null)
                {
                    reportAnticipateDate += $"On or before {input.HighTransactionDate.Value.ToString("M/d/yyyy")}";
                }

                var reportHeaderAnticipatedDate = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = reportAnticipateDate,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowReporDateTime = new HeaderRowDefinition();

                rowReporDateTime.RowIndex = rowIndex;
                rowReporDateTime.RowCells.Add(reportHeaderAnticipatedDate);
                exportBankTransactionsResponseModel.HeaderData.Rows.Add(rowReporDateTime);
            }
            #endregion TransactionDate

            #region ExpirationDate
            if (input.ExpirationDateLow != null && input.ExpirationDateHigh != null)
            {
                rowIndex++;
                var reportExpirationDate = "Expiration Date: ";
                if (input.ExpirationDateLow == input.ExpirationDateHigh)
                {
                    reportExpirationDate += $"{input.ExpirationDateLow.Value.ToString("M/d/yyyy")}";
                }
                if (input.ExpirationDateLow != null && input.ExpirationDateHigh != null && input.ExpirationDateLow < input.ExpirationDateHigh)
                {
                    reportExpirationDate += $"{input.ExpirationDateLow.Value.ToString("M/d/yyyy")} thru {input.ExpirationDateHigh.Value.ToString("M/d/yyyy")}";
                }
                if (input.ExpirationDateLow != null && input.ExpirationDateHigh == null)
                {
                    reportExpirationDate += $"On or after {input.ExpirationDateLow.Value.ToString("M/d/yyyy")}";
                }
                if (input.ExpirationDateLow == null && input.ExpirationDateHigh != null)
                {
                    reportExpirationDate += $"On or before {input.ExpirationDateHigh.Value.ToString("M/d/yyyy")}";
                }

                var reportHeaderExpirationDate = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = reportExpirationDate,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowReporDateTime = new HeaderRowDefinition();

                rowReporDateTime.RowIndex = rowIndex;
                rowReporDateTime.RowCells.Add(reportHeaderExpirationDate);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowReporDateTime);
            }
            #endregion ExpirationDate

            #region VendorGroup
            if (input.VendorGroupId > 0)
            {
                rowIndex++;
                string vendorGroupDescription = context.VendorGroupEntities.Where(x => x.GroupId == input.VendorGroupId).Select(x => x.Description).FirstOrDefault();
                var vendorGroupCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Vendor Group: {vendorGroupDescription}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowVendorGroup = new HeaderRowDefinition();
                rowVendorGroup.RowIndex = rowIndex;
                rowVendorGroup.RowCells.Add(vendorGroupCell);
                exportVendorTransactionAgingResponseModel.HeaderData.Rows.Add(rowVendorGroup);
            }
            #endregion VendorGroup

            #region Vendor
            if (input.VendorId > 0)
            {
                rowIndex++;
                string vendorName = context.VendorEntities.Where(x => x.VendorId == input.VendorId).Select(x => x.VendorName).FirstOrDefault();
                var vendorGroupCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Vendor: {vendorName}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowVendor = new HeaderRowDefinition();
                rowVendor.RowIndex = rowIndex;
                rowVendor.RowCells.Add(vendorGroupCell);
                exportVendorTransactionAgingResponseModel.HeaderData.Rows.Add(rowVendor);
            }
            #endregion Vendor

            #region CustomerGroupId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowCustomerGroup);
            }
            #endregion CustomerGroupId

            #region CustomerId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowCustomerName);
            }
            #endregion CustomerId

            #region CategoryGroupId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowCategoryGroup);
            }
            #endregion CategoryGroupId

            #region CategoryId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowCategory);
            }
            #endregion CategoryId

            #region ItemId
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
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowCategory);
            }
            #endregion ItemId

            #region SalesQuoteDetailUDFs

            if (!string.IsNullOrEmpty(input.UDF1))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF1Label}: {input.UDF1}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.UDF2))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF2Label}: {input.UDF2}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.UDF3))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF3Label}: {input.UDF3}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.UDF4))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF4Label}: {input.UDF4}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.UDF5))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF5Label}: {input.UDF5}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.UDF6))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF6Label}: {input.UDF6}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.UDF7))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF7Label}: {input.UDF7}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.UDF8))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF8Label}: {input.UDF8}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.UDF9))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF9Label}: {input.UDF9}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            if (!string.IsNullOrEmpty(input.UDF10))
            {
                rowIndex++;
                string udf = $"{salesInit.SalesDetailUDF10Label}: {input.UDF10}";
                var udfCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = udf,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowUDF = new HeaderRowDefinition();
                rowUDF.RowIndex = rowIndex;
                rowUDF.RowCells.Add(udfCell);
                exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowUDF);
            }

            #endregion SalesQuoteDetailUDFs

            #region AsOfDate
            rowIndex++;

            var asOfDateCell = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = $"AsOfDate: {input.AsOfDate.ToString("M/d/yyyy")}",
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            var rowAsOfDate = new HeaderRowDefinition();
            rowAsOfDate.RowIndex = rowIndex;
            rowAsOfDate.RowCells.Add(asOfDateCell);
            exportCustomerTransactionAgingResponseModel.HeaderData.Rows.Add(rowAsOfDate);
            #endregion AsOfDate

            #region PaymentMethodTypeEnum
            if (input.TransactionType.HasValue && input.TransactionType > 0)
            {
                string transactionType = "Transaction Type: ";
                if (input.TransactionType == (int)VendorTransactionTypeEnum.VendorCredit)
                    transactionType += "Credit";
                if (input.TransactionType == (int)VendorTransactionTypeEnum.VendorDebit)
                    transactionType += "Debit";
                if (input.TransactionType == (int)VendorTransactionTypeEnum.VendorPayment)
                    transactionType += "Payment";
                if (input.TransactionType == (int)VendorTransactionTypeEnum.Refund)
                    transactionType += "Refund";
                if (input.TransactionType == (int)VendorTransactionTypeEnum.DiscountTaken)
                    transactionType += "Discount Taken";

                var rowTransactionType = new HeaderRowDefinition();
                var reportTransactionType = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = transactionType,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                rowTransactionType.RowIndex = rowIndex++;
                rowTransactionType.RowCells.Add(reportTransactionType);
                vendorTransactionsDataResponseModel.HeaderData.Rows.Add(rowTransactionType);
            }
            #endregion PaymentMethodTypeEnum

            #region reportRowseparation
            var reportHeaderRowSeparation = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = string.Empty,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            var rowSeparator = new HeaderRowDefinition();
            rowSeparator.RowIndex = rowIndex++;
            rowSeparator.RowCells.Add(reportHeaderRowSeparation);
            exportSalesQuoteLinesDataResponseModel.HeaderData.Rows.Add(rowSeparator);
            #endregion reportRowseparation

            #endregion Column Headers

            #region GetDatas

            bool IncludeExpiredQuotesFlag = input.IncludeExpiredQuotesFlag;
            int? includeType = input.IncludeType;
            int? salesQuoteHeaderId = ((input.SalesQuoteHeaderId ?? 0) == 0) ? null : input.SalesQuoteHeaderId;
            int? BranchId = ((input.BranchId ?? 0) == 0) ? null : input.BranchId;
            int? WarehouseId = ((input.WarehouseId ?? 0) == 0) ? null : input.WarehouseId;
            int? CreatedLoginUserId = ((input.CreatedLoginUserId ?? 0) == 0) ? null : input.CreatedLoginUserId;
            int? PrimarySalesRepId = ((input.PrimarySalesRepId ?? 0) == 0) ? null : input.PrimarySalesRepId;
            int? InsideSalesRepGroupId = ((input.InsideSalesRepGroupId ?? 0) == 0) ? null : input.InsideSalesRepGroupId;
            int? InsideSalesRepId = ((input.InsideSalesRepId ?? 0) == 0) ? null : input.InsideSalesRepId;
            int? OutsideSalesRepGroupId = ((input.OutsideSalesRepGroupId ?? 0) == 0) ? null : input.OutsideSalesRepGroupId;
            int? OutsideSalesRepId = ((input.OutsideSalesRepId ?? 0) == 0) ? null : input.OutsideSalesRepId;
            int? PreparedBySalesRepId = ((input.PreparedBySalesRepId ?? 0) == 0) ? null : input.PreparedBySalesRepId;
            int? CustomerGroupId = ((input.CustomerGroupId ?? 0) == 0) ? null : input.CustomerGroupId;
            int? CustomerId = ((input.CustomerId ?? 0) == 0) ? null : input.CustomerId;
            int? CategoryGroupId = ((input.CategoryGroupId ?? 0) == 0) ? null : input.CategoryGroupId;
            int? CategoryId = ((input.CategoryId ?? 0) == 0) ? null : input.CategoryId;
            int? ItemId = ((input.ItemId ?? 0) == 0) ? null : input.ItemId;
            string SalesQuoteDetailUDF1 = input.UDF1 == string.Empty ? null : input.UDF1;
            string SalesQuoteDetailUDF2 = input.UDF2 == string.Empty ? null : input.UDF2;
            string SalesQuoteDetailUDF3 = input.UDF3 == string.Empty ? null : input.UDF3;
            string SalesQuoteDetailUDF4 = input.UDF4 == string.Empty ? null : input.UDF4;
            string SalesQuoteDetailUDF5 = input.UDF5 == string.Empty ? null : input.UDF5;
            string SalesQuoteDetailUDF6 = input.UDF6 == string.Empty ? null : input.UDF6;
            string SalesQuoteDetailUDF7 = input.UDF7 == string.Empty ? null : input.UDF7;
            string SalesQuoteDetailUDF8 = input.UDF8 == string.Empty ? null : input.UDF8;
            string SalesQuoteDetailUDF9 = input.UDF9 == string.Empty ? null : input.UDF9;
            string SalesQuoteDetailUDF10 = input.UDF10 == string.Empty ? null : input.UDF10;

            DateTime? QuoteDateLow = input.QuoteDateLow.HasValue ? input.QuoteDateLow.Value.Date : (DateTime?)null;
            DateTime? QuoteDateHigh = input.QuoteDateHigh.HasValue ? input.QuoteDateHigh.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

            DateTime? ExpirationDateLow = input.ExpirationDateLow.HasValue ? input.ExpirationDateLow.Value.Date : (DateTime?)null;
            DateTime? ExpirationDateHigh = input.ExpirationDateHigh.HasValue ? input.ExpirationDateHigh.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

            DateTime? AnticipatedDateLow = input.AnticipatedDateLow.HasValue ? input.AnticipatedDateLow.Value.Date : (DateTime?)null;
            DateTime? AnticipatedDateHigh = input.AnticipatedDateHigh.HasValue ? input.AnticipatedDateHigh.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

            var salesQuoteDetails = context.ExportQuoteLines
                (
                    salesQuoteHeaderId: salesQuoteHeaderId,
                    includeExpiredQuotesFlag: IncludeExpiredQuotesFlag,
                    includeType: includeType,
                    branchId: BranchId,
                    warehouseId: WarehouseId,
                    createdLoginUserId: CreatedLoginUserId,
                    primarySalesRepId: PrimarySalesRepId,
                    insideSalesRepGroupId: InsideSalesRepGroupId,
                    insideSalesRepId: InsideSalesRepId,
                    outsideSalesRepGroupId: OutsideSalesRepGroupId,
                    outsideSalesRepId: OutsideSalesRepId,
                    preparedBySalesRepId: PreparedBySalesRepId,
                    quoteDateLow: QuoteDateLow,
                    quoteDateHigh: QuoteDateHigh,
                    expirationDateLow: ExpirationDateLow,
                    expirationDateHigh: ExpirationDateHigh,
                    anticipatedDateLow: AnticipatedDateLow,
                    anticipatedDateHigh: AnticipatedDateHigh,
                    customerGroupId: CustomerGroupId,
                    customerId: CustomerId,
                    categoryGroupId: CategoryGroupId,
                    categoryId: CategoryId,
                    itemId: ItemId,
                    salesQuoteDetailUDF1: SalesQuoteDetailUDF1,
                    salesQuoteDetailUDF2: SalesQuoteDetailUDF2,
                    salesQuoteDetailUDF3: SalesQuoteDetailUDF3,
                    salesQuoteDetailUDF4: SalesQuoteDetailUDF4,
                    salesQuoteDetailUDF5: SalesQuoteDetailUDF5,
                    salesQuoteDetailUDF6: SalesQuoteDetailUDF6,
                    salesQuoteDetailUDF7: SalesQuoteDetailUDF7,
                    salesQuoteDetailUDF8: SalesQuoteDetailUDF8,
                    salesQuoteDetailUDF9: SalesQuoteDetailUDF9,
                    salesQuoteDetailUDF10: SalesQuoteDetailUDF10
                ).ToList();

            #endregion GetData

            if (!salesQuoteDetails.Any())
            {
                log_.Info("salesQuoteDetails : No records returned for given criteria ExportQuoteLines.");
                runningReport.ErrorMessage = "No records found.";
                return;
            }

            #region Step 3

            if (salesQuoteDetails.Count() > 0)
            {
                bool WholeNumbersOnlyFlag = false;

                foreach (var record in salesQuoteDetails)
                {
                    saleQuote = new SalesQuoteLinesResult();

                    // Step 3: compute some values - 3a
                    decimal? orderExtendedAmount = 0m;
                    decimal? orderExtendedCost = 0;

                    orderExtendedAmount = record.UnitPriceExtended;
                    orderExtendedCost = record.UnitCostExtended;
                    decimal? marginPercentage = (orderExtendedAmount - orderExtendedCost);

                    if (orderExtendedAmount > 0)
                    {
                        marginPercentage = marginPercentage / orderExtendedAmount;
                    }

                    //3b - Check Order Line Against Inside or Outside Sales Reps
                    // If CheckInsideRepOrGroupForCustomer returns false / CheckOutsideRepOrGroupForCustomer returns false,
                    // do not include this row in the excel document to be created.
                    if (record.ShouldReturnInsideSalesRep <= 0)
                    {
                        continue;
                    }
                    if (record.ShouldReturnOutsideSalesRep <= 0)
                    {
                        continue;
                    }
                    // 3c Check the Line Type
                    if (record.QuoteLineType == 4)
                    {
                        WholeNumbersOnlyFlag = (bool)record.WholeNumbersOnlyFlag;
                    }

                    // 3d - Populate the Row in the Grid                            

                    saleQuote.SalesQuoteHeaderId = record.SalesQuoteHeader_Id;
                    saleQuote.Line = record.LineNumber;
                    saleQuote.QuoteDate = record.Quote_Date;
                    saleQuote.ExpirationDate = record.Expiration_Date;
                    saleQuote.AnticipatedReadyDateTime = string.Format("{0:yyyy-MM-dd hh:mm:ss}", record.AnticipatedReady_Date);
                    saleQuote.ConvertedToOrderDate = string.Format("{0:yyyy-MM-dd hh:mm:ss}", record.ConvertedToOrder_DateTime);
                    saleQuote.CustomerCode = record.CustomerCode;
                    saleQuote.CustomerName = record.CustomerName;
                    saleQuote.DeliveryType = EnumHelper.GetDescription((DeliveryAddressType)record.DeliveryAddressType);
                    saleQuote.CustomerJob = record.JobName;
                    saleQuote.Territory = record.Territory;
                    saleQuote.Market = record.Market;
                    saleQuote.PrimaryRep = context.SalesRepEntities.FirstOrDefault(x => x.RepId == record.SalesRepId).Name;
                    saleQuote.EnteredByUser = record.PreparedByUser;
                    saleQuote.PreparedByRep = record.PreparedByRep;
                    saleQuote.LineType = EnumHelper.GetDescription((OrderLineType)record.QuoteLineType);
                    saleQuote.ItemCode = record.ItemCode;
                    if (record.QuoteLineType == 10 || record.QuoteLineType == 13)
                    {
                        saleQuote.FullItemDecriptionLine1 = record.SQDDescription;
                        saleQuote.BriefDescription = record.SQDBriefDescription;
                    }
                    else
                    {
                        saleQuote.FullItemDecriptionLine1 = record.ITMDescription;
                        saleQuote.FullItemDecriptionLine2 = record.Description2;
                        saleQuote.BriefDescription = record.BriefDescription;
                    }
                    saleQuote.Category = record.CATDescription;
                    if (record.QuoteLineType == 4)
                    {
                        if (WholeNumbersOnlyFlag)
                        {
                            /// format as a whole number) //
                            saleQuote.QuoteQuantity = Math.Truncate(record.OrderQuantity).ToString();
                        }
                        else
                        {
                            saleQuote.QuoteQuantity = string.Format("{0:###,###,###0.###}", record.OrderQuantity);
                        }
                    }
                    else
                    {
                        if (record.OrderQuantityUOM == "pcs")
                        {
                            /// format as a whole number) *** //
                            saleQuote.QuoteQuantity = Math.Truncate(record.OrderQuantity).ToString();
                        }
                        else
                        {
                            saleQuote.QuoteQuantity = string.Format("{0:###,###,###0.###}", record.OrderQuantity);
                        }
                    }
                    saleQuote.QuoteQuantityUOM = record.OrderQuantityUOM;
                    saleQuote.Size = record.SizeDescription;
                    saleQuote.Weight = record.TotalLineWeight;
                    saleQuote.WeightUOM = record.WeightUOM;
                    saleQuote.UnitPrice = NumberFormatUtil.FormatNumber(record.UnitPrice, 4); ///  (Format with our 2 or 4 rule)
                    saleQuote.UnitPriceUOM = record.UnitPrice_UOM;
                    saleQuote.ExtendedPrice = record.UnitPriceExtended;
                    saleQuote.ExtendedCost = record.UnitCostExtended;
                    saleQuote.MarginPercentage = marginPercentage.HasValue ? (Math.Round((decimal)(marginPercentage * 100), 2, MidpointRounding.AwayFromZero)).ToString() : "0";
                    saleQuote.UDF1 = record.SQDUDF1;
                    saleQuote.UDF2 = record.SQDUDF2;
                    saleQuote.UDF3 = record.SQDUDF3;
                    saleQuote.UDF4 = record.SQDUDF4;
                    saleQuote.UDF5 = record.SQDUDF5;
                    saleQuote.UDF6 = record.SQDUDF6;
                    saleQuote.UDF7 = record.SQDUDF7;
                    saleQuote.UDF8 = record.SQDUDF8;
                    saleQuote.UDF9 = record.SQDUDF9;
                    saleQuote.UDF10 = record.SQDUDF10;
                    saleQuote.InternalDocument = record.SQDInternalNote;
                    saleQuote.CustomerFacingDocument = record.SQDExternalNote;
                    saleQuote.InternalFacingDocument = record.SQDInternalAndExternalNote;
                    saleQuote.InternalSalesOrderDocument = record.SQDNonPrintingNote;
                    saleQuote.HeaderUDF1 = record.SQHUDF1;
                    saleQuote.HeaderUDF2 = record.SQHUDF2;
                    saleQuote.HeaderUDF3 = record.SQHUDF3;
                    saleQuote.HeaderUDF4 = record.SQHUDF4;
                    saleQuote.HeaderUDF5 = record.SQHUDF5;
                    saleQuote.HeaderUDF6 = record.SQHUDF6;
                    saleQuote.QuoteInternalNote = record.SQHInternalNote;
                    saleQuote.QuoteExternalNote = record.SQHExternalNote;
                    saleQuote.QuoteInternalAndExternalNote = record.SQHInternalAndExternalNote;
                    saleQuote.QuoteNonPrintingNote = record.SQHNonPrintingNote;

                    salesQuoteLinesResult.Add(saleQuote);
                } // foreach
            } // if
            else
            {
                log_.Info("salesQuoteDetails : No records returned for given criteria ExportQuoteLines.");
                runningReport.ErrorMessage = "No records found.";
                return;
            }
            #endregion Step 3
        }


        exportSalesQuoteLinesDataResponseModel.DetailData.DetailRows.AddRange(salesQuoteLinesResult);
        // CREATE LOGIC FOR REPORT
        var workbook = (new ExportSalesQuoteLinesLogic()).ExportToExcel(exportSalesQuoteLinesDataResponseModel.TotalColumns,
                                                                        ImportExportConstants.ExportSalesQuoteLines, exportSalesQuoteLinesDataResponseModel);

        Shared.Utilities.OutPutDestinationPathValidator.ValidateDestinationPath(filePath);

        using (var fileStream = new FileStream(filefullPath, FileMode.Create, FileAccess.Write))
        {
            workbook.Write(fileStream);
            runningReport.CompletedFileName = fileName;
            runningReport.CompletedUTCDateTime = DateTime.UtcNow;
            runningReport.ErrorMessage = string.Empty;
        }

    }
    catch (OutOfMemoryException ome)
    {
        runningReport.ErrorMessage = $"{ome.Message}";
        runningReport.CanceledUTCDateTime = DateTime.UtcNow;
        throw new CapstoneException(BusinessLogicException.ExportToExcelFailed, "Too many rows returned. Please narrow the filter criteria.");
    }
    catch (Exception ex)
    {
        log_.Info($"Work Order Output Export failed, Exception: {ex.Message}.");
        runningReport.ErrorMessage = "Work Order Output Export failed.";
        runningReport.CanceledUTCDateTime = DateTime.UtcNow;
        throw new CapstoneException(BusinessLogicException.ExportToExcelFailed, "Work Order Output Export failed.");
    }
    finally
    {
        var runningReportAccessor = new RunningReportEntityAccessor(this.Context);
        runningReportAccessor.Update(runningReport);
    }
}
