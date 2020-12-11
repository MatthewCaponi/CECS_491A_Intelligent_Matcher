CREATE TABLE [dbo].[SecurityQuestions]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Question] NVARCHAR(100) NOT NULL CHECK ([Question] IN('What is your mother''s maiden name?', 'What is the name of your first pet?', 
	'What was your first car?', 'What elementary school did you attend?', 'What is the name of the town where you were born',
	'When you were young, what did you want to be when you grew up?', 'What was your childhood nickname?', 'Who was your childhood hero',
	'Where was your best family vacation as a kid', 'What was the color of your first car?')), 
)
