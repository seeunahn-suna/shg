# Práctica del módulo 6

Versión de Unity: 2022.3.62f3. Rama de trabajo: `Practica-Modulo-6`.

## Primera parte

Se reutilizan tus tres prefabs y tus tres clases. Los nombres de los archivos de script se normalizaron para que coincidan con `FristGO`, `SecondGO` y `ThirthGO`, conservando sus archivos .meta y sus GUID.

La carpeta `Assets/Módulo 6` contiene:

- `Script`: los tres scripts de color y los scripts de creación de cubos.
- `Prefab`: Cube1, Sphere1 y Capsule1, además de los materiales de la actividad.
- `Modulo6.unity`: escena preparada para demostrar las dos partes.

Cada prefab tiene MeshFilter con su malla asignada y MeshRenderer con material. Cube1 usa BoxCollider, Sphere1 usa SphereCollider y Capsule1 usa CapsuleCollider. La escena contiene una instancia de cada prefab.

| Script | Función que cambia el color | Resultado |
| --- | --- | --- |
| FristGO | Awake | Un color aleatorio que permanece durante la ejecución. |
| SecondGO | Update | Un color nuevo por fotograma. |
| ThirthGO | FixedUpdate | Un color nuevo por paso fijo de simulación. |

Cada color se construye con `new Color(Random.value, Random.value, Random.value)`. `Random.value` es una propiedad de Unity, no una función llamada `Random.Value()`. Cada objeto obtiene su propio material de ejecución; cambiar un objeto no recolorea los otros ni modifica el material del prefab.

## Segunda parte

`CuboProcedural.Crear` construye la geometría con ocho vértices y 36 índices (doce triángulos), recalcula las normales y añade MeshFilter, MeshRenderer y BoxCollider. Los tres scripts invocan esa construcción desde la función solicitada:

1. `CrearCuboAwake`: crea `Cubo_Awake` una vez, en Awake.
2. `CrearCubosUpdate`: crea `Cubo_Update_1`, `Cubo_Update_2`, etc., uno en cada Update hasta el límite configurable de 20. El límite permite observar el ejemplo sin dejar un generador infinito en la escena.
3. `CrearCuboActivacion`: crea `Cubo_OnEnable_...` al activarse y `Cubo_OnDisable_...` al desactivarse. Se comprueba tanto al cambiar la casilla del componente como la del GameObject. Durante el cierre de Play no se crean objetos nuevos.

Los cubos se agrupan bajo `Cubos generados`, que permanece activo cuando desactivas el generador. Así puedes ver el resultado de OnDisable.

## Cómo demostrarlo

1. Abre `Assets/Módulo 6/Modulo6.unity`.
2. Pulsa Play y observa el cubo, la esfera y la cápsula de la fila posterior.
3. Comprueba que solo el cubo mantiene el mismo color. La esfera y la cápsula lo cambian continuamente.
4. En la fila delantera aparecen el cubo azul de Awake, los cubos naranjas de Update y un cubo verde de OnEnable.
5. Selecciona `Generador_OnEnable_OnDisable`. Desactívalo desde la casilla junto al nombre del GameObject: aparece otro cubo verde y los cubos anteriores siguen visibles. Actívalo: aparece un nuevo cubo.
6. También puedes desactivar y activar la casilla de su componente `Crear Cubo Activacion` para demostrar ambos callbacks.
7. Detén Play; los objetos de ejecución se limpian y la escena vuelve a su estado inicial.

La actividad del módulo 5 sigue en `Assets/Scenes/SampleScene.unity`. Sus scripts de ejemplos anteriores permanecen disponibles.

## Comprobación

Se comprobó la compilación y la ejecución de ambas partes. El menú `Practicas > Modulo 6 > Comprobar en Play` verifica además que Update continúa cambiando colores con timeScale=0 mientras FixedUpdate deja de cambiarlos, que los materiales son independientes, que los callbacks de activación crean los cubos esperados y que las mallas y colliders existen. El informe de la última ejecución queda en `Temp/Modulo6Checks.txt`.

## Entrega

El commit y el push se realizan sobre `Practica-Modulo-6`. Se abre un Pull Request hacia `main` para revisar la práctica antes de fusionarla. Comparte con tu tutor el enlace de la rama o el del Pull Request para que vea esta versión de la actividad.
