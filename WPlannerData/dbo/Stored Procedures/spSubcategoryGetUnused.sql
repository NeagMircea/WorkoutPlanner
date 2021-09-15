CREATE PROCEDURE [dbo].[spSubcategoryGetUnused]
	@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [SubcategoryId], [SubcategoryName]
	FROM [Subcategories]
	WHERE [SubcategoryId] NOT IN
	(	
	SELECT [SubcategoryId]
	FROM [Category_Subcategories]
	WHERE CategoryId = @CategoryId
	);
END;
