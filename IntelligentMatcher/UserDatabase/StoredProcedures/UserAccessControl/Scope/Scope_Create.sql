CREATE PROCEDURE [dbo].[Scope_Create]
	@type nvarchar(50),
	@description nvarchar(200),
	@isDefault bit,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[Scope]([type], [description], [isDefault])
		values (@type, @description, @isDefault);

	set @Id = SCOPE_IDENTITY();
end