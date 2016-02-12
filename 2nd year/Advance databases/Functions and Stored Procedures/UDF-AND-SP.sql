/*
	File Name: UDF-AND-SP.sql
	Assignment: Assignment 2 Advanced SQL
	By: Naween Mehanmal and William Pring
	Description: The sql script demonstrates the use of Stored Procedures and User-Defined Functions offered
	in T-SQL to allow code reusability and modular code for functionality.
*/

USE NORTHWND

GO
CREATE TABLE NewOrders
(
	OrderID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CustomerID nchar(5),
	EmployeeID int,
	OrderDate DATETIME,
	RequiredDate DATETIME,
	ShippedDate DATETIME,
	ShipVia int,
	Freight MONEY,
	ShipName nvarchar(40),
	ShipAddress NVARCHAR(60),
	ShipCity NVARCHAR(15),
	ShipRegion NVARCHAR(15),
	ShipPostalCode NVARCHAR(10),
	ShipCountry NVARCHAR(15),
	FOREIGN KEY (CustomerID) REFERENCES customers(CustomerID),
	FOREIGN KEY (EmployeeID) REFERENCES employees(EmployeeID),
	FOREIGN KEY (ShipVia) REFERENCES shippers(ShipperID)
);
GO

GO
CREATE TABLE NewOrderDetails
(
	OrderID INT NOT NULL,
	ProductID INT NOT NULL,
	UnitPrice MONEY NOT NULL,
	Quantity SMALLINT NOT NULL,
	Discount REAL NOT NULL,
	PRIMARY KEY (OrderID, ProductID),
	FOREIGN KEY (OrderID) REFERENCES NewOrders(OrderID),
	FOREIGN KEY (ProductID) REFERENCES products(ProductID)
);
GO


GO
ALTER PROCEDURE OrderGenerator  

@StartDate date,
@EndDate date,
@RecordCount int
AS
BEGIN
	DECLARE @retValue int = 0 /* True */
	DECLARE @cnt int = 0
	DECLARE @productCount int = 0 

	DECLARE @randEmployee   nvarchar(10)
	DECLARE @randCustomer   nvarchar(10)	
	DECLARE @differenceDate nvarchar(10)
	DECLARE @newOrderID	  int 
	DECLARE @randProduct  int

	DECLARE @newProduct   int
	DECLARE @newQuantity  int
	DECLARE @newDiscount  int

	DECLARE @unitPrice int

	SET @newDiscount = 0
	SET @unitPrice = 10
	 
	BEGIN TRY
	
	WHILE @cnt < @RecordCount
		BEGIN
		
		SET @differenceDate = dateadd(day, rand(checksum(newid()))*(1+datediff(day, @StartDate, @EndDate)), @StartDate)
		SET @randCustomer = (SELECT TOP 1 CustomerID FROM Customers ORDER BY NEWID())
		SET @randEmployee = (SELECT TOP 1 EmployeeID FROM Employees ORDER BY NEWID())

		
		INSERT INTO NewOrders (CustomerID,EmployeeID, OrderDate) VALUES (@randCustomer,@randEmployee, @DifferenceDate)	
					
	
		SELECT @newOrderID = SCOPE_IDENTITY() FROM NewOrders;

			WHILE @productCount < @randProduct
			BEGIN
			
				SET @newQuantity = RAND()*(10)+1
				SET @newProduct =  (SELECT TOP 1 ProductID FROM Products ORDER BY NEWID())
				INSERT INTO NewOrderDetails (OrderID, ProductID, UnitPrice, Quantity, Discount) VALUES (@newOrderID, @newProduct, @unitPrice, @newQuantity, @newDiscount)
				SET @productCount = @productCount + 1			
			END


			SET @productCount = 0 
	
		SET @cnt = @cnt + 1;

		END	

	END TRY 

	BEGIN CATCH
		SET @retValue = 1
		SELECT ErrorLine = ERROR_LINE(),
		ErrorMessage = ERROR_MESSAGE(),
		ErrorNumber = ERROR_NUMBER(),
		ErrorProcedure = ERROR_SEVERITY(),
		ErrorState = ERROR_STATE()
	END CATCH

	RETURN @retValue
END
GO


GO
ALTER FUNCTION DaysBetweenOrders

(@Customer nchar(10))
RETURNS int
AS
BEGIN
	DECLARE @StartingDay as nchar(40) 
	DECLARE @EndingDay   as nchar(40) 
	DECLARE @OrderNumber INT
	DECLARE @Difference  FLOAT
	DECLARE @CustomerOrderNumber INT
	DECLARE @Counter     INT = 0
	DECLARE @ReturnValue INT = 1

	

		SET @CustomerOrderNumber = (SELECT COUNT(*) FROM NewOrders WHERE CustomerID = @Customer)
	
		/* Get the avg # of days between orders of that customer */
		
		
		IF @CustomerOrderNumber > 1
		BEGIN	
			SET @StartingDay = (SELECT OrderDate FROM NewOrders WHERE CustomerID = @Customer ORDER BY OrderDate ASC OFFSET @Counter ROWS FETCH NEXT 1 ROWS ONLY) 

			WHILE @Counter < @CustomerOrderNumber
			BEGIN 
				SET @EndingDay   = (SELECT OrderDate FROM NewOrders WHERE CustomerID = @Customer ORDER BY OrderDate ASC OFFSET (@Counter) ROWS FETCH NEXT 1 ROWS ONLY)			
				SET @ReturnValue = @Difference			
				SET @Counter = @Counter + 1	
			END

			SET @Difference  = DATEDIFF(day ,@StartingDay, @EndingDay)
			SET @ReturnValue = CEILING(@Difference)

		END
		ELSE 
		BEGIN
			SET @ReturnValue = 0
		END	

	RETURN @ReturnValue
END
GO


EXEC OrderGenerator '1995-01-01', '2002-05-07', 4 /* Testing Stored Procedure */
EXEC DaysBetweenOrders 'ANTON' /* Testing User-Defined Function */