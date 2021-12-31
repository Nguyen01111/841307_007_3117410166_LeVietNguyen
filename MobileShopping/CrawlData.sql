USE [CrawlData]
GO
/****** Object:  Table [dbo].[DoAn]    Script Date: 12/31/2021 10:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoAn](
	[ID] [nchar](10) NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Price] [nvarchar](255) NULL,
	[Img] [nvarchar](255) NULL,
	[Link] [nvarchar](255) NULL
) ON [PRIMARY]
GO
