-- ==============================================================
-- Credit Card Feature - Database Schema
-- ==============================================================
-- This script creates all necessary tables for the credit card feature
-- Tables: CreditCard, CreditCardApplication, CreditCardTransaction, 
--         CreditCardStatement, CreditCardStatementDetail, CreditCardFee
-- ==============================================================

-- Drop existing foreign key constraints
IF (OBJECT_ID('FK_CreditCardApplication_CustomerId') IS NOT NULL)
BEGIN
	ALTER TABLE [CreditCardApplication] DROP CONSTRAINT [FK_CreditCardApplication_CustomerId]
END
GO

IF (OBJECT_ID('FK_CreditCard_CustomerId') IS NOT NULL)
BEGIN
	ALTER TABLE [CreditCard] DROP CONSTRAINT [FK_CreditCard_CustomerId]
END
GO

IF (OBJECT_ID('FK_CreditCardTransaction_CreditCardId') IS NOT NULL)
BEGIN
	ALTER TABLE [CreditCardTransaction] DROP CONSTRAINT [FK_CreditCardTransaction_CreditCardId]
END
GO

IF (OBJECT_ID('FK_CreditCardStatement_CreditCardId') IS NOT NULL)
BEGIN
	ALTER TABLE [CreditCardStatement] DROP CONSTRAINT [FK_CreditCardStatement_CreditCardId]
END
GO

IF (OBJECT_ID('FK_CreditCardStatementDetail_StatementId') IS NOT NULL)
BEGIN
	ALTER TABLE [CreditCardStatementDetail] DROP CONSTRAINT [FK_CreditCardStatementDetail_StatementId]
END
GO

IF (OBJECT_ID('FK_CreditCardStatementDetail_TransactionId') IS NOT NULL)
BEGIN
	ALTER TABLE [CreditCardStatementDetail] DROP CONSTRAINT [FK_CreditCardStatementDetail_TransactionId]
END
GO

IF (OBJECT_ID('FK_CreditCardFee_CreditCardId') IS NOT NULL)
BEGIN
	ALTER TABLE [CreditCardFee] DROP CONSTRAINT [FK_CreditCardFee_CreditCardId]
END
GO

-- Drop existing tables if they exist
IF EXISTS (SELECT * FROM sys.tables WHERE name = N'CreditCardFee')
BEGIN
	DROP TABLE [CreditCardFee]
END
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = N'CreditCardStatementDetail')
BEGIN
	DROP TABLE [CreditCardStatementDetail]
END
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = N'CreditCardStatement')
BEGIN
	DROP TABLE [CreditCardStatement]
END
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = N'CreditCardTransaction')
BEGIN
	DROP TABLE [CreditCardTransaction]
END
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = N'CreditCard')
BEGIN
	DROP TABLE [CreditCard]
END
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = N'CreditCardApplication')
BEGIN
	DROP TABLE [CreditCardApplication]
END
GO

