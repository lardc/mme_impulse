﻿CREATE TABLE [dbo].[CONDITIONS] (
    [COND_ID]         INT           IDENTITY (1, 1) NOT NULL,
    [COND_NAME]       CHAR (32)     NOT NULL,
    [COND_NAME_LOCAL] NVARCHAR (64) NOT NULL,
    [COND_IS_TECH]    BIT           NOT NULL,
    CONSTRAINT [PK_CONDITIONS] PRIMARY KEY CLUSTERED ([COND_ID] ASC)
);

