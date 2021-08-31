CREATE PROCEDURE [dbo].[spCategoryGetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [CategoryId], [CategoryName] 
	FROM [dbo].[Categories];
END;
