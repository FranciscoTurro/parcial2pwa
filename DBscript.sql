USE [PWADB]
GO

/****** Object: Table [dbo].[Alumno] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Alumno](
    [DNI] [varchar](50) NOT NULL,
    [Nombre] [varchar](50) NOT NULL,
    [Apellido] [varchar](50) NOT NULL,
    [Email] [varchar](255) NOT NULL,
    [Domicilio] [varchar](255) NOT NULL,
    [FechaNacimiento] [date] NOT NULL,
	[SituacionBeca] [varchar](20) NOT NULL,
    [Foto] [varchar](max) NULL,
    CONSTRAINT [PK_Alumno] PRIMARY KEY CLUSTERED 
    (
        [DNI] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object: Table [dbo].[Materia] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Materia](
    [ID] [varchar](10) NOT NULL,
    [Cupo] [int] NOT NULL,
    [Sede] [varchar](50) NOT NULL,
    [FechaInicio] [date] NOT NULL,
    [Turno] [varchar](10) NOT NULL,
    [ImporteInscripcion] [int] NOT NULL,
    CONSTRAINT [PK_Materia] PRIMARY KEY CLUSTERED 
    (
        [ID] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object: Table [dbo].[Inscripcion] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inscripcion](
    [DNIAlumno] [varchar](50) NOT NULL,
    [IDMateria] [varchar](10) NOT NULL,
	[Abono] [decimal](18, 2) NOT NULL,
    CONSTRAINT [PK_Inscripcion] PRIMARY KEY CLUSTERED 
    (
        [DNIAlumno] ASC,
        [IDMateria] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Inscripcion] WITH CHECK 
    ADD CONSTRAINT [FK_Inscripcion_Alumno] FOREIGN KEY([DNIAlumno])
    REFERENCES [dbo].[Alumno] ([DNI])

GO

ALTER TABLE [dbo].[Inscripcion] CHECK CONSTRAINT [FK_Inscripcion_Alumno]

GO

ALTER TABLE [dbo].[Inscripcion] WITH CHECK 
    ADD CONSTRAINT [FK_Inscripcion_Materia] FOREIGN KEY([IDMateria])
    REFERENCES [dbo].[Materia] ([ID])

GO

ALTER TABLE [dbo].[Inscripcion] CHECK CONSTRAINT [FK_Inscripcion_Materia]

GO

USE [master]
GO
ALTER DATABASE [PWADB] SET READ_WRITE
GO
