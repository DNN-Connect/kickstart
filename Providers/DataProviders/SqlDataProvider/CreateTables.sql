/****** Object:  Table [dbo].[Connect_Kickstart_Project]    Script Date: 10.02.2014 20:54:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Connect_Kickstart_Project](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[ContentItemId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Subject] [nvarchar](255) NOT NULL,
	[Summary] [nvarchar](500) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[ProjectUrl] [nvarchar](255) NULL,
	[ProjectPlatform] [nvarchar](255) NULL,
	[PlatformRssUrl] [nvarchar](255) NULL,
	[DateScheduled] [datetime] NULL,
	[DateDelivered] [datetime] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateLocked] [datetime] NULL,
	[DateDeleted] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
	[LockedBy] [int] NULL,
	[DeletedBy] [int] NULL,
	[LeadBy] [int] NULL,
	[IsVisible] [bit] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsDelivered] [bit] NOT NULL,
	[Views] [int] NOT NULL,
	[Comments] [int] NOT NULL,
	[Votes] [int] NOT NULL,
 CONSTRAINT [PK_Connect_Kickstart_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


/****** Object:  Table [dbo].[Connect_Kickstart_ProjectConfig]    Script Date: 10.02.2014 20:54:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Connect_Kickstart_ProjectConfig](
	[SettingId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
	[Setting] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Connect_Kickstart_ProjectConfig] PRIMARY KEY CLUSTERED 
(
	[SettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Connect_Kickstart_ProjectConfig]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_ProjectConfig_Connect_Kickstart_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Connect_Kickstart_Project] ([ProjectId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Connect_Kickstart_ProjectConfig] CHECK CONSTRAINT [FK_Connect_Kickstart_ProjectConfig_Connect_Kickstart_Project]
GO


/****** Object:  Table [dbo].[Connect_Kickstart_Participant]    Script Date: 10.02.2014 20:54:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Connect_Kickstart_Participant](
	[FundingId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ProjectRole] [int] NOT NULL,
 CONSTRAINT [PK_Connect_Kickstart_Participant] PRIMARY KEY CLUSTERED 
(
	[FundingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Connect_Kickstart_Participant]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_Participant_Connect_Kickstart_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Connect_Kickstart_Project] ([ProjectId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Connect_Kickstart_Participant] CHECK CONSTRAINT [FK_Connect_Kickstart_Participant_Connect_Kickstart_Project]
GO


/****** Object:  Table [dbo].[Connect_Kickstart_Incentive]    Script Date: 10.02.2014 20:54:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Connect_Kickstart_Incentive](
	[IncentiveId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[Incentive] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Connect_Kickstart_Incentive] PRIMARY KEY CLUSTERED 
(
	[IncentiveId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Connect_Kickstart_Incentive]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_Incentive_Connect_Kickstart_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Connect_Kickstart_Project] ([ProjectId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Connect_Kickstart_Incentive] CHECK CONSTRAINT [FK_Connect_Kickstart_Incentive_Connect_Kickstart_Project]
GO


/****** Object:  Table [dbo].[Connect_Kickstart_Funding]    Script Date: 10.02.2014 20:55:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Connect_Kickstart_Funding](
	[FundingId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Funding] [money] NOT NULL,
	[Anonymous] [bit] NOT NULL,
	[FundingReceived] [bit] NOT NULL,
 CONSTRAINT [PK_Connect_Kickstart_Funding] PRIMARY KEY CLUSTERED 
(
	[FundingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Connect_Kickstart_Funding]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_Funding_Connect_Kickstart_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Connect_Kickstart_Project] ([ProjectId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Connect_Kickstart_Funding] CHECK CONSTRAINT [FK_Connect_Kickstart_Funding_Connect_Kickstart_Project]
GO


/****** Object:  Table [dbo].[Connect_Kickstart_Comment]    Script Date: 10.02.2014 20:55:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Connect_Kickstart_Comment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[ContentItemId] [int] NOT NULL,
	[ParentId] [int] NULL,
	[Content] [nvarchar](max) NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Votes] [int] NOT NULL,
	[Comments] [int] NOT NULL,
	[IsTeamResponse] [bit] NOT NULL,
 CONSTRAINT [PK_Connect_Kickstart_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Connect_Kickstart_Comment]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_Comment_Connect_Kickstart_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Connect_Kickstart_Project] ([ProjectId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Connect_Kickstart_Comment] CHECK CONSTRAINT [FK_Connect_Kickstart_Comment_Connect_Kickstart_Project]
GO












