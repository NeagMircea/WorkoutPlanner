CREATE PROCEDURE [dbo].[spSubcategoryRemoveAt]
	@SubcategoryId int = 0
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM [Subcategories]
	WHERE [SubcategoryId] = @SubcategoryId;
END;
