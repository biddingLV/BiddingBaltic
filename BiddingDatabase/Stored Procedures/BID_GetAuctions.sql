CREATE PROCEDURE [dbo].[BID_GetAuctions] @start int,
@end int,
@searchValue varchar(100) = NULL
AS
BEGIN
	DECLARE @categories TABLE ([CategoryId] [int] INDEX IX1 CLUSTERED);
	DECLARE @types TABLE ([TypeId] [int] INDEX IX2 CLUSTERED);

  INSERT INTO @categories (CategoryId)
    SELECT
      CategoryId
    FROM @categories

  INSERT INTO @types (TypeId)
    SELECT
      TypeId
    FROM @types

  SELECT
    auct.AuctionId,
    auct.Name AS AuctionName,
    auct.StartingPrice AS AuctionStartingPrice,
		auct.ApplyTillDate AS AuctionApplyTillDate,
    auct.EndDate AS AuctionEndDate,
    asta.Name AS AuctionStatusName
  FROM Auctions auct
  INNER JOIN AuctionStatuses asta
    ON auct.AuctionStatusId = asta.AuctionStatusId
    AND auct.Deleted = 0
    AND (auct.EndDate >= CONVERT(date, GETDATE()))
    AND (@searchValue IS NULL
    OR auct.Name LIKE '%' + @searchValue + '%')
  ORDER BY (CASE
    WHEN auct.StartDate IS NULL THEN 1
    ELSE 0
  END) DESC,
  auct.StartDate DESC
  OFFSET @start ROWS

  FETCH NEXT @end ROWS ONLY
END