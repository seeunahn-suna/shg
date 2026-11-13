using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SecondGO : MonoBehaviour
{
    private Material materialDeInstancia;

    private void Awake()
    {
        materialDeInstancia = GetComponent<MeshRenderer>().material;
    }

    // Update cambia el color en cada fotograma.
    private void Update()
    {
        materialDeInstancia.color = new Color(Random.value, Random.value, Random.value);
    }

    private void OnDestroy()
    {
        if (materialDeInstancia != null)
            Destroy(materialDeInstancia);
    }
}
