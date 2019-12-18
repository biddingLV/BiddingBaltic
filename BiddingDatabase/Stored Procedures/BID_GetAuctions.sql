CREATE PROCEDURE [dbo].[BID_GetAuctions] @selectedCategories varchar(1000) = NULL,
@selectedTypes varchar(1000) = NULL,
@start int,
@end int,
@searchValue varchar(100) = NULL
AS
BEGIN

  DECLARE @TempCategoryIds TABLE (
    CategoryId int
  );

  DECLARE @TempTypeIds TABLE (
    TypeId int
  );

  IF @selectedCategories IS NULL
  BEGIN
    INSERT INTO @TempCategoryIds (CategoryId)
      SELECT
        CategoryId
      FROM Categories;
  END
  ELSE
  BEGIN
    INSERT INTO @TempCategoryIds (CategoryId)
      SELECT
        value
      FROM STRING_SPLIT(@selectedCategories, ',');
  END;

  IF @selectedTypes IS NULL
  BEGIN
    INSERT INTO @TempTypeIds (TypeId)
      SELECT
        TypeId
      FROM Types;
  END
  ELSE
  BEGIN
    INSERT INTO @TempTypeIds (TypeId)
      SELECT
        value
      FROM STRING_SPLIT(@selectedTypes, ',');
  END;

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
    AND (auct.EndDate >= CONVERT(date, GETDATE()))
    AND auct.AuctionCategoryId IN (SELECT
      CategoryId
    FROM @TempCategoryIds)
    AND auct.AuctionTypeId IN (SELECT
      TypeId
    FROM @TempTypeIds)
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