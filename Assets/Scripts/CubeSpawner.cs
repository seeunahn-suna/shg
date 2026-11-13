using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject PrefabCubo;
    public List<GameObject> ListaDeCubos;
    public float FactorDeEscalamiento;
    public int numCubos = 0; 

    // Start is called before the first frame update
    void Start()
    {
        ListaDeCubos = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 30 == 0) // cada ~0.5s
        {
            numCubos++;

            Vector3 randomPos = new Vector3(
                Random.Range(-10f, 10f),
                1f,
                Random.Range(-10f, 10f)
            );

            GameObject tempGameObject = Instantiate(PrefabCubo, randomPos, Quaternion.identity);
            Rigidbody rb = tempGameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(new Vector3(
                    Random.Range(-2f, 2f),
                    0f,
                    Random.Range(-2f, 2f)
                ), ForceMode.Impulse);

                rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
            }
            tempGameObject.name = "CuboNumero " + numCubos;
         
            ListaDeCubos.Add(tempGameObject);
        }
    }
}
