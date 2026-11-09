using UnityEngine;

public class CrearCubosUpdate : MonoBehaviour
{
    public Material material;
    public Transform contenedor;
    [Min(1)] public int maximoCubos = 20;

    private int cubosCreados;

    // Crea un cubo por fotograma hasta el limite configurable del ejemplo.
    private void Update()
    {
        if (cubosCreados >= maximoCubos)
            return;

        int columna = cubosCreados % 5;
        int fila = cubosCreados / 5;
        Vector3 posicion = transform.position + new Vector3(columna * 1.1f, 0f, fila * 1.1f);
        cubosCreados++;
        CuboProcedural.Crear("Cubo_Update_" + cubosCreados, posicion, material, contenedor);
    }
}
