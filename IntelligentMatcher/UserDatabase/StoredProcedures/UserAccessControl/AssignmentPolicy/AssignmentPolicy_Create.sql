CREATE PROCEDURE [dbo].[AssignmentPolicy_Create]
	@name nvarchar(50),
	@isDefault bit,
	@requiredAccountType nvarchar(50),
	@priority int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[AssignmentPolicy]([name], [isDefault], [requiredAccountType], [priority])
		values (@name, @isDefault, @requiredAccountType, @priority);

	set @Id = SCOPE_IDENTITY();
end