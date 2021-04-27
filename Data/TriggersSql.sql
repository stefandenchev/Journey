-- GAME
CREATE TRIGGER tr_OnGameUpdateAddLogRecord
ON [17114092].Games FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Games', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnGameInsertAddLogRecord
ON [17114092].Games FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Games', 'INSERT', GETDATE() FROM inserted i
GO

-- GENRE
CREATE TRIGGER tr_OnGenreUpdateAddLogRecord
ON [17114092].Genres FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Genres', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnGenreInsertAddLogRecord
ON [17114092].Genres FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Genres', 'INSERT', GETDATE() FROM inserted i
GO

-- LANGUAGE
CREATE TRIGGER tr_OnLanguageUpdateAddLogRecord
ON [17114092].Languages FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Languages', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnLanguageInsertAddLogRecord
ON [17114092].Languages FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Languages', 'INSERT', GETDATE() FROM inserted i
GO

-- TAG
CREATE TRIGGER tr_OnTagUpdateAddLogRecord
ON [17114092].Tags FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Tags', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnTagInsertAddLogRecord
ON [17114092].Tags FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Tags', 'INSERT', GETDATE() FROM inserted i
GO

-- PUBLISHER
CREATE TRIGGER tr_OnPublisherUpdateAddLogRecord
ON [17114092].Publishers FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Publishers', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnPublisherInsertAddLogRecord
ON [17114092].Publishers FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Publishers', 'INSERT', GETDATE() FROM inserted i
GO

-- GAMEGENRE
CREATE TRIGGER tr_OnGameGenreUpdateAddLogRecord
ON [17114092].GamesGenres FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'GamesGenres', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnGameGenreInsertAddLogRecord
ON [17114092].GamesGenres FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'GamesGenres', 'INSERT', GETDATE() FROM inserted i
GO

-- GAMELANGUAGE
CREATE TRIGGER tr_OnGameLanguageUpdateAddLogRecord
ON [17114092].GamesLanguages FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'GamesLanguages', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnGameLanguageInsertAddLogRecord
ON [17114092].GamesLanguages FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'GamesLanguages', 'INSERT', GETDATE() FROM inserted i
GO

-- GAMETAG
CREATE TRIGGER tr_OnGameTagUpdateAddLogRecord
ON [17114092].GamesTags FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'GamesTags', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnGameTagInsertAddLogRecord
ON [17114092].GamesTags FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'GamesTags', 'INSERT', GETDATE() FROM inserted i
GO