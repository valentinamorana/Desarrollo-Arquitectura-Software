# Auditoría técnica — TP Generala

Fecha: 27/08/2026

## 1. Alcance verificado

### Funcionalidades del núcleo del juego

| Requisito | Estado | Dónde |
|---|---|---|
| Generala para 2 jugadores | OK | `GUI.FormPrincipal` |
| Hasta 3 tiradas por turno | OK | `tirosRestantes` en `FormPrincipal` |
| Conservación individual de dados | OK | checkboxes `chkGuardar1..5` + `BLL.GENERALA.Tirar` |
| Cálculo automático 1-6, escalera, full, póker, generala | OK | `BLL.GENERALA.CalcularPuntaje` |
| Generala doble (opcional) | OK | `BLL.GENERALA.GENERALA_DOBLE`, habilitada solo tras anotar "Generala" |
| Elegir categoría a anotar | OK | `cmbCategoria` |
| Tachar categoría sin puntaje | OK (implícito) | anotar una categoría que no matchea los dados devuelve 0 y la cierra |
| Cambio automático de turno | OK | `btnAnotar_Click` alterna `jugadorActual` |
| Impedir reutilizar categorías | OK | `ActualizarComboCategorias` solo lista categorías sin anotar del jugador activo |
| Determinar ganador o empate | OK | `FinalizarPartida` |
| Nueva partida | OK | se rehabilita "Comenzar Partida" al terminar |
| Abandono de partida | OK | `btnAbandonar` finaliza con el puntaje parcial y bitácora "Partida abandonada" |

### Requisitos LUG

| Requisito | Estado | Dónde |
|---|---|---|
| C# orientado a objetos | OK | todo el proyecto |
| Base de datos MS SQL Server | OK | `Script.sql`, base `GENERALA` |
| Arquitectura de 4 capas (proyectos físicamente separados) | OK | `BE.csproj`, `DAL.csproj`, `BLL.csproj`, `GUI.csproj`, referencias en cadena |
| Registro de usuarios con credenciales | OK | `BE.USUARIO` + `DAL.USUARIO.Insertar` |
| Login / Logout | OK | `BLL.USUARIO.Login` / `.Logout` |
| Bitácora (inicio/cierre sesión, inicio/fin partida) | OK | `BLL.BITACORA` + tabla `LOG` / `TIPO_LOG` |
| Estadísticas ganadas/perdidas/empatadas | OK | `GUI.FormEstadisticas` |
| Movimientos de partida en XML | OK | `BLL.MOVIMIENTOXML` |
| Promedio de victorias | OK | `FormEstadisticas` (`ganadas / totalPartidas * 100`) |
| Tiempo total jugado | OK | `FormEstadisticas` (suma de `FechaFin - FechaInicio`) |
| Backup y Restore de la base | OK | `BLL.BACKUPRESTORE` → `DAL.BACKUPRESTORE` |

## 2. Compilación

`TP_Generala.sln` compila los 4 proyectos (BE, DAL, BLL, GUI) sin errores ni warnings con MSBuild (.NET Framework 4.7.2).

**Corrección aplicada tras comparar con un proyecto de referencia de un compañero (Batalla Naval, misma cátedra):** la cadena de conexión apuntaba a `Data Source=.` (instancia default de SQL Server). La instancia real de la usuaria es SQL Server Express con nombre (`SQLEXPRESS`), así que se corrigió a `Data Source=.\SQLEXPRESS` en `DAL/ACCESO.cs` y `DAL/BACKUPRESTORE.cs`. De paso se limpiaron los 4 warnings `CS0168` cambiando `catch (Exception ex)` por `catch (Exception)` donde la excepción no se usaba.

## 3. Revisión de código (bugs / riesgos)

No se encontraron errores de lógica en la revisión manual de `BLL.GENERALA` (escalera, full, póker, generala), el flujo de turnos de `FormPrincipal`, ni en el cálculo de estadísticas de `FormEstadisticas`.

**Cobertura de pruebas:** no hay tests automatizados (no se pidieron en la consigna). La verificación fue manual: compilación limpia + revisión de código línea por línea de las clases de negocio y de la UI.

**Riesgo aceptado y documentado:** las contraseñas (`USUARIO.Contraseña`) se guardan y comparan en texto plano. No hashea ninguno de los 2 TP de referencia de la cátedra consultados (Batalla Naval) y la consigna no lo exige, así que se mantiene así a propósito para no apartarse del estilo esperado.

### 3.1 Correcciones aplicadas (02/09/2026)

Tras una segunda revisión se encontraron y corrigieron los siguientes problemas:

