using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour
{
    public GameObject PrefabEsfera;
    public List<GameObject> ListaDeEsferas;
    public float factordeescala;
    public int numEsferas = 0;

    // Start is called before the first frame update
    void Start()
    {
        ListaDeEsferas = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        numEsferas++;

        Vector3 randomPos = Random.insideUnitSphere * 5f + new Vector3(0, 5f, 0);

        GameObject tempGameObject = Instantiate(PrefabEsfera, randomPos, Quaternion.identity);
        tempGameObject.name = "EsferaNumero " + numEsferas;

        Color c = new Color(Random.value, Random.value, Random.value);
        tempGameObject.GetComponent<Renderer>().material.color = c;

        ListaDeEsferas.Add(tempGameObject);
    }
}
