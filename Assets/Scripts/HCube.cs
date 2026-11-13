using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HCube : MonoBehaviour
{

    GameObject objToSpawn;
    Vector3[] vertices = {
        new Vector3 (0, 0, 0), //vertice0
        new Vector3 (1, 0, 0), //vertice1
        new Vector3 (1, 1, 0), //vertice2
        new Vector3 (0, 1, 0), //Vertice3
        new Vector3 (0, 1, 1), //vertice4
        new Vector3 (1, 1, 1), //vertice5
        new Vector3 (1, 0, 1), //vertice6
        new Vector3 (0, 0, 1), //vertice7
    };

    int[] triangulos = {
            0, 2, 1,  //Cara1
            0, 3, 2,
            2, 3, 4, //Cara2
            2, 4, 5, 
            1, 2, 5, //Cara3
            1, 5, 6, 
            0, 7, 4, //Cara4
            0, 4, 3, 
            5, 4, 7, //Cara5
            5, 7, 6,
            0, 6, 7, //Cara6
            0, 1, 6
            
    };
    // Start is called before the first frame update
    void Start()
    {
        objToSpawn = new GameObject("HCUBE");
        objToSpawn.AddComponent<MeshFilter>();
        var meshFiltrer = objToSpawn.GetComponent<MeshFilter>().mesh;
        meshFiltrer.Clear();
        meshFiltrer.vertices = vertices;
        meshFiltrer.triangles = triangulos;
        meshFiltrer.Optimize();
        meshFiltrer.RecalculateNormals();
        objToSpawn.AddComponent<BoxCollider>();
        var BoxCollider = objToSpawn.GetComponent<BoxCollider>();
        BoxCollider.center = new Vector3(0.5f, 0.5f, 0.5f);
        objToSpawn.transform.position = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
