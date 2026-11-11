using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EjerciciosVariablesMod8 : MonoBehaviour
{
    [SerializeField] private Renderer cuboPalabra;
    [SerializeField] private string palabraColor = "azul";
    [SerializeField] private string nombreCompleto = "Ana Sofia Torres";

    [SerializeField] private int fotogramas;
    [SerializeField] private int pasosFisica;
    [SerializeField] private float acumulado = 1f;

    private const float Factor = 1.001f;
    private static readonly int PropiedadColor = Shader.PropertyToID("_Color");
    private Renderer cuboParidad;
    private MaterialPropertyBlock propiedades;
    private string ultimaPalabraColor;

    private void Awake()
    {
        cuboParidad = GetComponent<MeshRenderer>();
        propiedades = new MaterialPropertyBlock();
        fotogramas = 0;
        pasosFisica = 0;
        acumulado = 1f;
    }

    private void Start()
    {
        AplicarColorPorPalabra();
        MostrarNumeros();
        SepararNombre();
        MostrarOperacionesConCadenas();
    }

    private void Update()
    {
        fotogramas++;
        if (fotogramas == 1)
            Debug.Log($"[M8 1.1] Update {fotogramas}", this);

        if (palabraColor != ultimaPalabraColor)
            AplicarColorPorPalabra();
    }

    private void FixedUpdate()
    {
        pasosFisica++;
        acumulado *= Factor;

        bool esPar = pasosFisica % 2 == 0;
        Pintar(cuboParidad, esPar ? Color.cyan : Color.magenta);

        // Las dos primeras muestras muestran un paso impar y uno par en la consola.
        if (pasosFisica <= 2)
        {
            string valor = acumulado.ToString("F6", CultureInfo.InvariantCulture);
            Debug.Log($"[M8 1.1] FixedUpdate {pasosFisica} acumulado {valor}", this);
            string paridad = esPar ? "par (cian)" : "impar (magenta)";
            Debug.Log($"[M8 1.3] {pasosFisica} es {paridad}", this);
        }
    }

    private void MostrarNumeros()
    {
        float primero = 7.5f;
        float segundo = 2.25f;
        float suma = primero + segundo;
        int entero = (int)suma;
        Debug.Log($"[M8 1.2] {suma.ToString(CultureInfo.InvariantCulture)} convertido a int da {entero}", this);

        float numero = 3.14159265f;
        string cuatroDecimales = numero.ToString("F4", CultureInfo.InvariantCulture);
        Debug.Log($"[M8 1.5] Cuatro decimales: {cuatroDecimales}", this);
    }

    private void AplicarColorPorPalabra()
    {
        ultimaPalabraColor = palabraColor;
        string palabra = (palabraColor ?? string.Empty).Trim().ToLowerInvariant();
        Color color;
        switch (palabra)
        {
            case "rojo":
                color = Color.red;
                break;
            case "verde":
                color = Color.green;
                break;
            case "azul":
                color = Color.blue;
                break;
            default:
                color = Color.white;
                break;
        }

        Pintar(cuboPalabra, color);
        Debug.Log($"[M8 1.4] Palabra de color: {palabra}", this);
    }

    private void SepararNombre()
    {
        var partes = new List<string>((nombreCompleto ?? string.Empty)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries));
        Debug.Log($"[M8 1.6] Split: {string.Join(" | ", partes)}", this);

        if (partes.Count < 3)
        {
            Debug.LogWarning("[M8 1.6] Escribe al menos tres partes en Nombre Completo.", this);
            return;
        }

        string nombre = string.Join(" ", partes);
        int primerEspacio = nombre.IndexOf(' ');
        int segundoEspacio = nombre.IndexOf(' ', primerEspacio + 1);
        string primeraParte = nombre.Substring(0, primerEspacio);
        string segundaParte = nombre.Substring(primerEspacio + 1, segundoEspacio - primerEspacio - 1);
        string terceraParte = nombre.Substring(segundoEspacio + 1);
        Debug.Log($"[M8 1.6] Substring: {primeraParte} | {segundaParte} | {terceraParte}", this);
    }

    private void MostrarOperacionesConCadenas()
    {
        string textoA = "1200";
        string textoB = "3400";
        if (int.TryParse(textoA, out int numeroA) && int.TryParse(textoB, out int numeroB))
            Debug.Log($"[M8 2.1] TryParse: {numeroA} + {numeroB} = {numeroA + numeroB}", this);
        else
            Debug.LogWarning("[M8 2.1] No se pudieron convertir los dos números.", this);

        string oracion = "Hola Mundo";
        var caracteresPares = new StringBuilder();
        for (int indice = 0; indice < oracion.Length; indice++)
        {
            if (indice % 2 == 0)
                caracteresPares.Append(oracion[indice]);
        }
        Debug.Log($"[M8 2.2] Índices pares: \"{caracteresPares}\"", this);

        string otraOracion = "Hola Unity, hoy practico variables.";
        string recortada = otraOracion.Substring(5);
        Debug.Log($"[M8 2.3] Sin los primeros cinco caracteres: \"{recortada}\"", this);
    }

    private void Pintar(Renderer cubo, Color color)
    {
        if (cubo == null)
            return;

        cubo.GetPropertyBlock(propiedades);
        propiedades.SetColor(PropiedadColor, color);
        cubo.SetPropertyBlock(propiedades);
    }
}
