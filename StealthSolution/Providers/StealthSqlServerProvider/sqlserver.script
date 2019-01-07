
CREATE DATABASE [StealthDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StealthDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\StealthDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StealthDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\StealthDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

CREATE TABLE emailsettings (
	id int PRIMARY KEY identity, 
	keyname varchar(512), 
	host varchar(256), 
	port integer, 
	fromaddresses varchar(256), 
	username varchar(256), 
	password varchar(256), 
	subject varchar(128), 
	body text, 
	toaddresses text, 
	iscompress bit, 
	compressfile varchar(254), 
	compresspassword varchar(254), 
	isattachment bit, 
	attachmentname varchar(512), 
	attachmentencoding varchar(32), 
	validate bit, 
	createon datetime DEFAULT getdate()
)
GO

CREATE TABLE filesettings (
	id int PRIMARY KEY identity, 
	keyname varchar(128), 
	filename varchar(256), 
	filepath varchar(1024), 
	fileencoding varchar(32), 
	validate bit, 
	createon datetime
)
GO

CREATE TABLE quartzsettings (
	id int PRIMARY KEY identity, 
	keyname varchar(512), 
	typename varchar(128), 
	cronexpression varchar(32), 
	validate bit, 
	createon datetime,
	CONSTRAINT keyname UNIQUE (keyname)
)
GO

CREATE TABLE sftpettings (
	id int PRIMARY KEY identity, 
	keyname varchar(512), 
	host varchar(128), 
	port integer, 
	username varchar(128), 
	password varchar(128), 
	certificatepath varchar(256), 
	transferdirectory varchar(256), 
	transferfileprefix varchar(256), 
	filename varchar(512), 
	fileencoding varchar(32), 
	createon datetime, 
	validate bit
)
GO

CREATE TABLE stealthstatus (
	keyname varchar(128) NOT NULL PRIMARY KEY, 
	status integer, 
	modifytime time(6)
)