/****** Object:  Table [dbo].[Connect_Kickstart_Project]    Script Date: 03.03.2014 20:46:37 ******/
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
	[TeamMembers] [int] NOT NULL,
 CONSTRAINT [PK_Connect_Kickstart_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Connect_Kickstart_Project]  WITH CHECK ADD  CONSTRAINT [FK_Connect_Kickstart_Project_Modules] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Modules] ([ModuleID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Connect_Kickstart_Project] CHECK CONSTRAINT [FK_Connect_Kickstart_Project_Modules]
GO

/****** Object:  Table [dbo].[Connect_Kickstart_ProjectConfig]    Script Date: 03.03.2014 20:46:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Connect_Kickstart_ProjectConfig](
	[SettingId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
	[Setting] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Connect_Kickstart_ProjectConfig] PRIMARY KEY CLUSTERED 
(
	[SettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Connect_Kickstart_ProjectConfig]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_ProjectConfig_Connect_Kickstart_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Connect_Kickstart_Project] ([ProjectId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Connect_Kickstart_ProjectConfig] CHECK CONSTRAINT [FK_Connect_Kickstart_ProjectConfig_Connect_Kickstart_Project]
GO
/****** Object:  Table [dbo].[Connect_Kickstart_Comment]    Script Date: 03.03.2014 20:47:21 ******/
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

ALTER TABLE [dbo].[Connect_Kickstart_Comment]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_Comment_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Connect_Kickstart_Comment] CHECK CONSTRAINT [FK_Connect_Kickstart_Comment_Users]
GO


/****** Object:  Table [dbo].[Connect_Kickstart_Incentive]    Script Date: 03.03.2014 20:47:45 ******/
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

/****** Object:  Table [dbo].[Connect_Kickstart_Funding]    Script Date: 03.03.2014 20:47:58 ******/
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

ALTER TABLE [dbo].[Connect_Kickstart_Funding]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_Funding_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Connect_Kickstart_Funding] CHECK CONSTRAINT [FK_Connect_Kickstart_Funding_Users]
GO

/****** Object:  Table [dbo].[Connect_Kickstart_Participant]    Script Date: 03.03.2014 20:48:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Connect_Kickstart_Participant](
	[ParticipationId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ProjectRole] [int] NOT NULL,
 CONSTRAINT [PK_Connect_Kickstart_Participant] PRIMARY KEY CLUSTERED 
(
	[ParticipationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Connect_Kickstart_Participant]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_Participant_Connect_Kickstart_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Connect_Kickstart_Project] ([ProjectId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Connect_Kickstart_Participant] CHECK CONSTRAINT [FK_Connect_Kickstart_Participant_Connect_Kickstart_Project]
GO

ALTER TABLE [dbo].[Connect_Kickstart_Participant]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_Participant_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Connect_Kickstart_Participant] CHECK CONSTRAINT [FK_Connect_Kickstart_Participant_Users]
GO

/****** Object:  Table [dbo].[Connect_Kickstart_UserVote]    Script Date: 03.03.2014 20:48:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Connect_Kickstart_UserVote](
	[VoteId] [int] IDENTITY(1,1) NOT NULL,
	[CommentId] [int] NULL,
	[ProjectId] [int] NULL,
	[UserId] [int] NOT NULL,
	[Votes] [int] NOT NULL,
 CONSTRAINT [PK_Connect_Kickstart_UserVote] PRIMARY KEY CLUSTERED 
(
	[VoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Connect_Kickstart_UserVote]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_UserVote_Connect_Kickstart_Comment] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Connect_Kickstart_Comment] ([CommentId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Connect_Kickstart_UserVote] NOCHECK CONSTRAINT [FK_Connect_Kickstart_UserVote_Connect_Kickstart_Comment]
GO

ALTER TABLE [dbo].[Connect_Kickstart_UserVote]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_UserVote_Connect_Kickstart_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Connect_Kickstart_Project] ([ProjectId])
GO

ALTER TABLE [dbo].[Connect_Kickstart_UserVote] NOCHECK CONSTRAINT [FK_Connect_Kickstart_UserVote_Connect_Kickstart_Project]
GO

ALTER TABLE [dbo].[Connect_Kickstart_UserVote]  WITH NOCHECK ADD  CONSTRAINT [FK_Connect_Kickstart_UserVote_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Connect_Kickstart_UserVote] CHECK CONSTRAINT [FK_Connect_Kickstart_UserVote_Users]
GO


