CREATE TABLE [dbo].[MovementOrder] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [OrderNo]            VARCHAR (MAX) NOT NULL,
    [Subject]            VARCHAR (MAX) NOT NULL,
    [Date]               DATETIME      NOT NULL,
    [InspectorName]      VARCHAR (MAX) NOT NULL,
    [Designation]        VARCHAR (MAX) NOT NULL,
    [FirmName]           VARCHAR (MAX) NOT NULL,
    [OnwardStartDate]    DATETIME      NOT NULL,
    [OnwardEndDate]      DATETIME      NOT NULL,
    [OnwordFrom]         VARCHAR (MAX) NOT NULL,
    [OnwordTo]           VARCHAR (MAX) NOT NULL,
    [OnwordModeOfTravel] VARCHAR (MAX) NOT NULL,
    [ReturnStartDate]    DATETIME      NOT NULL,
    [ReturnEndDate]      DATETIME      NOT NULL,
    [ReturnFrom]         VARCHAR (MAX) NOT NULL,
    [ReturnTo]           VARCHAR (MAX) NOT NULL,
    [ReturnModeOfTravel] VARCHAR (MAX) NOT NULL,
    [SigningAuthority]   VARCHAR (MAX) NOT NULL,
    [SADesignation]      VARCHAR (MAX) NOT NULL,
    [Distribution] VARCHAR(MAX) NOT NULL, 
    [CreatedBy] VARCHAR(MAX) NULL, 
    [CreatedDate] DATETIME NULL, 
    [UpdatedBy] VARCHAR(MAX) NULL, 
    [UpdatedDate] VARCHAR(MAX) NULL, 
    [Flag] VARCHAR(MAX) NOT NULL DEFAULT 'OPEN', 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

ALTER TABLE [dbo].[User]
ADD [Designation]   VARCHAR (MAX)  NULL

