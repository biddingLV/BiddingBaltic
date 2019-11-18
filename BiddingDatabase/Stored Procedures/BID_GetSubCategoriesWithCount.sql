CREATE PROCEDURE BID_GetSubCategoriesWithCount AS
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
  LEFT JOIN Auctions auct ON typ.TypeId = auct.AuctionTypeId
  GROUP BY typ.TypeId,
           typ.Name,
           typ.AuctionCategoryId;
END;