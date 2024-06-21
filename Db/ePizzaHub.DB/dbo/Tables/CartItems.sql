CREATE TABLE [dbo].[CartItems] (
    [Id]        INT              IDENTITY (1, 1) NOT NULL,
    [CartId]    UNIQUEIDENTIFIER NOT NULL,
    [ItemId]    INT              NOT NULL,
    [UnitPrice] DECIMAL (18, 2)  NOT NULL,
    [Quantity]  INT              NOT NULL,
    CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CartItems_Carts_CartId] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Carts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartItems_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([Id])
);

