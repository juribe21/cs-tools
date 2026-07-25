IF NOT EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'RxPortalOrderDetail' 
				AND [COLUMN_NAME] = 'FrameManufacturer')
BEGIN
	ALTER TABLE [dbo].[RxPortalOrderDetail]
	ADD [FrameManufacturer] nvarchar(25)   NULL 
	PRINT 'FrameManufacturer-ADDED'
END
GO

IF NOT EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'RxPortalOrderDetail' 
				AND [COLUMN_NAME] = 'FrameCollection')
BEGIN
	ALTER TABLE [dbo].[RxPortalOrderDetail]
	ADD [FrameCollection] nvarchar(25)   NULL 
	PRINT 'FrameCollection-ADDED'
END
GO

IF NOT EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'RxPortalOrderDetail' 
				AND [COLUMN_NAME] = 'FrameBrand')
BEGIN
	ALTER TABLE [dbo].[RxPortalOrderDetail]
	ADD [FrameBrand] nvarchar(40)   NULL 
	PRINT 'FrameBrand-ADDED'
END
GO


RETURN;
RETURN;
IF NOT EXISTS ( SELECT TOP 1 1  FROM INFORMATION_SCHEMA.COLUMNS
				WHERE 	[TABLE_NAME] = 'VisionStarOrderHeader' 
				AND [COLUMN_NAME] = 'ShipToCountryCode')
BEGIN
	ALTER TABLE [dbo].[VisionStarOrderHeader]
	ADD [ShipToCountryCode] nvarchar(40)   NULL 
	PRINT 'ShipToCountryCode-ADDED'
END
GO
