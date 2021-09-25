CREATE PROCEDURE [dbo].[spSubcategoryInsert]
	@SubcategoryName NVARCHAR(100),
	@SubcategoryInfo NVARCHAR(3000)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [Subcategories]([SubcategoryName], [SubcategoryInfo])
	VALUES(@SubcategoryName, @SubcategoryInfo);
END;
