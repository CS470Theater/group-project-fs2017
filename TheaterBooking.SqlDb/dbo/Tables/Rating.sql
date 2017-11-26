CREATE TABLE [dbo].[Rating] (
    [Rating_ID]     INT           IDENTITY (1, 1) NOT NULL,
    [Rating_Symbol] NVARCHAR (10) NOT NULL,
    CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED ([Rating_ID] ASC)
);

