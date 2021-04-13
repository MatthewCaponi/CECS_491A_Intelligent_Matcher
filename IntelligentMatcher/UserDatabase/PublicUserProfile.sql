CREATE TABLE [dbo].[PublicUserProfile]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[UserId] INT NOT NULL,
	[Description] NVARCHAR(1000),
	[Intrests] NVARCHAR(1000),
	[Hobbies] NVARCHAR(1000),
	[Jobs] NVARCHAR(1000),
	[Goals] NVARCHAR(1000),
	[Age] INT,
	[Gender] NVARCHAR(6),
	[Ethnicity] NVARCHAR(100),
	[SexualOrientation] NVARCHAR(100),
	[Height] NVARCHAR(1000),
	[Visibility] NVARCHAR(20),
	[Photo] NVARCHAR(1000)
)
