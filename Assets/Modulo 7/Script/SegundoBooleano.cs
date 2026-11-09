using UnityEngine;

namespace Modulo7
{
    // Adaptacion de SecondGO del modulo 6.
    [DefaultExecutionOrder(-200)]
    public class SegundoBooleano : ObjetoBooleano
    {
        // Color aleatorio anterior en Update, desactivado para el paso 3:
        // materialDeInstancia.color = new Color(Random.value, Random.value, Random.value);

        private void FixedUpdate()
        {
            valor = !valor;
            ActualizarColor();
        }
    }
}
