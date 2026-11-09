using UnityEngine;

namespace Modulo7
{
    // Controles de demostracion: los operadores permanecen en sus propios scripts.
    public class PanelBooleanos : MonoBehaviour
    {
        public PrimerBooleano primero;
        public SegundoBooleano segundo;
        public TercerBooleanoAND tercero;
        public CuartoBooleanoOR cuarto;
        public QuintoBooleano quinto;

        private bool pausado;
        private bool lento = true;
        private float escalaAnterior;
        private GUIStyle titulo;
        private GUIStyle texto;

        private void OnEnable()
        {
            escalaAnterior = Time.timeScale;
            AplicarVelocidad();
        }

        private void OnDisable() { Time.timeScale = escalaAnterior; }

        private void AplicarVelocidad()
        {
            // No cambia fixedDeltaTime ni los ajustes guardados del proyecto.
            Time.timeScale = pausado ? 0f : (lento ? 0.02f : 1f);
        }

        public void Recalcular()
        {
            primero.ActualizarColor();
            segundo.ActualizarColor();
            tercero.Evaluar();
            cuarto.Evaluar();
            quinto.Evaluar();
        }

        private void OnGUI()
        {
            if (primero == null || segundo == null || tercero == null || cuarto == null || quinto == null)
                return;
            if (titulo == null)
            {
                titulo = new GUIStyle(GUI.skin.label) { fontSize = 24, alignment = TextAnchor.MiddleCenter };
                texto = new GUIStyle(GUI.skin.label) { fontSize = 16, alignment = TextAnchor.MiddleCenter };
            }
            Matrix4x4 anterior = GUI.matrix;
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 1100f, Screen.height / 500f, 1f));
            GUI.Label(new Rect(0, 8, 1100, 34), "MODULO 7  |  VARIABLES BOOLEANAS", titulo);
            GUI.Label(new Rect(0, 43, 1100, 25), "Verdadero = blanco     |     Falso = negro", texto);

            string[] nombres = { "1. A", "2. B", "3. A && B", "4. A || B", "5. GO3 && GO4" };
            bool[] valores = { primero.valor, segundo.valor, tercero.valor, cuarto.valor, quinto.valor };
            Transform[] objetos = { primero.transform, segundo.transform, tercero.transform, cuarto.transform, quinto.transform };
            for (int i = 0; i < valores.Length; i++)
            {
                float centro = Camera.main.WorldToScreenPoint(objetos[i].position).x * 1100f / Screen.width;
                GUI.Label(new Rect(centro - 100, 316, 200, 58), nombres[i] + "\n" + (valores[i] ? "true / blanco" : "false / negro"), texto);
            }

            GUI.Box(new Rect(15, 388, 1070, 99), GUIContent.none);
            if (GUI.Button(new Rect(32, 405, 140, 30), pausado ? "Reanudar" : "Pausar"))
            {
                pausado = !pausado;
                AplicarVelocidad();
            }
            bool nuevoLento = GUI.Toggle(new Rect(194, 411, 240, 25), lento, "Modo lento (1 paso/segundo)");
            if (nuevoLento != lento)
            {
                lento = nuevoLento;
                AplicarVelocidad();
            }
            GUI.Label(new Rect(28, 447, 1044, 25), "Cada FixedUpdate invierte A y B. Pausa para probar las cuatro combinaciones manualmente.", texto);
            bool guiAnterior = GUI.enabled;
            GUI.enabled = pausado;
            bool a = GUI.Toggle(new Rect(510, 411, 115, 25), primero.valor, "A = true");
            bool b = GUI.Toggle(new Rect(652, 411, 115, 25), segundo.valor, "B = true");
            if (pausado && (a != primero.valor || b != segundo.valor))
            {
                primero.valor = a;
                segundo.valor = b;
                Recalcular();
            }
            GUI.enabled = guiAnterior;
            GUI.matrix = anterior;
        }
    }
}
