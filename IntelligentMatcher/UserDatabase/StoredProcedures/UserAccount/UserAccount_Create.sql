CREATE PROCEDURE [dbo].[UserAccount_Create]
	@Username nvarchar(50),
	@Password nvarchar(50),
	@Salt nvarchar(100),
	@EmailAddress nvarchar(50),
	@AccountType nvarchar(50),
	@AccountStatus nvarchar(50),
	@CreationDate date,
	@UpdationDate date,
	@StatusToken nvarchar(200),
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[UserAccount]([Username], [Password], [Salt], [EmailAddress], 
	[AccountType], [AccountStatus], [CreationDate], [UpdationDate], [StatusToken])
	values (@Username, @Password, @Salt, @EmailAddress,
	@AccountType, @AccountStatus, @CreationDate, @UpdationDate, @StatusToken);

	set @Id = SCOPE_IDENTITY();
end