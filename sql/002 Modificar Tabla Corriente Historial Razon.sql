USE [nx_data]
GO

/****** Object:  Table [dbo].[corrientes]    Script Date: 07/12/2020 13:14:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[familias_historial]
ADD razon text;

GO

ALTER TABLE [dbo].[corrientes_historial]
ADD razon text;

GO

SET ANSI_PADDING OFF
GO


