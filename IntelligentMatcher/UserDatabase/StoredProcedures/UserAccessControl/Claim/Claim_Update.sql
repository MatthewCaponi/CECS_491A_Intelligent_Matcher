CREATE PROCEDURE [dbo].[Claim_Update]
	@Id int,
	@type nvarchar(50),
	@value nvarchar(200),
	@isDefault bit
AS
begin
	set nocount on;

	update dbo.[Claim]
	set type = @type,
	value = @value,
	isDefault = @isDefault
	where Id = @Id;
end
