# Generala — TP individual DAS (UAI)

Juego de la Generala para 2 jugadores en la misma PC, desarrollado en C# (Windows Forms, .NET Framework 4.7.2) con SQL Server, en arquitectura de 4 capas (BE / DAL / BLL / GUI).

**Autora:** Valentina Morana · **Materia:** Desarrollo y Arquitectura de Software (UAI, 2026)

---

## Funcionalidades

| Área | Qué resuelve |
|------|--------------|
| **Login y menú** | Registro e inicio de sesión de dos jugadores. Después del login, un menú con Jugar, Estadísticas, Bitácora, Reglas y Cerrar sesión |
| **Juego** | Generala completa: 3 tiradas por turno, dados guardables, las 11 categorías, tirada servida, Generala Doble, abandono de partida y revancha con el mismo Jugador 2 |
| **Tachar** | Tachado implícito (si los dados no dan puntos) y voluntario (botón "Tachar", aunque los dados den puntos). Ambos piden confirmación |
| **Estadísticas** | Ganadas, perdidas, empatadas, promedio de victorias, tiempo total jugado e historial filtrable por resultado |
| **Movimientos** | Un XML por partida con cada tirada, anotación y cierre. Se consulta desde "Ver movimientos" y se puede descargar con "Descargar XML" |
| **Bitácora** | Inicios y cierres de sesión, y comienzo y fin de partida, en la tabla `LOG`. Se consulta desde el menú |
| **Backup / Restore** | De la base `GENERALA` desde la propia aplicación (deshabilitados durante una partida) |

---

## Requisitos

- Windows con **Visual Studio** (2019 o posterior) y el workload "Desarrollo de escritorio de .NET".
- **SQL Server** (Express alcanza) y **SQL Server Management Studio** (SSMS) para correr el script.
- .NET Framework 4.7.2.

---

## Instalación

### 1. Crear la base de datos

Abrir `Script.sql` en SSMS, conectado a la instancia de SQL Server, y ejecutarlo completo (F5).

El script crea la base `GENERALA` con sus tablas, stored procedures y usuarios de prueba. Se puede volver a correr sin errores: solo crea lo que falte.

Si la base ya existía de una versión anterior, hay que volver a correr el script: actualiza los stored procedures (por ejemplo `USUARIO_INSERTAR`, que devuelve 0 cuando el nombre ya existe, y `BITACORA_LISTAR`, que usa la pantalla de bitácora) sin borrar datos.

### 2. Configurar la conexión (solo si hace falta)

La app se conecta por defecto a `.\SQLEXPRESS` con autenticación de Windows. Si SQL Server tiene otro nombre de instancia, cambiar el `Data Source` de las dos cadenas (`GENERALA` y `MASTER`) en `GUI/App.config`:

