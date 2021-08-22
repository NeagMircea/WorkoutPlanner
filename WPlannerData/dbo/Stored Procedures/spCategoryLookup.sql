CREATE PROCEDURE [dbo].[spCategoryLookup]
	@Id int = 0
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Name] 
	FROM [dbo].[Categories]
	WHERE Id = @Id;
END;
