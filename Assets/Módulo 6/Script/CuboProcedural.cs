using UnityEngine;

// Construye el cubo con vertices y triangulos, como en HCube del modulo anterior.
public class CuboProcedural : MonoBehaviour
{
    private Mesh mallaPropia;
    private Material materialPropio;

    public static GameObject Crear(string nombre, Vector3 posicion, Material material, Transform contenedor)
    {
        Mesh malla = new Mesh { name = "MallaCuboProcedural" };
        malla.vertices = new[]
        {
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f)
        };
        malla.triangles = new[]
        {
            0, 2, 1, 0, 3, 2,
            2, 3, 4, 2, 4, 5,
            1, 2, 5, 1, 5, 6,
            0, 7, 4, 0, 4, 3,
            5, 4, 7, 5, 7, 6,
            0, 6, 7, 0, 1, 6
        };
        malla.RecalculateNormals();
        malla.RecalculateBounds();

        GameObject cubo = new GameObject(nombre);
        cubo.transform.SetParent(contenedor, false);
        cubo.transform.position = posicion;
        cubo.AddComponent<MeshFilter>().sharedMesh = malla;
        MeshRenderer renderer = cubo.AddComponent<MeshRenderer>();
        cubo.AddComponent<BoxCollider>();
        CuboProcedural recursos = cubo.AddComponent<CuboProcedural>();
        recursos.mallaPropia = malla;
        if (material == null)
        {
            recursos.materialPropio = new Material(Shader.Find("Standard"));
            material = recursos.materialPropio;
        }
        renderer.sharedMaterial = material;
        return cubo;
    }

    private void OnDestroy()
    {
        if (mallaPropia != null)
            Destroy(mallaPropia);
        if (materialPropio != null)
            Destroy(materialPropio);
    }
}
