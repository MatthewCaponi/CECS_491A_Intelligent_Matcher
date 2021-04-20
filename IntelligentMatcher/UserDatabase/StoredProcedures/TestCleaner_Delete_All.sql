CREATE PROCEDURE [dbo].[TestCleaner_Delete_All]
	@TableName sysname
AS
begin
	set nocount on;

	DECLARE @DynamicSQL NVARCHAR(4000)
	SET @DynamicSQL = 'DELETE FROM ' + @TableName
	EXECUTE sp_executesql @DynamicSQL
end
GO