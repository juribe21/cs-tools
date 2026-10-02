DECLARE @SapCompanyCode VARCHAR(10)
DECLARE @FrameName VARCHAR(75)

CREATE TABLE #TempTable (OrderTrackingID BIGINT, SourceFileSpec VARCHAR(25), OrderType VARCHAR(10), FrameReceivedStatus VARCHAR(2), 
	SapCustomerID VARCHAR(25), OrderStatusCode VARCHAR(25), OrderStatusGroup VARCHAR(25), AssignedLabID VARCHAR(10), OrderStatusDesc VARCHAR(125), 
	StatusDTM VARCHAR(35), LmsReferenceNumber VARCHAR(25), PatientLastName VARCHAR(50), ZeissLab VARCHAR(25))

CREATE TABLE #TmpFrameName (FrameName VARCHAR(75), OrderTrackingID BIGINT, SourceFileSpec VARCHAR(25), OrderType VARCHAR(10), FrameReceivedStatus VARCHAR(2), 
	SapCustomerID VARCHAR(25), OrderStatusCode VARCHAR(25), OrderStatusGroup VARCHAR(25), AssignedLabID VARCHAR(10), OrderStatusDesc VARCHAR(125), 
	StatusDTM VARCHAR(35), LmsReferenceNumber VARCHAR(25), PatientLastName VARCHAR(50), ZeissLab VARCHAR(25))


	INSERT INTO #TempTable
	SELECT 
		ROX.OrderTrackingID, 
		ROX.SourceFileSpec,
		ROX.FrameAction AS OrderType,
		IPC.FrameReceivedStatus,
		ROX.SapCustomerID,
		ROX.OrderStatusCode, 
		ROX.OrderStatusGroup, 
		ROX.AssignedLabID, 
		SCM.OrderStatusDesc, 
		CONVERT(datetime,ROX.OrderStatusDate + ' ' + ROX.OrderStatusTime, 120) AS StatusDTM, 
		ROX.LmsInvoiceNumber AS LmsReferenceNumber, 
		ROX.PatientLastName,
		RLM.LabShortName as ZeissLab		
	FROM RxOrderXref ROX
	INNER JOIN StatusCodeMaster SCM
	ON ROX.OrderStatusCode = SCM.OrderStatusCode AND ROX.AssignedLabID = SCM.LabID
	INNER JOIN RxLabMaster RLM
	ON RLM.LabID = ROX.AssignedLabID 
	INNER JOIN InvProcessControl IPC  ON ROX.OrderTrackingID = IPC.OrderTrackingID
	WHERE ROX.OrderStatusCode NOT IN ('SHP030') AND ROX.OrderStatusCode NOT LIKE 'CNX%' 
		AND ROX.SapCompanyCode = @SapCompanyCode 
		-- AND ROX.OrderStatusDate >= DATEADD(DAY, -180, GETDATE())
	ORDER BY ROX.OrderTrackingID DESC


	INSERT INTO #TmpFrameName
	SELECT 
		[dbo].[udf_Lookup_FrameNameValue] (OrderTrackingID,SourceFileSpec) As FrameName,
		* 	
	FROM #TempTable

	SELECT * FROM #TmpFrameName WHERE FrameName = @FrameName

	DROP TABLE #TempTable;
	DROP TABLE #TmpFrameName;
