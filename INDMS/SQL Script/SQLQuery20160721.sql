USE [INDMS20160716]
GO
/****** Object:  Table [dbo].[Firms]    Script Date: 21-Jul-16 10:36:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
drop table [dbo].[Firms]
Go
CREATE TABLE [dbo].[Firms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirmName] [nvarchar](max) NULL,
	[FirmAddress] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[Active] [nvarchar](50) NULL DEFAULT ('1'),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ParameterMaster]    Script Date: 21-Jul-16 10:36:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
Drop table [dbo].[ParameterMaster]
GO
CREATE TABLE [dbo].[ParameterMaster](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[KeyName] [varchar](50) NOT NULL,
	[KeyValue] [varchar](max) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_ParameterMaster_CreatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_ParameterMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 21-Jul-16 10:36:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
DROP TABLE [dbo].[Roles]
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Roles__3214EC07DA5A1121] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 21-Jul-16 10:36:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
DROP TABLE [dbo].[User]
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[Role] [varchar](10) NOT NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[Active] [varchar](10) NULL DEFAULT ('Y'),
	[Designation] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Firms] ON 

GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (1, N'M/S KRISHANA INDUSTRIES', N'9-25/A, CHANDANWALI 138, CP TANK ROAD MUMBAI-400004', CAST(N'2016-07-04 14:04:05.133' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (2, N'M/S AIR CONTROL * CHEMICAL ENGINEERING CO LTD.', N'63, THE CHAMBERS, NR. GOYALK PLACE OPP. GURUDWARA SARKEJ HIGHWAY, AHMEDABAD-302415', CAST(N'2016-07-04 14:04:05.270' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (3, N'M/S ALTOP CONTROLS PVT. LTD.', N'5, TURAKHIA PARK, SHOPPING COMPLEX, BUILDING NO.1 NEXT TO CORPORATION BANK, MG ROAD, KANDIVALI(W), MUMBAI-400067', CAST(N'2016-07-04 14:04:05.300' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (4, N'M/S MEGHA ROTO TECH PVT. LTD.', N'PLOT NO. C-1/A-4, NR. AEC GRID STATION GIDC. ODHAV, AHMEDABAD-302415', CAST(N'2016-07-04 14:04:05.310' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (5, N'M/S POLYPHASE MOTORS', N'GIDC, MAKARPURA, VAODARA-390010', CAST(N'2016-07-04 14:04:05.323' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (6, N'M/S ELECON ENGINEERING CO. LTD.', N'POST BOX NO.6, ANAND SOJITRA ROAD, VALLABH VIDHYANAGAR-308120', CAST(N'2016-07-04 14:04:05.330' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (7, N'M/S MAN DIESEL & TUBO INDIA LIMITED', N'PLOT NO. 219-220, GIDC, RANOLI VADODARA-391350', CAST(N'2016-07-04 14:04:05.333' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (8, N'M/S SPX FLOW TECHNOLOGY PVT. LTD.', N'SURVEY NO. 275 ODHAV ROAD ODHAV, AHMEDABAD-382415', CAST(N'2016-07-04 14:04:05.340' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (9, N'M/S ERHARDT + LEIMER PVT. LTD.', N'SURVEY NO. 252-1, 252/2 NR. ARVEE DEHIM SARKHEJ-BAVLA HIGHWAY, SARI SANAND-302220', CAST(N'2016-07-04 14:04:05.347' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (10, N'M/S FLEXA THERM EXPANFLOW PVT. LTD.', N'354 GIDC MAKARPURA VADODARA', CAST(N'2016-07-04 14:04:05.360' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (11, N'M/S EVEREST KANTO CYLINDERS LIMITED', N'204, RAHEJA CENTER, FREE PRESS JOURNAL MARG, 214 NARIMAN POINT, MUMBAI-400021', CAST(N'2016-07-04 14:04:05.367' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (12, N'M/S JYOTI LIMITED', N'NANUBHAI AMIN MARG, INDUSTRIUAL AREA, PO CHEMICAL INDUSTRIES, VADODARA-390003', CAST(N'2016-07-04 14:04:05.387' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (13, N'M/S GEA REFRIGERATION PVT. LTD.', N'471, SAKARDA BHADARWA ROAD, NEAR MOXI BUS STAND MOXI-391780', CAST(N'2016-07-04 14:04:05.393' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (14, N'M/S PATEL BRASS WORKS', N'2, BHAKTI NAGAR STATION PLOT, RAJKOT-360022', CAST(N'2016-07-04 14:04:05.407' AS DateTime), N'1')
GO
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (15, N'M/S SUPER WAUDITE JOINTINGS PVT. LTD.', N'G/1, SHYAM SUNDER APPARTMENT, NR. SUSHRUSHA HOSPITAL, 6 LAXMI SOCIETY, NAVRANGPURA, AHMEDABAD-380006', CAST(N'2016-07-04 14:04:05.410' AS DateTime), N'1')
GO
SET IDENTITY_INSERT [dbo].[Firms] OFF
GO
SET IDENTITY_INSERT [dbo].[ParameterMaster] ON 

GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(8 AS Numeric(18, 0)), N'StdSubject', N'EQUIPMENT', NULL, CAST(N'2016-05-09 21:49:59.890' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(9 AS Numeric(18, 0)), N'StdSubject', N'SPARES FOR', NULL, CAST(N'2016-05-09 21:50:02.013' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(10 AS Numeric(18, 0)), N'StdEquipment', N'COMPRESSOR', NULL, CAST(N'2016-05-09 21:50:03.617' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(11 AS Numeric(18, 0)), N'StdEquipment', N'PUMP', NULL, CAST(N'2016-05-09 21:50:11.723' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(12 AS Numeric(18, 0)), N'StdEquipment', N'GEAR BOX', NULL, CAST(N'2016-05-09 21:51:15.570' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(13 AS Numeric(18, 0)), N'StdEquipment', N'VALVES', NULL, CAST(N'2016-05-09 21:51:17.413' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(14 AS Numeric(18, 0)), N'StdEquipment', N'WINDOW WIPER', NULL, CAST(N'2016-05-09 21:51:18.930' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(15 AS Numeric(18, 0)), N'StdEquipment', N'BULB BAR', NULL, CAST(N'2016-05-09 21:51:20.723' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(16 AS Numeric(18, 0)), N'StdEquipment', N'RC PLANT', NULL, CAST(N'2016-05-09 21:51:22.303' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(17 AS Numeric(18, 0)), N'StdEquipment', N'MOTOR', NULL, CAST(N'2016-05-09 21:51:29.680' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(18 AS Numeric(18, 0)), N'StdSpareFor', N'COMPRESSOR', NULL, CAST(N'2016-05-09 21:52:14.993' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(19 AS Numeric(18, 0)), N'StdSpareFor', N'PUMP', NULL, CAST(N'2016-05-09 21:52:16.540' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(20 AS Numeric(18, 0)), N'StdSpareFor', N'GEAR BOX', NULL, CAST(N'2016-05-09 21:52:18.007' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(21 AS Numeric(18, 0)), N'StdSpareFor', N'VALVES', NULL, CAST(N'2016-05-09 21:52:19.413' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(22 AS Numeric(18, 0)), N'StdSpareFor', N'WINDOW WIPER', NULL, CAST(N'2016-05-09 21:52:24.693' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(23 AS Numeric(18, 0)), N'StdSpareFor', N'RC PLANT', NULL, CAST(N'2016-05-09 21:52:45.647' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(24 AS Numeric(18, 0)), N'StdSpareFor', N'MOTOR', NULL, CAST(N'2016-05-09 21:52:47.990' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(30 AS Numeric(18, 0)), N'StdType', N'IS', NULL, CAST(N'2016-05-09 21:53:20.357' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(31 AS Numeric(18, 0)), N'StdType', N'BS', NULL, CAST(N'2016-05-09 21:53:36.670' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(32 AS Numeric(18, 0)), N'StdType', N'ASTM', NULL, CAST(N'2016-05-09 21:53:37.980' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(33 AS Numeric(18, 0)), N'StdType', N'NES', NULL, CAST(N'2016-05-09 21:53:39.900' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(34 AS Numeric(18, 0)), N'StdType', N'EN', NULL, CAST(N'2016-05-09 21:53:41.243' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(35 AS Numeric(18, 0)), N'StdType', N'DIN', NULL, CAST(N'2016-05-09 21:53:42.917' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(36 AS Numeric(18, 0)), N'StdType', N'INS', NULL, CAST(N'2016-05-09 21:53:44.087' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(37 AS Numeric(18, 0)), N'StdType', N'JSS', NULL, CAST(N'2016-05-09 21:53:45.773' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(38 AS Numeric(18, 0)), N'StdType', N'DME', NULL, CAST(N'2016-05-09 21:53:47.133' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(40 AS Numeric(18, 0)), N'StdType', N'DGS', NULL, CAST(N'2016-05-09 21:53:50.333' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(41 AS Numeric(18, 0)), N'StdType', N'ASME', NULL, CAST(N'2016-05-09 21:53:51.743' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(43 AS Numeric(18, 0)), N'StdType', N'ISO', NULL, CAST(N'2016-05-09 21:53:54.383' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(44 AS Numeric(18, 0)), N'StdType', N'ZAN', NULL, CAST(N'2016-05-09 21:53:55.870' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(45 AS Numeric(18, 0)), N'SoSubject', N'TECH', NULL, CAST(N'2016-05-09 21:53:57.837' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(46 AS Numeric(18, 0)), N'SoSubject', N'ADMIN', NULL, CAST(N'2016-05-09 21:55:45.810' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(49 AS Numeric(18, 0)), N'IssuingAuthority', N'DQA(N)', N'4b23c201-84e9-47d0-834d-8aa8b40f0389', NULL)
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(50 AS Numeric(18, 0)), N'IssuingAuthority', N'DQA(WP)', N'4b23c201-84e9-47d0-834d-8aa8b40f0389', NULL)
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(51 AS Numeric(18, 0)), N'IssuingAuthority', N'DGQA', N'4b23c201-84e9-47d0-834d-8aa8b40f0389', NULL)
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(52 AS Numeric(18, 0)), N'ModeOfTravel', N'BUS', NULL, CAST(N'2016-07-05 14:34:06.030' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(53 AS Numeric(18, 0)), N'ModeOfTravel', N'AUTO', NULL, CAST(N'2016-07-05 14:34:06.050' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(54 AS Numeric(18, 0)), N'ModeOfTravel', N'TRAIN', NULL, CAST(N'2016-07-05 14:34:06.060' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(55 AS Numeric(18, 0)), N'POPlacingAuthority', N'CGHQ', NULL, CAST(N'2016-07-08 15:04:18.297' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(56 AS Numeric(18, 0)), N'POPlacingAuthority', N'CPRO(K)', NULL, CAST(N'2016-07-08 15:07:05.933' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(57 AS Numeric(18, 0)), N'POPlacingAuthority', N'CPRO(MB)', NULL, CAST(N'2016-07-08 15:07:05.933' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(58 AS Numeric(18, 0)), N'POPlacingAuthority', N'CPRO(KAR)', NULL, CAST(N'2016-07-08 15:07:05.933' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(59 AS Numeric(18, 0)), N'POPlacingAuthority', N'CPRO(V)', NULL, CAST(N'2016-07-08 15:07:05.933' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(60 AS Numeric(18, 0)), N'POPlacingAuthority', N'GRSEL', NULL, CAST(N'2016-07-08 15:07:05.933' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(61 AS Numeric(18, 0)), N'POPlacingAuthority', N'CSL', NULL, CAST(N'2016-07-08 15:07:05.933' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(62 AS Numeric(18, 0)), N'POPlacingAuthority', N'DPRO', NULL, CAST(N'2016-07-08 15:07:05.933' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(63 AS Numeric(18, 0)), N'POPlacingAuthority', N'DMDE', NULL, CAST(N'2016-07-08 15:07:05.933' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(64 AS Numeric(18, 0)), N'POPlacingAuthority', N'GSL', NULL, CAST(N'2016-07-08 15:07:05.933' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(65 AS Numeric(18, 0)), N'POPlacingAuthority', N'NSRY(K)', NULL, CAST(N'2016-07-08 15:07:05.940' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(66 AS Numeric(18, 0)), N'POPlacingAuthority', N'MDL', NULL, CAST(N'2016-07-08 15:07:05.943' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(67 AS Numeric(18, 0)), N'POPlacingAuthority', N'PIPAVAV', NULL, CAST(N'2016-07-08 15:07:05.943' AS DateTime))
GO
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(68 AS Numeric(18, 0)), N'POPlacingAuthority', N'ABG', NULL, CAST(N'2016-07-08 15:07:05.943' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ParameterMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

GO
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (2, N'General')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[User] ([UserId], [UserName], [Password], [Role], [Name], [Email], [CreatedBy], [CreatedDate], [Active], [Designation]) VALUES (N'4b23c201-84e9-47d0-834d-8aa8b40f0389', N'admin', N'YWRtaW4=', N'Admin', N'Atmiya', N'advaghela@techelecon.com', N'00000000-0000-0000-0000-000000000000', NULL, N'Y', N'Jr. Engineer')
GO
INSERT [dbo].[User] ([UserId], [UserName], [Password], [Role], [Name], [Email], [CreatedBy], [CreatedDate], [Active], [Designation]) VALUES (N'e78667e8-2912-4184-a767-f7d7028adbe1', N'atmiya', N'YXRtaXlh', N'General', N'Atmiya Vaghela', N'advaghela@eleconinfotech.com', N'4b23c201-84e9-47d0-834d-8aa8b40f0389', CAST(N'2016-07-07 16:37:00.350' AS DateTime), NULL, N'Jr. Engineer')
GO
