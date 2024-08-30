USE [Tareas]
GO

/****** Object:  Table [dbo].[tareasPendientes]    Script Date: 29/08/2024 13:31:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tareasPendientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](20) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Fecha_Creacion] [datetime] NOT NULL,
	[Fecha_Vencimiento] [datetime] NOT NULL,
	[Completada] [bit] NOT NULL,
	[Estado] [varchar](1) NOT NULL,
 CONSTRAINT [PK_especie] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tareasPendientes] ADD  CONSTRAINT [DF_tareasPendientes_Estado]  DEFAULT ('A') FOR [Estado]
GO


