IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Contacts] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [TelephoneNumber] nvarchar(15) NOT NULL,
    [StreetNumber] nvarchar(80) NOT NULL,
    [City] nvarchar(40) NOT NULL,
    [PostalCode] nvarchar(20) NULL,
    [Country] nvarchar(50) NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
);

GO

CREATE UNIQUE INDEX [IX_Contacts_FirstName_LastName] ON [Contacts] ([FirstName], [LastName]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181219220803_InitialCreate', N'2.2.0-rtm-35687');

GO

ALTER TABLE [Contacts] ADD [FullAddress] nvarchar(200) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181220232523_FullAddress', N'2.2.0-rtm-35687');

GO

