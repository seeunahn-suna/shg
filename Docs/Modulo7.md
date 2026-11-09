# Módulo 7: variables booleanas

Rama: `Practica-Modulo-7`. Proyecto: `shg`, Unity **2022.3.62f3**.

## Abrir y probar

1. Abre `Assets/Modulo 7/Modulo7.unity` y pulsa **Play**.
2. Observa la fila de cinco objetos y los valores debajo de cada uno. Blanco significa `true`; negro significa `false`.
3. El **Modo lento** está activado al comenzar. La simulación avanza al 2 %: con el paso fijo de 0,02 s del proyecto, hay aproximadamente un `FixedUpdate` por segundo real. Al desmarcarlo recuperas la velocidad normal. Los scripts de A y B invierten su valor en **cada** `FixedUpdate` en ambos modos.
4. Pulsa **Pausar**. Las casillas A y B se habilitan para comprobar las cuatro filas de la tabla siguiente. AND, OR y el quinto objeto se actualizan al cambiar cualquiera de las dos entradas.
5. Pulsa **Reanudar** para continuar las inversiones automáticas. Al salir de Play se restaura la escala de tiempo anterior; no se modifica `ProjectSettings/TimeManager.asset`.

| A | B | GO3: A && B | GO4: A \|\| B | GO5: GO3 && GO4 |
| --- | --- | --- | --- | --- |
| false | false | false | false | false |
| false | true | false | true | false |
| true | false | false | true | false |
| true | true | true | true | true |

## Relación con el enunciado

Las rutas de los scripts siguientes parten de `Assets/Modulo 7/Script/`.

| Paso | Implementación |
| --- | --- |
| 3 | `PrimerBooleano.cs`, `SegundoBooleano.cs` y `TercerBooleanoAND.cs` conservan comentadas las líneas de color con `Random.value` de los tres scripts anteriores. Se trabaja sobre copias para conservar el módulo 6. |
| 4 | `GO1_A` tiene `PrimerBooleano`: `valor = !valor` en `FixedUpdate`. |
| 5 | `GO2_B` tiene `SegundoBooleano`, que realiza la misma inversión en `FixedUpdate`. |
| 6 | `GO3_AND` tiene referencias a los componentes del primer y segundo GO, asignadas en el Inspector. |
| 7 | `TercerBooleanoAND.Evaluar()` calcula `primero.valor && segundo.valor`. |
| 8 | `GO4_OR` es una copia del tercer objeto. Su script independiente `CuartoBooleanoOR` duplica la estructura del tercero y cambia el operador por `||`. |
| 9 | `GO5_AND_Resultados`, con `QuintoBooleano`, obtiene los resultados de GO3 y GO4. Se elige el operador AND: `tercero.valor && cuarto.valor`. |
| 10 | Cinco GO colocados de izquierda a derecha en una misma fila, con valores y operadores visibles. La clase común `ObjetoBooleano` aplica `Color.white` o `Color.black`. |
| 11 | Commit, push de `Practica-Modulo-7` y Pull Request hacia `main`. |
| 12 | Compartir el enlace de la rama o del Pull Request con el tutor. El PR puede permanecer abierto para su revisión. |

Los cinco prefabs están en `Assets/Modulo 7/Prefab/`. Cada instancia de escena ya tiene sus referencias. Si añades un prefab de operador a otra escena, debes asignar sus entradas en el Inspector.

## Por qué se usan estos valores iniciales

A comienza en `false` y B en `true`. Como ambos se invierten en cada paso fijo, continúan siendo opuestos: GO3 permanece negro y GO4 blanco durante esa ejecución. Esto es el resultado correcto de la lógica. Si comienzan iguales, AND y OR coinciden y cambian a la vez. Para explorar las cuatro combinaciones se utilizan la pausa y las dos casillas, sin introducir frecuencias diferentes a las que pide el enunciado.

El atributo `DefaultExecutionOrder` garantiza que, dentro del mismo paso de física, primero cambien A y B (orden -200), después se evalúen AND y OR (-100) y por último el quinto objeto (0). Así los resultados corresponden a las entradas actuales.

`ObjetoBooleano` crea un material por instancia y lo libera al destruirse. Los objetos no comparten sus cambios de color ni modifican el material del prefab.

## Verificación

Con la escena guardada y Play detenido, ejecuta **Practicas > Modulo 7 > Comprobar en Play**. La herramienta entra en Play, verifica las referencias y la fila de cinco objetos, observa las inversiones en pasos reales de física y prueba las cuatro combinaciones de la tabla. También verifica los colores, la independencia de materiales, la pausa y la ausencia de errores durante la ejecución. Al terminar, sale de Play.

El informe queda en `Temp/Modulo7Checks.txt`; la captura, en `Temp/Modulo7Play.png`. Una copia del informe de validación y de la captura está en `Docs/` para la revisión del tutor.

El menú **Practicas > Modulo 7 > Preparar escena** abre la escena existente. Si no existe, crea la escena, sus copias de prefabs y los materiales. No sobrescribe una escena ya creada.

## Archivos y escenas anteriores

Se añaden `Assets/Modulo 7/`, `Assets/Editor/Modulo7SceneSetup.cs`, `Assets/Editor/Modulo7Checks.cs` y esta documentación. Se actualiza el índice `README.md`. Las escenas, scripts y prefabs de los módulos anteriores se conservan.
