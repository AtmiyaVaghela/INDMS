CREATE TABLE [dbo].[Firms]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirmName] NVARCHAR(MAX) NULL, 
    [FirmAddress] NVARCHAR(MAX) NULL, 
    [CreatedDate] DATETIME NULL DEFAULT getdate(), 
    [Active] NVARCHAR(50) NULL DEFAULT '1'
)

insert into Firms(FirmName,FirmAddress) values ('M/S KRISHANA INDUSTRIES','9-25/A, CHANDANWALI 138, CP TANK ROAD MUMBAI-400004');
insert into Firms(FirmName,FirmAddress) values ('M/S AIR CONTROL * CHEMICAL ENGINEERING CO LTD.','63, THE CHAMBERS, NR. GOYALK PLACE OPP. GURUDWARA SARKEJ HIGHWAY, AHMEDABAD-302415');
insert into Firms(FirmName,FirmAddress) values ('M/S ALTOP CONTROLS PVT. LTD.','5, TURAKHIA PARK, SHOPPING COMPLEX, BUILDING NO.1 NEXT TO CORPORATION BANK, MG ROAD, KANDIVALI(W), MUMBAI-400067');
insert into Firms(FirmName,FirmAddress) values ('M/S MEGHA ROTO TECH PVT. LTD.','PLOT NO. C-1/A-4, NR. AEC GRID STATION GIDC. ODHAV, AHMEDABAD-302415');
insert into Firms(FirmName,FirmAddress) values ('M/S POLYPHASE MOTORS','GIDC, MAKARPURA, VAODARA-390010');
insert into Firms(FirmName,FirmAddress) values ('M/S ELECON ENGINEERING CO. LTD.','POST BOX NO.6, ANAND SOJITRA ROAD, VALLABH VIDHYANAGAR-308120');
insert into Firms(FirmName,FirmAddress) values ('M/S MAN DIESEL & TUBO INDIA LIMITED','PLOT NO. 219-220, GIDC, RANOLI VADODARA-391350');
insert into Firms(FirmName,FirmAddress) values ('M/S SPX FLOW TECHNOLOGY PVT. LTD.','SURVEY NO. 275 ODHAV ROAD ODHAV, AHMEDABAD-382415');
insert into Firms(FirmName,FirmAddress) values ('M/S ERHARDT + LEIMER PVT. LTD.','SURVEY NO. 252-1, 252/2 NR. ARVEE DEHIM SARKHEJ-BAVLA HIGHWAY, SARI SANAND-302220');
insert into Firms(FirmName,FirmAddress) values ('M/S FLEXA THERM EXPANFLOW PVT. LTD.','354 GIDC MAKARPURA VADODARA');
insert into Firms(FirmName,FirmAddress) values ('M/S EVEREST KANTO CYLINDERS LIMITED','204, RAHEJA CENTER, FREE PRESS JOURNAL MARG, 214 NARIMAN POINT, MUMBAI-400021');
insert into Firms(FirmName,FirmAddress) values ('M/S JYOTI LIMITED','NANUBHAI AMIN MARG, INDUSTRIUAL AREA, PO CHEMICAL INDUSTRIES, VADODARA-390003');
insert into Firms(FirmName,FirmAddress) values ('M/S GEA REFRIGERATION PVT. LTD.','471, SAKARDA BHADARWA ROAD, NEAR MOXI BUS STAND MOXI-391780');
insert into Firms(FirmName,FirmAddress) values ('M/S PATEL BRASS WORKS','2, BHAKTI NAGAR STATION PLOT, RAJKOT-360022');
insert into Firms(FirmName,FirmAddress) values ('M/S SUPER WAUDITE JOINTINGS PVT. LTD.','G/1, SHYAM SUNDER APPARTMENT, NR. SUSHRUSHA HOSPITAL, 6 LAXMI SOCIETY, NAVRANGPURA, AHMEDABAD-380006');

commit;

insert into ParameterMaster(KeyName,KeyValue) values ('ModeOfTravel','BUS');
insert into ParameterMaster(KeyName,KeyValue) values ('ModeOfTravel','AUTO');
insert into ParameterMaster(KeyName,KeyValue) values ('ModeOfTravel','TRAIN');

commit;


drop table  MovementOrder;
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
    [Distribution]       VARCHAR (MAX) NULL,
    [CreatedBy]          VARCHAR (MAX) NULL,
    [CreatedDate]        DATETIME      NOT NULL,
    [UpdatedBy]          VARCHAR (MAX) NULL,
    [UpdatedDate]        VARCHAR(MAX) NULL,
    [Flag]               VARCHAR (MAX) DEFAULT ('OPEN') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);





