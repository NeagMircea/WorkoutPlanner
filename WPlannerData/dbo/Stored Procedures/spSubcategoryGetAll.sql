CREATE PROCEDURE [dbo].[spSubcategoryGetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [SubcategoryId], [SubcategoryName]
	FROM [Subcategories];
END;
