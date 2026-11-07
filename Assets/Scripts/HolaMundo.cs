using UnityEngine;

public class HolaMundo : MonoBehaviour
{
    private bool mensajeDeUpdateMostrado;

    // Se ejecuta al cargar esta instancia, antes de Start.
    private void Awake()
    {
        Debug.Log("Hola Mundo - Awake: el componente se ha cargado.", this);
    }

    // Se ejecuta una vez antes del primer Update del componente.
    private void Start()
    {
        Debug.LogWarning("Hola Mundo - Start: advertencia de ejemplo al iniciar la escena.", this);
    }

    // Se ejecuta en cada fotograma mientras el componente esta activo.
    private void Update()
    {
        // Mostramos el error de ejemplo solo una vez para no llenar la consola.
        if (mensajeDeUpdateMostrado)
            return;

        Debug.LogError("Hola Mundo - Update: error intencionado para practicar Debug.LogError.", this);
        mensajeDeUpdateMostrado = true;
    }
}
