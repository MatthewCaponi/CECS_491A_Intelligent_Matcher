﻿CREATE PROCEDURE [dbo].[Channel_Get_Id.]
	@Id int
AS
begin

	set nocount on

	select [Id], [OwnerId], [Name]
	from dbo.[Channels]
	where Id = @Id;
end