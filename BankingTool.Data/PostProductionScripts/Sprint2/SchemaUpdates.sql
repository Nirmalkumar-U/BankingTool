If(OBJECT_ID('[FK_CalendarEvent_Users_UserId]') Is Not Null)
BEGIN
	ALTER TABLE [CalendarEvent] DROP CONSTRAINT [FK_CalendarEvent_Users_UserId]
END
GO
IF EXISTS (SELECT * FROM sys.tables where name = N'CalendarEvent')
BEGIN
	DROP TABLE CalendarEvent
END
GO

CREATE TABLE CalendarEvent (
    CalendarEventId INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT CONSTRAINT FK_CalendarEvent_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId) NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    StartDateTime DATETIME2 NOT NULL,
    EndDateTime DATETIME2 NULL,
    EventType NVARCHAR(50) NOT NULL,
    Recurrence NVARCHAR(50) NULL,
    Duration NVARCHAR(50) NULL,
    Priority NVARCHAR(50) NOT NULL DEFAULT 'Normal',
    IsAllDay BIT NOT NULL,
    IsPublic BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME2 NOT NULL,
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedDate DATETIME2 NULL,
    ModifiedBy NVARCHAR(100) NULL,
    IsDeleted BIT NOT NULL
);
------------------------------------------------------------------------------------------------------------------------
If(OBJECT_ID('[FK_WalletAccount_CustomerId]') Is Not Null)
BEGIN
	ALTER TABLE [WalletAccount] DROP CONSTRAINT [FK_WalletAccount_CustomerId]
END
GO

If(OBJECT_ID('[FK_WalletTransactionDetail_WalletTransactionId]') Is Not Null)
BEGIN
	ALTER TABLE [WalletTransactionDetail] DROP CONSTRAINT [FK_WalletTransactionDetail_WalletTransactionId]
END
GO

If(OBJECT_ID('[FK_WalletTransactionDetail_WalletAccountId]') Is Not Null)
BEGIN
	ALTER TABLE [WalletTransactionDetail] DROP CONSTRAINT [FK_WalletTransactionDetail_WalletAccountId]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'WalletAccount')
BEGIN
	DROP TABLE [WalletAccount]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'WalletTransaction')
BEGIN
	DROP TABLE [WalletTransaction]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'WalletTransactionDetail')
BEGIN
	DROP TABLE [WalletTransactionDetail]
END
GO

CREATE TABLE [WalletAccount] (
	WalletAccountId INT CONSTRAINT [PK_WalletAccount] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CustomerId INT NOT NULL CONSTRAINT [FK_WalletAccount_CustomerId] FOREIGN KEY REFERENCES Customer(CustomerId),
	WalletName VARCHAR(50) NOT NULL,
	WalletNumber VARCHAR(8) NOT NULL,
	Balance INT NOT NULL,
	WalletStatus VARCHAR(10) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)

CREATE TABLE [WalletTransaction] (
    WalletTransactionId INT CONSTRAINT [PK_WalletTransaction] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
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

CREATE TABLE [WalletTransactionDetail] (
    WalletTransactionDetailId INT CONSTRAINT [PK_WalletTransactionDetail] PRIMARY KEY IDENTITY(1,1),
    WalletTransactionId INT NOT NULL CONSTRAINT [FK_WalletTransactionDetail_WalletTransactionId] FOREIGN KEY REFERENCES [WalletTransaction](WalletTransactionId),
	WalletAccountId INT NOT NULL CONSTRAINT [FK_WalletTransactionDetail_WalletAccountId] FOREIGN KEY REFERENCES WalletAccount(WalletAccountId),
    TransactionRole VARCHAR(10) NOT NULL,
    TransactionType VARCHAR(10) NOT NULL,
    StageBalance INT NOT NULL
);
--------------------------------------------------------------------------------------------------
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Account' AND COLUMN_NAME = 'Balance' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE [Account]
    ALTER COLUMN [Balance] DECIMAL(18,2) NOT NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BudgetAccount' AND COLUMN_NAME = 'Balance' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE BudgetAccount
    ALTER COLUMN [Balance] DECIMAL(18,2) NOT NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BudgetNotification' AND COLUMN_NAME = 'Amount' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE BudgetNotification
    ALTER COLUMN Amount DECIMAL(18,2) NOT NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BudgetTransaction' AND COLUMN_NAME = 'Amount' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE BudgetTransaction
    ALTER COLUMN Amount DECIMAL(18,2) NOT NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BudgetTransactionDetail' AND COLUMN_NAME = 'StageBalance' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE BudgetTransactionDetail
    ALTER COLUMN [StageBalance] DECIMAL(18,2) NOT NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Card' AND COLUMN_NAME = 'CardLimit' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE [Card]
    ALTER COLUMN CardLimit DECIMAL(18,2) NOT NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CreditScore' AND COLUMN_NAME = 'CreditScoreValue' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE CreditScore
    ALTER COLUMN CreditScoreValue DECIMAL(18,2) NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Transaction' AND COLUMN_NAME = 'Amount' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE [Transaction]
    ALTER COLUMN Amount DECIMAL(18,2) NOT NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TransactionDetail' AND COLUMN_NAME = 'StageBalance' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE TransactionDetail
    ALTER COLUMN [StageBalance] DECIMAL(18,2) NOT NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'WalletAccount' AND COLUMN_NAME = 'Balance' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE WalletAccount
    ALTER COLUMN [Balance] DECIMAL(18,2) NOT NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'WalletTransaction' AND COLUMN_NAME = 'Amount' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE WalletTransaction
    ALTER COLUMN Amount DECIMAL(18,2) NOT NULL;
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'WalletTransactionDetail' AND COLUMN_NAME = 'StageBalance' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE WalletTransactionDetail
    ALTER COLUMN [StageBalance] DECIMAL(18,2) NOT NULL;
END
GO