-- ==============================================================
-- CREATE TABLE: CreditCardApplication
-- Description: Stores credit card applications submitted by customers
-- ==============================================================
CREATE TABLE [dbo].[CreditCardApplication] (
	[ApplicationId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[CustomerId] INT NOT NULL,
	[CardType] NVARCHAR(50) NOT NULL,
	[DesiredLimit] DECIMAL(18,2) NOT NULL,
	[ApprovedLimit] DECIMAL(18,2) NULL,
	[Status] NVARCHAR(50) NOT NULL,
	[ApplicationDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ApprovedDate] DATETIME NULL,
	[ApprovedBy] NVARCHAR(255) NULL,
	[DecisionRemarks] NVARCHAR(MAX) NULL,
	[CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	CONSTRAINT [FK_CreditCardApplication_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer]([CustomerId]) ON DELETE CASCADE
)
GO

-- Create indexes on CreditCardApplication
CREATE NONCLUSTERED INDEX [IX_CreditCardApplication_CustomerId_Status] ON [dbo].[CreditCardApplication] ([CustomerId], [Status])
GO

CREATE NONCLUSTERED INDEX [IX_CreditCardApplication_ApplicationDate] ON [dbo].[CreditCardApplication] ([ApplicationDate])
GO

-- ==============================================================
-- CREATE TABLE: CreditCard
-- Description: Stores active credit cards issued to customers
-- ==============================================================
CREATE TABLE [dbo].[CreditCard] (
	[CreditCardId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[CustomerId] INT NOT NULL,
	[CardNumber] NVARCHAR(MAX) NOT NULL,
	[CVV] NVARCHAR(MAX) NOT NULL,
	[CardHolderName] NVARCHAR(255) NOT NULL,
	[CardType] NVARCHAR(50) NOT NULL,
	[Status] NVARCHAR(50) NOT NULL,
	[IssueDate] DATETIME NOT NULL,
	[ExpiryDate] DATETIME NOT NULL,
	[CreditLimit] DECIMAL(18,2) NOT NULL,
	[CurrentBalance] DECIMAL(18,2) NOT NULL DEFAULT 0,
	[AvailableCredit] DECIMAL(18,2) NOT NULL,
	[CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	CONSTRAINT [FK_CreditCard_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer]([CustomerId]) ON DELETE CASCADE
)
GO

-- Create indexes on CreditCard
CREATE NONCLUSTERED INDEX [IX_CreditCard_CustomerId_Status] ON [dbo].[CreditCard] ([CustomerId], [Status])
GO

CREATE NONCLUSTERED INDEX [IX_CreditCard_CardNumber] ON [dbo].[CreditCard] ([CardNumber])
GO

-- ==============================================================
-- CREATE TABLE: CreditCardTransaction
-- Description: Stores all credit card transactions (purchases, payments, fees, interest)
-- ==============================================================
CREATE TABLE [dbo].[CreditCardTransaction] (
	[TransactionId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[CreditCardId] INT NOT NULL,
	[TransactionDate] DATETIME NOT NULL,
	[TransactionType] NVARCHAR(50) NOT NULL,
	[Amount] DECIMAL(18,2) NOT NULL,
	[Description] NVARCHAR(500) NULL,
	[MerchantName] NVARCHAR(255) NULL,
	[TransactionStatus] NVARCHAR(50) NOT NULL,
	[ReferenceNumber] NVARCHAR(100) NULL,
	[CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	CONSTRAINT [FK_CreditCardTransaction_CreditCardId] FOREIGN KEY ([CreditCardId]) REFERENCES [dbo].[CreditCard]([CreditCardId]) ON DELETE CASCADE
)
GO

-- Create indexes on CreditCardTransaction
CREATE NONCLUSTERED INDEX [IX_CreditCardTransaction_CreditCardId_Date] ON [dbo].[CreditCardTransaction] ([CreditCardId], [TransactionDate] DESC)
GO

CREATE NONCLUSTERED INDEX [IX_CreditCardTransaction_TransactionType] ON [dbo].[CreditCardTransaction] ([TransactionType])
GO

CREATE NONCLUSTERED INDEX [IX_CreditCardTransaction_TransactionStatus] ON [dbo].[CreditCardTransaction] ([TransactionStatus])
GO

-- ==============================================================
-- CREATE TABLE: CreditCardStatement
-- Description: Stores monthly statements for credit cards
-- ==============================================================
CREATE TABLE [dbo].[CreditCardStatement] (
	[StatementId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[CreditCardId] INT NOT NULL,
	[BillingCycleStartDate] DATETIME NOT NULL,
	[BillingCycleEndDate] DATETIME NOT NULL,
	[StatementDate] DATETIME NOT NULL,
	[TotalCharges] DECIMAL(18,2) NOT NULL,
	[TotalPayments] DECIMAL(18,2) NOT NULL,
	[Interest] DECIMAL(18,2) NOT NULL DEFAULT 0,
	[Fees] DECIMAL(18,2) NOT NULL DEFAULT 0,
	[PreviousBalance] DECIMAL(18,2) NOT NULL,
	[CardBalance] DECIMAL(18,2) NOT NULL,
	[MinimumDueAmount] DECIMAL(18,2) NOT NULL,
	[DueDate] DATETIME NOT NULL,
	[StatementStatus] NVARCHAR(50) NOT NULL,
	[CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	CONSTRAINT [FK_CreditCardStatement_CreditCardId] FOREIGN KEY ([CreditCardId]) REFERENCES [dbo].[CreditCard]([CreditCardId]) ON DELETE CASCADE
)
GO

-- Create indexes on CreditCardStatement
CREATE NONCLUSTERED INDEX [IX_CreditCardStatement_CreditCardId_StatementDate] ON [dbo].[CreditCardStatement] ([CreditCardId], [StatementDate] DESC)
GO

CREATE NONCLUSTERED INDEX [IX_CreditCardStatement_DueDate] ON [dbo].[CreditCardStatement] ([DueDate])
GO

CREATE NONCLUSTERED INDEX [IX_CreditCardStatement_StatementStatus] ON [dbo].[CreditCardStatement] ([StatementStatus])
GO

-- ==============================================================
-- CREATE TABLE: CreditCardStatementDetail
-- Description: Stores line items for each statement (transaction details)
-- ==============================================================
CREATE TABLE [dbo].[CreditCardStatementDetail] (
	[StatementDetailId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[StatementId] INT NOT NULL,
	[TransactionId] INT NOT NULL,
	[TransactionDate] DATETIME NOT NULL,
	[Description] NVARCHAR(500) NULL,
	[Amount] DECIMAL(18,2) NOT NULL,
	[TransactionType] NVARCHAR(50) NOT NULL,
	CONSTRAINT [FK_CreditCardStatementDetail_StatementId] FOREIGN KEY ([StatementId]) REFERENCES [dbo].[CreditCardStatement]([StatementId]) ON DELETE CASCADE,
	CONSTRAINT [FK_CreditCardStatementDetail_TransactionId] FOREIGN KEY ([TransactionId]) REFERENCES [dbo].[CreditCardTransaction]([TransactionId]) ON DELETE CASCADE
)
GO

-- Create indexes on CreditCardStatementDetail
CREATE NONCLUSTERED INDEX [IX_CreditCardStatementDetail_StatementId] ON [dbo].[CreditCardStatementDetail] ([StatementId])
GO

-- ==============================================================
-- CREATE TABLE: CreditCardFee
-- Description: Stores fees applied to credit cards (annual, late payment, over-limit, etc.)
-- ==============================================================
CREATE TABLE [dbo].[CreditCardFee] (
	[FeeId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[CreditCardId] INT NOT NULL,
	[FeeType] NVARCHAR(50) NOT NULL,
	[Amount] DECIMAL(18,2) NOT NULL,
	[EffectiveDate] DATETIME NOT NULL,
	[FeeReason] NVARCHAR(500) NULL,
	[CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	CONSTRAINT [FK_CreditCardFee_CreditCardId] FOREIGN KEY ([CreditCardId]) REFERENCES [dbo].[CreditCard]([CreditCardId]) ON DELETE CASCADE
)
GO

-- Create indexes on CreditCardFee
CREATE NONCLUSTERED INDEX [IX_CreditCardFee_CreditCardId_Date] ON [dbo].[CreditCardFee] ([CreditCardId], [EffectiveDate] DESC)
GO

CREATE NONCLUSTERED INDEX [IX_CreditCardFee_FeeType] ON [dbo].[CreditCardFee] ([FeeType])
GO

-- ==============================================================
-- End of Credit Card Schema Script
-- ==============================================================
