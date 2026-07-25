/* **** Temp table **** */

BEGIN TRY  

DECLARE @TempCzvOpticOrderDetail TABLE 
(
    OrderTrackingID	varchar(15) NOT NULL,
    FrameOmaData	varchar(MAX)NOT NULL
)

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