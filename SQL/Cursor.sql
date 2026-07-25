Declare cursorJU cursor Fast_Forward
For Select distinct( o.OperationId), o.OperationName, o.Description from tblOperation o 
where o.OperationId > 60 and o.OperationId < 80

CREATE TABLE #temp ( id INT, Name VARCHAR(100), Description varchar(60))
Declare @OperId int, @OperName varchar(100), @Desc varchar(60)  -- << variables que utiliza el cursor

--
Open cursorJU

Fetch Next From cursorJU Into @OperId,  @OperName, @Desc  -- << recorre el cursor
While @@FETCH_STATUS <> -1
	Begin
		Insert into #temp (id, Name, Description)
		Select @OperId, @OperName, @Desc  -- << Select sobre el cursor
		Fetch Next From cursorJU Into @OperId, @OperName, @Desc -- << recorre el cursor
	End
Close cursorJU
Deallocate cursorJU

Select * from #temp
drop table #temp

--Select * from tblRepairPath



/* ****************************************************************************************************************** */



CREATE TABLE #temp ( id INT, Name VARCHAR(100), Description varchar(60))

Declare @ClientID int, @OperName varchar(100), @Desc varchar(60)  -- << variables que utiliza el cursor

Declare ClientsCursor cursor Fast_Forward
	For Select Id From Clients Where IsActive = 1

Open ClientsCursor

Fetch Next From ClientsCursor Into @ClientId  -- << recorre el cursor
While @@FETCH_STATUS <> -1
	Begin

		Print Convert(varchar, @ClientId)
		
		Fetch Next From ClientsCursor Into @ClientId  -- << recorre el cursor		
	End
Close ClientsCursor
Deallocate ClientsCursor