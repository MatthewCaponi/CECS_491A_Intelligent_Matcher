CREATE PROCEDURE [dbo].[Scope_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [type], [description], [isDefault]
	from dbo.[Scope];
end