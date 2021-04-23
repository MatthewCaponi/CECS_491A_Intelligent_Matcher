CREATE PROCEDURE [dbo].[UserAccount_Update_AccountStatusToken]
	@Id int,
	@StatusToken nvarchar(200)
AS
begin
	set nocount on;

	update dbo.[UserAccount]
	set StatusToken = @StatusToken
	where Id = @Id;
end
