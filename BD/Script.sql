USE [master]
GO
CREATE DATABASE [GENERALA]
GO
USE [GENERALA]
GO

CREATE TABLE [dbo].[USUARIO](
	[ID] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Contraseña] [nvarchar](50) COLLATE Latin1_General_CS_AS NOT NULL,
 CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [UQ_USUARIO_NOMBRE] UNIQUE ([Nombre])
)
GO

CREATE TABLE [dbo].[TIPO_LOG](
	[ID_TIPO] [int] NOT NULL,
	[Tipo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TIPO_LOG] PRIMARY KEY CLUSTERED ([ID_TIPO] ASC)
)
GO

CREATE TABLE [dbo].[LOG](
	[ID_LOG] [int] NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[ID_Usuario] [int] NOT NULL,
	[ID_TIPO] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_LOG] PRIMARY KEY CLUSTERED ([ID_LOG] ASC)
)
GO

CREATE TABLE [dbo].[PARTIDA](
	[ID] [int] NOT NULL,
	[ID_JUGADOR1] [int] NOT NULL,
	[ID_JUGADOR2] [int] NOT NULL,
	[ID_GANADOR] [int] NOT NULL,
	[PUNTAJE_JUGADOR1] [int] NOT NULL,
	[PUNTAJE_JUGADOR2] [int] NOT NULL,
	[FECHA_INICIO] [datetime] NOT NULL,
	[FECHA_FIN] [datetime] NOT NULL,
	[RUTA_XML] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_PARTIDA] PRIMARY KEY CLUSTERED ([ID] ASC)
)
GO

ALTER TABLE [dbo].[LOG] WITH CHECK ADD CONSTRAINT [FK_LOG_TIPO_LOG] FOREIGN KEY([ID_TIPO]) REFERENCES [dbo].[TIPO_LOG] ([ID_TIPO])
GO
ALTER TABLE [dbo].[LOG] WITH CHECK ADD CONSTRAINT [FK_LOG_USUARIO] FOREIGN KEY([ID_Usuario]) REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[PARTIDA] WITH CHECK ADD CONSTRAINT [FK_PARTIDA_JUGADOR1] FOREIGN KEY([ID_JUGADOR1]) REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[PARTIDA] WITH CHECK ADD CONSTRAINT [FK_PARTIDA_JUGADOR2] FOREIGN KEY([ID_JUGADOR2]) REFERENCES [dbo].[USUARIO] ([ID])
GO

CREATE PROCEDURE [dbo].[USUARIO_INSERTAR]
@usu nvarchar(50), @pass nvarchar(50)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM USUARIO WHERE Nombre = @usu)
	BEGIN
		SELECT 0 AS ID
		RETURN
	END

	DECLARE @ID int
	SET @ID = (SELECT ISNULL(MAX(ID),0)+1 FROM USUARIO)

	INSERT INTO USUARIO (ID, Nombre, Contraseña)
	VALUES (@ID, @usu, @pass)

	SELECT @ID AS ID
END
GO

CREATE PROCEDURE [dbo].[USUARIO_LOGIN]
@usu nvarchar(50), @pass nvarchar(50)
AS
BEGIN
	SELECT ID, Nombre FROM USUARIO
	WHERE Nombre = @usu AND Contraseña = @pass
END
GO

CREATE PROCEDURE [dbo].[LOG_INSERTAR]
@descripcion nvarchar(100), @idUsuario int, @idTipo int
AS
BEGIN
	DECLARE @ID int
	SET @ID = (SELECT ISNULL(MAX(ID_LOG),0)+1 FROM LOG)

	INSERT INTO LOG (ID_LOG, Descripcion, ID_Usuario, ID_TIPO, Fecha)
	VALUES (@ID, @descripcion, @idUsuario, @idTipo, GETDATE())
END
GO

CREATE PROCEDURE [dbo].[PARTIDA_INSERTAR]
@idJugador1 int, @idJugador2 int, @idGanador int, @puntajeJugador1 int, @puntajeJugador2 int,
@fechaInicio datetime, @fechaFin datetime, @rutaXml nvarchar(500)
AS
BEGIN
	DECLARE @ID int
	SET @ID = (SELECT ISNULL(MAX(ID),0)+1 FROM PARTIDA)

	INSERT INTO PARTIDA (ID, ID_JUGADOR1, ID_JUGADOR2, ID_GANADOR, PUNTAJE_JUGADOR1, PUNTAJE_JUGADOR2, FECHA_INICIO, FECHA_FIN, RUTA_XML)
	VALUES (@ID, @idJugador1, @idJugador2, @idGanador, @puntajeJugador1, @puntajeJugador2, @fechaInicio, @fechaFin, @rutaXml)

	SELECT @ID AS ID
