using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Modulo7;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public static class Modulo7Checks
{
    private const string RunKey = "shg.Modulo7Checks.Running";
    private static readonly List<string> results = new List<string>();
    private static readonly List<string> errors = new List<string>();
    private static PanelBooleanos panel;
    private static double started;
    private static double fixedStart;
    private static bool aStart;
    private static bool bStart;
    private static int phase;
    private static float previousScale;

    [MenuItem("Practicas/Modulo 7/Comprobar en Play")]
    public static void Run()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (EditorApplication.isPlayingOrWillChangePlaymode || scene.isDirty || scene.path != Modulo7SceneSetup.ScenePath)
            throw new InvalidOperationException("Open and save Modulo7.unity, then stop Play before checking.");
        SessionState.SetBool(RunKey, true);
        EditorApplication.isPlaying = true;
    }

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        if (!SessionState.GetBool(RunKey, false))
            return;
        results.Clear();
        errors.Clear();
        phase = 0;
        started = EditorApplication.timeSinceStartup;
        Application.logMessageReceived += OnLog;
        EditorApplication.update += Tick;
    }

    private static void Tick()
    {
        if (!EditorApplication.isPlaying)
            return;
        try
        {
            if (EditorApplication.timeSinceStartup - started > 30)
                throw new InvalidOperationException("Module 7 check timed out.");
            if (phase == 0)
            {
                panel = Object.FindFirstObjectByType<PanelBooleanos>();
                if (panel == null || Time.frameCount < 3)
                    return;
                previousScale = Time.timeScale;
                ObjetoBooleano[] objects = Objects();
                Check(Object.FindObjectsByType<ObjetoBooleano>(FindObjectsSortMode.None).Length == 5, "Exactly five boolean objects in the scene.");
                Check(objects.Select(o => o.GetType()).Distinct().Count() == 5, "Five attached scripts, including separate AND and OR scripts.");
                Check(panel.tercero.primero == panel.primero && panel.tercero.segundo == panel.segundo &&
                    panel.cuarto.primero == panel.primero && panel.cuarto.segundo == panel.segundo &&
                    panel.quinto.tercero == panel.tercero && panel.quinto.cuarto == panel.cuarto,
                    "All operator references point to the intended scene objects.");
                for (int i = 0; i < objects.Length; i++)
                {
                    Check(objects[i].GetComponent<MeshFilter>().sharedMesh != null && objects[i].GetComponent<Collider>() != null,
                        objects[i].name + " has its shape and collider.");
                    if (i > 0)
                        Check(objects[i].transform.position.x > objects[i - 1].transform.position.x &&
                            objects[i].transform.position.z == objects[0].transform.position.z,
                            objects[i].name + " is next to the previous object in the same row.");
                }
                fixedStart = Time.fixedTimeAsDouble;
                aStart = panel.primero.valor;
                bStart = panel.segundo.valor;
                Time.timeScale = 1;
                phase = 1;
                return;
            }
            if (phase == 1)
            {
                int steps = (int)Math.Round((Time.fixedTimeAsDouble - fixedStart) / Time.fixedDeltaTime);
                if (steps < 5)
                    return;
                Check(panel.primero.valor == (aStart ^ (steps % 2 == 1)), "A toggles once for every actual FixedUpdate.");
                Check(panel.segundo.valor == (bStart ^ (steps % 2 == 1)), "B toggles once for every actual FixedUpdate.");
                CheckOperators("Actual physics tick");
                Time.timeScale = 0;
                for (int a = 0; a < 2; a++)
                    for (int b = 0; b < 2; b++)
                    {
                        panel.primero.valor = a == 1;
                        panel.segundo.valor = b == 1;
                        panel.Recalcular();
                        CheckOperators("Truth table A=" + a + ", B=" + b);
                    }
                Material shared = AssetDatabase.LoadAssetAtPath<Material>(Modulo7SceneSetup.Root + "/Prefab/Material_Booleano.mat");
                Material[] materials = Objects().Select(o => o.GetComponent<MeshRenderer>().material).ToArray();
                Check(shared.color == Color.white && materials.Distinct().Count() == 5 && materials.All(m => m != shared),
                    "Five independent runtime materials; the shared material stays white.");
                // Paused render frames must not toggle either input.
                aStart = panel.primero.valor;
                bStart = panel.segundo.valor;
                fixedStart = Time.fixedTimeAsDouble;
                phase = 2;
                started = EditorApplication.timeSinceStartup;
                return;
            }
            if (phase == 2 && EditorApplication.timeSinceStartup - started > 0.3)
            {
                Check(Time.fixedTimeAsDouble == fixedStart && panel.primero.valor == aStart && panel.segundo.valor == bStart,
                    "No input toggles across rendered frames when physics is paused.");
                panel.primero.valor = false;
                panel.segundo.valor = true;
                panel.Recalcular();
                CheckOperators("Screenshot state");
                ScreenCapture.CaptureScreenshot("Temp/Modulo7Play.png");
                phase = 3;
                started = EditorApplication.timeSinceStartup;
                return;
            }
            if (phase == 3 && EditorApplication.timeSinceStartup - started > 0.5)
            {
                Check(errors.Count == 0, "No runtime errors: " + string.Join("; ", errors));
                Finish(null);
            }
        }
        catch (Exception exception) { Finish(exception); }
    }

    private static ObjetoBooleano[] Objects()
    {
        return new ObjetoBooleano[] { panel.primero, panel.segundo, panel.tercero, panel.cuarto, panel.quinto };
    }

    private static void CheckOperators(string context)
    {
        bool a = panel.primero.valor;
        bool b = panel.segundo.valor;
        Check(panel.tercero.valor == (a && b), context + ": AND reads both current inputs.");
        Check(panel.cuarto.valor == (a || b), context + ": OR reads both current inputs.");
        Check(panel.quinto.valor == (panel.tercero.valor && panel.cuarto.valor), context + ": fifth GO combines AND/OR results with AND.");
        foreach (ObjetoBooleano item in Objects())
            Check(item.GetComponent<MeshRenderer>().material.color == (item.valor ? Color.white : Color.black),
                context + ": " + item.name + " shows the correct white/black color.");
    }

    private static void Check(bool condition, string description)
    {
        if (!condition)
            throw new InvalidOperationException(description);
        results.Add("PASS: " + description);
    }

    private static void OnLog(string message, string stack, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
            errors.Add(message);
    }

    private static void Finish(Exception exception)
    {
        if (exception != null)
            results.Add("FAIL: " + exception);
        Directory.CreateDirectory("Temp");
        File.WriteAllLines("Temp/Modulo7Checks.txt", results);
        SessionState.SetBool(RunKey, false);
        EditorApplication.update -= Tick;
        Application.logMessageReceived -= OnLog;
        Time.timeScale = previousScale;
        if (exception == null)
            Debug.Log("Modulo 7 checks passed: " + results.Count + ". Report: Temp/Modulo7Checks.txt");
        else
            Debug.LogError(exception);
        EditorApplication.isPlaying = false;
    }
}
