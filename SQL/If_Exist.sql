/* ******************** Drop Table ******************** */

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'VcaOrderRequestData'))
BEGIN
    Print 'exist'
    -- DROP TABLE VcaOrderRequestData_DB
END



/* ******************** Drop Temp Table ******************** */

IF OBJECT_ID('tempdb..#VcaOrderRequestData') IS NOT NULL
BEGIN
    DROP TABLE #VcaOrderRequestData
END