END
GO

CREATE PROCEDURE [dbo].[PARTIDA_LISTAR_POR_USUARIO]
@idUsuario int
AS
BEGIN
	SELECT p.ID, p.ID_JUGADOR1, p.ID_JUGADOR2, p.ID_GANADOR, p.PUNTAJE_JUGADOR1, p.PUNTAJE_JUGADOR2,
	       p.FECHA_INICIO, p.FECHA_FIN, p.RUTA_XML,
	       u1.Nombre AS NOMBRE_JUGADOR1, u2.Nombre AS NOMBRE_JUGADOR2
	FROM PARTIDA p
	JOIN USUARIO u1 ON u1.ID = p.ID_JUGADOR1
	JOIN USUARIO u2 ON u2.ID = p.ID_JUGADOR2
	WHERE p.ID_JUGADOR1 = @idUsuario OR p.ID_JUGADOR2 = @idUsuario
	ORDER BY p.FECHA_INICIO DESC
END
GO

CREATE PROCEDURE [dbo].[BITACORA_LISTAR]
AS
BEGIN
	SELECT l.ID_LOG, l.Fecha, l.ID_Usuario, u.Nombre AS NombreUsuario, l.ID_TIPO, t.Tipo AS NombreTipo, l.Descripcion
	FROM LOG l
	JOIN USUARIO u ON u.ID = l.ID_Usuario
	JOIN TIPO_LOG t ON t.ID_TIPO = l.ID_TIPO
	ORDER BY l.Fecha DESC, l.ID_LOG DESC
END
GO

INSERT [dbo].[TIPO_LOG] ([ID_TIPO], [Tipo]) VALUES (1, 'INICIO_SESION')
INSERT [dbo].[TIPO_LOG] ([ID_TIPO], [Tipo]) VALUES (2, 'CIERRE_SESION')
INSERT [dbo].[TIPO_LOG] ([ID_TIPO], [Tipo]) VALUES (3, 'INICIO_PARTIDA')
INSERT [dbo].[TIPO_LOG] ([ID_TIPO], [Tipo]) VALUES (4, 'FIN_PARTIDA')
INSERT [dbo].[TIPO_LOG] ([ID_TIPO], [Tipo]) VALUES (5, 'BACKUP')
INSERT [dbo].[TIPO_LOG] ([ID_TIPO], [Tipo]) VALUES (6, 'RESTORE')
GO

INSERT [dbo].[USUARIO] ([ID], [Nombre], [Contraseña]) VALUES (1, N'valentina', N'1234')
INSERT [dbo].[USUARIO] ([ID], [Nombre], [Contraseña]) VALUES (2, N'jugador2', N'1234')
INSERT [dbo].[USUARIO] ([ID], [Nombre], [Contraseña]) VALUES (3, N'jugador3', N'1234')
GO

