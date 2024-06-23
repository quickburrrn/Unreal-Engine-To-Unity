using UnityEngine;
using UnityEditor;

public class SelectObjectWithMeshRenderer : Editor
{
    //gives a name Ctrl+Alt+Shift+M
    [MenuItem("Tools/Select All Object with MeshRenderer %#&m")]
    static void SelectAllObjectWithMeshRenderer(){
        
        //finner alle med Mesh Renderer
        MeshRenderer[] meshRenderers = GameObject.FindObjectsOfType<MeshRenderer>();

        //list til alle objektene
        GameObject[] objectsWithMeshRenderer = new GameObject[meshRenderers.Length];

        for (int i = 0; i < meshRenderers.Length; i++){
            objectsWithMeshRenderer[i] = meshRenderers[i].gameObject;
        }

        Selection.objects = objectsWithMeshRenderer;
    }
}
