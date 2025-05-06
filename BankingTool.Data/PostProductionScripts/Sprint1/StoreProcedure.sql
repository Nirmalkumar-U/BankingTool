IF OBJECT_ID('GetAccountTransactions', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE GetAccountTransactions;
END
GO

CREATE PROCEDURE GetAccountTransactions
    @AccountId INT,
	@TransactionTag VARCHAR(50) NULL,
	@TransactionFromDate DATE NULL,
	@TransactionToDate DATE NULL,
	@SenderAccountId INT NULL,
	@ReceiverAccountId INT NULL,
	@TransactionCategory VARCHAR(50) NULL
AS
BEGIN
    SET NOCOUNT ON;

	IF @TransactionTag IS NULL OR @TransactionTag = ''
	BEGIN 
		SET @TransactionTag = NULL
	END
	IF @TransactionFromDate IS NULL OR @TransactionFromDate = ''
	BEGIN 
		SET @TransactionFromDate = NULL
	END
	IF @TransactionToDate IS NULL OR @TransactionToDate = ''
	BEGIN 
		SET @TransactionToDate = NULL
	END
	IF @SenderAccountId IS NULL OR @SenderAccountId = ''
	BEGIN 
		SET @SenderAccountId = NULL
	END
	IF @ReceiverAccountId IS NULL OR @ReceiverAccountId = ''
	BEGIN 
		SET @ReceiverAccountId = NULL
	END
	IF @TransactionCategory IS NULL OR @TransactionCategory = ''
	BEGIN 
		SET @TransactionCategory = NULL
	END

	SELECT * INTO #TempTransactionList FROM (
    SELECT 
        t.TransactionId,
		t.Amount,
		td.StageBalance,
		t.TransactionTime AS TransactionDate,
		t.Description,
		td.TransactionType,
		t.TransactionCategory,
        CASE WHEN sender_acc.AccountNumber IS NULL THEN NULL ELSE dbo.GetCombinedInfoByAccountId(sender_acc.AccountId) END AS FromAccountId,
        CASE WHEN receiver_acc.AccountNumber  IS NULL THEN NULL ELSE dbo.GetCombinedInfoByAccountId(receiver_acc.AccountId) END AS ToAccountId,
		t.TransactionTag,
		sender_acc.AccountId AS SenderAccountId,
		receiver_acc.AccountId AS ReceiverAccountId
   FROM TransactionDetail td
    JOIN [Transaction] t ON td.TransactionId = t.TransactionId
    JOIN Account acc ON td.AccountId = acc.AccountId
    LEFT JOIN (
        SELECT td2.TransactionId, a2.AccountNumber, a2.AccountId
        FROM TransactionDetail td2
        JOIN Account a2 ON td2.AccountId = a2.AccountId
        WHERE td2.TransactionRole = 'Sender'
    ) sender_acc ON sender_acc.TransactionId = t.TransactionId
    LEFT JOIN (
        SELECT td3.TransactionId, a3.AccountNumber, a3.AccountId
        FROM TransactionDetail td3
        JOIN Account a3 ON td3.AccountId = a3.AccountId
        WHERE td3.TransactionRole = 'Receiver'
    ) receiver_acc ON receiver_acc.TransactionId = t.TransactionId
    WHERE td.AccountId = @AccountId
      AND t.IsDeleted = 0) AS T

	SELECT 
		tl.TransactionId,
		tl.Amount,
		tl.StageBalance,
		tl.TransactionDate,
		tl.Description,
		tl.TransactionType,
		tl.TransactionCategory,
        tl.FromAccountId,
        tl.ToAccountId,
		tl.TransactionTag
	FROM #TempTransactionList tl
	WHERE (@TransactionTag IS NULL OR @TransactionTag = tl.TransactionTag)
	AND ((@TransactionFromDate IS NULL OR @TransactionFromDate >= tl.TransactionDate) 
	AND (@TransactionToDate IS NULL OR @TransactionToDate <= tl.TransactionDate))
	AND (@SenderAccountId IS NULL OR @SenderAccountId = tl.SenderAccountId)
	AND (@ReceiverAccountId IS NULL OR @ReceiverAccountId = tl.ReceiverAccountId)
	AND (@TransactionCategory IS NULL OR @TransactionCategory = tl.TransactionCategory)
	ORDER BY tl.TransactionDate DESC

	IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #TempTransactionList 

END
GO
--------------------------------------------------------------------------------------------