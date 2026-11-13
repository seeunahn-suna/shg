using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public static class Modulo6SceneSetup
{
    public const string Root = "Assets/Módulo 6";
    public const string ScenePath = Root + "/Modulo6.unity";

    [MenuItem("Practicas/Modulo 6/Preparar escena")]
    public static void Prepare()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            throw new InvalidOperationException("Stop Play before preparing the practice.");
        if (SceneManager.GetActiveScene().isDirty)
            throw new InvalidOperationException("Save the current scene before preparing the practice.");
        NormalizeFolders();
        if (File.Exists(ScenePath))
        {
            EditorSceneManager.OpenScene(ScenePath);
            Debug.Log("Modulo 6: existing practice scene opened.");
            return;
        }

        EnsureFolder(Root + "/Prefab");
        Scene previous = SceneManager.GetActiveScene();
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        SceneManager.SetActiveScene(scene);
        Material baseMaterial = CreateMaterial("Base", Color.white);
        GameObject cube = ConfigurePrefab("Cube1", PrimitiveType.Cube, typeof(FristGO), baseMaterial);
        GameObject sphere = ConfigurePrefab("Sphere1", PrimitiveType.Sphere, typeof(SecondGO), baseMaterial);
        GameObject capsule = ConfigurePrefab("Capsule1", PrimitiveType.Capsule, typeof(ThirthGO), baseMaterial);

        Camera camera = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener)).GetComponent<Camera>();
        camera.tag = "MainCamera";
        camera.transform.position = new Vector3(0f, 9f, -12f);
        camera.transform.LookAt(new Vector3(0f, 0.5f, 1.7f));
        camera.fieldOfView = 50f;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.10f, 0.13f, 0.17f);

        Light light = new GameObject("Directional Light", typeof(Light)).GetComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.2f;
        light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.6f, 0.6f, 0.6f);

        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Suelo";
        floor.transform.position = new Vector3(0f, -0.15f, 1.8f);
        floor.transform.localScale = new Vector3(14f, 0.3f, 11f);
        floor.GetComponent<MeshRenderer>().sharedMaterial = CreateMaterial("Suelo", new Color(0.22f, 0.27f, 0.31f));

        InstantiateDemo(cube, new Vector3(-4f, 0.5f, 4f), scene);
        InstantiateDemo(sphere, new Vector3(0f, 0.5f, 4f), scene);
        InstantiateDemo(capsule, new Vector3(4f, 1f, 4f), scene);
        AddLabel("Awake", new Vector3(-4f, 1.8f, 4f), camera);
        AddLabel("Update", new Vector3(0f, 1.8f, 4f), camera);
        AddLabel("FixedUpdate", new Vector3(4f, 2.6f, 4f), camera);

        Transform container = new GameObject("Cubos generados").transform;
        CrearCuboAwake awake = new GameObject("Generador_Awake").AddComponent<CrearCuboAwake>();
        awake.transform.position = new Vector3(-5f, 0.5f, -1.5f);
        awake.material = CreateMaterial("CuboAwake", new Color(0.25f, 0.58f, 0.85f));
        awake.contenedor = container;

        CrearCubosUpdate update = new GameObject("Generador_Update").AddComponent<CrearCubosUpdate>();
        update.transform.position = new Vector3(-2.2f, 0.5f, -2f);
        update.material = CreateMaterial("CubosUpdate", new Color(0.90f, 0.58f, 0.20f));
        update.contenedor = container;
        update.maximoCubos = 20;

        CrearCuboActivacion activation = new GameObject("Generador_OnEnable_OnDisable").AddComponent<CrearCuboActivacion>();
        activation.transform.position = new Vector3(4f, 0.5f, -2f);
        activation.material = CreateMaterial("CubosActivacion", new Color(0.32f, 0.72f, 0.42f));
        activation.contenedor = container;
        AddLabel("Cubo en Awake", new Vector3(-5f, 0.3f, -3.3f), camera);
        AddLabel("Cubos en Update", new Vector3(0f, 0.3f, -3.3f), camera);
        AddLabel("OnEnable / OnDisable", new Vector3(4.6f, 0.3f, -3.3f), camera);

        AssetDatabase.SaveAssets();
        if (!EditorSceneManager.SaveScene(scene, ScenePath))
            throw new IOException("Could not save the module 6 scene.");
        EditorSceneManager.CloseScene(previous, true);
        AssetDatabase.Refresh();
        Selection.activeGameObject = activation.gameObject;
        Debug.Log("Modulo 6 ready: three configured prefabs, three color callbacks, and all cube creation callbacks.");
    }

    private static GameObject ConfigurePrefab(string name, PrimitiveType primitive, Type script, Material material)
    {
        string path = Root + "/Prefab/" + name + ".prefab";
        GameObject root = PrefabUtility.LoadPrefabContents(path);
        GameObject reference = GameObject.CreatePrimitive(primitive);
        try
        {
            root.GetComponent<MeshFilter>().sharedMesh = reference.GetComponent<MeshFilter>().sharedMesh;
            root.GetComponent<MeshRenderer>().sharedMaterial = material;
            foreach (Collider collider in root.GetComponents<Collider>())
                Object.DestroyImmediate(collider);
            if (primitive == PrimitiveType.Cube)
                root.AddComponent<BoxCollider>();
            else if (primitive == PrimitiveType.Sphere)
                root.AddComponent<SphereCollider>();
            else
                root.AddComponent<CapsuleCollider>();
            if (root.GetComponent(script) == null)
                root.AddComponent(script);
            root.transform.localPosition = Vector3.zero;
            root.transform.localRotation = Quaternion.identity;
            root.transform.localScale = Vector3.one;
            return PrefabUtility.SaveAsPrefabAsset(root, path);
        }
        finally
        {
            Object.DestroyImmediate(reference);
            PrefabUtility.UnloadPrefabContents(root);
        }
    }

    private static void InstantiateDemo(GameObject prefab, Vector3 position, Scene scene)
    {
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, scene);
        instance.transform.position = position;
        PrefabUtility.RecordPrefabInstancePropertyModifications(instance.transform);
    }

    private static void AddLabel(string text, Vector3 position, Camera camera)
    {
        TextMesh label = new GameObject("Etiqueta_" + text, typeof(TextMesh)).GetComponent<TextMesh>();
        label.text = text;
        label.fontSize = 48;
        label.characterSize = 0.08f;
        label.anchor = TextAnchor.MiddleCenter;
        label.alignment = TextAlignment.Center;
        label.color = Color.white;
        label.transform.position = position;
        label.transform.rotation = camera.transform.rotation;
    }

    private static Material CreateMaterial(string name, Color color)
    {
        string path = Root + "/Prefab/Material_" + name + ".mat";
        Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (material == null)
        {
            material = new Material(Shader.Find("Standard")) { name = "Modulo6_" + name };
            AssetDatabase.CreateAsset(material, path);
        }
        material.color = color;
        material.SetFloat("_Glossiness", 0.15f);
        EditorUtility.SetDirty(material);
        return material;
    }

    // Conserva los GUID al organizar las carpetas creadas inicialmente para esta practica.
    private static void NormalizeFolders()
    {
        string[,] moves =
        {
            { "Assets/Modulo 6", Root },
            { Root + "/Scripts", Root + "/Script" },
            { Root + "/prefabs", Root + "/Prefab" }
        };
        for (int i = 0; i < moves.GetLength(0); i++)
        {
            if (!AssetDatabase.IsValidFolder(moves[i, 0]) || AssetDatabase.IsValidFolder(moves[i, 1]))
                continue;
            string error = AssetDatabase.MoveAsset(moves[i, 0], moves[i, 1]);
            if (!string.IsNullOrEmpty(error))
                throw new IOException(error);
        }
    }

    private static void EnsureFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path))
            return;
        string parent = Path.GetDirectoryName(path).Replace('\\', '/');
        EnsureFolder(parent);
        AssetDatabase.CreateFolder(parent, Path.GetFileName(path));
    }
}
