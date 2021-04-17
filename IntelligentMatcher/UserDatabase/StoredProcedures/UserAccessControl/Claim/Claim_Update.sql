CREATE PROCEDURE [dbo].[Claim_Update]
	@Id int,
	@name nvarchar(50),
	@description nvarchar(200),
	@isDefault bit
AS
begin
	set nocount on;

	update dbo.[Claim]
	set name = @name,
	description = @description,
	isDefault = @isDefault
	where Id = @Id;
end
