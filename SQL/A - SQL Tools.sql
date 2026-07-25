-- *** SQL Tools *** ---
RETURN;

-- RENAME COLUMN
IF EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'VendorTransaction' 
				AND [COLUMN_NAME] = 'ErrorTime')
BEGIN
	EXEC sp_rename 'dbo.ErrorLog.ErrorTime', 'ErrorDateTime', 'COLUMN';
END

-- Check if temp table exists
IF OBJECT_ID('tempdb..#VcaOrderRequestData') IS NOT NULL
BEGIN
    DROP TABLE #VcaOrderRequestData
END


--EXECUTE COMMANDS--
-- XXXXXXXX STORED PROCEDURES XXXXXXXXXXXX
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[StoredProcedure_Name]'))
  DROP PROCEDURE [dbo].[StoredProcedure_Name] 
GO

--EXECUTE COMMANDS--
-- UPDATE IF EXISTS [LoginProgram] --
IF EXISTS(SELECT TOP 1 1  FROM [dbo].[LoginProgram]
	WHERE 	[Program_Id] = 0000)
BEGIN
	-- CODE HERE
END
GO

--EXECUTE COMMANDS--
--- INSERTS
IF NOT EXISTS(SELECT TOP 1 1  FROM [dbo].[LoginProgram]
	WHERE 	[Program_Id] = 0000)
BEGIN
	RETURN;
	INSERT INTO LoginProgram ([Program_Id]
           ,[FlexMenuName]
           ,[FlexMenu_Type]
           ,[SearchMostPopularName]
           ,[ScreenTitle]) 
    VALUES (
	 0000
	,'Print/Preview Processing and Transportation Orders'
	,1
	,'Print/Preview Processing and Transportation Orders'
	,'Print/Preview Processing and Transportation Orders')
END
GO


--EXECUTE COMMANDS--
-- DROP VIEW IF EXIST--
IF EXISTS(SELECT 1 FROM sys.views WHERE name='v_SOLines' and type='v')
	PRINT 'VIEW DELETED';
GO

--EXECUTE COMMANDS--
-- ADD COLUMN
IF NOT EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'BankAccount' 
				AND [COLUMN_NAME] = 'Retired_Flag')
BEGIN
	ALTER TABLE [dbo].[BankAccount]
	ADD [Retired_Flag] [bit]  NOT NULL CONSTRAINT DF_Retired_Flag DEFAULT 0 WITH VALUES,
	ADD [ExchangeRate] [DECIMAL] (15, 10) NOT NULL CONSTRAINT DF_BankDeposit_ExchangeRate DEFAULT (0.00)
END
GO

--EXECUTE COMMANDS--
-- DROP COLUMN AND INDEX
IF EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'VendorTransaction1' 
				AND [COLUMN_NAME] = 'Discount_Date')
BEGIN
	-- DROP INDEX [Vendor_Id + Currency_Id] ON [dbo].[VendorCredit]
	ALTER TABLE Customerss DROP COLUMN ContactName11;
END
GO

--- EXECUTE FUNCTIONS
SELECT dbo.udf_Utility_VCA_BuildChiralRecord ('P', 31.5,29.5) AS Result
SELECT DBO.udf_Utility_VCA_BuildChiralRecord ('P', 7.8, 7.0) AS Result


-- DROP INDEX
IF EXISTS (SELECT NAME FROM sys.indexes  
           WHERE NAME = N'noncl_LensDesignMaster_PreCalcMethod'
           AND object_id = OBJECT_ID(N'dbo.LensDesignMaster', N'U'))  
BEGIN
	PRINT 'DROP INDEX noncl_LensDesignMaster_PreCalcMethod'
	DROP INDEX noncl_LensDesignMaster_PreCalcMethod ON [dbo].[LensDesignMaster]
END
GO

--EXECUTE COMMANDS--
-- DROP CONSTRAINT
IF (OBJECT_ID('dbo.[FK_VendorPaymentHistory_CreditCardAccount_Id1]', 'F') IS NOT NULL)
BEGIN
	PRINT 'DROP CONSTRAINT [FK_VendorPaymentHistory_BankAccount_Id1]'
	ALTER TABLE Persons DROP CONSTRAINT UC_Person
