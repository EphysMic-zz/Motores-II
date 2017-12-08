using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Test : EditorWindow
{

    #region Scriptable Object 
    public static void CreatePowerConfig()
    {
        if (GUILayout.Button("So"))
            ScriptableObjectUtility.CreateAsset<PowerConfig>();
    }
    #endregion
}
