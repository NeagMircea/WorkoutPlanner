CREATE VIEW [dbo].[Category_Subcategories]
	AS 
	SELECT * 
	FROM [Categories]
	JOIN [CategorySubcategories] ON [Categories].[CategoryId] = [CategorySubcategories].[fk_CategoryId]
	JOIN [Subcategories] ON [CategorySubcategories].[fk_SubcategoryId] = [Subcategories].[SubcategoryId]
