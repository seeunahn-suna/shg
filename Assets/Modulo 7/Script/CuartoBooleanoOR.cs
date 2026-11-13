using UnityEngine;

namespace Modulo7
{
    // Copia de TercerBooleanoAND para el paso 8: cambia && por ||.
    [DefaultExecutionOrder(-100)]
    public class CuartoBooleanoOR : ObjetoBooleano
    {
        public PrimerBooleano primero;
        public SegundoBooleano segundo;

        private void Start() { Evaluar(); }
        private void FixedUpdate() { Evaluar(); }

        public void Evaluar()
        {
            if (primero == null || segundo == null)
                return;
            valor = primero.valor || segundo.valor;
            ActualizarColor();
        }
    }
}
