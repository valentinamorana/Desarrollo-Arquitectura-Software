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

`TP_Generala.sln` compila los 4 proyectos (BE, DAL, BLL, GUI) sin errores con MSBuild (.NET Framework 4.7.2). Únicas advertencias: 4 warnings `CS0168` (variable `ex` no usada en bloques `catch` de `ACCESO`/`BACKUPRESTORE`), no bloqueantes — es el mismo patrón de manejo de errores que ya usaban los ejemplos de cátedra.

## 3. Revisión de código (bugs / riesgos)

No se encontraron errores de lógica en la revisión manual de `BLL.GENERALA` (escalera, full, póker, generala), el flujo de turnos de `FormPrincipal`, ni en el cálculo de estadísticas de `FormEstadisticas`.

**Riesgo aceptado y documentado:** `DAL.BACKUPRESTORE` arma el `BACKUP DATABASE` / `RESTORE DATABASE` concatenando la ruta de archivo directamente en el texto SQL (no usa `SqlParameter`, porque `BACKUP`/`RESTORE` no lo permiten como sentencia parametrizable simple). La ruta viene de un `SaveFileDialog`/`OpenFileDialog` nativo de Windows elegido por el propio usuario logueado, no de un campo de texto libre — el riesgo de inyección es mínimo y es una limitación conocida de T-SQL, no un descuido.

**Cobertura de pruebas:** no hay tests automatizados (no se pidieron en la consigna). La verificación fue manual: compilación limpia + revisión de código línea por línea de las clases de negocio y de la UI.

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
