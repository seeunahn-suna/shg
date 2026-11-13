using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public static class Modulo6Checks
{
    private const string RunKey = "shg.Modulo6Checks.Running";
    private static readonly List<string> results = new List<string>();
    private static readonly List<string> errors = new List<string>();
    private static double started;
    private static int phase;
    private static MeshRenderer awake;
    private static MeshRenderer update;
    private static MeshRenderer fixedUpdate;
    private static CrearCuboActivacion activation;
    private static Color awakeColor;
    private static Color updateColor;
    private static Color fixedColor;
    private static int frame;
    private static float originalTimeScale;

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        if (!SessionState.GetBool(RunKey, false))
            return;
        started = EditorApplication.timeSinceStartup;
        originalTimeScale = Time.timeScale;
        Application.logMessageReceived += OnLog;
        EditorApplication.update += Tick;
    }

    [MenuItem("Practicas/Modulo 6/Comprobar en Play")]
    public static void Run()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (EditorApplication.isPlayingOrWillChangePlaymode || scene.isDirty || scene.path != Modulo6SceneSetup.ScenePath)
            throw new InvalidOperationException("Open and save Modulo6.unity, then stop Play before checking.");
        SessionState.SetBool(RunKey, true);
        EditorApplication.isPlaying = true;
    }

    private static void Tick()
    {
        if (!EditorApplication.isPlaying)
            return;
        try
        {
            if (phase == 3)
            {
                if (EditorApplication.timeSinceStartup - started >= 0.25)
                    Finish(null);
                return;
            }
            if (EditorApplication.timeSinceStartup - started > 30)
                throw new InvalidOperationException("The check timed out.");
            if (phase == 0)
            {
                FristGO first = Object.FindFirstObjectByType<FristGO>();
                SecondGO second = Object.FindFirstObjectByType<SecondGO>();
                ThirthGO third = Object.FindFirstObjectByType<ThirthGO>();
                if (first == null || second == null || third == null || Time.frameCount < 2)
                    return;
                awake = first.GetComponent<MeshRenderer>();
                update = second.GetComponent<MeshRenderer>();
                fixedUpdate = third.GetComponent<MeshRenderer>();
                Check(first.GetComponent<BoxCollider>() != null, "The cube has a BoxCollider.");
                Check(second.GetComponent<SphereCollider>() != null, "The sphere has a SphereCollider.");
                Check(third.GetComponent<CapsuleCollider>() != null, "The capsule has a CapsuleCollider.");
                foreach (MeshRenderer renderer in new[] { awake, update, fixedUpdate })
                    Check(renderer.GetComponent<MeshFilter>().sharedMesh != null && renderer.material != null,
                        renderer.name + " has its actual shape and a visible material.");
                awakeColor = awake.material.color;
                updateColor = update.material.color;
                fixedColor = fixedUpdate.material.color;
                activation = Object.FindFirstObjectByType<CrearCuboActivacion>();
                Check(Count("Cubo_Awake") == 1, "Awake creates exactly one procedural cube.");
                Check(Count("Cubo_OnEnable_") == 1 && Count("Cubo_OnDisable_") == 0,
                    "OnEnable creates the initial cube; OnDisable has not fired yet.");
                phase = 1;
                started = EditorApplication.timeSinceStartup;
                return;
            }
            if (EditorApplication.timeSinceStartup - started < 0.25)
                return;
            if (phase == 1)
            {
                Check(awake.material.color == awakeColor, "The Awake color remains unchanged across later frames.");
                Check(update.material.color != updateColor, "Update changes the sphere color across rendered frames.");
                Check(fixedUpdate.material.color != fixedColor, "FixedUpdate changes the capsule color across physics steps.");
                Material baseMaterial = AssetDatabase.LoadAssetAtPath<Material>(Modulo6SceneSetup.Root + "/Prefab/Material_Base.mat");
                Check(baseMaterial.color == Color.white && new[] { awake, update, fixedUpdate }.All(renderer => renderer.material != baseMaterial),
                    "Each object has its own runtime material and leaves the shared prefab material intact.");
                Time.timeScale = 0f;
                updateColor = update.material.color;
                fixedColor = fixedUpdate.material.color;
                frame = Time.frameCount;
                phase = 2;
                started = EditorApplication.timeSinceStartup;
                return;
            }
            if (Time.frameCount - frame < 25)
                return;
            Check(update.material.color != updateColor, "Update continues changing color while timeScale is zero.");
            Check(fixedUpdate.material.color == fixedColor, "FixedUpdate color stays still when physics steps are paused.");
            Check(awake.material.color == awakeColor, "The Awake color still remains unchanged.");
            CrearCubosUpdate generator = Object.FindFirstObjectByType<CrearCubosUpdate>();
            Check(Count("Cubo_Update_") == generator.maximoCubos, "Update creates its configured twenty cubes and respects the cap.");

            activation.enabled = false;
            Check(Count("Cubo_OnDisable_") == 1, "Disabling the component creates one visible cube.");
            activation.enabled = true;
            Check(Count("Cubo_OnEnable_") == 2, "Re-enabling the component creates another cube.");
            activation.gameObject.SetActive(false);
            Check(Count("Cubo_OnDisable_") == 2, "Deactivating the GameObject also creates a visible cube outside that object.");
            activation.gameObject.SetActive(true);
            Check(Count("Cubo_OnEnable_") == 3, "Reactivating the GameObject creates another cube.");
            Check(Count("Cubo_Awake") == 1, "The Awake generator has still created only one cube.");

            foreach (CuboProcedural cube in Object.FindObjectsByType<CuboProcedural>(FindObjectsSortMode.None))
            {
                Mesh mesh = cube.GetComponent<MeshFilter>().sharedMesh;
                Check(mesh.vertexCount == 8 && mesh.triangles.Length == 36 && cube.GetComponent<BoxCollider>() != null &&
                    cube.GetComponent<MeshRenderer>().sharedMaterial.shader.isSupported,
                    cube.name + " has its procedural mesh, visible material, and collider.");
            }
            Check(errors.Count == 0, "No runtime errors: " + string.Join("; ", errors));
            ScreenCapture.CaptureScreenshot("Temp/Modulo6Play.png");
            phase = 3;
            started = EditorApplication.timeSinceStartup;
        }
        catch (Exception exception) { Finish(exception); }
    }

    private static int Count(string prefix)
    {
        return Object.FindObjectsByType<CuboProcedural>(FindObjectsSortMode.None)
            .Count(cube => cube.name.StartsWith(prefix, StringComparison.Ordinal));
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
        File.WriteAllLines("Temp/Modulo6Checks.txt", results);
        SessionState.SetBool(RunKey, false);
        EditorApplication.update -= Tick;
        Application.logMessageReceived -= OnLog;
        Time.timeScale = originalTimeScale;
        if (exception == null)
            Debug.Log("Modulo 6 checks passed: " + results.Count + ". Report: Temp/Modulo6Checks.txt");
        else
            Debug.LogError(exception);
        EditorApplication.isPlaying = false;
    }
}
