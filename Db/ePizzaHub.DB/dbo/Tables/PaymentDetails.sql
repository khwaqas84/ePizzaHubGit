CREATE TABLE [dbo].[PaymentDetails] (
    [Id]            NVARCHAR (450)   NOT NULL,
    [TransactionId] NVARCHAR (MAX)   NOT NULL,
    [Tax]           DECIMAL (18, 2)  NOT NULL,
    [Currency]      NVARCHAR (MAX)   NOT NULL,
    [Total]         DECIMAL (18, 2)  NOT NULL,
    [Email]         NVARCHAR (MAX)   NOT NULL,
    [Status]        NVARCHAR (MAX)   NOT NULL,
    [CartId]        UNIQUEIDENTIFIER NOT NULL,
    [GrandTotal]    DECIMAL (18, 2)  NOT NULL,
    [CreatedDate]   DATETIME2 (7)    NOT NULL,
    [UserId]        INT              NOT NULL,
    CONSTRAINT [PK_PaymentDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentDetails_Carts] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Carts] ([Id]),
    CONSTRAINT [FK_PaymentDetails_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);

