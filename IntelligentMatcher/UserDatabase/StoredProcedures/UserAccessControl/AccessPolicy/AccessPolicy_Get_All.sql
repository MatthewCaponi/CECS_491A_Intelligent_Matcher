CREATE PROCEDURE [dbo].[AccessPolicy_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [name], [resourceId], [priority]
	from dbo.[AccessPolicy];
end