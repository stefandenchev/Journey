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

CREATE TRIGGER tr_OnGameDeleteAddLogRecord
ON [17114092].Games FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Games', 'DELETE', GETDATE() FROM deleted d
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

CREATE TRIGGER tr_OnGenreDeleteAddLogRecord
ON [17114092].Genres FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Genres', 'DELETE', GETDATE() FROM deleted d
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

CREATE TRIGGER tr_OnLanguageDeleteAddLogRecord
ON [17114092].Languages FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Languages', 'DELETE', GETDATE() FROM deleted d
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

CREATE TRIGGER tr_OnTagDeleteAddLogRecord
ON [17114092].Tags FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Tags', 'DELETE', GETDATE() FROM deleted d
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

CREATE TRIGGER tr_OnPublisherDeleteAddLogRecord
ON [17114092].Publishers FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Publishers', 'DELETE', GETDATE() FROM deleted d
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

CREATE TRIGGER tr_OnGameLanguageDeleteAddLogRecord
ON [17114092].GamesLanguages FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'GamesLanguages', 'DELETE', GETDATE() FROM deleted d
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

CREATE TRIGGER tr_OnGameTagDeleteAddLogRecord
ON [17114092].GamesTags FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'GamesTags', 'DELETE', GETDATE() FROM deleted d
GO

-- IMAGE
CREATE TRIGGER tr_OnImageUpdateAddLogRecord
ON [17114092].Images FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Images', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnImageInsertAddLogRecord
ON [17114092].Images FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Images', 'INSERT', GETDATE() FROM inserted i
GO

CREATE TRIGGER tr_OnImageDeleteAddLogRecord
ON [17114092].Images FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Images', 'DELETE', GETDATE() FROM deleted d
GO

-- CREDITCARD
CREATE TRIGGER tr_OnCreditCardUpdateAddLogRecord
ON [17114092].CreditCards FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'CreditCards', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnCreditCardInsertAddLogRecord
ON [17114092].CreditCards FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'CreditCards', 'INSERT', GETDATE() FROM inserted i
GO

CREATE TRIGGER tr_OnCreditCardDeleteAddLogRecord
ON [17114092].CreditCards FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'CreditCards', 'DELETE', GETDATE() FROM deleted d
GO

-- ORDERITEM
CREATE TRIGGER tr_OnOrderUpdateAddLogRecord
ON [17114092].Orders FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Orders', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnOrderItemInsertAddLogRecord
ON [17114092].Orders FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Orders', 'INSERT', GETDATE() FROM inserted i
GO

CREATE TRIGGER tr_OnOrderItemDeleteAddLogRecord
ON [17114092].Orders FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Orders', 'DELETE', GETDATE() FROM deleted d
GO

-- USERCARTITEM
CREATE TRIGGER tr_OnUserCartItemUpdateAddLogRecord
ON [17114092].UserCartItems FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'UserCartItems', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnUserCartItemItemInsertAddLogRecord
ON [17114092].UserCartItems FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'UserCartItems', 'INSERT', GETDATE() FROM inserted i
GO

CREATE TRIGGER tr_OnUserCartItemItemDeleteAddLogRecord
ON [17114092].UserCartItems FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'UserCartItems', 'DELETE', GETDATE() FROM deleted d
GO

-- WISHLIST
CREATE TRIGGER tr_OnWishlistUpdateAddLogRecord
ON [17114092].Wishlists FOR UPDATE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Wishlists', 'UPDATE', GETDATE() FROM inserted i
		JOIN deleted d ON i.Id = d.Id
GO

CREATE TRIGGER tr_OnWishlistInsertAddLogRecord
ON [17114092].Wishlists FOR INSERT
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Wishlists', 'INSERT', GETDATE() FROM inserted i
GO

CREATE TRIGGER tr_OnWishlistItemDeleteAddLogRecord
ON [17114092].Wishlists FOR DELETE
AS
	INSERT Logs_17114092 (TableName, OperationType, DateOfChange_17114092)
	SELECT 'Wishlists', 'DELETE', GETDATE() FROM deleted d
GO
