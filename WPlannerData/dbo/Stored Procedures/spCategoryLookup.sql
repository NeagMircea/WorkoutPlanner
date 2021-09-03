CREATE PROCEDURE [dbo].[spCategoryLookup]
	@Id int = 0
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [CategoryId], [CategoryName], [CategoryOrder]
	FROM [dbo].[Categories]
	WHERE CategoryId = @Id;
END;
