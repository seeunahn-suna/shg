using UnityEngine;

namespace Modulo7
{
    [DefaultExecutionOrder(0)]
    public class QuintoBooleano : ObjetoBooleano
    {
        public TercerBooleanoAND tercero;
        public CuartoBooleanoOR cuarto;

        private void Start() { Evaluar(); }
        private void FixedUpdate() { Evaluar(); }

        public void Evaluar()
        {
            if (tercero == null || cuarto == null)
                return;
            // Paso 9: se elige AND para combinar los resultados de los GO 3 y 4.
            valor = tercero.valor && cuarto.valor;
            ActualizarColor();
        }
    }
}
