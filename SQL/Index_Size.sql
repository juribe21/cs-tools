/* CALCULATE THE INDEX SIZE COLUMN 2→ 'size_mb'  */

SELECT TOP 10 schema_name(T.schema_id) schema_name,
       T.name table_name, 
       I.name index_name, 
       PS.used_page_count / 8. / 1024 size_mb,
       row_count,
       row_count / nullif(PS.used_page_count,0) rows_per_page
FROM sys.tables T
	join sys.indexes I ON T.object_id = I.object_id
	join sys.dm_db_partition_stats PS ON I.object_id = PS.object_id and I.index_id = PS.index_id
WHERE T.name = 'InvProcessControl' -- 'RxOrderXref'
ORDER BY table_name, index_name