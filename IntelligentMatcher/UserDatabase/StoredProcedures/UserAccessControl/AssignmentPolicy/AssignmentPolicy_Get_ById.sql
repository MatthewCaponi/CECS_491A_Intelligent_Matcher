CREATE PROCEDURE [dbo].[AssignmentPolicy_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [name], [isDefault], [requiredAccountType], [priority]
	from dbo.[AssignmentPolicy]
	where Id = @Id;
end