INSERT [dbo].[PARTIDA] ([ID], [ID_JUGADOR1], [ID_JUGADOR2], [ID_GANADOR], [PUNTAJE_JUGADOR1], [PUNTAJE_JUGADOR2], [FECHA_INICIO], [FECHA_FIN], [RUTA_XML]) VALUES (1, 1, 2, 2, 18, 88, '2026-10-18T19:00:00', '2026-10-18T19:27:14', N'Partida_20261018_190000001.xml')
INSERT [dbo].[PARTIDA] ([ID], [ID_JUGADOR1], [ID_JUGADOR2], [ID_GANADOR], [PUNTAJE_JUGADOR1], [PUNTAJE_JUGADOR2], [FECHA_INICIO], [FECHA_FIN], [RUTA_XML]) VALUES (2, 1, 3, 3, 30, 33, '2026-10-22T20:10:00', '2026-10-22T20:38:27', N'Partida_20261022_201000002.xml')
INSERT [dbo].[PARTIDA] ([ID], [ID_JUGADOR1], [ID_JUGADOR2], [ID_GANADOR], [PUNTAJE_JUGADOR1], [PUNTAJE_JUGADOR2], [FECHA_INICIO], [FECHA_FIN], [RUTA_XML]) VALUES (3, 2, 3, 3, 24, 39, '2026-10-25T18:30:00', '2026-10-25T19:04:49', N'Partida_20261025_183000003.xml')
INSERT [dbo].[PARTIDA] ([ID], [ID_JUGADOR1], [ID_JUGADOR2], [ID_GANADOR], [PUNTAJE_JUGADOR1], [PUNTAJE_JUGADOR2], [FECHA_INICIO], [FECHA_FIN], [RUTA_XML]) VALUES (4, 1, 2, 1, 33, 21, '2026-10-29T21:00:00', '2026-10-29T21:17:24', N'Partida_20261029_210000004.xml')
INSERT [dbo].[PARTIDA] ([ID], [ID_JUGADOR1], [ID_JUGADOR2], [ID_GANADOR], [PUNTAJE_JUGADOR1], [PUNTAJE_JUGADOR2], [FECHA_INICIO], [FECHA_FIN], [RUTA_XML]) VALUES (5, 1, 3, 0, 33, 33, '2026-11-02T19:45:00', '2026-11-02T20:16:25', N'Partida_20261102_194500005.xml')
INSERT [dbo].[PARTIDA] ([ID], [ID_JUGADOR1], [ID_JUGADOR2], [ID_GANADOR], [PUNTAJE_JUGADOR1], [PUNTAJE_JUGADOR2], [FECHA_INICIO], [FECHA_FIN], [RUTA_XML]) VALUES (6, 1, 2, 1, 78, 56, '2026-11-03T20:00:00', '2026-11-03T20:33:53', N'Partida_20261103_200000006.xml')
INSERT [dbo].[PARTIDA] ([ID], [ID_JUGADOR1], [ID_JUGADOR2], [ID_GANADOR], [PUNTAJE_JUGADOR1], [PUNTAJE_JUGADOR2], [FECHA_INICIO], [FECHA_FIN], [RUTA_XML]) VALUES (7, 2, 3, 3, 25, 33, '2026-11-05T18:15:00', '2026-11-05T18:47:40', N'Partida_20261105_181500007.xml')
INSERT [dbo].[PARTIDA] ([ID], [ID_JUGADOR1], [ID_JUGADOR2], [ID_GANADOR], [PUNTAJE_JUGADOR1], [PUNTAJE_JUGADOR2], [FECHA_INICIO], [FECHA_FIN], [RUTA_XML]) VALUES (8, 1, 3, 3, 17, 25, '2026-11-07T21:30:00', '2026-11-07T22:01:49', N'Partida_20261107_213000008.xml')
GO

INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (1, N'Inicio de sesión de valentina', 1, 1, '2026-10-18T18:45:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (2, N'Inicio de sesión de jugador2', 2, 1, '2026-10-18T18:46:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (3, N'Inicio de partida', 1, 3, '2026-10-18T19:00:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (4, N'Inicio de partida', 2, 3, '2026-10-18T19:00:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (5, N'Fin de partida', 1, 4, '2026-10-18T19:27:14')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (6, N'Fin de partida', 2, 4, '2026-10-18T19:27:14')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (7, N'Cierre de sesión de valentina', 1, 2, '2026-10-18T19:29:14')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (8, N'Cierre de sesión de jugador2', 2, 2, '2026-10-18T19:30:14')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (9, N'Inicio de sesión de valentina', 1, 1, '2026-10-22T19:55:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (10, N'Inicio de sesión de jugador3', 3, 1, '2026-10-22T19:56:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (11, N'Inicio de partida', 1, 3, '2026-10-22T20:10:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (12, N'Inicio de partida', 3, 3, '2026-10-22T20:10:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (13, N'Fin de partida', 1, 4, '2026-10-22T20:38:27')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (14, N'Fin de partida', 3, 4, '2026-10-22T20:38:27')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (15, N'Cierre de sesión de valentina', 1, 2, '2026-10-22T20:40:27')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (16, N'Cierre de sesión de jugador3', 3, 2, '2026-10-22T20:41:27')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (17, N'Inicio de sesión de jugador2', 2, 1, '2026-10-25T18:15:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (18, N'Inicio de sesión de jugador3', 3, 1, '2026-10-25T18:16:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (19, N'Inicio de partida', 2, 3, '2026-10-25T18:30:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (20, N'Inicio de partida', 3, 3, '2026-10-25T18:30:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (21, N'Fin de partida', 2, 4, '2026-10-25T19:04:49')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (22, N'Fin de partida', 3, 4, '2026-10-25T19:04:49')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (23, N'Cierre de sesión de jugador2', 2, 2, '2026-10-25T19:06:49')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (24, N'Cierre de sesión de jugador3', 3, 2, '2026-10-25T19:07:49')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (25, N'Inicio de sesión de valentina', 1, 1, '2026-10-29T20:45:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (26, N'Inicio de sesión de jugador2', 2, 1, '2026-10-29T20:46:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (27, N'Inicio de partida', 1, 3, '2026-10-29T21:00:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (28, N'Inicio de partida', 2, 3, '2026-10-29T21:00:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (29, N'Partida abandonada por jugador2', 1, 4, '2026-10-29T21:17:24')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (30, N'Partida abandonada por jugador2', 2, 4, '2026-10-29T21:17:24')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (31, N'Cierre de sesión de valentina', 1, 2, '2026-10-29T21:19:24')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (32, N'Cierre de sesión de jugador2', 2, 2, '2026-10-29T21:20:24')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (33, N'Inicio de sesión de valentina', 1, 1, '2026-11-02T19:30:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (34, N'Inicio de sesión de jugador3', 3, 1, '2026-11-02T19:31:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (35, N'Inicio de partida', 1, 3, '2026-11-02T19:45:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (36, N'Inicio de partida', 3, 3, '2026-11-02T19:45:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (37, N'Fin de partida', 1, 4, '2026-11-02T20:16:25')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (38, N'Fin de partida', 3, 4, '2026-11-02T20:16:25')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (39, N'Cierre de sesión de valentina', 1, 2, '2026-11-02T20:18:25')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (40, N'Cierre de sesión de jugador3', 3, 2, '2026-11-02T20:19:25')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (41, N'Inicio de sesión de valentina', 1, 1, '2026-11-03T19:45:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (42, N'Inicio de sesión de jugador2', 2, 1, '2026-11-03T19:46:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (43, N'Inicio de partida', 1, 3, '2026-11-03T20:00:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (44, N'Inicio de partida', 2, 3, '2026-11-03T20:00:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (45, N'Fin de partida', 1, 4, '2026-11-03T20:33:53')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (46, N'Fin de partida', 2, 4, '2026-11-03T20:33:53')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (47, N'Cierre de sesión de valentina', 1, 2, '2026-11-03T20:35:53')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (48, N'Cierre de sesión de jugador2', 2, 2, '2026-11-03T20:36:53')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (49, N'Inicio de sesión de jugador2', 2, 1, '2026-11-05T18:00:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (50, N'Inicio de sesión de jugador3', 3, 1, '2026-11-05T18:01:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (51, N'Inicio de partida', 2, 3, '2026-11-05T18:15:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (52, N'Inicio de partida', 3, 3, '2026-11-05T18:15:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (53, N'Fin de partida', 2, 4, '2026-11-05T18:47:40')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (54, N'Fin de partida', 3, 4, '2026-11-05T18:47:40')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (55, N'Cierre de sesión de jugador2', 2, 2, '2026-11-05T18:49:40')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (56, N'Cierre de sesión de jugador3', 3, 2, '2026-11-05T18:50:40')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (57, N'Inicio de sesión de valentina', 1, 1, '2026-11-07T21:15:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (58, N'Inicio de sesión de jugador3', 3, 1, '2026-11-07T21:16:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (59, N'Inicio de partida', 1, 3, '2026-11-07T21:30:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (60, N'Inicio de partida', 3, 3, '2026-11-07T21:30:00')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (61, N'Fin de partida', 1, 4, '2026-11-07T22:01:49')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (62, N'Fin de partida', 3, 4, '2026-11-07T22:01:49')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (63, N'Cierre de sesión de valentina', 1, 2, '2026-11-07T22:03:49')
INSERT [dbo].[LOG] ([ID_LOG], [Descripcion], [ID_Usuario], [ID_TIPO], [Fecha]) VALUES (64, N'Cierre de sesión de jugador3', 3, 2, '2026-11-07T22:04:49')
GO
