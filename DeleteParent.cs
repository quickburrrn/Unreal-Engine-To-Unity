using UnityEngine;
using UnityEditor;

public class DeleteParent : Editor
{  
    //name      ctrl+shift+alt+u
    [MenuItem("Tools/Remove Crap %#&u")]
    static void DeleteSelectedParent()
    {
        GameObject [] selectedGameObjects = Selection.gameObjects;

        if (selectedGameObjects.Length == 0){
            Debug.LogWarning("No Gameobject selected.");
            return;
        }


        foreach (GameObject selectedObject in selectedGameObjects){
            Transform selectedTransform = selectedObject.transform;

            PrefabAssetType assetType = PrefabUtility.GetPrefabAssetType(selectedObject);

            if (assetType != PrefabAssetType.NotAPrefab) {
                PrefabUtility.UnpackPrefabInstance(selectedObject.gameObject, PrefabUnpackMode.Completely, InteractionMode.UserAction);
            }

            //check if object is useless
            if (!selectedObject.GetComponent<MeshRenderer>() && !selectedObject.GetComponent<LODGroup>()){
                Debug.Log("Selected object is useless it's removed");
                foreach (Transform child in selectedTransform){
                    child.SetParent(null);
                }
                DestroyImmediate(selectedObject.gameObject);
                continue;
            }

            //looks for UCX_ objects
            foreach (Transform child in selectedTransform){
                //checks of UCX_
                if (child.name.Length > 3)
                {
                    if (child.name.Substring(0,4) == "UCX_"){
                        DestroyImmediate(child.gameObject);
                    }
                }
            }

            //looks for useless objects
            foreach (Transform child in selectedTransform){
                //checls if object has any mesh related componetnts
                if (!child.gameObject.GetComponent<MeshRenderer>() && !child.gameObject.GetComponent<LODGroup>()){
                    Debug.Log(child.name + "is useless so it's removed");
                    
                    for (int i = child.childCount - 1; i >= 0 ; --i)
                    {
                        child.GetChild(i).SetParent(selectedObject.transform);
                    }
                    
                    DestroyImmediate(child.gameObject);
                }
            }
        }
    }
}
