CREATE PROCEDURE [dbo].[spCategorySubcategoryRemoveAt]
	@CategoryId INT,
	@SubcategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE 
	FROM [CategorySubcategories]
	WHERE [fk_CategoryId] = @CategoryId AND [fk_SubcategoryId] = @SubcategoryId;
END;
