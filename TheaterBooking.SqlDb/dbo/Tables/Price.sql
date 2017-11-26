CREATE TABLE [dbo].[Price] (
    [Price_ID]     INT             IDENTITY (1, 1) NOT NULL,
    [Price_Desc]   NVARCHAR (15)   NOT NULL,
    [Price_Amount] DECIMAL (10, 2) NOT NULL,
    CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED ([Price_ID] ASC)
);

