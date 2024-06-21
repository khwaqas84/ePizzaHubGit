CREATE TABLE [dbo].[Users] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX) NOT NULL,
    [Email]          NVARCHAR (MAX) NOT NULL,
    [Password]       NVARCHAR (MAX) NOT NULL,
    [PhoneNumber]    NVARCHAR (MAX) NOT NULL,
    [EmailConfirmed] BIT            NOT NULL,
    [CreatedDate]    DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

