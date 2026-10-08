/* **************************************************************************************************************************** */

WAITFOR DELAY '00:00:10'


-- Read Uncommitted Data
SELECT * FROM Orders WITH (NOLOCK);
SELECT * FROM Employees WITH (NOLOCK) WHERE DepartmentID = 3;


-- Force Row-Level Allocation
UPDATE Inventory WITH (ROWLOCK) 
SET Stock = Stock - 1 
WHERE ItemID = 101;


-- Force Exclusive Locks
BEGIN TRANSACTION;
	SELECT * FROM BankAccounts WITH (XLOCK, ROWLOCK) 
	WHERE AccountID = 5432;
	-- Perform business logic safely knowing no other process can touch this row
COMMIT TRANSACTION;


---- 
SET DEADLOCK_PRIORITY HIGH;
BEGIN TRANSACTION;

	UPDATE Inventory 
	SET Stock = Stock - 1 
	WHERE ProductID = 101;

	UPDATE Orders 
	SET Quantity = Quantity + 1 
	WHERE OrderID = 5005;

COMMIT TRANSACTION;
