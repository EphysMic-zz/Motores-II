using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class PowerDatabaseEditor : EditorWindow
{
    //NO SE QUE TAN VILLA ES ESTO
    private enum State
    {
        empty,
        edit,
        add
    }
    private State _state;   //ok respira      

    public int ammmount;
    public int selectedPower;
    public PowerConfigEditor _pw;
    private PowerConfigEditor _currentPw;
    private Vector2 _scrollPos;

    #region varibales
    private string newName;
    private float newDamage;
    private float newCriticalHit;
    private float newCoolDown;
    private float newCostoDeUso;
    private float newRangoDeAlcance;
    #endregion

    public static void ShowWindow()
    {
        PowerDatabaseEditor window = GetWindow<PowerDatabaseEditor>();
        window.minSize = new Vector2(800, 400);
        window.Show();
    }

    void OnEnable()
    {
        if (_currentPw == null)
            LoadDatabase();
        _state = State.empty; //tiene un estado vacio por lo tanto...ABAJO 
    }

    void OnGUI()
    {
      /*  if (GUILayout.Button("Create Scriptable")) //acomodar esto porque ta feo asi todo grande
        {
            ammmount++;
        }

        #region info
        EditorGUILayout.LabelField("Ubicación: " + AssetDatabase.GetAssetPath(_currentPw));
        EditorGUILayout.Space();
        #endregion*/

        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        LeftArea();
        RightArea();
        EditorGUILayout.EndHorizontal();
    }

    void LoadDatabase()
    {
        //buscar otra forma de llamarlo  VER ESTO ESTO ESTO ESTO ESTO
        _currentPw = (PowerConfigEditor)AssetDatabase.LoadAssetAtPath("PowerConfig :t ScriptableObject" + ammmount, typeof(PowerConfigEditor));

        if (_currentPw == null)
            CreateDatabase();
    }

    void CreateDatabase()
    {
        _currentPw = ScriptableObject.CreateInstance<PowerConfigEditor>();
        //   AssetDatabase.CreateAsset(_currentPw, "PowerConfig" + ammmount);// No se como ponerlo :c
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void LeftArea()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(250));
        EditorGUILayout.Space();

        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, "", GUILayout.ExpandHeight(true));

        for (int i = 0; i < _currentPw.count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Delete", GUILayout.Width(50)))
            {
                //   AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(pw));
                _state = State.empty;
                _pw.RemoveAt(i);
                _pw.Orden();
                EditorUtility.SetDirty(_pw);
            }

            if (GUILayout.Button(_pw.Pw(i).nameOfPower, "", GUILayout.ExpandWidth(true)))
            {
                ammmount = i;
                selectedPower = i;
                _state = State.edit;
            }
            GUILayout.Label("Aca irian los scriptable objects creados para poder editarlos por separado");

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Pw: " + ammmount, GUILayout.Width(100));


        //ACA ENUM
        if (GUILayout.Button("New Power"))
            _state = State.add;

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
    }

    void RightArea()
    {
        EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
        GUILayout.Label("Aca va para poder editar el scriptable");

        switch (_state)
        {
            case State.add:
                AddArea();
                break;
            case State.edit:
                EditArea();
                break;
            default:
                AreaEmpty();
                break;
        }

        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
    }

    public static void CreatePowerConfig()
    {
        ScriptableObjectUtility.CreateAsset<PowerConfig>();
    }

    #region estados

    void AreaEmpty()
    {
        EditorGUILayout.LabelField(
           "oki, ta vacio esto",
            GUILayout.ExpandHeight(true));
    }

    void EditArea()
    {
        _pw.Pw(selectedPower).nameOfPower = EditorGUILayout.TextField(new GUIContent("Name: "), _pw.Pw(selectedPower).nameOfPower);
        _pw.Pw(selectedPower).damage = EditorGUILayout.FloatField(new GUIContent("Damage: "), _pw.Pw(selectedPower).damage);
        _pw.Pw(selectedPower).criticalHit = EditorGUILayout.FloatField(new GUIContent("Critical: "), _pw.Pw(selectedPower).criticalHit);
        _pw.Pw(selectedPower).coolDown = EditorGUILayout.FloatField(new GUIContent("Cooldown: "), _pw.Pw(selectedPower).coolDown);
        _pw.Pw(selectedPower).costoDeUso = EditorGUILayout.FloatField(new GUIContent("Costo: "), _pw.Pw(selectedPower).costoDeUso);
        _pw.Pw(selectedPower).rangoDeAlcance = EditorGUILayout.FloatField(new GUIContent("Rango: "), _pw.Pw(selectedPower).rangoDeAlcance);

        EditorGUILayout.Space();

        if (GUILayout.Button("modify", GUILayout.Width(100)))
        {
            _currentPw.Orden();
            EditorUtility.SetDirty(_pw);
        }
    }

    void AddArea()
    {
        newName = EditorGUILayout.TextField(new GUIContent("Name: "), newName);
        newDamage = EditorGUILayout.FloatField(new GUIContent("Damage: "), newDamage);
        newCriticalHit = EditorGUILayout.FloatField(new GUIContent("Critical: "), newCriticalHit);
        newCoolDown = EditorGUILayout.FloatField(new GUIContent("Cooldown: "), newCoolDown);
        newCostoDeUso = EditorGUILayout.FloatField(new GUIContent("Costo: "), newCostoDeUso);
        newRangoDeAlcance = EditorGUILayout.FloatField(new GUIContent("Rango: "), newRangoDeAlcance);


        if (GUILayout.Button("Done", GUILayout.Width(100)))
        {
            CreatePowerConfig();

            _pw.Add(new PowerConfig(newName, newDamage, newCriticalHit, newCoolDown, newCostoDeUso, newRangoDeAlcance));
            _pw.Orden();

            newName = "";
            newDamage = 0;
            newCriticalHit = 0;
            newCoolDown = 0;
            newCostoDeUso = 0;
            newRangoDeAlcance = 0;
            EditorUtility.SetDirty(_pw);
            _state = State.empty;
        }
    }
    #endregion
}