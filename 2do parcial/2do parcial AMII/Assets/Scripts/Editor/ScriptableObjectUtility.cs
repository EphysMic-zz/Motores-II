using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


public class ScriptableObjectUtility 
{
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        //Creamos el cosito
        T asset = ScriptableObject.CreateInstance<T>();

        //Ubico
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/" + typeof(T).ToString() + ".asset");

        //Creamos el cosito x2
        AssetDatabase.CreateAsset(asset, assetPathAndName);

        //guardo
        AssetDatabase.SaveAssets();

        //refresca por los cositos modificados
        AssetDatabase.Refresh();

        //ommmm
        EditorUtility.FocusProjectWindow();

        //Marcamos el cosito que creamos :3
        Selection.activeObject = asset;
    }
}
