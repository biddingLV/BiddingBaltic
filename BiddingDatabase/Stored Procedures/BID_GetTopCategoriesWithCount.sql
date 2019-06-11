CREATE PROCEDURE BID_GetTopCategoriesWithCount
AS
BEGIN
	SELECT
		cat.CategoryId,
		cat.Name as CategoryName,
		SUM(CASE
			WHEN auct.AuctionCategoryId = cat.CategoryId THEN 1
			ELSE 0
		END) AS CategoryTotalCount
	FROM Categories cat
	INNER JOIN Auctions auct ON cat.CategoryId = auct.AuctionCategoryId
	GROUP BY cat.CategoryId,
	         cat.Name
END;