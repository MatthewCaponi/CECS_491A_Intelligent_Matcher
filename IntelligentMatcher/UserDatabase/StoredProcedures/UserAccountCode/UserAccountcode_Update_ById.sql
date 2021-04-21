CREATE PROCEDURE [dbo].[UserAccountCode_Update_ById]
	@Code nvarchar(50),
	@ExpirationTime datetimeoffset(7),
	@Id int
AS
begin
	set nocount on;

	update dbo.[UserAccountCode]
	set Code = @Code,
		ExpirationTime = @ExpirationTime
	where Id = @Id;
end