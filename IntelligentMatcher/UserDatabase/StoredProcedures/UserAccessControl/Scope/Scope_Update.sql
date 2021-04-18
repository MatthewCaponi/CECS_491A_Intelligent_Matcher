CREATE PROCEDURE [dbo].[Scope_Update]
	@Id int,
	@name nvarchar(50),
	@description nvarchar(200),
	@isDefault bit
AS
begin
	set nocount on;

	update dbo.[Scope]
	set name = @name,
	description = @description,
	isDefault = @isDefault
	where Id = @Id;
end
