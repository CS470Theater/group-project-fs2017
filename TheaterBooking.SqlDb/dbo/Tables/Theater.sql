CREATE TABLE [dbo].[Theater] (
    [Theater_ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Theater_Name]    NVARCHAR (50) NOT NULL,
    [Theater_Owner]   NVARCHAR (50) NOT NULL,
    [Theater_Address] NVARCHAR (75) NOT NULL,
    [Theater_Phone]   NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Theater] PRIMARY KEY CLUSTERED ([Theater_ID] ASC)
);

