CREATE TABLE [dbo].[Items] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX)  NOT NULL,
    [Description] NVARCHAR (MAX)  NOT NULL,
    [UnitPrice]   DECIMAL (18, 2) NOT NULL,
    [ImageUrl]    NVARCHAR (MAX)  NOT NULL,
    [CategoryId]  INT             NOT NULL,
    [ItemTypeId]  INT             NOT NULL,
    [CreatedDate] DATETIME2 (7)   NOT NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Items_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Items_ItemTypes_ItemTypeId] FOREIGN KEY ([ItemTypeId]) REFERENCES [dbo].[ItemTypes] ([Id]) ON DELETE CASCADE
);

