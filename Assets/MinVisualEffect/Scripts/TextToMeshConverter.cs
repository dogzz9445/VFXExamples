using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

[ExecuteInEditMode]
public class TextToMeshConverter : MonoBehaviour
{
    [SerializeField] 
    public List<TMP_MeshInfo> _meshInfos = new List<TMP_MeshInfo>();

    private void OnEnable()
    {
        //var textPro = GetComponent<TMP_Text>();
        //var mesh = textPro.mesh;
        //textPro.OnPreRenderText += info =>
        //{
        //    _meshInfos.Clear();
        //    _meshInfos.AddRange((TMP_MeshInfo[])info.meshInfo.Clone());
        //};
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConvertTextToMesh()
    {
        //var textmesh = GetComponent<TextMesh>();
        //var meshrenderer = GetComponent<MeshRenderer>();
        ////var material = meshrenderer.sharedMaterial;
        //var mesh = meshrenderer.additionalVertexStreams;
        //Debug.Log($"{_meshInfos.Count}");
        //foreach (var meshInfo in _meshInfos)
        //{
        //    var mesh = meshInfo.mesh;
        //    MeshRenderer
        //    SaveMesh(mesh, "test", true, true);
        //    //Generate(Path.Combine(Application.persistentDataPath, "test"));
        //}
        var textPro = GetComponent<TMP_Text>();
        var mesh = textPro.mesh;
        SaveMesh(mesh, "test", true, true);
        //Generate(Path.Combine(Application.persistentDataPath, "test"));
    }

    void Generate(string prefabPath)
    {
        string modelTemplate = "Assets/Templates/Model.prefab";
        // clone the model template
        Object templatePrefab = AssetDatabase.LoadAssetAtPath(modelTemplate, typeof(GameObject));
        GameObject template = (GameObject)EditorUtility.InstantiatePrefab(templatePrefab);
        // this way links will persist when we regenerate the mesh
        Object prefab = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
        if (!prefab)
        {
            prefab = EditorUtility.CreateEmptyPrefab(prefabPath);
        }

        var textPro = GetComponent<TMP_Text>();
        var mesh = textPro.mesh;
        // sort of the same...
        //Mesh mesh = (Mesh)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(Mesh));
        if (!mesh)
        {
            mesh = new Mesh();
            mesh.name = name;
            AssetDatabase.AddObjectToAsset(mesh, prefabPath);
        }
        else
        {
            mesh.Clear();
        }

        // generate your mesh in place
        //BlaBlaBla(mesh);
        // assume that MeshFilter is already there. could check and AddComponent
        template.GetComponent<MeshFilter>().sharedMesh = mesh;
        // make sure 
        EditorUtility.ReplacePrefab(template, prefab, ReplacePrefabOptions.ReplaceNameBased);
        // get rid of the temporary object (otherwise it stays over in scene)
        Object.DestroyImmediate(template);
    }
    //[MenuItem("CONTEXT/MeshFilter/Save Mesh...")]
    //public static void SaveMeshInPlace(MenuCommand menuCommand)
    //{
    //    MeshFilter mf = menuCommand.context as MeshFilter;
    //    Mesh m = mf.sharedMesh;
    //    SaveMesh(m, m.name, false, true);
    //}

    //[MenuItem("CONTEXT/MeshFilter/Save Mesh As New Instance...")]
    //public static void SaveMeshNewInstanceItem(MenuCommand menuCommand)
    //{
    //    MeshFilter mf = menuCommand.context as MeshFilter;
    //    Mesh m = mf.sharedMesh;
    //    SaveMesh(m, m.name, true, true);
    //}

    public static void SaveMesh(Mesh mesh, string name, bool makeNewInstance, bool optimizeMesh)
    {
        string path = EditorUtility.SaveFilePanel("Save Separate Mesh Asset", "Assets/", name, "asset");
        if (string.IsNullOrEmpty(path)) return;

        path = FileUtil.GetProjectRelativePath(path);

        Mesh meshToSave = (makeNewInstance) ? Object.Instantiate(mesh) as Mesh : mesh;
        //Mesh meshToSave = mesh;

        if (optimizeMesh)
            MeshUtility.Optimize(meshToSave);

        AssetDatabase.CreateAsset(meshToSave, path);
        AssetDatabase.SaveAssets();
    }
}
