CREATE TABLE [dbo].[SearchRequest] (
    [Request]     NVARCHAR (450) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [URL]         NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_SearchRequest] PRIMARY KEY CLUSTERED ([Request] ASC, [URL] ASC)
);
