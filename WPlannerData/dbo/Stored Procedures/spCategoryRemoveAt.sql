CREATE PROCEDURE [dbo].[spCategoryRemoveAt]
@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE 
	FROM [Categories]
	WHERE [CategoryId] = @CategoryId; 
END;
