CREATE PROCEDURE [dbo].[Scope_Update]
	@Id int,
	@type nvarchar(50),
	@description nvarchar(200),
	@isDefault bit
AS
begin
	set nocount on;

	update dbo.[Scope]
	set type = @type,
	description = @description,
	isDefault = @isDefault
	where Id = @Id;
end
