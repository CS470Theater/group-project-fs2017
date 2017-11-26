CREATE TABLE [dbo].[Genre] (
    [Genre_ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Genre_Name] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED ([Genre_ID] ASC)
);

