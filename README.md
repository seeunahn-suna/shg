# shg — Prácticas de Unity

Proyecto para Unity **2022.3.62f3**. La actividad actual está en la rama `Practica-Modulo-11`.

## Módulo 11 · Estructuras de datos

Abre `Assets/Modulo 11/Modulo11.unity`, pulsa **Play** y filtra la consola por `[M11`. El componente **Ejercicios Estructuras** ejecuta las cuatro funciones al iniciar la escena.

1. `GenerarListaAleatoria(6, 1, 21)` devuelve seis enteros entre 1 y 20. El límite inferior es inclusivo y el superior es exclusivo.
2. `OrdenarDescendente` devuelve una copia ordenada de mayor a menor y conserva los valores repetidos. El ejemplo produce `[9, 4, 4, 0, -2]`.
3. `QuitarDuplicados<T>` devuelve un `HashSet<T>`. El ejemplo contiene los tres valores distintos `Unity`, `C#` y `Datos`. El conjunto admite enteros o cadenas y su orden de enumeración no está garantizado.
4. `ImprimirPilaYCola` usa `Push` para reconstruir una copia de la pila. Imprime de la cima a la base con `Peek` y `Pop`, y encola cada elemento con `Enqueue`. Luego imprime la cola con `Dequeue`. Ambos recorridos del ejemplo muestran `Tercero`, `Segundo` y `Primero`.

## Módulo 9: ciclos y arreglos

Abre `Assets/Modulo 9/Modulo9.unity`, pulsa **Play** y filtra la consola por `[M9`. El objeto **Ejercicios de ciclos y arreglos** tiene el componente que ejecuta los tres ejercicios al iniciar la escena.

1. Dos arreglos de seis enteros se llenan con `Random.Range(1, 21)`. El tercero suma los valores del mismo índice y la consola muestra los tres arreglos.
2. Un ciclo `foreach` une las palabras del arreglo e imprime «Estoy aprendiendo ciclos y arreglos en Unity.» en un solo mensaje.
3. Una matriz de 2 × 3 multiplica al vector columna `[2, 3, 4]` y produce `[20, 47]`. El vector tiene una entrada por cada columna y el resultado tiene una entrada por cada fila.

## Módulo 8: variables numéricas y cadenas

Abre `Assets/Modulo 8/Modulo8.unity`, pulsa **Play** y filtra la consola por `[M8`. Los mensajes numerados corresponden a los ejercicios de las dos partes. Las primeras muestras de `Update` y `FixedUpdate` aparecen en la consola, y los valores siguen cambiando en el Inspector.

Selecciona **Cubo de paridad** para ver el componente **Ejercicios Variables Mod 8**. El cubo alterna entre magenta (impar) y cian (par). Cambia **Palabra Color** a `rojo`, `verde` o `azul` durante Play para cambiar el segundo cubo. Escribe tu nombre en **Nombre Completo** antes de pulsar Play para ver su separación con `Substring` y `Split`.

## Módulo 7: variables booleanas, AND y OR

Abre `Assets/Modulo 7/Modulo7.unity` y pulsa **Play**. Hay cinco objetos en fila: A, B, `A && B`, `A || B` y `GO3 && GO4`. Cada uno se muestra blanco si su valor es `true`, negro si es `false`.

Los dos primeros invierten su booleano en cada `FixedUpdate`. El **Modo lento** permite observar los cambios. Pulsa **Pausar** y cambia las casillas **A = true** y **B = true** para probar las cuatro combinaciones. Los tres resultados se recalculan al cambiar una entrada.

Los scripts están en `Assets/Modulo 7/Script` y los cinco prefabs en `Assets/Modulo 7/Prefab`. Son adaptaciones independientes de la práctica anterior: las tres líneas de color aleatorio están comentadas en las copias del módulo 7. Los módulos 5 y 6 siguen disponibles en sus escenas.

La [guía del módulo 7](Docs/Modulo7.md) relaciona cada paso con su implementación e incluye la tabla de verdad. Para repetir la verificación utiliza **Practicas > Modulo 7 > Comprobar en Play** con la escena guardada y Play detenido.

![Cinco objetos del módulo 7, sus valores y los controles de prueba](Docs/Modulo7Play.png)

## Módulo 6: colores y creación de cubos

Abre `Assets/Módulo 6/Modulo6.unity` y pulsa **Play**. Los scripts están en `Assets/Módulo 6/Script` y los tres prefabs en `Assets/Módulo 6/Prefab`.

| Objeto | Script | Comportamiento |
| --- | --- | --- |
| Cube1 | FristGO | Color aleatorio en `Awake`, una vez por instancia. |
| Sphere1 | SecondGO | Color aleatorio en cada `Update`. |
| Capsule1 | ThirthGO | Color aleatorio en cada `FixedUpdate`. |
| Generador_Awake | CrearCuboAwake | Construye un cubo al cargarse. |
| Generador_Update | CrearCubosUpdate | Construye un cubo por fotograma hasta el máximo de 20, ajustable en el Inspector. |
| Generador_OnEnable_OnDisable | CrearCuboActivacion | Construye un cubo cada vez que se activa o desactiva durante Play. |

Los cubos de la segunda parte se construyen con una malla de ocho vértices y doce triángulos, MeshRenderer y BoxCollider. En la primera parte se usa `Random.value`: en Unity es una propiedad, se escribe en minúscula y sin paréntesis.

Para probar la activación, selecciona **Generador_OnEnable_OnDisable** durante Play y desmarca y marca su casilla de activo. Los cubos quedan en **Cubos generados** y siguen visibles. Al detener Play no se generan cubos adicionales.

La escena incluye etiquetas para distinguir cada ejercicio. La guía completa y los pasos de entrega están en [Docs/Modulo6.md](Docs/Modulo6.md). El menú **Practicas > Modulo 6 > Comprobar en Play** permite repetir las comprobaciones de comportamiento.

![Escena del módulo 6 durante la comprobación de ambas partes](Docs/Modulo6Play.png)

## Módulo 5: Hola Mundo

Proyecto de Unity 2022.3.62f3 para practicar el ciclo de vida de un componente y los mensajes de la consola.

## Ejecución

1. Abre el proyecto con Unity 2022.3.62f3.
2. Abre `Assets/Scenes/SampleScene.unity`.
3. Selecciona **Main Camera**: tiene añadido el componente **Hola Mundo** (`Assets/Scripts/HolaMundo.cs`).
4. Abre **Window > General > Console**, activa los filtros de mensajes, advertencias y errores y pulsa **Play**.

| Función | Cuándo se ejecuta | Mensaje de consola |
| --- | --- | --- |
| `Awake` | Al cargar la instancia del componente, antes de `Start`. | `Debug.Log` |
| `Start` | Una vez, antes del primer `Update` del componente. | `Debug.LogWarning` |
| `Update` | En cada fotograma mientras el componente está activo. | `Debug.LogError` en su primera ejecución. |

El mensaje rojo es intencionado y forma parte del ejercicio: no es una excepción ni un error de compilación. Un booleano permite que `Update` siga ejecutándose cada fotograma y que su mensaje aparezca solo una vez por ejecución. Si **Error Pause** está activado, Unity puede pausar al mostrarlo; puedes continuar o detener Play.

El archivo y la clase se llaman `HolaMundo`, sin espacio, para que Unity pueda reconocer y añadir el componente. El Inspector lo muestra como **Hola Mundo**.

## Entrega

La práctica se prepara en la rama `Practica-Modulo-5`, con un Pull Request hacia `main`. El enlace del repositorio y el del Pull Request permiten revisar el código, la escena y los cambios.
