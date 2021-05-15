CREATE PROCEDURE [dbo].[Claim_Create]
	@type nvarchar(50),
	@value nvarchar(200),
	@isDefault bit,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[Claim]([type], [value], [isDefault])
		values (@type, @value, @isDefault);

	set @Id = SCOPE_IDENTITY();
end