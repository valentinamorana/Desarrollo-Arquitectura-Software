-- =========================================================
-- Este script se puede correr las veces que haga falta:
-- no falla si la base, las tablas o los usuarios de prueba
-- ya existen (crea/inserta solo lo que falte).
-- =========================================================

USE [master]
GO
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'GENERALA')
BEGIN
	CREATE DATABASE [GENERALA]
END
GO
USE [GENERALA]
GO

-- =========================================================
-- TABLAS
-- =========================================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'USUARIO')
BEGIN
	CREATE TABLE [dbo].[USUARIO](
		[ID] [int] NOT NULL,
		[Nombre] [varchar](50) NOT NULL,
		[Contraseña] [varchar](50) NOT NULL,
	 CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED ([ID] ASC)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TIPO_LOG')
BEGIN
	CREATE TABLE [dbo].[TIPO_LOG](
		[ID_TIPO] [int] NOT NULL,
		[Tipo] [varchar](50) NOT NULL,
	 CONSTRAINT [PK_TIPO_LOG] PRIMARY KEY CLUSTERED ([ID_TIPO] ASC)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LOG')
BEGIN
	CREATE TABLE [dbo].[LOG](
		[ID_LOG] [int] NOT NULL,
		[Descripcion] [varchar](100) NOT NULL,
		[ID_Usuario] [int] NOT NULL,
		[ID_TIPO] [int] NOT NULL,
		[Fecha] [datetime] NOT NULL,
	 CONSTRAINT [PK_LOG] PRIMARY KEY CLUSTERED ([ID_LOG] ASC)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_LOG_TIPO_LOG')
BEGIN
	ALTER TABLE [dbo].[LOG] WITH CHECK ADD CONSTRAINT [FK_LOG_TIPO_LOG] FOREIGN KEY([ID_TIPO]) REFERENCES [dbo].[TIPO_LOG] ([ID_TIPO])
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_LOG_USUARIO')
BEGIN
	ALTER TABLE [dbo].[LOG] WITH CHECK ADD CONSTRAINT [FK_LOG_USUARIO] FOREIGN KEY([ID_Usuario]) REFERENCES [dbo].[USUARIO] ([ID])
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PARTIDA')
BEGIN
	CREATE TABLE [dbo].[PARTIDA](
		[ID] [int] NOT NULL,
		[ID_JUGADOR1] [int] NOT NULL,
		[ID_JUGADOR2] [int] NOT NULL,
		[ID_GANADOR] [int] NULL,
		[PUNTAJE_JUGADOR1] [int] NOT NULL DEFAULT 0,
		[PUNTAJE_JUGADOR2] [int] NOT NULL DEFAULT 0,
		[FECHA_INICIO] [datetime] NOT NULL,
		[FECHA_FIN] [datetime] NULL,
		[RUTA_XML] [varchar](200) NULL,
	 CONSTRAINT [PK_PARTIDA] PRIMARY KEY CLUSTERED ([ID] ASC)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PARTIDA_JUGADOR1')
BEGIN
	ALTER TABLE [dbo].[PARTIDA] WITH CHECK ADD CONSTRAINT [FK_PARTIDA_JUGADOR1] FOREIGN KEY([ID_JUGADOR1]) REFERENCES [dbo].[USUARIO] ([ID])
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PARTIDA_JUGADOR2')
BEGIN
	ALTER TABLE [dbo].[PARTIDA] WITH CHECK ADD CONSTRAINT [FK_PARTIDA_JUGADOR2] FOREIGN KEY([ID_JUGADOR2]) REFERENCES [dbo].[USUARIO] ([ID])
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PARTIDA_GANADOR')
BEGIN
	ALTER TABLE [dbo].[PARTIDA] WITH CHECK ADD CONSTRAINT [FK_PARTIDA_GANADOR] FOREIGN KEY([ID_GANADOR]) REFERENCES [dbo].[USUARIO] ([ID])
END
GO

-- =========================================================
-- DATOS INICIALES
-- =========================================================

IF NOT EXISTS (SELECT 1 FROM [dbo].[TIPO_LOG] WHERE ID_TIPO = 1)
	INSERT INTO [dbo].[TIPO_LOG] (ID_TIPO, Tipo) VALUES (1, 'INICIO_SESION')
GO
IF NOT EXISTS (SELECT 1 FROM [dbo].[TIPO_LOG] WHERE ID_TIPO = 2)
	INSERT INTO [dbo].[TIPO_LOG] (ID_TIPO, Tipo) VALUES (2, 'CIERRE_SESION')
GO
IF NOT EXISTS (SELECT 1 FROM [dbo].[TIPO_LOG] WHERE ID_TIPO = 3)
	INSERT INTO [dbo].[TIPO_LOG] (ID_TIPO, Tipo) VALUES (3, 'INICIO_PARTIDA')
GO
IF NOT EXISTS (SELECT 1 FROM [dbo].[TIPO_LOG] WHERE ID_TIPO = 4)
	INSERT INTO [dbo].[TIPO_LOG] (ID_TIPO, Tipo) VALUES (4, 'FIN_PARTIDA')
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[USUARIO] WHERE Nombre = 'valentina')
	INSERT INTO [dbo].[USUARIO] (ID, Nombre, Contraseña) VALUES (1, 'valentina', '1234')
GO
IF NOT EXISTS (SELECT 1 FROM [dbo].[USUARIO] WHERE Nombre = 'invitado')
	INSERT INTO [dbo].[USUARIO] (ID, Nombre, Contraseña) VALUES (2, 'invitado', '1234')
GO

-- =========================================================
-- STORED PROCEDURES
-- (CREATE OR ALTER: siempre quedan actualizados, se puede
-- volver a correr el script sin errores de "ya existe")
-- =========================================================

CREATE OR ALTER PROC [dbo].[USUARIO_INSERTAR]
@usu varchar(50), @pass varchar(50)
AS
BEGIN
	DECLARE @ID int
	SET @ID = (SELECT ISNULL(MAX(ID),0)+1 FROM USUARIO)

	INSERT INTO USUARIO (ID, Nombre, Contraseña)
	VALUES (@ID, @usu, @pass)

	SELECT @ID AS ID
END
GO

CREATE OR ALTER PROC [dbo].[USUARIO_LOGIN]
@usu varchar(50), @pass varchar(50)
AS
BEGIN
	SELECT ID, Nombre, Contraseña FROM USUARIO WHERE Nombre = @usu AND Contraseña = @pass
END
GO

CREATE OR ALTER PROC [dbo].[USUARIO_BUSCAR_POR_ID]
@id int
AS
BEGIN
	SELECT ID, Nombre, Contraseña FROM USUARIO WHERE ID = @id
END
GO

CREATE OR ALTER PROC [dbo].[LOG_INSERTAR]
@descripcion varchar(100), @idUsuario int, @idTipo int
AS
BEGIN
	DECLARE @ID int
	SET @ID = (SELECT ISNULL(MAX(ID_LOG),0)+1 FROM LOG)

	INSERT INTO LOG (ID_LOG, Descripcion, ID_Usuario, ID_TIPO, Fecha)
	VALUES (@ID, @descripcion, @idUsuario, @idTipo, GETDATE())
END
GO

CREATE OR ALTER PROC [dbo].[PARTIDA_INSERTAR]
@idJugador1 int, @idJugador2 int, @rutaXml varchar(200)
AS
BEGIN
	DECLARE @ID int
	SET @ID = (SELECT ISNULL(MAX(ID),0)+1 FROM PARTIDA)

	INSERT INTO PARTIDA (ID, ID_JUGADOR1, ID_JUGADOR2, PUNTAJE_JUGADOR1, PUNTAJE_JUGADOR2, FECHA_INICIO, RUTA_XML)
	VALUES (@ID, @idJugador1, @idJugador2, 0, 0, GETDATE(), @rutaXml)

	SELECT @ID AS ID
END
GO

CREATE OR ALTER PROC [dbo].[PARTIDA_FINALIZAR]
@id int, @puntajeJugador1 int, @puntajeJugador2 int, @idGanador int = NULL
AS
BEGIN
	UPDATE PARTIDA
	SET PUNTAJE_JUGADOR1 = @puntajeJugador1,
	    PUNTAJE_JUGADOR2 = @puntajeJugador2,
	    ID_GANADOR = @idGanador,
	    FECHA_FIN = GETDATE()
	WHERE ID = @id
END
GO

CREATE OR ALTER PROC [dbo].[PARTIDA_LISTAR_POR_USUARIO]
@idUsuario int
AS
BEGIN
	SELECT ID, ID_JUGADOR1, ID_JUGADOR2, ID_GANADOR, PUNTAJE_JUGADOR1, PUNTAJE_JUGADOR2, FECHA_INICIO, FECHA_FIN, RUTA_XML
	FROM PARTIDA
	WHERE (ID_JUGADOR1 = @idUsuario OR ID_JUGADOR2 = @idUsuario) AND FECHA_FIN IS NOT NULL
	ORDER BY FECHA_INICIO DESC
END
GO
