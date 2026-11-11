using UnityEngine;

namespace Modulo7
{
    // Adaptacion de ThirthGO: ahora obtiene los valores de los otros dos GO.
    [DefaultExecutionOrder(-100)]
    public class TercerBooleanoAND : ObjetoBooleano
    {
        public PrimerBooleano primero;
        public SegundoBooleano segundo;

        // Color aleatorio anterior en FixedUpdate, desactivado para el paso 3:
        // materialDeInstancia.color = new Color(Random.value, Random.value, Random.value);

        private void Start() { Evaluar(); }
        private void FixedUpdate() { Evaluar(); }

        public void Evaluar()
        {
            if (primero == null || segundo == null)
                return;
            valor = primero.valor && segundo.valor;
            ActualizarColor();
        }
    }
}
