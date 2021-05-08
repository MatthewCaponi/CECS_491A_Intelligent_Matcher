CREATE PROCEDURE [dbo].[CreateListingParent]
	@Title nvarchar(50),
	@Details nvarchar(1000),
	@City nvarchar(50),
	@State nvarchar(50),
	@NumberOfParticipants int,
	@InPersonOrRemote nvarchar(50),
	@UserAccountId int,
	@CreationDate datetime,
	@Id int output

AS
begin 
	set nocount on;
	insert into dbo.[Listing]([Title],[Details],[City],[State],[NumberOfParticipants],
	[InPersonOrRemote],[UserAccountID],[CreationDate])
	values (@Title,@Details,@City,@State,@NumberOfParticipants,@InPersonOrRemote,@UserAccountId,@CreationDate);

	set @Id = SCOPE_IDENTITY();
end;
