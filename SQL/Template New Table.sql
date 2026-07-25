--EXECUTE COMMANDS--
-- #13104 Add New Table BankDeposit
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankDeposit]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[BankDeposit] (
		[BankDeposit_Id] [int] CONSTRAINT [PK_BankDeposit_Id] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
		[CreatedBy_LoginUser_Id] [int] NOT NULL 
			CONSTRAINT [FK_BankDeposit_CreatedBy_LoginUser_Id] 
			FOREIGN KEY([CreatedBy_LoginUser_Id]) REFERENCES [dbo].[LoginUser] ([LoginUser_Id]),
		[Voided_Flag] [BIT] NOT NULL CONSTRAINT DF_BankDeposit_Voided_Flag DEFAULT (0),
		[VoidedBy_LoginUser_Id] [INT] NULL,
			CONSTRAINT [FK_BankDeposit_VoidedBy_LoginUser_Id] 
			FOREIGN KEY([VoidedBy_LoginUser_Id]) REFERENCES [dbo].[LoginUser] ([LoginUser_Id]),
		[BankAccount_Id] [int] NOT NULL 
			CONSTRAINT [FK_BankDeposit_BankAccount_Id] 
			FOREIGN KEY([BankAccount_Id]) REFERENCES [dbo].[BankAccount] ([BankAccount_Id]),
		[VendorRefund_Id] [INT] NOT NULL
			CONSTRAINT [FK_BankDeposit_VendorRefund_Id] 
			FOREIGN KEY([VendorRefund_Id]) REFERENCES [dbo].[VendorRefund] ([VendorRefund_Id]),
		[Item_Id] [INT] NOT NULL
			CONSTRAINT [FK_BankDeposit_Item_Id] 
			FOREIGN KEY([Item_Id]) REFERENCES [dbo].[Item] ([Item_Id]),
		[ItemStandardSize_Id] [INT] NOT NULL
			CONSTRAINT [FK_BankDeposit_ItemStandardSize_Id] 
			FOREIGN KEY([ItemStandardSize_Id]) REFERENCES [dbo].[ItemStandardSize] ([StandardSize_Id]),
		[Warehouse_Id] [INT] NOT NULL
			CONSTRAINT [FK_TagBundle_Warehouse_Id] 
			FOREIGN KEY([Warehouse_Id]) REFERENCES [dbo].[Warehouse] ([Warehouse_Id]),
		[WarehouseLocation_Id] [INT] NOT NULL
			CONSTRAINT [FK_TagBundle_WarehouseLocation_Id] 
			FOREIGN KEY([WarehouseLocation_Id]) REFERENCES [dbo].[WarehouseLocation] ([WarehouseLocation_Id]),		
		[Observation1_TagObservationCode_Id] [INT] NULL
			CONSTRAINT [FK_TagBundle_Observation1_TagObservationCode_Id] 
			FOREIGN KEY([Observation1_TagObservationCode_Id]) REFERENCES [dbo].[TagObservationCode] ([Code_Id]),		
		[Voided_Flag] [BIT] NOT NULL CONSTRAINT DF_BankDeposit_Voided_Flag DEFAULT (0),
		[CashAmount] [DECIMAL] (11, 2) NOT NULL CONSTRAINT DF_BankDeposit_CashAmount DEFAULT (0.00),
		[Amount] [DECIMAL] (11, 2) NOT NULL CONSTRAINT DF_BankDeposit_Amount DEFAULT (0.00),
		[ExchangeRate] [DECIMAL] (15, 10) NOT NULL CONSTRAINT DF_BankDeposit_ExchangeRate DEFAULT (0.00),
		[Amount] [decimal](11, 2) NOT NULL CONSTRAINT DF_BankDeposit_Amount DEFAULT (0.00),
		[Deposited_Flag] [bit] NOT NULL CONSTRAINT DF_BankDeposit_Deposited_Flag DEFAULT (0)
	)
END
GO