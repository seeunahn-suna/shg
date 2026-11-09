using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ThirthGO : MonoBehaviour
{
    private Material materialDeInstancia;

    private void Awake()
    {
        materialDeInstancia = GetComponent<MeshRenderer>().material;
    }

    // FixedUpdate cambia el color en cada paso fijo de la simulacion.
    private void FixedUpdate()
    {
        materialDeInstancia.color = new Color(Random.value, Random.value, Random.value);
    }

    private void OnDestroy()
    {
        if (materialDeInstancia != null)
            Destroy(materialDeInstancia);
    }
}
