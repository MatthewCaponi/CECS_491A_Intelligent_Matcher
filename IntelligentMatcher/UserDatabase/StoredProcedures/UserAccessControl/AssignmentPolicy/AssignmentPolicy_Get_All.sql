CREATE PROCEDURE [dbo].[AssignmentPolicy_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [name], [isDefault], [requiredAccountType], [priority]
	from dbo.[AssignmentPolicy];
end