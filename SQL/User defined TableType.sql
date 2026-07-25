--  CREATE TABLE TYPE - Defined Table Type
CREATE TYPE TestTable as TABLE 
(
    Name		varchar(10) NOT NULL,
    CurrenDate	date        NOT NULL,
	Rate		float       NOT NULL
)

/* ********************************************** */
--Create SP with type table as parameter.
USE [DB_JURIBE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_text_insertTableType
(	
    @MyTable TestTable READONLY
)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @CurrenDate DATE = (SELECT GETDATE())
	PRINT @CurrenDate
	
	-- Insert data into persistent object (physical table)
	SELECT *
		INTO dbo.NewTable
	FROM @MyTable
	WHERE CurrenDate > @CurrenDate

	-- Perform a select over received table
	SELECT TOP(10) * 
	FROM @MyTable
	WHERE CurrenDate > @CurrenDate                

END
GO
