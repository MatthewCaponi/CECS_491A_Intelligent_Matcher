﻿CREATE PROCEDURE [dbo].[CreateParentListing]
	@Title nvarchar(50),
	@Details nvarchar(1000),
	@City nvarchar(50),
	@State nvarchar(50),
	@NumberOfParticipants int,
	@InPersonOrRemote nvarchar(50),
	@CreationDate date,
	@Id int output

AS
begin 
	set nocount on;
	insert into dbo.[Listing]([Title],[Details],[City],[State],[NumberOfParticipants],
	[InPersonOrRemote],[CreationDate])
	values (@Title,@Details,@City,@State,@NumberOfParticipants,@InPersonOrRemote,
	@CreationDate);

	set @Id = SCOPE_IDENTITY();
end;
