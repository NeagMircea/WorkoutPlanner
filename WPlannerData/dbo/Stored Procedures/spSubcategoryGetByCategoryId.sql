CREATE PROCEDURE [dbo].[spSubcategoryGetByCategoryId]
	@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [SubcategoryId], [SubcategoryName]
	FROM [Category_Subcategories]
	WHERE [CategoryId] = @CategoryId;
END;
