IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Transaction' AND COLUMN_NAME = 'TransactionTag')
BEGIN
    ALTER TABLE [Transaction]
    ADD TransactionTag VARCHAR(50) NULL;
END
GO
--------------------------------------------------------------------------------------------------------------------------------
If(OBJECT_ID('[FK_BudgetAccount_CustomerId]') Is Not Null)
BEGIN
	ALTER TABLE [BudgetAccount] DROP CONSTRAINT [FK_BudgetAccount_CustomerId]
END
GO

If(OBJECT_ID('[FK_BudgetTransactionDetail_BudgetTransactionId]') Is Not Null)
BEGIN
	ALTER TABLE [BudgetTransactionDetail] DROP CONSTRAINT [FK_BudgetTransactionDetail_BudgetTransactionId]
END
GO

If(OBJECT_ID('[FK_BudgetTransactionDetail_AccountId]') Is Not Null)
BEGIN
	ALTER TABLE [BudgetTransactionDetail] DROP CONSTRAINT [FK_BudgetTransactionDetail_AccountId]
END
GO

If(OBJECT_ID('[FK_BudgetTransactionDetail_BudgetAccountId]') Is Not Null)
BEGIN
	ALTER TABLE [BudgetTransactionDetail] DROP CONSTRAINT [FK_BudgetTransactionDetail_BudgetAccountId]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'BudgetAccount')
BEGIN
	DROP TABLE [BudgetAccount]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'BudgetTransaction')
BEGIN
	DROP TABLE [BudgetTransaction]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'BudgetTransactionDetail')
BEGIN
	DROP TABLE [BudgetTransactionDetail]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'BudgetNotification')
BEGIN
	DROP TABLE [BudgetNotification]
END
GO

CREATE TABLE [BudgetAccount] (
	BudgetAccountId INT CONSTRAINT [PK_BudgetAccount] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CustomerId INT NOT NULL CONSTRAINT [FK_BudgetAccount_CustomerId] FOREIGN KEY REFERENCES Customer(CustomerId),
	BudgetName VARCHAR(50) NOT NULL,
	BudgetNumber VARCHAR(8) NOT NULL,
	Balance INT NOT NULL,
	BudgetStatus VARCHAR(10) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)

CREATE TABLE [BudgetTransaction] (
    BudgetTransactionId INT CONSTRAINT [PK_BudgetTransaction] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    TransactionCategory VARCHAR(20) NOT NULL,
    Amount INT NOT NULL,
    Description VARCHAR(500),
    TransactionTime DATETIME NOT NULL DEFAULT GETDATE(),
	TransactionTag VARCHAR(50) NULL,
    CreatedBy VARCHAR(30) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy VARCHAR(30) NULL,
    ModifiedDate DATETIME NULL,
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE [BudgetTransactionDetail] (
    BudgetTransactionDetailId INT CONSTRAINT [PK_BudgetTransactionDetail] PRIMARY KEY IDENTITY(1,1),
    BudgetTransactionId INT NOT NULL CONSTRAINT [FK_BudgetTransactionDetail_BudgetTransactionId] FOREIGN KEY REFERENCES [BudgetTransaction](BudgetTransactionId),
    AccountId INT NOT NULL CONSTRAINT [FK_BudgetTransactionDetail_AccountId] FOREIGN KEY REFERENCES Account(AccountId),
	BudgetAccountId INT NOT NULL CONSTRAINT [FK_BudgetTransactionDetail_BudgetAccountId] FOREIGN KEY REFERENCES BudgetAccount(BudgetAccountId),
    TransactionRole VARCHAR(10) NOT NULL,
    TransactionType VARCHAR(10) NOT NULL,
    StageBalance INT NOT NULL
);

CREATE TABLE [BudgetNotification] (
	BudgetNotificationId INT CONSTRAINT [PK_BudgetNotification] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CustomerId INT NOT NULL,
	AccountId INT NOT NULL,
	TransactionRole VARCHAR(20) NOT NULL,
	TransactionType VARCHAR(20) NOT NULL,
	TransactionTag VARCHAR(20) NOT NULL,
	TransactionCatagory VARCHAR(20) NOT NULL,
	Description VARCHAR(20) NOT NULL,
	Amount INT NOT NULL,
	TransactionDate DATETIME NOT NULL,
	NotificationStatus VARCHAR(30) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)
---------------------------------------------------------------------------------------------------------
If(OBJECT_ID('[FK_Notes_Users_UserId]') Is Not Null)
BEGIN
	ALTER TABLE [Note] DROP CONSTRAINT [FK_Notes_Users_UserId]
END
GO
IF EXISTS (SELECT * FROM sys.tables where name = N'Note')
BEGIN
	DROP TABLE [Note]
END
GO
CREATE TABLE [dbo].[Note] (
    [NoteId] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT CONSTRAINT FK_Notes_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId) NOT NULL,
    [Title] NVARCHAR(200) NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [CreatedBy] NVARCHAR(100) NOT NULL,
    [ModifiedDate] DATETIME2 NULL,
    [ModifiedBy] NVARCHAR(100) NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
);

