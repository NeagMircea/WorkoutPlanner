CREATE TABLE [dbo].[CategorySubcategories]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [fk_CategoryId] INT NOT NULL, 
    [fk_SubcategoryId] INT NOT NULL,

    CONSTRAINT [FK_CategorySubcategories_Category] FOREIGN KEY ([fk_CategoryId]) REFERENCES [Categories]([CategoryId])
	ON UPDATE NO ACTION ON DELETE CASCADE,

    CONSTRAINT [FK_CategorySubcategories_Subcategory] FOREIGN KEY ([fk_SubcategoryId]) REFERENCES [Subcategories]([SubcategoryId])
	ON UPDATE NO ACTION ON DELETE CASCADE,

	CONSTRAINT CatSubcatConstraint UNIQUE([fk_CategoryId], [fk_SubcategoryId])
)
