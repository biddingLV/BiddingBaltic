CREATE PROCEDURE BID_GetTopCategoriesWithCount AS
BEGIN
  SELECT 
		cat.CategoryId,
    cat.Name AS CategoryName,
    SUM(
		CASE
			WHEN auct.AuctionCategoryId = cat.CategoryId AND (auct.EndDate >= CONVERT(date, GETDATE())) 
			THEN 1
			ELSE 0
    END) AS CategoryTotalCount
  FROM Categories cat
  LEFT JOIN Auctions auct ON cat.CategoryId = auct.AuctionCategoryId
  GROUP BY cat.CategoryId,
           cat.Name;
END;