# Reglas de negocio — TP Generala

Fecha: 02/09/2026
Para confirmar con la cátedra antes de la entrega.

## 1. Modalidad de juego

- **2 jugadores humanos, en la misma PC, en la misma ventana** (Jugador 1 y Jugador 2 se turnan frente al mismo teclado/mouse). No hay IA ni juego contra la máquina, y no hay red/multijugador remoto.
- **Ambos jugadores tienen que ser usuarios registrados y logueados por separado.** Jugador 1 es quien inició sesión al abrir la app (`FormLogin`). Jugador 2 se loguea desde `FormPrincipal` con el botón "Ingresar Jugador 2", reutilizando la misma pantalla de login — tiene que ser un usuario **distinto** al Jugador 1 (si intenta loguearse con el mismo usuario, se rechaza con un mensaje y puede reintentar).
- Una vez logueado el Jugador 2, se puede arrancar la partida ("Comenzar Partida"). Si se juega una revancha sin volver a loguear, se reutiliza el mismo Jugador 2 de la partida anterior (no obliga a re-loguear en cada partida nueva).

## 2. Estructura de una partida

- Cada partida tiene **11 categorías** para anotar, una sola vez cada una por jugador: Unos, Doses, Treses, Cuatros, Cincos, Seises, Escalera, Full, Póker, Generala y Generala Doble.
- La partida termina cuando **ambos jugadores anotaron las 11 categorías**. Gana quien tenga más puntos sumados; si empatan, es empate. También se puede abandonar la partida en curso: abandona el jugador al que le toca el turno, la partida se registra con el puntaje parcial acumulado y **la gana siempre el rival**, sin importar cómo venían los puntos.
- Si después de una partida se loguea un Jugador 2 distinto, al Jugador 2 anterior se le registra el cierre de sesión en la bitácora.
- El orden de turnos alterna estrictamente entre Jugador 1 y Jugador 2 después de cada anotación.

## 3. Mecánica de un turno

1. El jugador tiene **hasta 3 tiradas** de los 5 dados.
2. Después de cada tirada puede elegir, con un checkbox por dado, cuáles conservar para la próxima tirada (los no marcados se vuelven a tirar). *Los checkboxes de "guardar" solo se pueden usar después de la primera tirada del turno* — no se puede "guardar" un dado que todavía no salió.
3. En cualquier momento después de la primera tirada, el jugador elige una categoría **todavía no anotada** (de las suyas — cada jugador anota sus propias 11 categorías de forma independiente) y anota el puntaje que corresponde según los dados que tiene en ese momento.
4. Si los dados no alcanzan para puntuar en la categoría elegida, se anota **0** igual y esa categoría queda tachada para ese jugador (no se puede volver a usar).
5. **Generala Doble** solo aparece como opción para anotar después de que ese mismo jugador ya anotó "Generala" (no antes).

## 4. Cálculo de puntaje por categoría

- **Unos a Seises**: suma de los dados que muestran ese número (ej. Cincos = suma de todos los dados que salieron 5).
- **Escalera**: 20 puntos si los 5 dados son 1-2-3-4-5 o 2-3-4-5-6 (sin repetir).
- **Full**: 30 puntos si hay exactamente 3 dados de un valor y 2 de otro.
- **Póker**: 40 puntos si hay 4 o más dados iguales.
- **Generala**: 50 puntos si los 5 dados son iguales.
- **Generala Doble**: 100 puntos fijos, solo se puede anotar si en esa tirada volvió a salir una Generala (5 dados iguales) y ya se había anotado "Generala" **con puntos** antes en esa partida. Si la Generala quedó tachada (0), la Generala Doble sigue apareciendo para elegir (así cada jugador siempre juega 11 turnos), pero solo se puede tachar: vale 0 aunque salgan 5 dados iguales.

## 5. Bonus por tirada "servida"

- Si un jugador logra Escalera, Full, Póker o Generala **en la primera tirada del turno** (sin usar la 2ª ni la 3ª tirada), el puntaje de esa categoría se **duplica** (ej. Generala servida = 100 en vez de 50).
- Este bonus **no aplica** a las categorías numéricas (Unos a Seises) ni a Generala Doble — solo a las 4 combinaciones (Escalera, Full, Póker, Generala).
- Se avisa al jugador con un mensaje cuando logra una tirada servida.

## 6. Bitácora y estadísticas

- Se registra en la bitácora: inicio de sesión, cierre de sesión, inicio de partida y fin de partida (de ambos jugadores en cada evento de partida).
- Cada movimiento (tirada anotada) de cada partida se graba en un archivo XML propio de esa partida, con jugador, dados, categoría, puntaje y fecha/hora.
- Las estadísticas por jugador muestran: partidas ganadas / perdidas / empatadas, porcentaje de victorias, y tiempo total acumulado jugado (suma de la duración de todas sus partidas terminadas).

## 7. Puntos a confirmar con el profe

- ¿"2 jugadores" tiene que ser necesariamente pasar-y-jugar en la misma PC, o se espera algún tipo de separación/red entre ambos jugadores? (La consigna no lo aclara explícitamente).
- ¿Generala Doble (100 puntos, requiere haber anotado Generala antes) y el bonus x2 por tirada servida son una interpretación válida de "combinatoria obtenida (Generala, Full, Póker, etc.)", o la cátedra espera solo las categorías básicas sin extras?
- ¿Alcanza con que la revancha reutilice al mismo Jugador 2 sin re-loguear, o se espera loguear de nuevo en cada partida?
