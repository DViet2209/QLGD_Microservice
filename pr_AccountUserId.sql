IF OBJECT_ID('pr_AccountUserId', 'P') IS NOT NULL
    DROP PROCEDURE pr_AccountUserId
GO

CREATE PROCEDURE pr_AccountUserId
AS
BEGIN
    IF NOT EXISTS (SELECT TOP 1 account_id FROM account ORDER BY account_id DESC)
    BEGIN
        SELECT CONVERT(NVARCHAR(10), LEFT(CONVERT(varchar, GETDATE(), 112),6)) +'-'+ RIGHT('0001', 4) AS AccountUserId;
    END
    ELSE
    BEGIN
        DECLARE @KH1 INT, @KH2 INT;
        SELECT @KH1 = MAX(account_id) FROM account;
        SET @KH2 = @KH1 % 10000 + 1;

        DECLARE @YearMonth NVARCHAR(7) = CONVERT(varchar, GETDATE(), 112);

        SELECT @YearMonth + '-' + RIGHT('000' + CAST(@KH2 AS NVARCHAR(4)), 4) AS AccountUserId;
    END
END
GO

EXEC pr_AccountUserId;
