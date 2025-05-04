IF OBJECT_ID('GetCombinedInfoByAccountId', 'FN') IS NOT NULL
    DROP FUNCTION GetCombinedInfoByAccountId;
GO

CREATE FUNCTION GetCombinedInfoByAccountId (@AccountId INT)
RETURNS VARCHAR(200)
AS
BEGIN
    DECLARE @Result VARCHAR(200)
    DECLARE @FirstName VARCHAR(100)
    DECLARE @AccountNumber VARCHAR(50)
    DECLARE @BankAbbrivation VARCHAR(50)

    SELECT 
        @FirstName = u.FirstName,
        @AccountNumber = a.AccountNumber,
        @BankAbbrivation = b.BankAbbrivation
    FROM Account a
    JOIN Customer c ON a.CustomerId = c.CustomerId
    JOIN Users u ON c.UserId = u.UserId
    JOIN Bank b ON a.BankId = b.BankId
    WHERE a.AccountId = @AccountId

    SET @Result = 
        LEFT(@FirstName, 6) + '/' +
        RIGHT(@AccountNumber, 4) + '/' + 
        @BankAbbrivation

    RETURN @Result
END
GO
