using UnityEngine;

public class CrearCuboAwake : MonoBehaviour
{
    public Material material;
    public Transform contenedor;

    // Una instancia de este componente crea un cubo al cargarse.
    private void Awake()
    {
        CuboProcedural.Crear("Cubo_Awake", transform.position, material, contenedor);
    }
}
