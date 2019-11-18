CREATE PROCEDURE BID_GetTopCategoriesWithCount AS
BEGIN
  SELECT 
	cat.CategoryId,
    cat.Name AS CategoryName,
    SUM(CASE
        WHEN auct.AuctionCategoryId = cat.CategoryId THEN 1
        ELSE 0
    END) AS CategoryTotalCount
  FROM Categories cat
  LEFT JOIN Auctions auct ON cat.CategoryId = auct.AuctionCategoryId
  WHERE cat.Deleted = 0
  GROUP BY cat.CategoryId,
           cat.Name;
END;