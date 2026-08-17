    ' Call SP from code
    
    Protected Sub GetOrderHeader(ByVal OrderTrackingID As String)

        Dim SqlConn As New System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("RxPortal").ConnectionString)
        Dim SqlCmd As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand()
        Dim QueryString As String

        QueryString = "EXEC [dbo].[usp_CustomerCare_OrderHeaderBySource] '" & OrderTrackingID & "' "
        SqlConn.Open()
        SqlCmd = New System.Data.SqlClient.SqlCommand(QueryString, SqlConn)

        Dim SqlReader As System.Data.SqlClient.SqlDataReader = SqlCmd.ExecuteReader()

        Try

            While SqlReader.Read()

                HiddenRestartDiscountCode.Value = SqlReader("RestartDiscountCode")

                OrderSourcHidden.Value = SqlReader("SourceFileSpec")
                OrderBillingLabHidden.Value = SqlReader("BillingLabID")
                DirectToConsumerHidden.Value = SqlReader("DirectToConsumer")
                HiddenSapCustomerIDModal.Value = SqlReader("SapCustomerID")
                SapCustomerIDHidden.Value = SqlReader("SapCustomerID")
                HiddenCustomerNameModal.Value = SqlReader("CustomerName")
                OrderStatusCodeHidden.Value = SqlReader("OrderStatusCode")
                AssignedLabIdHidden.Value = SqlReader("AssignedLabID")
                SapCompanyCodeHidden.Value = SqlReader("SapCompanyCode")

                lblCustomerGroupMC.Text = SqlReader("CustomerGroup")
                lblOrderTrackingID.Text = SqlReader("OrderTrackingID")
                lblOrderTrakcingID_MC.Text = SqlReader("OrderTrackingID")
                lblCustomerPO.Text = SqlReader("CustomerPO")
                lblCustomerPO_MC.Text = SqlReader("CustomerPO")
                lblCustomerJob.Text = SqlReader("CustomerJobNumber")
                lblCustomerJob_MC.Text = SqlReader("CustomerJobNumber")
                lblPatienName.Text = SqlReader("PatientName")
                lblPatienName_MC.Text = SqlReader("PatientName")
                lblSapCustomerID.Text = SqlReader("SapCustomerID")
                lblSapCustomerID_MC.Text = SqlReader("SapCustomerID")
                lblSapCustomerID_MC2.Text = SqlReader("SapCustomerID")
                ViewState("CustomerPriceGroup") = SqlReader("CustomerPriceGroup")
                lblLmsAccount.Text = SqlReader("LmsAccountNumber")
                lblLmsAccount_MC.Text = SqlReader("LmsAccountNumber")
                lblLmsAccount.Text = SqlReader("LmsAccountNumber")
                lblTrayNumber.Text = SqlReader("LmsTrayNumber")
                'lblExpediteOrder.Text = FormatExpediteJob(SqlReader("ExpediteJob"))
                lblSapSalesOrder.Text = SqlReader("SapSalesOrder")
                SapSalesOrderHidden.Value = lblSapSalesOrder.Text
                lblSapSalesOrder_MC.Text = SqlReader("SapSalesOrder")
                lblLmsInvoice.Text = SqlReader("LmsInvoiceNumber")
                lblLmsInvoice_MC.Text = SqlReader("LmsInvoiceNumber")
                lblShipReference.Text = SqlReader("CartonID")
                lblRxOrderTypeHeader.Text = GlobalFunctionsClass.GetRxJobTypeDesc(SqlReader("FrameAction"))
                lblOrderSource.Text = OrderSourcHidden.Value
                lblShipReference.Text = SqlReader("CartonID")
                lblReferenceID.Text = GlobalFunctionsClass.FormatReferenceID(SqlReader("OrderReferenceID").ToString)
                lblBillingLab.Text = GlobalFunctionsClass.FormatLabID(SqlReader("BillingLabID"))
                lblProducingLab.Text = GlobalFunctionsClass.FormatLabID(SqlReader("AssignedLabID"))

                lblOrderStatus.Text = GlobalFunctionsClass.EvalOrderStatusCode(SqlReader("OrderStatusCode"))
                lblDepartment.Text = GlobalFunctionsClass.EvalOrderStatusGroup(SqlReader("OrderStatusCode"), SqlReader("OrderStatusGroup"))
                lblOrderStatusDec.Text = GlobalFunctionsClass.EvalOrderStatusDesc(SqlReader("OrderStatusCode"), SqlReader("OrderStatusDesc"))
                lblOrderStatusDTM.Text = SqlReader("StatusDTM")

                OrderTrackingIDHidden.Value = OrderTrackingID

                SapSalesOrgHidden.Value = SqlReader("SapSalesOrg")
                SapDistributionChannelHidden.Value = SqlReader("SapDistributionChannel")
                SapDivisionHidden.Value = SqlReader("SapDivision")
                SapPriceGroupHidden.Value = SqlReader("CustomerPriceGroup")

                lblSapSalesOrg.Text = SapSalesOrgHidden.Value
                lblSapDistributionChannel.Text = SapDistributionChannelHidden.Value
                lblSapDivision.Text = SapDivisionHidden.Value
                lblCustomerPriceGroup.Text = SapPriceGroupHidden.Value

                'ser view state
                ViewState("CustomerPO") = SqlReader("CustomerPO")

            End While



        Finally
            '*** Always Call Close When Done Reading ***
            SqlReader.Close()
            SqlCmd.Connection.Close()

        End Try

        ViewState("CustomerPO") = lblCustomerPO.Text

    End Sub
