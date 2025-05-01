IF(OBJECT_ID('[FK_Users_RoleId]') IS NOT NULL)
BEGIN
	ALTER TABLE [Users] DROP CONSTRAINT [FK_Users_RoleId]
END
GO

If(OBJECT_ID('[FK_UserRole_RoleId]') Is Not Null)
BEGIN
	ALTER TABLE [UserRole] DROP CONSTRAINT [FK_UserRole_RoleId]
END
GO

If(OBJECT_ID('[FK_UserRole_UserId]') Is Not Null)
BEGIN
	ALTER TABLE [UserRole] DROP CONSTRAINT [FK_UserRole_UserId]
END
GO

If(OBJECT_ID('[FK_RoleAccess_RoleId]') Is Not Null)
BEGIN
	ALTER TABLE [RoleAccess] DROP CONSTRAINT [FK_RoleAccess_RoleId]
END
GO

If(OBJECT_ID('[FK_RoleAccess_ActionId]') Is Not Null)
BEGIN
	ALTER TABLE [RoleAccess] DROP CONSTRAINT [FK_RoleAccess_ActionId]
END
GO

If(OBJECT_ID('[FK_Staff_UserId]') Is Not Null)
BEGIN
	ALTER TABLE [Staff] DROP CONSTRAINT [FK_Staff_UserId]
END
GO

If(OBJECT_ID('[FK_Customer_UserId]') Is Not Null)
BEGIN
	ALTER TABLE [Customer] DROP CONSTRAINT [FK_Customer_UserId]
END
GO

If(OBJECT_ID('FK_Account_CustomerId') Is Not Null)
BEGIN
	ALTER TABLE [Account] DROP CONSTRAINT [FK_Account_CustomerId]
END
GO

If(OBJECT_ID('FK_TransactionDetail_AccountId') Is Not Null)
BEGIN
	ALTER TABLE [TransactionDetail] DROP CONSTRAINT [FK_TransactionDetail_AccountId]
END
GO

If(OBJECT_ID('FK_TransactionDetail_TransactionId') Is Not Null)
BEGIN
	ALTER TABLE [TransactionDetail] DROP CONSTRAINT [FK_TransactionDetail_TransactionId]
END
GO

If(OBJECT_ID('FK_Card_AccountId') Is Not Null)
BEGIN
	ALTER TABLE [Card] DROP CONSTRAINT [FK_Card_AccountId]
END
GO

If(OBJECT_ID('FK_CreditScore_CustomerId') Is Not Null)
BEGIN
	ALTER TABLE [CreditScore] DROP CONSTRAINT [FK_CreditScore_CustomerId]
END
GO

IF(OBJECT_ID('[FK_City_StateId]') IS NOT NULL)
BEGIN
	ALTER TABLE [City] DROP CONSTRAINT [FK_City_StateId]
END
GO

IF(OBJECT_ID('[FK_Account_BankId]') IS NOT NULL)
BEGIN
	ALTER TABLE [Account] DROP CONSTRAINT [FK_Account_BankId]
END
GO

------------------------------------------------------------------------DROP CONSTRAINT-------------------------------
IF EXISTS (SELECT * FROM sys.tables where name = N'Role')
BEGIN
	DROP TABLE [Role]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'Users')
BEGIN
	DROP TABLE [Users]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'Action')
BEGIN
	DROP TABLE [Action]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'RoleAccess')
BEGIN
	DROP TABLE [RoleAccess]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'UserRole')
BEGIN
	DROP TABLE [UserRole]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'Staff')
BEGIN
	DROP TABLE [Staff]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'Customer')
BEGIN
	DROP TABLE [Customer]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'Account')
BEGIN
	DROP TABLE [Account]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'Transaction')
BEGIN
	DROP TABLE [Transaction]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'TransactionDetail')
BEGIN
	DROP TABLE [TransactionDetail]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'Card')
BEGIN
	DROP TABLE [Card]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'CreditScore')
BEGIN
	DROP TABLE [CreditScore]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'State')
BEGIN
	DROP TABLE [State]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'City')
BEGIN
	DROP TABLE [City]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'Bank')
BEGIN
	DROP TABLE [Bank]
END
GO

IF EXISTS (SELECT * FROM sys.tables where name = N'CodeValue')
BEGIN
	DROP TABLE [CodeValue]
END
GO
----------------------------------------------------------------------------------DROP TABLE--------------------------
CREATE TABLE [Role] (
	RoleId INT CONSTRAINT [PK_RoleId] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	RoleName VARCHAR(100) NOT NULL,
	RoleCode VARCHAR(10) NOT NULL,
	RoleLevel INT NOT NULL,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)

CREATE TABLE [Users] (
	UserId INT CONSTRAINT [PK_UserId] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Password VARCHAR(500) NOT NULL,
	EmailId VARCHAR(500) NOT NULL,
	FirstName VARCHAR(100) NOT NULL,
	LastName VARCHAR(100) NOT NULL,
	City INT NOT NULL,
	State INT NOT NULL,
	IsActive BIT DEFAULT 1,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)

CREATE TABLE [Action] (
	ActionId INT CONSTRAINT [PK_Actions] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	ActionName VARCHAR(100) NOT NULL,
	ActionPath VARCHAR(100) NULL,
	ActionType VARCHAR(100) NOT NULL,
	Access VARCHAR(15) NULL,
	MenuLevel INT NULL,
	ParrentMenuId INT NULL,
	Sequence INT NULL,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)

