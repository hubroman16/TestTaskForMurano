USE [SearchDB]
GO

/****** Объект: Table [dbo].[SearchRequest] Дата скрипта: 20.08.2023 18:33:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SearchRequest] (
    [Request]     NVARCHAR (450) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [URL]         NVARCHAR (450) NOT NULL
);


