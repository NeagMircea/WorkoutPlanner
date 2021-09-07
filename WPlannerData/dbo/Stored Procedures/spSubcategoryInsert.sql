CREATE PROCEDURE [dbo].[spSubcategoryInsert]
	@SubcategoryName VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [Subcategories]([SubcategoryName])
	VALUES(@SubcategoryName);
END;
