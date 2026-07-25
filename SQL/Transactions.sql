BEGIN TRY  
    ---- Execute the STORED PROCEDURE inside the TRY block.  
    --EXECUTE usp_VCA_CZVOptic_BuildRequestData '1043315007', 'P'; 

	DECLARE @TempCzvOpticOrderDetail TABLE 
	(
		OrderTrackingID	varchar(15) NOT NULL,
		FrameOmaData	varchar(MAX)NOT NULL		
	)

	INSERT INTO @TempCzvOpticOrderDetail
	SELECT TOP 10 OrderTrackingID, FrameOmaData AS  FrameOmaDataNotReplaced --,REPLACE(FrameOmaData,  CHAR(0x1E), '')
	FROM CzvOpticOrderDetail
	WHERE FrameOmaData like '%' + CHAR(0x1E) +'%' 
	ORDER BY OrderTrackingID

	SELECT * FROM @TempCzvOpticOrderDetail

	/* REPLACE ESPECIAL CHARACTER (HEXA) BY EMPTY VALUE */
	--SELECT TOP 10 OrderTrackingID ,REPLACE(FrameOmaData,  CHAR(0x1E), '') AS FrameOmaData_Replaced
	--FROM CzvOpticOrderDetail
	--WHERE FrameOmaData like '%' + CHAR(0x1E) +'%' 
	--ORDER BY OrderTrackingID


	UPDATE  COOD
		SET COOD.FrameOmaData = REPLACE(COOD.FrameOmaData,  CHAR(0x1E), '')
	FROM CzvOpticOrderDetail COOD
		JOIN @TempCzvOpticOrderDetail TCOOD ON COOD.OrderTrackingID = TCOOD.OrderTrackingID
	WHERE COOD.FrameOmaData like '%' + CHAR(0x1E) +'%' 

	SELECT TOP 10 OrderTrackingID, FrameOmaData AS  FrameOmaData_AfterUpdate
	FROM CzvOpticOrderDetail
	WHERE FrameOmaData like '%' + CHAR(0x1E) +'%' 
	ORDER BY OrderTrackingID

END TRY  
BEGIN CATCH  
	SELECT
		ERROR_NUMBER() AS ErrorNumber,  
        ERROR_SEVERITY() AS ErrorSeverity,  
        ERROR_STATE() AS ErrorState,  
        ERROR_PROCEDURE() AS ErrorProcedure,  
        ERROR_LINE() AS ErrorLine,  
        ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;  
GO  

--DECLARE @STRING VARCHAR(100)
--SET @STRING = (REPLACE ('Your String with cityname here', 'cityname', 'xyz'))
--SELECT @STRING

