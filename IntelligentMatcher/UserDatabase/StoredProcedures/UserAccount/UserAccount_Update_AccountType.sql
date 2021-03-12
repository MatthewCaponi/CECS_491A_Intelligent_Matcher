CREATE PROCEDURE [dbo].[UserAccount_Update_AccountType]
	@Id int,
	@AccountType nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[UserAccount]
	set AccountType = @AccountType
	where Id = @Id;
end
