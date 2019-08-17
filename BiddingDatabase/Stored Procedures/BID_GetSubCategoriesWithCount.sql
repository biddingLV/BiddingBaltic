CREATE PROCEDURE BID_GetSubCategoriesWithCount
AS
BEGIN
  SELECT
    typ.AuctionCategoryId AS CategoryId,
    typ.TypeId,
    typ.Name AS TypeName,
    SUM(CASE
      WHEN auct.AuctionTypeId = typ.TypeId THEN 1
      ELSE 0
    END) AS TypeTotalCount
  FROM Types typ
  INNER JOIN Auctions auct
    ON typ.TypeId = auct.AuctionTypeId
  WHERE auct.Deleted = 0
  AND typ.Deleted = 0
  GROUP BY typ.TypeId,
           typ.Name,
           typ.AuctionCategoryId;
END;