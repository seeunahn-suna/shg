using UnityEngine;

public class EjerciciosDeCiclosYArreglos : MonoBehaviour
{
    private void Start()
    {
        SumarArreglos();
        ConcatenarOracion();
        MultiplicarMatrizPorVector();
    }

    private void SumarArreglos()
    {
        int[] primero = new int[6];
        int[] segundo = new int[primero.Length];
        int[] suma = new int[primero.Length];

        for (int i = 0; i < primero.Length; i++)
        {
            primero[i] = Random.Range(1, 21);
            segundo[i] = Random.Range(1, 21);
        }

        for (int i = 0; i < suma.Length; i++)
        {
            suma[i] = primero[i] + segundo[i];
        }

        Debug.Log($"[M9 1] Primer arreglo: [{string.Join(", ", primero)}]", this);
        Debug.Log($"[M9 1] Segundo arreglo: [{string.Join(", ", segundo)}]", this);
        Debug.Log($"[M9 1] Suma por índice: [{string.Join(", ", suma)}]", this);
    }

    private void ConcatenarOracion()
    {
        string[] palabras = { "Estoy", "aprendiendo", "ciclos", "y", "arreglos", "en", "Unity." };
        string oracion = "";

        foreach (string palabra in palabras)
        {
            if (oracion.Length > 0)
                oracion += " ";
            oracion += palabra;
        }

        Debug.Log($"[M9 2] {oracion}", this);
    }

    private void MultiplicarMatrizPorVector()
    {
        int[,] matriz =
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };
        int[] vector = { 2, 3, 4 };
        int[] resultado = new int[matriz.GetLength(0)];

        // Cada fila produce un valor. El vector tiene una entrada por columna.
        for (int fila = 0; fila < matriz.GetLength(0); fila++)
        {
            for (int columna = 0; columna < matriz.GetLength(1); columna++)
            {
                resultado[fila] += matriz[fila, columna] * vector[columna];
            }
        }

        Debug.Log($"[M9 3] Matriz 2x3 por vector [{string.Join(", ", vector)}]: [{string.Join(", ", resultado)}]", this);
    }
}
