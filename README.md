# Generala — TP individual DAS (UAI)

Juego de la Generala para 2 jugadores en la misma PC, en C# (Windows Forms, .NET Framework 4.7.2) con SQL Server, en arquitectura de 4 capas (BE / DAL / BLL / GUI).

Alumna: Valentina Morana

## Requisitos

- Windows con **Visual Studio** (2019 o posterior) y el workload "Desarrollo de escritorio de .NET".
- **SQL Server** (Express alcanza) y **SQL Server Management Studio** (SSMS) para correr el script.
- .NET Framework 4.7.2.

## Instalación

### 1. Crear la base de datos

Abrir `Script.sql` en SSMS, conectado a la instancia de SQL Server, y ejecutarlo completo (F5).

El script crea la base `GENERALA` con sus tablas, stored procedures y usuarios de prueba. Se puede volver a correr sin errores: solo crea lo que falte.

### 2. Configurar la conexión (solo si hace falta)

La app se conecta por defecto a la instancia `.\SQLEXPRESS` con autenticación de Windows. Si SQL Server está instalado con otro nombre de instancia, cambiar el `Data Source` en `GUI/App.config`:

```xml
<connectionStrings>
    <add name="GENERALA" connectionString="Data Source=.\SQLEXPRESS; Initial Catalog=GENERALA; Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

Ejemplos de `Data Source`:

| Instalación | Data Source |
|---|---|
| SQL Server Express (por defecto) | `.\SQLEXPRESS` |
| SQL Server instancia por defecto (Developer / Standard) | `.` |
| Instancia con nombre | `.\NOMBRE_INSTANCIA` |

Si ya se compiló, también se puede editar `GUI/bin/Debug/GUI.exe.config` directamente, sin recompilar.

### 3. Compilar y ejecutar

Abrir `TP_Generala.sln` en Visual Studio, dejar `GUI` como proyecto de inicio y ejecutar (F5).

## Usuarios de prueba

Los carga `Script.sql`:

| Usuario | Contraseña |
|---|---|
| valentina | 1234 |
| jugador2 | 1234 |
| jugador3 | 1234 |

También se pueden crear usuarios nuevos con el botón "Registrarme" de la pantalla de login.

## Cómo se juega

1. Ingresar con el Jugador 1 (pantalla inicial).
2. En la pantalla principal, "Ingresar Jugador 2" con **otro** usuario.
3. "Comenzar Partida". Cada turno: hasta 3 tiradas, tildar los dados a guardar después de cada tirada y elegir la categoría a anotar.
4. La partida termina cuando los dos completan las 11 categorías. También se puede abandonar: la gana el rival.

Las reglas completas están en el botón "Reglas" de la app y en `Documentacion/Reglas_de_Negocio_Generala.md`.

## Dónde ver cada requisito

| Requisito | Dónde |
|---|---|
| Registro / Login / Logout | Pantalla de login → "Registrarme"; botón "Salir" (o cerrar la ventana) |
| Bitácora | Tabla `LOG` de la base (tipos en `TIPO_LOG`) |
| Estadísticas, promedio de victorias, tiempo jugado | Botón "Estadísticas" |
| XML de movimientos | Se guardan en `GUI/bin/Debug/Partidas/`. Se consultan desde Estadísticas → elegir partida → "Ver movimientos" |
| Backup / Restore | Botones "Backup" y "Restore" (deshabilitados durante una partida) |

**Nota sobre el backup:** `BACKUP DATABASE` lo ejecuta el servicio de SQL Server, no el usuario de Windows, así que en carpetas personales (Escritorio, Documentos) suele fallar por permisos. Por eso el diálogo arranca en la carpeta de backups de SQL Server, que es donde conviene guardarlo.

## Estructura

```
TP_Generala/
├── BE/             entidades (USUARIO, PARTIDA, MOVIMIENTO, filas de grillas)
├── DAL/            acceso a SQL Server (ACCESO + una clase por tabla, backup/restore)
├── BLL/            reglas del juego, partida, bitácora, XML de movimientos
├── GUI/            Windows Forms (ejecutable)
├── Documentacion/  documento del TP (.docx), auditoría, reglas, diagramas
├── Script.sql      creación de la base, stored procedures y datos de prueba
└── TP_Generala.sln
```
