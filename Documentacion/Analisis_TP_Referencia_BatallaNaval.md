# Análisis de reglas de negocio — TP de referencia "Juego" (Batalla Naval)

Fecha: 02/09/2026
Fuente: `Ejemplos TPS/Juego-20260902T130513Z-1-001.zip` → `JUEGO BATALLA NAVAL/` (mismo enunciado de cátedra, TP #2 de la lista).

## Pregunta: ¿es de a 2 personas? ¿vs. máquina o vs. otra persona en la misma compu?

**Es 2 jugadores humanos, en la misma PC y en la misma ventana (modo "hotseat" / pasar y jugar), sin ningún oponente automático (IA) y sin red.** No hay ninguna clase, método ni lógica de decisión automática en todo el proyecto — `jugador1` y `jugador2` son dos objetos `BE.JUGADOR` manejados exclusivamente por clicks de mouse de quien esté sentado frente a la pantalla.

### Cómo funciona en detalle

1. **Login no funcional.** `FrmLogin.button1_Click` abre `FrmJUEGO` directo, sin llamar a ninguna clase de `BLL`/`DAL` ni validar usuario/contraseña contra la base (`DAL` de este proyecto solo tiene `ACCESO.cs`, no hay `DAL.USUARIO`). `jugador2` queda como un `BE.JUGADOR` en blanco, sin nombre ni ID asignado nunca — no hay una segunda persona "identificada", solo un segundo turno.

2. **Los 2 tableros están en la misma ventana, uno al lado del otro.** `BLL.TABLERO.CrearTablero1()` posiciona sus casillas a partir de X=200, y `CrearTablero2()` a partir de X=1100 — ambos tableros de 15×15 se dibujan simultáneamente en el mismo `Form` (`FrmJUEGO`), lado a lado.

3. **Fase de colocar barcos:** primero se habilita solo el tablero 1 (`bloqueartablero2()` bloquea el otro) para que el jugador 1 coloque sus 5 barcos y confirme con `button1`; recién ahí se habilita el tablero 2 para que el jugador 2 coloque los suyos y confirme con `button2`. Es coordinación por turnos, pero **sin ocultar nada**: como los dos tableros están en la misma ventana visible todo el tiempo, no hay ningún mecanismo (pantalla separada, ventana modal, "pasále la compu y mirá para otro lado") que impida que un jugador vea dónde el otro coloca sus barcos mientras están sentados juntos frente al mismo monitor. Rompe la premisa central de Batalla Naval (el tablero propio es secreto).

4. **Fase de ataque, y un bug real de turnos:** `button3_Click` arranca el ataque con `jugadoractual = jugador1`. El click en una casilla del tablero rival lo maneja `MiPictureBox.OnClick`, que solo tiene una rama:
   ```csharp
   if (formulario.jugadoractual == formulario.jugador1) { ... }
   ```
   No existe ningún `else` para `jugador2`, y en todo el código fuente **no hay una sola línea que reasigne `jugadoractual = jugador2`** — ni siquiera en el caso de "agua" (que solo muestra el mensaje "Cambio de turno" pero no cambia el turno de verdad). Conclusión: tal como está el código, el jugador 2 nunca puede atacar; el juego, tal cual está en este zip, no es jugable de punta a punta. Es un prototipo incompleto, no una implementación terminada de "2 jugadores".

5. **Sin persistencia de la partida ni bitácora conectadas al juego.** `FrmJUEGO` no usa `BLL.PARTIDA`, no hay `DAL.USUARIO`/`DAL.PARTIDA`/`DAL.LOG` en el proyecto, y no se ve ningún backup/restore. La capa de datos de este ejemplo quedó, en los hechos, sin conectar a la partida jugable.

### En una frase

Es el mismo concepto que "Jugador 2" en tu Generala (pasar y jugar en la misma máquina), pero acá **sin ocultamiento de tableros durante la colocación de barcos y con el turno del jugador 2 nunca implementado** — a diferencia de tu proyecto, donde el login de Jugador 2 sí es real (usuario distinto autenticado contra la base) y el cambio de turno sí alterna correctamente entre ambos jugadores en cada `btnAnotar_Click`.
