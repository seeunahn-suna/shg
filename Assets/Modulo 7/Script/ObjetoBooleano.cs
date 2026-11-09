using UnityEngine;

namespace Modulo7
{
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class ObjetoBooleano : MonoBehaviour
    {
        [Tooltip("Verdadero = blanco; falso = negro.")]
        public bool valor;

        private Material materialDeInstancia;

        protected virtual void Awake()
        {
            materialDeInstancia = GetComponent<MeshRenderer>().material;
            ActualizarColor();
        }

        public void ActualizarColor()
        {
            if (materialDeInstancia != null)
                materialDeInstancia.color = valor ? Color.white : Color.black;
        }

        protected virtual void OnDestroy()
        {
            if (materialDeInstancia != null)
                Destroy(materialDeInstancia);
        }
    }
}
