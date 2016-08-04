ALTER TABLE [dbo].[QAP]
    ADD [POId] NUMERIC (18) NULL;
	
CREATE TABLE [dbo].[Files]
(
	[Id] NUMERIC NOT NULL PRIMARY KEY IDENTITY, 
    [FileNo] VARCHAR(MAX) NULL, 
    [Subject] VARCHAR(MAX) NULL, 
    [FilePath] VARCHAR(MAX) NULL, 
    [CreatedBy] VARCHAR(MAX) NULL, 
    [CreatedDate] DATETIME NULL DEFAULT GETDATE()
);

CREATE TABLE [dbo].[AdminCorrespondence]
(
	[Id] NUMERIC NOT NULL PRIMARY KEY IDENTITY, 
    [FileNo] VARCHAR(MAX) NULL, 
    [LetterNo] VARCHAR(MAX) NULL, 
    [Subject] VARCHAR(MAX) NULL, 
    [TypeOf] VARCHAR(MAX) NULL, 
    [From] VARCHAR(MAX) NULL, 
    [To] VARCHAR(MAX) NULL, 
    [CopyTo] VARCHAR(MAX) NULL, 
    [Date] DATETIME NULL, 
    [FilePath] VARCHAR(MAX) NULL, 
    [CreatedBy] VARCHAR(MAX) NULL, 
    [CreatedDate] DATETIME NULL DEFAULT GETDATE()
);

CREATE TABLE [dbo].[Photographs] (
    [Id]          NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [FolderName]  VARCHAR (MAX) NULL,
    [Subject]     VARCHAR (MAX) NULL,
    [Date]        DATETIME      NULL,
    [FilePath] VARCHAR(MAX) NULL, 
	[CreatedDate] DATETIME      DEFAULT (getdate()) NULL,
    [CreatedBy]   VARCHAR (MAX) NULL,
    
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[TYMemo]
(
	[Id] NUMERIC NOT NULL PRIMARY KEY, 
    [FileNo] VARCHAR(MAX) NULL, 
    [TYMemoNO] VARCHAR(MAX) NULL, 
    [Subject] VARCHAR(MAX) NULL, 
    [Date] DATETIME NULL , 
    [FilePath] VARCHAR(MAX) NULL, 
    [CreatedDate] VARCHAR(MAX) NULL DEFAULT GETDATE(), 
    [CreatedBy] VARBINARY(MAX) NULL
);
