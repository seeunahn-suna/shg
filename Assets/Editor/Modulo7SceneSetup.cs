using System;
using System.IO;
using Modulo7;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public static class Modulo7SceneSetup
{
    public const string Root = "Assets/Modulo 7";
    public const string ScenePath = Root + "/Modulo7.unity";

    [MenuItem("Practicas/Modulo 7/Preparar escena")]
    public static void Prepare()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode || SceneManager.GetActiveScene().isDirty)
            throw new InvalidOperationException("Save the current scene and stop Play before preparing module 7.");
        if (File.Exists(ScenePath))
        {
            EditorSceneManager.OpenScene(ScenePath);
            return;
        }
        EnsureFolder(Root + "/Prefab");
        Scene previous = SceneManager.GetActiveScene();
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        SceneManager.SetActiveScene(scene);

        Camera camera = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener)).GetComponent<Camera>();
        camera.tag = "MainCamera";
        camera.transform.position = new Vector3(0, 5, -14);
        camera.transform.LookAt(new Vector3(0, 0.8f, 0));
        camera.orthographic = true;
        camera.orthographicSize = 3.4f;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.07f, 0.10f, 0.14f);
        Light light = new GameObject("Directional Light", typeof(Light)).GetComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.1f;
        light.transform.rotation = Quaternion.Euler(45, -30, 0);
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.6f, 0.6f, 0.6f);

        Material white = MaterialAsset("Booleano", Color.white);
        Material ground = MaterialAsset("Suelo", new Color(0.28f, 0.34f, 0.4f));
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Suelo";
        floor.transform.position = new Vector3(0, -0.2f, 0);
        floor.transform.localScale = new Vector3(17, 0.4f, 4);
        floor.GetComponent<MeshRenderer>().sharedMaterial = ground;

        GameObject first = CopyPrevious("Cube1", "GO1_A", typeof(PrimerBooleano), white);
        GameObject second = CopyPrevious("Sphere1", "GO2_B", typeof(SegundoBooleano), white);
        GameObject third = CopyPrevious("Capsule1", "GO3_AND", typeof(TercerBooleanoAND), white);
        GameObject fourth = Object.Instantiate(third);
        fourth.name = "GO4_OR";
        Object.DestroyImmediate(fourth.GetComponent<TercerBooleanoAND>());
        fourth.AddComponent<CuartoBooleanoOR>();
        GameObject fifth = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fifth.name = "GO5_AND_Resultados";
        fifth.GetComponent<MeshRenderer>().sharedMaterial = white;
        fifth.AddComponent<QuintoBooleano>();
        GameObject[] objects = { first, second, third, fourth, fifth };
        second.GetComponent<SegundoBooleano>().valor = true;

        // Guarda copias independientes: no modifica los prefabs ni los scripts del modulo 6.
        for (int i = 0; i < objects.Length; i++)
        {
            string path = Root + "/Prefab/" + objects[i].name + ".prefab";
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(objects[i], path);
            Object.DestroyImmediate(objects[i]);
            objects[i] = (GameObject)PrefabUtility.InstantiatePrefab(prefab, scene);
            objects[i].transform.position = new Vector3(-6 + i * 3, i == 2 || i == 3 ? 1 : 0.6f, 0);
            PrefabUtility.RecordPrefabInstancePropertyModifications(objects[i].transform);
        }

        PrimerBooleano a = objects[0].GetComponent<PrimerBooleano>();
        SegundoBooleano b = objects[1].GetComponent<SegundoBooleano>();
        TercerBooleanoAND and = objects[2].GetComponent<TercerBooleanoAND>();
        CuartoBooleanoOR or = objects[3].GetComponent<CuartoBooleanoOR>();
        QuintoBooleano result = objects[4].GetComponent<QuintoBooleano>();
        and.primero = a;
        and.segundo = b;
        or.primero = a;
        or.segundo = b;
        result.tercero = and;
        result.cuarto = or;
        PrefabUtility.RecordPrefabInstancePropertyModifications(and);
        PrefabUtility.RecordPrefabInstancePropertyModifications(or);
        PrefabUtility.RecordPrefabInstancePropertyModifications(result);

        PanelBooleanos panel = new GameObject("Controles de la practica").AddComponent<PanelBooleanos>();
        panel.primero = a;
        panel.segundo = b;
        panel.tercero = and;
        panel.cuarto = or;
        panel.quinto = result;
        AssetDatabase.SaveAssets();
        if (!EditorSceneManager.SaveScene(scene, ScenePath))
            throw new IOException("Could not save module 7.");
        EditorSceneManager.CloseScene(previous, true);
        Selection.activeGameObject = panel.gameObject;
        Debug.Log("Modulo 7 listo: cinco GO, valores booleanos, AND, OR y controles de prueba.");
    }

    private static GameObject CopyPrevious(string sourceName, string name, Type script, Material material)
    {
        string path = Modulo6SceneSetup.Root + "/Prefab/" + sourceName + ".prefab";
        GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (source == null)
            throw new FileNotFoundException("The module 6 prefab is missing.", path);
        GameObject copy = Object.Instantiate(source);
        copy.name = name;
        foreach (MonoBehaviour previous in copy.GetComponents<MonoBehaviour>())
            Object.DestroyImmediate(previous);
        copy.GetComponent<MeshRenderer>().sharedMaterial = material;
        copy.AddComponent(script);
        return copy;
    }

    private static Material MaterialAsset(string name, Color color)
    {
        string path = Root + "/Prefab/Material_" + name + ".mat";
        Material material = new Material(Shader.Find("Standard")) { name = "Modulo7_" + name, color = color };
        material.SetFloat("_Glossiness", 0f);
        AssetDatabase.CreateAsset(material, path);
        return material;
    }

    private static void EnsureFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path))
            return;
        EnsureFolder(Path.GetDirectoryName(path).Replace('\\', '/'));
        AssetDatabase.CreateFolder(Path.GetDirectoryName(path).Replace('\\', '/'), Path.GetFileName(path));
    }
}
