GRANT ALL PRIVILEGES ON Historian.* to 'superuser'@'%' IDENTIFIED BY 'somepassword';

CREATE DATABASE Historian;
USE Historian;
CREATE TABLE Sample(
id INTEGER PRIMARY KEY AUTO_INCREMENT,
SampleData VARCHAR(45));
