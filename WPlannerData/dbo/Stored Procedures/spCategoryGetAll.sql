CREATE PROCEDURE [dbo].[spCategoryGetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Name] 
	FROM [dbo].[Categories];
END;
