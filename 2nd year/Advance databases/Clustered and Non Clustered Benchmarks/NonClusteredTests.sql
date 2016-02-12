/*
	File Name: NonClusteredTests.sql
	Assignment: Advanced SQL Assignment 1
	By: Naween Mehanmal and William Pring
	Date: January 27, 2015
	Description: SQL script the demonstrates a comparison between running queries with 
	non-clustered indexed items

*/

USE FoodMart; 

/* Insert more rows */

INSERT INTO sales_fact_1998 SELECT * FROM sales_fact_1998 

INSERT INTO sales_fact_1998 SELECT * FROM sales_fact_1998 

INSERT INTO sales_fact_1998 SELECT * FROM sales_fact_1998 
SELECT COUNT(*) FROM sales_fact_1998


/* Non-Clustered Comparison for sorted results */

CREATE NONCLUSTERED INDEX myCluster ON sales_fact_1998(store_sales)

CHECKPOINT;
GO
DBCC DROPCLEANBUFFERS;
GO
DBCC FREEPROCCACHE;
GO
SET STATISTICS TIME ON;
SET STATISTICS IO ON;
GO
SELECT product_id FROM sales_fact_1998 ORDER BY product_id DESC
GO
SET STATISTICS TIME OFF;
SET STATISTICS IO OFF;

DROP INDEX myCluster ON sales_fact_1998


/* Clustered and Non-Clustered Comparison with WHERE criteria (Range Criteria) */

CREATE NONCLUSTERED INDEX myCluster ON sales_fact_1998(store_sales)

CHECKPOINT;
GO
DBCC DROPCLEANBUFFERS;
GO
DBCC FREEPROCCACHE;
GO
SET STATISTICS TIME ON;
SET STATISTICS IO ON;
GO
SELECT store_sales FROM sales_fact_1998 WHERE store_sales >= 1 AND store_sales < 1000 
GO
SET STATISTICS TIME OFF;
SET STATISTICS IO OFF;
DROP INDEX myCluster ON sales_fact_1998


/* Clustered and Non-Clustered Comparison with WHERE criteria (Single Value) */

CREATE NONCLUSTERED INDEX myCluster ON sales_fact_1998(store_sales)

CHECKPOINT;
GO
DBCC DROPCLEANBUFFERS;
GO
DBCC FREEPROCCACHE;
GO
SET STATISTICS TIME ON;
SET STATISTICS IO ON;
GO
SELECT product_id FROM sales_fact_1998 WHERE product_id = 10 
GO
SET STATISTICS TIME OFF;
SET STATISTICS IO OFF;
DROP INDEX myCluster ON sales_fact_1998


/* Clustered and Non-Clustered Comparison with JOIN and WHERE clause */

CREATE NONCLUSTERED INDEX myCluster ON sales_fact_1998(store_sales)

CHECKPOINT;
GO
DBCC DROPCLEANBUFFERS;
GO
DBCC FREEPROCCACHE;
GO
SET STATISTICS TIME ON;
SET STATISTICS IO ON;
GO
SELECT sales_fact_1998.product_id FROM sales_fact_1998 INNER JOIN product ON sales_fact_1998.product_id = product.product_id WHERE gross_weight > 10 
GO
SET STATISTICS TIME OFF;
SET STATISTICS IO OFF;
DROP INDEX myCluster ON sales_fact_1998