END

-- ADD CONSTRAINT AFTER DROP ONE
IF (OBJECT_ID('dbo.[FK_CreditCardTransaction_Reconciled_CreditCardAccountStatement_Id]', 'F') IS NULL)
BEGIN
	PRINT 'ADD CONSTRAINT [FK_CreditCardTransaction_Reconciled_CreditCardAccountStatement_Id]'
	ALTER TABLE [dbo].[CreditCardTransaction]  WITH CHECK 
	ADD  CONSTRAINT [FK_CreditCardTransaction_Reconciled_CreditCardAccountStatement_Id]		
	FOREIGN KEY([Reconciled_CreditCardAccountStatement_Id]) REFERENCES [dbo].[CreditCardAccountStatement] ([CreditCardAccountStatement_Id])
END
GO


--EXECUTE COMMANDS--
-- ALTER COLUMN
IF EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'VendorTransaction' 
				AND [COLUMN_NAME] = 'Discount_Date')
BEGIN
	ALTER TABLE BankAccount 
	ALTER COLUMN AccountNumber NVARCHAR(120) NOT NULL;
END

--EXECUTE COMMANDS--
-- Rename Column
IF EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'CustomerPaymentTerms' 
				AND [COLUMN_NAME] = 'CustomerPaymentTerms')
BEGIN
	EXEC SP_RENAME 'TableName.[OldColumnName]' , '[NewColumnName]', 'COLUMN'
	EXEC sp_rename 'SalesJU.SalesTerritoryJU', 'SalesTerrJU';
END
GO

-- RENAME TABLE
IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[CustomerPaymentOnAccountHistory]') AND type in (N'U'))
BEGIN
	EXEC SP_RENAME 'Schema.CustomerPaymentOnAccountHistory' , 'CustomerPaymentHistory'
END
GO

--EXECUTE COMMANDS--
-- ADD NEW TABLE
--IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id) WHERE s.name = 'dbo' AND t.name = 'ProductionScheduleEvent2') 
IF NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[UndepositedFunds]') AND type in (N'U'))
BEGIN
	-- CODE HERE
	PRINT 'not exist'
END
----------
ELSE
BEGIN
	PRINT 'not exist'
END
GO

-- DROP foreign_keys
IF EXISTS (SELECT *  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_VendorDebit_Currency_Id')
   AND parent_object_id = OBJECT_ID(N'dbo.VendorDebit12'))
BEGIN
	ALTER TABLE [dbo].[VendorDebit1] DROP CONSTRAINT [FK_VendorDebit_Currency_IDd]
END

-- DROP INDEX
IF EXISTS (SELECT *  FROM sys.indexes  WHERE name='Index_Name' 
    AND object_id = OBJECT_ID('[dbo].[VendorDebit2]'))
BEGIN
    DROP INDEX [Index_Name] ON [SchmaName].[TableName];
END
GO

/* ************************************************************************************************************* */



Select j.name JobName, s.step_name StepName
From msdb.dbo.sysjobsteps s
	join msdb.dbo.sysjobs j on j.job_id=s.job_id
Where s.command like '%TempCzvRxCalcJobResults%'

SELECT  js.database_name as DatabaseName,
                 jobs.Name as JobName,
                 js.step_id as StepID,
                 js.step_name as StepName, 
                 js.command as StepCommand
FROM     msdb.dbo.sysjobs as jobs
                INNER JOIN msdb.dbo.sysjobsteps as js ON jobs.job_id = js.job_id
WHERE js.command LIKE  '%TempCzvRxCalcJobResults%' --OR database_name = 'FileGeneration_Details'
ORDER BY jobs.Name,js.step_id



-- Search Table
SELECT * FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE '%mirr%'

/* ******* Find table in Stored Procedure ******** */
-- Find table in Stored Procedure
SELECT Name As SP_Name
FROM sys.procedures
WHERE OBJECT_DEFINITION(OBJECT_ID) LIKE '%contract%' -- '%INSERT INTO #%'


