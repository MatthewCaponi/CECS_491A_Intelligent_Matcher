CREATE TABLE [dbo].[UserReports]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Report] NVARCHAR(1000) NOT NULL,	
	[Date] DateTime NOT NULL,
	[ReportingId] INT NOT NULL,
	[ReportedId] INT NOT NULL


)