| # | Problema | Corrección | Dónde |
|---|---|---|---|
| 1 | Si `RESTORE DATABASE` fallaba (`.bak` inválido/corrupto), la base quedaba trabada en `SINGLE_USER` para siempre — la app dejaba de poder conectarse hasta arreglarlo a mano desde SSMS. | El `ALTER DATABASE ... SET MULTI_USER` se ejecuta ahora en un `finally`, así se revierte pase lo que pase con el `RESTORE`. | `DAL/BACKUPRESTORE.cs` |
| 2 | `BACKUP`/`RESTORE DATABASE` concatenan la ruta de archivo en el texto SQL sin escapar. Como el nombre de archivo del `SaveFileDialog`/`OpenFileDialog` es texto editable, un `'` en el nombre rompe el string literal (inyección real, no solo teórica — se corrige la evaluación de riesgo de la versión anterior de esta auditoría). | Se agregó `EscaparRuta()`, que duplica las comillas simples (`'` → `''`) antes de concatenar, el escape estándar de T-SQL. | `DAL/BACKUPRESTORE.cs` |
| 3 | `ACCESO.Leer()` no atrapaba excepciones (a diferencia de `Escribir`/`LeerEscalar`). Un error de conexión durante un login o una consulta tiraba una excepción sin manejar que cerraba toda la aplicación de golpe. | Se agregó un manejador global (`Application.ThreadException`) que muestra un `MessageBox` con el error en vez de crashear. | `GUI/Program.cs` |
| 4 | Ningún método del DAL cerraba la conexión en un `finally`: si algo fallaba entre `Abrir()` y `Cerrar()`, la conexión quedaba abierta. | Se envolvió el cuerpo de cada método de `USUARIO`, `PARTIDA` y `LOG` en `try/finally` para garantizar `acceso.Cerrar()` siempre. | `DAL/USUARIO.cs`, `DAL/PARTIDA.cs`, `DAL/LOG.cs` |
| 5 | Los checkboxes "Guardar" quedaban habilitados desde que arrancaba la partida y nunca se deshabilitaban entre tiradas. Si un jugador tildaba uno antes de tirar por primera vez en el turno, ese dado quedaba en 0 (valor inválido) en vez de tirarse. | Los checkboxes ahora se deshabilitan al empezar cada turno y solo se habilitan después de la primera tirada (mientras queden tiradas), con dos helpers nuevos `HabilitarCheckboxesGuardar()`/`DeshabilitarCheckboxesGuardar()`. | `GUI/FormPrincipal.cs` |

### 3.2 Correcciones aplicadas (10/10/2026)

| # | Problema | Corrección | Dónde |
|---|---|---|---|
| 6 | Al abandonar, el ganador salía de los puntos parciales: quien abandonaba podía quedar como ganador. | Nuevo `BLL.PARTIDA.Abandonar(partida, tabla, idQuienAbandona)`: guarda los puntajes parciales y le da la partida al rival. Abandona el jugador del turno actual (el mensaje de confirmación lo nombra). | `BLL/PARTIDA.cs`, `GUI/FormPrincipal.cs` |
| 7 | La Generala Doble valía 100 aunque la Generala estuviera tachada con 0. | `BLL.PARTIDA.Anotar` anota 0 en Generala Doble si la Generala no tiene puntos. La categoría sigue disponible para tachar, así ambos jugadores siempre completan 11 categorías y la partida termina. | `BLL/PARTIDA.cs` |
| 8 | Si se logueaba un Jugador 2 distinto después de una partida, al anterior no se le registraba el cierre de sesión. | Se llama a `BLL.USUARIO.Logout` del Jugador 2 anterior antes de reemplazarlo. | `GUI/FormPrincipal.cs` |
| 9 | Reglas de juego en la GUI: armado de la planilla, categorías disponibles, cálculo y escritura del puntaje, totales y ganador. | Se mudaron a `BLL.PARTIDA` (`CrearTablaVacia`, `CategoriasDisponibles`, `Anotar`, `CalcularTotal`, `Finalizar(partida, tabla)`, `Abandonar`). `FormPrincipal` solo muestra datos y reacciona a eventos. El fin de partida se notifica a la GUI con el evento `PartidaFinalizada` (delegado `delPartidaFinalizada`). | `BLL/PARTIDA.cs`, `BLL/delPartidaFinalizada.cs`, `GUI/FormPrincipal.cs` |

### 3.3 Correcciones aplicadas (10/10/2026, segunda tanda)

