B. Use nested cursors to produce report output
This example shows how cursors can be nested to produce complex reports. The inner cursor is declared for each author.

SET NOCOUNT ON

DECLARE @au_id varchar(11), @au_fname varchar(20), @au_lname varchar(40),
   @message varchar(80), @title varchar(80)

PRINT "-------- Utah Authors report --------"

DECLARE authors_cursor CURSOR FOR 
SELECT au_id, au_fname, au_lname FROM authors WHERE state = "UT" ORDER BY au_id

OPEN authors_cursor

FETCH NEXT FROM authors_cursor INTO @au_id, @au_fname, @au_lname

WHILE @@FETCH_STATUS = 0
BEGIN
   PRINT " "
   SELECT @message = + " ← ← ← ----- Books by Author: " + 
      @au_fname + " " + @au_lname

   PRINT @message

               -- Declare an inner cursor based   
               -- on au_id from the outer cursor.

               DECLARE titles_cursor CURSOR FOR 
                  SELECT t.title 
                  FROM titleauthor ta, titles t
                  WHERE ta.title_id = t.title_id AND
                  ta.au_id = @au_id   -- Variable value from the outer cursor

                  OPEN titles_cursor
                  FETCH NEXT FROM titles_cursor INTO @title

                  IF @@FETCH_STATUS <> 0 
                     PRINT "         <<No Books>>"     

                  WHILE @@FETCH_STATUS = 0
                  BEGIN
                     
                     SELECT @message = "         " + @title
                     PRINT @message
                     FETCH NEXT FROM titles_cursor INTO @title
                  
                  END

               CLOSE titles_cursor
               DEALLOCATE titles_cursor
   
   -- Get the next author.
   FETCH NEXT FROM authors_cursor 
   INTO @au_id, @au_fname, @au_lname
END

CLOSE authors_cursor
DEALLOCATE authors_cursor
GO