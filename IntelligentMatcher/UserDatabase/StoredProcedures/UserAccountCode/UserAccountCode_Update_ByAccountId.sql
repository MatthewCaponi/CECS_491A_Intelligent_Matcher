CREATE PROCEDURE [dbo].[UserAccountCode_Update_ByUserAccountId]
	@Code nvarchar(50),
	@ExpirationTime datetimeoffset(7),
	@UserAccountId int
AS
begin
	set nocount on;

	update dbo.[UserAccountCode]
	set Code = @Code,
		ExpirationTime = @ExpirationTime
	where UserAccountId = @UserAccountId;
end