CREATE TABLE [UserRole] (
	RoleId INT NOT NULL CONSTRAINT [FK_UserRole_RoleId] FOREIGN KEY REFERENCES Role(RoleId),
	UserId INT NOT NULL CONSTRAINT [FK_UserRole_UserId] FOREIGN KEY REFERENCES Users(UserId))

CREATE TABLE [RoleAccess] (
	RoleId INT NOT NULL CONSTRAINT [FK_RoleAccess_RoleId] FOREIGN KEY REFERENCES Role(RoleId),
	ActionId INT NOT NULL CONSTRAINT [FK_RoleAccess_ActionId] FOREIGN KEY REFERENCES Action(ActionId))

--------------------------------------------------------------------------------------------------------------------
CREATE TABLE [Staff] (
	StaffId INT CONSTRAINT [PK_Staff] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	UserId INT NOT NULL CONSTRAINT [FK_Staff_UserId] FOREIGN KEY REFERENCES Users(UserId),
	StaffLevel INT NOT NULL,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)

CREATE TABLE [Customer] (
	CustomerId INT CONSTRAINT [PK_Customer] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	UserId INT NOT NULL CONSTRAINT [FK_Customer_UserId] FOREIGN KEY REFERENCES Users(UserId),
	CustomerLevel INT NOT NULL,
	PrimaryAccountNumber VARCHAR(16) NULL,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)

CREATE TABLE Bank (
    BankId INT CONSTRAINT [PK_Bank] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    BankName VARCHAR(100) NOT NULL,
    BankAbbrivation VARCHAR(10) NOT NULL
);

CREATE TABLE [Account] (
	AccountId INT CONSTRAINT [PK_Account] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CustomerId INT NOT NULL CONSTRAINT [FK_Account_CustomerId] FOREIGN KEY REFERENCES Customer(CustomerId),
	BankId INT NOT NULL CONSTRAINT [FK_Account_BankId] FOREIGN KEY REFERENCES Bank(BankId),
	Balance INT NOT NULL,
	AccountNumber VARCHAR(16) NOT NULL,
	AccountTypeId INT NOT NULL,
	AccountStatus VARCHAR(15) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)

CREATE TABLE [Transaction] (
    TransactionId INT CONSTRAINT [PK_Transaction] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    TransactionCategory VARCHAR(20) NOT NULL,
    Amount INT NOT NULL,
    Description VARCHAR(500),
    TransactionTime DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy VARCHAR(30) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy VARCHAR(30) NULL,
    ModifiedDate DATETIME NULL,
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE [TransactionDetail] (
    TransactionDetailsId INT PRIMARY KEY IDENTITY(1,1),
    TransactionId INT NOT NULL CONSTRAINT [FK_TransactionDetail_TransactionId] FOREIGN KEY REFERENCES [Transaction](TransactionId),
    AccountId INT NOT NULL CONSTRAINT [FK_TransactionDetail_AccountId] FOREIGN KEY REFERENCES Account(AccountId),
    TransactionRole VARCHAR(10) NOT NULL,
    TransactionType VARCHAR(10) NOT NULL,
    StageBalance INT NOT NULL
);

CREATE TABLE [Card] (
	CardId INT CONSTRAINT [PK_Card] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	AccountId INT NOT NULL CONSTRAINT [FK_Card_AccountId] FOREIGN KEY REFERENCES Account(AccountId),
	CardNumber VARCHAR(500) NULL,
	CardType VARCHAR(15) NOT NULL,
	CardLimit INT NOT NULL,
	ExpireDate DATETIME NOT NULL,
	CVV INT NOT NULL,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)

CREATE TABLE [CreditScore] (
	CreditScoreId INT CONSTRAINT [PK_CreditScore] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CustomerId INT NOT NULL CONSTRAINT [FK_CreditScore_CustomerId] FOREIGN KEY REFERENCES Customer(CustomerId),
	CreditScoreValue INT NULL,
	Status VARCHAR(15) NOT NULL,
	Description VARCHAR(500) NULL,
	CreatedDate DATETIME NOT NULL,
	CreatedBy VARCHAR(30) NOT NULL,
	ModifiedDate DATETIME NULL,
	ModifiedBy VARCHAR(30) NULL,
	IsDeleted BIT DEFAULT 0)
--------------------------------------------------------------------------------------------------------------
CREATE TABLE [State] (
    StateId INT CONSTRAINT [PK_State] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    StateName VARCHAR(100) NOT NULL,
    Abbreviation VARCHAR(10) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy VARCHAR(30) NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy VARCHAR(30) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
);

CREATE TABLE [City] (
    CityId INT CONSTRAINT [PK_City] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    StateId INT NOT NULL CONSTRAINT [FK_City_StateId] FOREIGN KEY REFERENCES [State](StateId),
    CityName VARCHAR(100) NOT NULL,
    PostalCode VARCHAR(15) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy VARCHAR(30) NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy VARCHAR(30) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
);

CREATE TABLE CodeValue (
    CodeValueId INT CONSTRAINT [PK_CodeValue] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    TypeName VARCHAR(100) NOT NULL,
    TypeCode INT NOT NULL,
	CodeValue1 VARCHAR(50) NULL,
	CodeValue2 VARCHAR(50) NULL,
	CodeValue3 VARCHAR(50) NULL,
	CodeValue1Description VARCHAR(100) NULL,
	CodeValue2Description VARCHAR(100) NULL,
	CodeValue3Description VARCHAR(100) NULL,
	InUse BIT NOT NULL,
	Sequence INT NULL
);