IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Transaction' AND COLUMN_NAME = 'TransactionTag')
BEGIN
    ALTER TABLE [Transaction]
    ADD TransactionTag VARCHAR(50) NULL;
END
GO
--------------------------------------------------------------------------------------------------------------------------------