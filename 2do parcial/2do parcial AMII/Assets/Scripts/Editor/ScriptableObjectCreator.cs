using UnityEngine;
using System.Collections;
using UnityEditor;
public class ScriptableObjectCreator : EditorWindow
{
    //ver si esto se puede poner en el asset data base
    public static void CreatePowerConfig()
    {
        ScriptableObjectUtility.CreateAsset<PowerConfig>();
    }

}
