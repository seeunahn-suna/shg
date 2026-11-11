using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FristGO : MonoBehaviour
{
    private Material materialDeInstancia;

    // Awake cambia el color una sola vez al cargar cada instancia.
    private void Awake()
    {
        materialDeInstancia = GetComponent<MeshRenderer>().material;
        materialDeInstancia.color = new Color(Random.value, Random.value, Random.value);
    }

    private void OnDestroy()
    {
        if (materialDeInstancia != null)
            Destroy(materialDeInstancia);
    }
}
