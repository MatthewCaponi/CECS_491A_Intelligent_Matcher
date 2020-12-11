﻿/*
Deployment script for UserDatabase

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "UserDatabase"
:setvar DefaultFilePrefix "UserDatabase"
:setvar DefaultDataPath "C:\Users\Matt\AppData\Local\Microsoft\VisualStudio\SSDT\IntelligentMatcher"
:setvar DefaultLogPath "C:\Users\Matt\AppData\Local\Microsoft\VisualStudio\SSDT\IntelligentMatcher"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Dropping [dbo].[FK_UserProfile_UserAccount]...';


GO
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK_UserProfile_UserAccount];


GO
PRINT N'Dropping <unnamed>...';


GO
ALTER TABLE [dbo].[SecurityQuestions] DROP CONSTRAINT [CK__SecurityQ__Quest__2B3F6F97];


GO
PRINT N'Dropping <unnamed>...';


GO
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [CK__UserProfi__Accou__2C3393D0];


GO
PRINT N'Dropping <unnamed>...';


GO
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [CK__UserProfi__Accou__2D27B809];


GO
PRINT N'Creating [dbo].[FK_UserProfile_UserAccount]...';


GO
ALTER TABLE [dbo].[UserProfile] WITH NOCHECK
    ADD CONSTRAINT [FK_UserProfile_UserAccount] FOREIGN KEY ([UserAccountId]) REFERENCES [dbo].[UserAccount] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating unnamed constraint on [dbo].[SecurityQuestions]...';


GO
ALTER TABLE [dbo].[SecurityQuestions] WITH NOCHECK
    ADD CHECK ([Question] IN('What is your mother''s maiden name?', 'What is the name of your first pet?', 
	'What was your first car?', 'What elementary school did you attend?', 'What is the name of the town where you were born?',
	'When you were young, what did you want to be when you grew up?', 'What was your childhood nickname?', 'Who was your childhood hero',
	'Where was your best family vacation as a kid?', 'What was the color of your first car?'));


GO
PRINT N'Creating unnamed constraint on [dbo].[UserProfile]...';


GO
ALTER TABLE [dbo].[UserProfile] WITH NOCHECK
    ADD CHECK ([AccountType] IN('Admin', 'User'));


GO
PRINT N'Creating unnamed constraint on [dbo].[UserProfile]...';


GO
ALTER TABLE [dbo].[UserProfile] WITH NOCHECK
    ADD CHECK ([AccountStatus] IN('Active', 'Disabled', 'Suspended', 'Banned', 'Deleted'));


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
CREATE TABLE [#__checkStatus] (
    id           INT            IDENTITY (1, 1) PRIMARY KEY CLUSTERED,
    [Schema]     NVARCHAR (256),
    [Table]      NVARCHAR (256),
    [Constraint] NVARCHAR (256)
);

SET NOCOUNT ON;

DECLARE tableconstraintnames CURSOR LOCAL FORWARD_ONLY
    FOR SELECT SCHEMA_NAME([schema_id]),
               OBJECT_NAME([parent_object_id]),
               [name],
               0
        FROM   [sys].[objects]
        WHERE  [parent_object_id] IN (OBJECT_ID(N'dbo.SecurityQuestions'), OBJECT_ID(N'dbo.UserProfile'))
               AND [type] IN (N'F', N'C')
                   AND [object_id] IN (SELECT [object_id]
                                       FROM   [sys].[check_constraints]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0
                                       UNION
                                       SELECT [object_id]
                                       FROM   [sys].[foreign_keys]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0);

DECLARE @schemaname AS NVARCHAR (256);

DECLARE @tablename AS NVARCHAR (256);

DECLARE @checkname AS NVARCHAR (256);

DECLARE @is_not_trusted AS INT;

DECLARE @statement AS NVARCHAR (1024);

BEGIN TRY
    OPEN tableconstraintnames;
    FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
    WHILE @@fetch_status = 0
        BEGIN
            PRINT N'Checking constraint: ' + @checkname + N' [' + @schemaname + N'].[' + @tablename + N']';
            SET @statement = N'ALTER TABLE [' + @schemaname + N'].[' + @tablename + N'] WITH ' + CASE @is_not_trusted WHEN 0 THEN N'CHECK' ELSE N'NOCHECK' END + N' CHECK CONSTRAINT [' + @checkname + N']';
            BEGIN TRY
                EXECUTE [sp_executesql] @statement;
            END TRY
            BEGIN CATCH
                INSERT  [#__checkStatus] ([Schema], [Table], [Constraint])
                VALUES                  (@schemaname, @tablename, @checkname);
            END CATCH
            FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
        END
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE();
END CATCH

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') >= 0
    CLOSE tableconstraintnames;

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') = -1
    DEALLOCATE tableconstraintnames;

SELECT N'Constraint verification failed:' + [Schema] + N'.' + [Table] + N',' + [Constraint]
FROM   [#__checkStatus];

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occurred while verifying constraints', 16, 127);
    END

SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
PRINT N'Update complete.';


GO
