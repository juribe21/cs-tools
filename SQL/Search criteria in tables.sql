-- Step 1: Define your search criteria
DECLARE @SearchStr NVARCHAR(100) = 'Eyefinity';

-- Drop temp tables if they exist from a previous run
IF OBJECT_ID('tempdb..#Results') IS NOT NULL DROP TABLE #Results;
IF OBJECT_ID('tempdb..#ColumnsToSearch') IS NOT NULL DROP TABLE #ColumnsToSearch;

-- Create a table to store match findings
CREATE TABLE #Results (
    TableName NVARCHAR(256),
    ColumnName NVARCHAR(256),
    MatchCount INT
);

-- Step 2: Grab all character/string columns from user tables
SELECT 
    QUOTENAME(s.name) + '.' + QUOTENAME(t.name) AS TableName,
    QUOTENAME(c.name) AS ColumnName,
    ROW_NUMBER() OVER (ORDER BY t.name) AS RowNum
INTO #ColumnsToSearch
FROM sys.tables t
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
INNER JOIN sys.columns c ON t.object_id = c.object_id
INNER JOIN sys.types y ON c.user_type_id = y.user_type_id
WHERE y.name IN ('varchar', 'nvarchar', 'char', 'nchar', 'text', 'ntext')
  AND t.is_ms_shipped = 0; -- Only search user tables, ignore system tables

-- Step 3: Loop through each column using dynamic SQL
DECLARE @CurrentRow INT = 1;
DECLARE @TotalRows INT = (SELECT COUNT(*) FROM #ColumnsToSearch);
DECLARE @Table NVARCHAR(256), @Column NVARCHAR(256), @Sql NVARCHAR(MAX);

WHILE @CurrentRow <= @TotalRows
BEGIN
    SELECT @Table = TableName, @Column = ColumnName 
    FROM #ColumnsToSearch 
    WHERE RowNum = @CurrentRow;

    -- Build a safe dynamic SQL string to count matches
    SET @Sql = N'
        INSERT INTO #Results (TableName, ColumnName, MatchCount)
        SELECT ''' + REPLACE(@Table, '''', '''''') + ''', ''' + REPLACE(@Column, '''', '''''') + ''', COUNT(*)
        FROM ' + @Table + N' WITH (NOLOCK)
        WHERE ' + @Column + N' LIKE @InnerSearchStr;';

    BEGIN TRY
        EXEC sp_executesql @Sql, N'@InnerSearchStr NVARCHAR(100)', @InnerSearchStr = @SearchStr;
    END TRY
    BEGIN CATCH
        -- Silently catch errors from incompatible data mapping or collation issues
    END CATCH

    SET @CurrentRow = @CurrentRow + 1;
END

-- Step 4: Show tables and columns containing your text
SELECT TableName, ColumnName, MatchCount
FROM #Results
WHERE MatchCount > 0
ORDER BY TableName, ColumnName;



/* ******************************************************************* */
/* *********************** SEARCH STRING VALUE *********************** */
/* ******************************************************************* */

DECLARE @SearchStr NVARCHAR(100) = 'fsv' -- <-- Change value here
DECLARE @Results TABLE (TableName NVARCHAR(256), ColumnName NVARCHAR(256), ActualValue NVARCHAR(MAX))

DECLARE @TableName NVARCHAR(256), @ColumnName NVARCHAR(256), @Sql NVARCHAR(MAX)

DECLARE ColumnCursor CURSOR FOR
SELECT QUOTENAME(t.name), QUOTENAME(c.name)
FROM sys.tables t
INNER JOIN sys.columns c ON t.object_id = c.object_id
INNER JOIN sys.types y ON c.user_type_id = y.user_type_id
WHERE y.name IN ('varchar', 'char', 'nvarchar', 'nchar', 'text', 'ntext') -- Text datatypes
  AND t.is_ms_shipped = 0

OPEN ColumnCursor
FETCH NEXT FROM ColumnCursor INTO @TableName, @ColumnName

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Construct dynamic SQL to check if the value exists in this column
    SET @Sql = 'SELECT ''' + @TableName + ''', ''' + @ColumnName + ''', CAST(' + @ColumnName + ' AS NVARCHAR(MAX)) ' +
               'FROM ' + @TableName + ' WHERE ' + @ColumnName + ' LIKE ''%' + @SearchStr + '%'''
    
    INSERT INTO @Results
    EXEC sp_executesql @Sql

    FETCH NEXT FROM ColumnCursor INTO @TableName, @ColumnName
END

CLOSE ColumnCursor
DEALLOCATE ColumnCursor

-- Display all occurrences
SELECT * FROM @Results

