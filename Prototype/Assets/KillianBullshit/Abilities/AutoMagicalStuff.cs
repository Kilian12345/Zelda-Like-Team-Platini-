using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AutoMagicalStuff
{
    [MenuItem("Assets/Create/Custom/Feedbacks")]
    public static void CreateControllerConfig()
    {
        Feedbacks dataHolder = ScriptableObject.CreateInstance<Feedbacks>();
        AssetDatabase.CreateAsset(dataHolder, "Assets/Feedbacks/Abilities/Feedbacks.asset");
        AssetDatabase.SaveAssets();
    }


    [MenuItem("CoolStuff/Browse scene and do things")]
    public static void FindAndReplaceStuffEverywhere()
    {
        GameObject go = Selection.activeGameObject;

        go.transform.localScale = Vector3.one * 3;

        EditorUtility.SetDirty(go.transform);


    }
}
