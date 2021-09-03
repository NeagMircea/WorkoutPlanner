CREATE PROCEDURE [dbo].[spCategoryGetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [CategoryId], [CategoryName], [CategoryOrder]
	FROM [dbo].[Categories]
	ORDER BY [CategoryOrder];
END;
