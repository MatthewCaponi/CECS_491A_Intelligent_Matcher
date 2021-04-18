CREATE PROCEDURE [dbo].[Resource_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [name]
	from dbo.[Resource];
end