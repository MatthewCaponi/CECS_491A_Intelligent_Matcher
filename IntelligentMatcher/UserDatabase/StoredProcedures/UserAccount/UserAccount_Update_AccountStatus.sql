CREATE PROCEDURE [dbo].[UserAccount_Update_AccountStatus]
	@Id int,
	@AccountStatus nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[UserAccount]
	set AccountStatus = @AccountStatus
	where Id = @Id;
end