-- Find Column in tables
SELECT 
	DISTINCT t.name AS table_name,
	SCHEMA_NAME(schema_id) AS schema_name,
	c.name AS column_name, c.is_nullable, ISC.DATA_TYPE
FROM sys.tables AS t
	JOIN sys.columns c ON t.OBJECT_ID = c.OBJECT_ID
	JOIN INFORMATION_SCHEMA.COLUMNS ISC ON ISC.COLUMN_NAME Like '%contract%'
WHERE c.name LIKE 'mirr%' --OR t.name  Like '%contract%' 
ORDER BY table_name, schema_name;


-- Search Column in All Objects
SELECT OBJECT_NAME(OBJECT_ID),
definition
FROM sys.sql_modules
WHERE definition LIKE '%' + 'SchneiderSurfaceLeft' + '%'
GO

-- Search Column in all Tables - INFORMATION_SCHEMA.COLUMNS
SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH AS LENGTH
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE 
	COLUMN_NAME LIKE '%RxFitPatientDOB%' 
	AND TABLE_NAME LIKE '%DigitalVisionOrderDetail%'

-- Search Column in all Tables
SELECT DISTINCT
	c.name  AS 'ColumnName' 
	,(SCHEMA_NAME(t.schema_id) + '.' + t.name) AS 'TableName'
	,c.max_length AS Length
FROM sys.columns c
	JOIN sys.tables  t   ON c.object_id = t.object_id
WHERE c.name LIKE '%RxFitPatientDOB%'
ORDER BY TableName ,ColumnName;


/* ******************************** --- ******************************************** */

/* ******************* Search Column in Stored Procedure Only  ******************* */
USE RxPortal
GO
SELECT DISTINCT OBJECT_NAME(OBJECT_ID),
object_definition(OBJECT_ID)
FROM sys.Procedures
WHERE object_definition(OBJECT_ID) LIKE '%' + 'SchneiderSurfaceLeft' + '%'
GO

/* ************************************ INDEX *********************************************** */

-- ADD UNIQUE INDEX - CLUSTERED
IF NOT EXISTS (SELECT name from sys.indexes  
           WHERE name = N'IX_ItemId_WarehouseId'
           AND object_id = OBJECT_ID(N'dbo.ItemUsableRemnantSize', N'U'))  
BEGIN
	PRINT 'CREATE'	
	CREATE UNIQUE INDEX IX_ItemId_WarehouseId ON dbo.ItemUsableRemnantSize 
	(
		Item_Id ASC,
		Warehouse_Id ASC
	);  
END
GO

-- ADD NON UNIQUE INDEX - NON-CLUSTERED
-- Create a non unique index called IX_VendorId_TransactionDate 
IF NOT EXISTS (SELECT name from sys.indexes  
           WHERE name = N'IX_VendorId_TransactionDate'
           AND object_id = OBJECT_ID(N'dbo.VendorTransaction', N'U'))  
BEGIN
	PRINT 'CREATE'	
	CREATE INDEX IX_VendorId_TransactionDate ON dbo.VendorTransaction 
	(
		Vendor_Id ASC,
		Transaction_Date ASC
	);  
END
GO

/* ************************************** TEMP TABLES **************************************************** */
DECLARE @tblResaleCert TABLE(CustomerCreditHeader_Id INT, ResaleCertificateNumber NVARCHAR(20))


SELECT DISTINCT (Vendor_Id), SUM(VT.Balance) AS sixtyOneBucket 
INTO #tmpSixtyOneBucket
FROM VendorTransaction VT WHERE VT.Balance <> 0

IF OBJECT_ID('tempdb.dbo.#TergetInvoices', 'U') IS NOT NULL
Begin
  DROP TABLE #TergetInvoices;
End



-- Find Table

-- Across All DB's
USE RxPortal
GO
EXEC sys.sp_msforeachdb 'SELECT ''?'' DatabaseName, Name FROM [?].sys.Tables WHERE Name LIKE ''%BankTransaction%'''

-- 
SELECT
    sys.tables.name AS 'BankTransaction', 
    sys.tables.object_id AS 'Object ID', 
    sys.columns.name AS 'Column Name'
FROM
    sys.tables INNER JOIN sys.columns 
        ON sys.tables.object_id = sys.columns.object_id
