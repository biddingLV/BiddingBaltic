CREATE PROCEDURE BID_GetSubCategoriesWithCount
AS
BEGIN
	SELECT
		typ.AuctionCategoryId as CategoryId,
		typ.TypeId,
		typ.Name as TypeName,
		SUM(CASE
			WHEN atyp.AuctionTypeId = typ.TypeId THEN 1
			ELSE 0
		END) AS TypeTotalCount
	FROM Types typ
	INNER JOIN Auctions atyp ON typ.TypeId = atyp.AuctionTypeId
	GROUP BY typ.TypeId,
						typ.Name,
						typ.AuctionCategoryId;
END;