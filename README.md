# shg — Práctica del módulo 5

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
