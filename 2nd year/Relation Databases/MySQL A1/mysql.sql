#	FILE		: RDBa3.sql
#	PROJECT		: Relational Databases assignment #3
#	PROGRAMMER	: WILLIAM PRING
#	FIRST VERSION	: OCT 22, 2015
#	DESCRIPTION	: Uses the Northwind database by modifying, updating and deleting it 


USE Northwind;

SELECT CustomerID, ContactName, Country, City FROM Customers;

SELECT DISTINCT Country FROM customers ORDER BY Country;

SELECT companyName, city FROM customers WHERE country='germany';
 
SELECT CustomerID, ContactName FROM customers WHERE fax IS NULL;

SELECT COUNT(*) FROM products;

SELECT ProductID, ProductName, UnitPrice FROM Products;

SELECT ProductName, UnitsInStock, UnitPrice FROM products WHERE UnitPrice > 20 ORDER BY `UnitPrice` DESC;

SELECT ProductName, UnitsInStock, UnitPrice FROM Products WHERE Discontinued = '-1';

SELECT categories.CategoryName, products.ProductName FROM products INNER JOIN categories ON products.CategoryID = categories.CategoryID;

Select CONCAT(Title, '' ,FirstName, '' ,LastName) AS 'Salutation' From Employees;

SELECT DISTINCT TerritoryDescription, RegionDescription From Territories INNER JOIN Region ON Territories.RegionID = Region.RegionID;

SELECT `Order Details`.OrderID, CustomerID, ProductID, Quantity FROM `Order Details` INNER JOIN Orders ON `Order Details`.OrderID = Orders.OrderID;

SELECT `Order Details`.OrderID, CustomerID, ProductID, Quantity * UnitPrice AS `Extended Price` FROM `Order Details` INNER JOIN Orders ON `Order Details`.OrderID = Orders.OrderID;

SELECT OrderID, OrderDate, CompanyName, CONCAT(FirstName, ' ', LastName) AS `Employee Name` FROM Orders 
INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID
INNER JOIN Employees ON Orders.EmployeeID =  Employees.EmployeeID;

SELECT DISTINCT Orders.CustomerID, CompanyName AS CustomerName From Orders INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID;

SELECT CustomerID FROM Customers WHERE CustomerID NOT IN (SELECT DISTINCT Orders.CustomerID From Orders);

INSERT INTO region VALUES(NULL, 'Europe');

DELETE from region WHERE RegionDescription = 'Europe';

UPDATE customers SET contactname = 'Hans Schmidt' WHERE CompanyName = 'Ernst Handel';

UPDATE products SET UnitPrice = UnitPrice +1 WHERE UnitPrice > 0;

INSERT INTO categories VALUES('9' ,'Discontinued', NULL, NULL);

UPDATE Products SET CategoryID=9 WHERE Discontinued = -1;
