using UnityEngine;

public class CrearCuboActivacion : MonoBehaviour
{
    public Material material;
    public Transform contenedor;

    private int activaciones;
    private int desactivaciones;
    private bool cerrandoAplicacion;

    private void OnEnable()
    {
        if (!Application.isPlaying)
            return;

        activaciones++;
        Vector3 posicion = transform.position + Vector3.forward * (activaciones - 1) * 1.1f;
        CuboProcedural.Crear("Cubo_OnEnable_" + activaciones, posicion, material, contenedor);
    }

    private void OnDisable()
    {
        // Al detener Play no creamos objetos nuevos durante la limpieza de la escena.
        if (!Application.isPlaying || cerrandoAplicacion)
            return;

        desactivaciones++;
        Vector3 posicion = transform.position + Vector3.right * 1.2f + Vector3.forward * (desactivaciones - 1) * 1.1f;
        CuboProcedural.Crear("Cubo_OnDisable_" + desactivaciones, posicion, material, contenedor);
    }

    private void OnApplicationQuit()
    {
        cerrandoAplicacion = true;
    }
}
