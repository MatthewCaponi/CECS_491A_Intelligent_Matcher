CREATE PROCEDURE [dbo].[Testing_Reseed]
	@TableName sysname,
	@NEWSEEDNUMBER int
AS
begin
	set nocount on;

	DBCC CHECKIDENT (@TableName, RESEED, @NEWSEEDNUMBER);
end
