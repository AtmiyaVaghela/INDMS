
 CREATE TABLE [dbo].[Standards1] (
    [Id]           NUMERIC (18)  NOT NULL identity(1,1) primary Key,
    [StandardNo]   VARCHAR (MAX) NOT NULL,
    [Year]         VARCHAR (10)  NOT NULL,
    [Revision]     VARCHAR (MAX) NOT NULL,
    [SubjectParam] VARCHAR (50)  NOT NULL,
    [Subject]      VARCHAR (MAX) NOT NULL,
    [Type]         VARCHAR (50)  NOT NULL,
    [FilePath]     VARCHAR (MAX) NOT NULL,
    [CreatedBy]    VARCHAR (50)  NULL,
    [CreatedDate]  DATETIME     
);

insert into Standards1( [StandardNo],[Year],[Revision],[SubjectParam],[Subject],[Type],[FilePath],[CreatedBy],[CreatedDate])
select  [StandardNo],[Year],[Revision],[SubjectParam],[Subject],[Type],[FilePath],[CreatedBy],[CreatedDate]
from Standards;

drop table Standards;

EXEC sp_rename 'Standards1', 'Standards';  

CREATE TABLE [dbo].[GeneralBooks1] (
    [ID]          NUMERIC (18)  NOT NULL identity(1,1) primary Key,
    [Title]       VARCHAR (MAX) NOT NULL,
    [Subject]     VARCHAR (MAX) NOT NULL,
    [Year]        VARCHAR (10)  NOT NULL,
    [FilePath]    VARCHAR (MAX) NOT NULL,
    [CreatedBy]   VARCHAR (50)  NULL,
    [CreatedDate] DATETIME      
);

insert into GeneralBooks1([Title],[Subject],[Year],[FilePath],[CreatedBy],[CreatedDate])
select [Title],[Subject],[Year],[FilePath],[CreatedBy],[CreatedDate] 
from  GeneralBooks;

drop table GeneralBooks;

EXEC sp_rename 'GeneralBooks1', 'GeneralBooks';  

CREATE TABLE [dbo].[QAP] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [PONo]         VARCHAR (MAX)   NULL,
    [FileNo]       VARCHAR (MAX)   NULL,
    [QAPNo]        VARCHAR (MAX)   NOT NULL,
    [Subject]      VARCHAR (MAX)   NOT NULL,
    [ApprovalDate] DATETIME        NOT NULL,
    [ApprovedBy]   VARCHAR (MAX)   NOT NULL,
    [DrawingNoRef] VARBINARY (MAX) NULL,
    [FilePath]     VARCHAR (MAX)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