| # | Problema | Corrección | Dónde |
|---|---|---|---|
| 10 | Cerrar la ventana con la X no registraba el logout: solo lo hacía el botón "Salir". | Nuevo `FormPrincipal_FormClosing`, que hace el logout de los dos jugadores. El botón "Salir" solo llama a `this.Close()`, así ambos caminos pasan por el mismo lugar. | `GUI/FormPrincipal.cs` |
| 11 | Cerrar la app a mitad de partida la dejaba abierta en la base (`FECHA_FIN = NULL`) y sin `FIN_PARTIDA` en la bitácora. Pasaba en los datos reales: partidas 6 y 7. | En el `FormClosing`, si hay partida en curso se pide confirmación. Si se confirma, la abandona el jugador del turno con `BLL.PARTIDA.Abandonar`; si no, se cancela el cierre. | `GUI/FormPrincipal.cs` |
| 12 | El XML de movimientos se generaba pero no se podía consultar desde la app ("para su posterior consulta"). | Botón "Ver movimientos" en Estadísticas: lee el XML de la partida elegida (`BLL.MOVIMIENTOXML.LeerMovimientos` → `BE.MOVIMIENTO`) y lo muestra en `FormMovimientos`. Si el archivo no existe, avisa. | `BE/MOVIMIENTO.cs`, `BE/FILAHISTORIAL.cs`, `BLL/MOVIMIENTOXML.cs`, `GUI/FormEstadisticas.*`, `GUI/FormMovimientos.*` |
| 13 | La cadena de conexión estaba fija en el código (`.\SQLEXPRESS`), en dos lugares. | Ahora está en `GUI/App.config` (`connectionStrings` "GENERALA") y se lee con `ConfigurationManager`. El backup/restore arma la conexión a `master` a partir de la misma cadena. | `GUI/App.config`, `DAL/ACCESO.cs`, `DAL/BACKUPRESTORE.cs` |
| 14 | El backup en una carpeta personal fallaba y solo mostraba "Ocurrió un error". Se probó: `BACKUP DATABASE` al Escritorio falla (el servicio de SQL Server no tiene permiso) y a la carpeta de backups de la instancia funciona. | Los diálogos de backup/restore arrancan en la carpeta de backups de SQL Server (`SERVERPROPERTY('InstanceDefaultBackupPath')`). Si falla, se muestra el mensaje real de SQL Server. El restore usa `WITH REPLACE`. | `DAL/BACKUPRESTORE.cs`, `BLL/BACKUPRESTORE.cs`, `GUI/FormPrincipal.cs` |
| 15 | Se podía hacer Backup o Restore con una partida en juego; un restore a mitad de partida deja la partida inconsistente. | `HabilitarJuego()` deshabilita Backup y Restore y `DeshabilitarJuego()` los vuelve a habilitar. "Salir" sigue habilitado porque ahora pasa por el abandono con confirmación. | `GUI/FormPrincipal.cs` |
| 16 | Si el Jugador 2 intentaba loguearse con el mismo usuario que el Jugador 1, se rechazaba, pero el login ya se había hecho y quedaba un inicio de sesión en la bitácora. | `FormLogin` recibe el nombre del Jugador 1 y lo rechaza antes de llamar a `BLL.USUARIO.Login` (sin distinguir mayúsculas, igual que la collation `CI` de la base). Así no se registra nada. La validación por ID en `FormPrincipal` queda como respaldo. | `GUI/FormLogin.cs`, `GUI/FormPrincipal.cs` |
| 17 | `Script.sql` estaba en UTF-8 sin BOM: `sqlcmd` (y SSMS según la configuración) lo leía como ANSI y fallaba en la ñ de `Contraseña` ("Incorrect syntax near ±"). | Se guardó en UTF-8 con BOM. Se verificó corriéndolo sobre la base existente y sobre una base nueva vacía (instalación desde cero): crea 4 tablas, 7 SPs, 3 usuarios y 4 tipos de log. | `Script.sql` |

Se evaluó también hashear las contraseñas, pero se descartó para no apartarse del estilo/nivel de complejidad de los TP de referencia de la cátedra (ver riesgo aceptado arriba).

Se recompiló la solución completa (`MSBuild TP_Generala.sln`) después de cada cambio: sigue compilando sin errores ni warnings de código.

## 4. Estructura final del repositorio

```
TP_Generala/
├── BE/            (Class Library — entidades)
├── DAL/           (Class Library — acceso a datos)
├── BLL/           (Class Library — lógica de negocio)
├── GUI/           (WinForms .exe — presentación)
├── Documentacion/ (esta auditoría + diagramas)
├── Script.sql     (creación de base + stored procedures)
└── TP_Generala.sln
```

## 5. Conclusión

El proyecto cubre el 100% de los puntos de "Funcionalidades fundamentales" y "Requisitos LUG" de la consigna, con una arquitectura de 4 capas físicamente separada y sin dependencias circulares (GUI nunca referencia DAL). No se detectaron bugs bloqueantes al momento de esta auditoría.
