USE [nx_data]
GO

/****** Object:  Table [dbo].[corrientes]    Script Date: 07/12/2020 13:14:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[familias_historial](
	[familiaAnterior] [varchar](20) NULL,
	[familiaNuevo] [varchar](20) NULL,
	[areainsqAnterior] [float] NULL,
	[areainsqNuevo] [float] NULL,
	usuario [varchar](50) NOT NULL,
	creado [datetime] default CURRENT_TIMESTAMP,
)

GO

CREATE TABLE [dbo].[corrientes_historial](
	[id] [int] NOT NULL,
	[oid] varchar(50) NOT NULL,
	[rectificador] int NOT NULL,
	[corrienteAnterior] [float] NULL,
	[corrienteNuevo] [float] NULL,
	usuario [varchar](50) NOT NULL,
	creado [datetime] default CURRENT_TIMESTAMP,
)

GO

CREATE INDEX corrientes_historial_id ON [dbo].[corrientes_historial] (id)

GO

SET ANSI_PADDING OFF
GO


