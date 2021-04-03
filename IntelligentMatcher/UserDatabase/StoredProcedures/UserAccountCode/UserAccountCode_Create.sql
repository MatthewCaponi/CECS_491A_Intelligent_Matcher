CREATE PROCEDURE [dbo].[UserAccountCode_Create]
	@Code nvarchar(50),
	@ExpirationTime datetimeoffset(7),
	@UserAccountId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[UserAccountCode]([Code], [ExpirationTime], [UserAccountId])
                        values (@Code, @ExpirationTime, @UserAccountId); 

	set @Id = SCOPE_IDENTITY();
end