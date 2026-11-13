using System;
using System.Collections.Generic;
using UnityEngine;

public class EjerciciosEstructuras : MonoBehaviour
{
    private void Start()
    {
        List<int> aleatorios = GenerarListaAleatoria(6, 1, 21);
        Debug.Log($"[M11 1] Lista aleatoria de 6 enteros entre 1 y 20: [{string.Join(", ", aleatorios)}]", this);

        int[] entrada = { 4, -2, 9, 4, 0 };
        int[] ordenados = OrdenarDescendente(entrada);
        Debug.Log($"[M11 2] [{string.Join(", ", entrada)}] → [{string.Join(", ", ordenados)}]", this);

        var palabras = new List<string> { "Unity", "C#", "Unity", "Datos", "C#" };
        HashSet<string> sinDuplicados = QuitarDuplicados(palabras);
        Debug.Log($"[M11 3] [{string.Join(", ", palabras)}] → {{{string.Join(", ", sinDuplicados)}}}", this);

        var pila = new Stack<string>();
        pila.Push("Primero");
        pila.Push("Segundo");
        pila.Push("Tercero");
        ImprimirPilaYCola(pila);
    }

    // El límite inferior se incluye y el superior se excluye, como en Random.Range con enteros.
    public List<int> GenerarListaAleatoria(int tamaño, int rangoInferior, int rangoSuperior)
    {
        if (tamaño < 0)
            throw new ArgumentOutOfRangeException(nameof(tamaño), "El tamaño debe ser mayor o igual que cero.");
        if (rangoInferior >= rangoSuperior)
            throw new ArgumentOutOfRangeException(nameof(rangoSuperior), "El límite superior debe ser mayor que el inferior.");

        var numeros = new List<int>(tamaño);
        for (int i = 0; i < tamaño; i++)
            numeros.Add(UnityEngine.Random.Range(rangoInferior, rangoSuperior));

        return numeros;
    }

    public int[] OrdenarDescendente(int[] numeros)
    {
        if (numeros == null)
            throw new ArgumentNullException(nameof(numeros));

        var ordenados = (int[])numeros.Clone();
        Array.Sort(ordenados);
        Array.Reverse(ordenados);
        return ordenados;
    }

    public HashSet<T> QuitarDuplicados<T>(List<T> elementos)
    {
        return new HashSet<T>(elementos);
    }

    public void ImprimirPilaYCola(Stack<string> pila)
    {
        if (pila == null)
            throw new ArgumentNullException(nameof(pila));

        // ToArray empieza por la cima. Push recorre al revés para reconstruir el mismo orden.
        string[] elementos = pila.ToArray();
        var copia = new Stack<string>(elementos.Length);
        for (int i = elementos.Length - 1; i >= 0; i--)
            copia.Push(elementos[i]);

        var cola = new Queue<string>(elementos.Length);
        while (copia.Count > 0)
        {
            string elemento = copia.Peek();
            Debug.Log($"[M11 4] Pila: {elemento}", this);
            cola.Enqueue(elemento);
            copia.Pop();
        }

        while (cola.Count > 0)
            Debug.Log($"[M11 4] Cola: {cola.Dequeue()}", this);
    }
}
