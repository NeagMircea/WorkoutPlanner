CREATE PROCEDURE [dbo].[spCategoryInsert]
	@CategoryName VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Categories]([CategoryName])
	VALUES(@CategoryName);
END;