```xml
<connectionStrings>
    <add name="GENERALA" connectionString="Data Source=.\SQLEXPRESS; Initial Catalog=GENERALA; Integrated Security=SSPI; Current Language=Spanish;" providerName="System.Data.SqlClient" />
    <add name="MASTER" connectionString="Data Source=.\SQLEXPRESS; Initial Catalog=master; Integrated Security=SSPI; Current Language=Spanish;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

| Instalación | Data Source |
|---|---|
| SQL Server Express (por defecto) | `.\SQLEXPRESS` |
| SQL Server instancia por defecto (Developer / Standard) | `.` |
| Instancia con nombre | `.\NOMBRE_INSTANCIA` |

Si ya se compiló, también se puede editar `GUI/bin/Debug/GUI.exe.config` directamente, sin recompilar.

### 3. Compilar y ejecutar

Abrir `TP_Generala.sln` en Visual Studio, dejar `GUI` como proyecto de inicio y ejecutar (F5).

---

## Usuarios de prueba

Los carga `Script.sql`:

| Usuario | Contraseña |
|---|---|
| valentina | 1234 |
| jugador2 | 1234 |
| jugador3 | 1234 |

También se pueden crear usuarios nuevos con "Registrarme" en la pantalla de login. El nombre de usuario tiene de 3 a 20 caracteres, sin espacios. La contraseña tiene de 4 a 20 caracteres y al menos un número.

---

## Cómo se usa

1. **Login:** ingresar con el Jugador 1.
2. **Menú:** elegir entre Jugar, Estadísticas, Bitácora, Reglas o Cerrar sesión.
3. **Jugar:** en la pantalla de juego, "Ingresar Jugador 2" con **otro** usuario, y después "Comenzar Partida". En cada turno: hasta 3 tiradas, tildar los dados a guardar después de cada tirada, y elegir la categoría a anotar (o tachar).
4. **Volver al menú:** con el botón "Volver al menú" o la X de la ventana. Si hay una partida en curso, primero hay que abandonarla: la app pregunta qué jugador abandona y la partida la gana el otro.
5. **Cerrar sesión:** solo desde el menú. Registra el cierre de sesión de los dos jugadores.

La partida termina cuando los dos completan las 11 categorías. Las reglas completas están en el botón "¿Cómo jugar?" de la app.

---

## Dónde ver cada requisito

| Requisito | Dónde |
|---|---|
| Registro / Login / Logout | Login → "Registrarme"; logout desde el menú ("Cerrar sesión") |
| Bitácora (inicios y cierres de sesión, comienzo y fin de partida) | Menú → Bitácora. Los datos están en la tabla `LOG` (tipos en `TIPO_LOG`). El inicio y el fin de partida se graban en la misma transacción que la partida |
| Estadísticas, promedio de victorias, tiempo jugado | Menú → Estadísticas |
| XML de movimientos | Un archivo por partida en `GUI/bin/Debug/Partidas/`, con cada tirada (dados y dados guardados), cada anotación y el cierre. Se consulta desde Estadísticas → elegir partida → "Ver movimientos", y se puede guardar una copia con "Descargar XML" |
| Backup / Restore | Botones "Backup" y "Restore" en la pantalla de juego (deshabilitados durante una partida) |

**Nota sobre el backup:** `BACKUP DATABASE` lo ejecuta el servicio de SQL Server, no el usuario de Windows, así que en carpetas personales (Escritorio, Documentos) puede fallar por permisos. El diálogo arranca en la carpeta de backups de SQL Server.

**Nota sobre el restore:** después de restaurar, la aplicación vuelve al login, porque los usuarios de la base restaurada pueden ser otros. Si el `.bak` es de una versión anterior, hay que volver a correr `Script.sql` para actualizar los stored procedures.

**Nota sobre los XML:** la ruta guardada en la base es la de la PC donde se jugó. Si el archivo no existe en esa PC, la app busca el mismo nombre en la carpeta `Partidas` de la aplicación.

---

## Pruebas

El proyecto `Tests.Unitarios` (MSTest, .NET Framework 4.7.2) cubre la lógica de la Generala (puntajes de las categorías, escalera, full, póker, generala, tirada servida y dados guardados) y la validación de los datos de registro. No usa la base de datos.

Para correrlas: en Visual Studio, *Prueba → Ejecutar todas las pruebas*, o con `dotnet test Tests.Unitarios/Tests.Unitarios.csproj`.

---

## Arquitectura

Cuatro capas, con dependencias en un solo sentido: la GUI nunca accede a datos y la BLL es la única que decide las reglas.

| Capa | Proyecto | Responsabilidad |
|------|----------|----------------|
| Presentación | `GUI` | Formularios Windows Forms. Solo muestran lo que devuelve la BLL y avisan lo que hace el usuario |
| Lógica de negocio | `BLL` | Reglas del juego (`PARTIDA`, `GENERALA`), usuarios, bitácora, estadísticas, XML de movimientos y backup/restore |
| Acceso a datos | `DAL` | SQL Server con stored procedures (`ACCESO`, `MAPPER<T>` y un mapper por tabla, backup/restore) y lectura y escritura de los XML |
| Entidades | `BE` | Clases planas (`USUARIO`, `PARTIDA`, `LOG`, `ESTADISTICA`, filas de las grillas) |

Los diagramas UML están en `Documentacion/Diagramas/` (imágenes y modelo de EA) y en los `.cd` de cada proyecto.

---

## Documentación

| Documento | Archivo |
|---|---|
| Informe del TP (PDF) | [`Documentacion/TP Generala - Desarrollo y Arquitectura de Software - Morana, Valentina.docx.pdf`](<Documentacion/TP Generala - Desarrollo y Arquitectura de Software - Morana, Valentina.docx.pdf>) |
| Diagramas (JPG) y modelo de EA | [`Documentacion/Diagramas/`](Documentacion/Diagramas/) |

Los diagramas de clases de Visual Studio (`ClassDiagram1.cd`) están dentro de cada proyecto (`BE/`, `BLL/`, `DAL/`, `GUI/`).

---

## Estructura

```
TP_Generala/
├── BE/              entidades (USUARIO, PARTIDA, LOG, ESTADISTICA, MOVIMIENTO, filas de grillas)
├── DAL/             acceso a SQL Server (ACCESO, MAPPER<T> y un mapper por tabla, backup/restore) y XML de movimientos
├── BLL/             reglas del juego, partida, usuarios, estadísticas, bitácora y XML de movimientos
├── GUI/             Windows Forms: login, menú, juego, estadísticas, bitácora, movimientos, reglas, mensajes y Tema (colores)
├── Tests.Unitarios/ pruebas unitarias (MSTest)
├── Documentacion/   informe del TP (PDF) y diagramas
├── Script.sql       creación de la base, stored procedures y datos de prueba
└── TP_Generala.sln
```
