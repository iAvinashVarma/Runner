PRINT REPLICATE('=', 30) + ' ' + CONVERT(NVARCHAR(55), GETDATE(), 113) + ' ' + REPLICATE('=', 30)

IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = N'Logger')
BEGIN
	CREATE DATABASE Logger
	PRINT N'+ Logger database created.'
END
ELSE
BEGIN
	PRINT N'* Logger database is already available.'
END
GO 

USE [Logger]
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = N'RunnerLog')
BEGIN
	CREATE TABLE [dbo].[RunnerLog]
	(
		[Id] [int] IDENTITY (1, 1) NOT NULL,
		[Date] [datetime] NOT NULL,
		[Thread] [varchar] (255) NOT NULL,
		[Level] [varchar] (50) NOT NULL,
		[Logger] [varchar] (255) NOT NULL,
		[Message] [varchar] (4000) NOT NULL,
		[Exception] [varchar] (2000) NULL
	)
	PRINT N'+ RunnerLog table created.'
END
ELSE
BEGIN
	PRINT N'* RunnerLog table is already available.'
END
GO

PRINT REPLICATE('=', 30) + ' ' + CONVERT(NVARCHAR(55), GETDATE(), 113) + ' ' + REPLICATE('=', 30)