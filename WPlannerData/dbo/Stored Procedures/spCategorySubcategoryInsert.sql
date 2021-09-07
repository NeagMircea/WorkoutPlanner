CREATE PROCEDURE [dbo].[spCategorySubcategoryInsert]
	@CategoryId INT,
	@SubcategoryId INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [CategorySubcategories] ([fk_CategoryId], [fk_SubcategoryId])
	VALUES(@CategoryId, @SubcategoryId);
END;
