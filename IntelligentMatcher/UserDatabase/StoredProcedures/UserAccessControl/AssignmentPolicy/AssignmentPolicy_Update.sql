CREATE PROCEDURE [dbo].[AssignmentPolicy_Update]
	@Id int,
	@name nvarchar(50),
	@isDefault bit,
	@requiredAccountType nvarchar(50),
	@priority int
AS
begin
	set nocount on;

	update dbo.[AssignmentPolicy]
	set name = @name,
	isDefault = @isDefault,
	requiredAccountType = @requiredAccountType,
	priority = @priority
	where Id = @Id;
end
