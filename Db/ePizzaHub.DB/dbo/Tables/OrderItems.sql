CREATE TABLE [dbo].[OrderItems] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [ItemId]    INT             NOT NULL,
    [UnitPrice] DECIMAL (18, 2) NOT NULL,
    [Quantity]  INT             NOT NULL,
    [Total]     DECIMAL (18, 2) NOT NULL,
    [OrderId]   NVARCHAR (450)  NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderItems_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([Id]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id]) ON DELETE CASCADE
);