WHERE
    sys.columns.name LIKE '%pass%'
ORDER BY 1;


-- Find table in Stored Procedure
SELECT Name
FROM sys.procedures
WHERE OBJECT_DEFINITION(OBJECT_ID) LIKE '%BankTransaction%'


---  Reseed Identity  ---
Select * From SalesTaxJurisdictionExemptionCertificate Where SalesTaxJurisdictionExemptionCertificate_Id >= 2282-- And SalesTaxJurisdictionExemptionCertificate_Id <= 2299
--Delete From SalesTaxJurisdictionExemptionCertificate Where SalesTaxJurisdictionExemptionCertificate_Id >= 2282 And SalesTaxJurisdictionExemptionCertificate_Id <= 2299
DBCC CHECKIDENT ('SSalesTaxJurisdictionExemptionCertificate', RESEED, 2281)
Select * From SalesTaxJurisdictionExemptionCertificate (NOLOCK) Order By SalesTaxJurisdictionExemptionCertificate_Id Desc

--Delet From Customer Where Customer_Id > 5000000
--DBCC CHECKIDENT ('Customer', RESEED, 0)

-- @Tables - Temporay
DECLARE @tempCustomerRevenue TABLE  (Customer_Id int NULL, InvoiceRevenueByCustomer decimal (11,2) NULL)

-- Replace string
DECLARE @STRING VARCHAR(100)
SET @STRING = (REPLACE ('Your String with cityname here', 'cityname', 'xyz'))
SELECT @STRING

/* **** REPLACE ESPECIAL CHARACTER (HEXA) BY EMPTY VALUE ***** */
	SELECT TOP 10 OrderTrackingID ,REPLACE(FrameOmaData,  CHAR(0x1E), '')
	FROM CzvOpticOrderDetail
	WHERE FrameOmaData like '%' + CHAR(0x1E) +'%' 
	ORDER BY OrderTrackingID


-- ***** Delete temp table if exists ***** --
IF OBJECT_ID('tempdb.dbo.#TergetInvoices', 'U') IS NOT NULL
Begin
  DROP TABLE #TergetInvoices;
End

IF OBJECT_ID('tempdb.dbo.#Invoice', 'U') IS NOT NULL
Begin
  DROP TABLE #Invoice; 
End

IF OBJECT_ID('dbo.tempCustomerRevenue', 'U') IS NOT NULL 
  DROP TABLE dbo.tempCustomerRevenue; 


--- Update table and Inner Join
Update CAT
	Set CAT.SortSequence = CATBK.SortSequence
From CategoryBK As CATBK 
	INNER JOIN Category AS CAT ON CAT.Category_Id = CATBK.Category_Id



/* **************************************************************************************************************************** */
/* Add new colum as FK */
--#12790 Database Change to WarehouseWorkTimerDetail Table 
IF NOT EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'WarehouseWorkTimerDetail' 
				AND [COLUMN_NAME] = 'SalesOrderDetailProcessingStep_Id')
BEGIN
		ALTER TABLE [dbo].[WarehouseWorkTimerDetail]
		ADD [SalesOrderDetailProcessingStep_Id]  INT NULL
		CONSTRAINT [FK_WarehouseWorkTimerDetail_SalesOrderDetailProcessingStep_Id] 
		FOREIGN KEY ([SalesOrderDetailProcessingStep_Id]) REFERENCES [SalesOrderDetailProcessingStep]([SalesOrderDetailProcessingStep_Id])

	Print 'WarehouseWorkTimerDetail Added'
END
GO

/* **************************************************************************************************************************** */

/* Add new colum Set NOT NULL, ADD CONSTRAINT and SET VALUE */
IF NOT EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'CustomerInit' 
				AND [COLUMN_NAME] = 'DefaultTestReportToDeliver_Type')
BEGIN	
	ALTER TABLE [dbo].[CustomerInit]
	ADD  [DefaultTestReportToDeliver_Type] [SMALLINT] NOT NULL 
	CONSTRAINT DF_CustomerInit_DefaultTestReportToDeliver_Type DEFAULT (0)
	WITH VALUES;
END
GO


