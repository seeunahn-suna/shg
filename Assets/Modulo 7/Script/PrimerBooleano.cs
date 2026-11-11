using UnityEngine;

namespace Modulo7
{
    // Adaptacion de FristGO del modulo 6. La escena anterior se conserva.
    [DefaultExecutionOrder(-200)]
    public class PrimerBooleano : ObjetoBooleano
    {
        // Color aleatorio anterior en Awake, desactivado para el paso 3:
        // materialDeInstancia.color = new Color(Random.value, Random.value, Random.value);

        private void FixedUpdate()
        {
            valor = !valor;
            ActualizarColor();
        }
    }
}
