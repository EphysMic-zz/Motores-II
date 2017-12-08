using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
public class MyAssetDataBase : EditorWindow
{
    private Object _currentObject; //prefab
    private Object _currentFolder;
    private Vector2 _scrollPosLower;
    private Vector2 _scrollPos;
    private List<Object> _assets = new List<Object>(); //lista de assets en unity para el searcher
    private PowerConfigEditor pw;
    private string[] _folders;
    private string _folderName; //la nueva carpeta
    private string _NameCopy; //nombre del nuevo objeto
    private string _newName; //nombre del objeto
    private string _name;
    private string _currentName;
    private string _searchAssetByName; //para buscar
    private int selectedPw;
    private int ammount;
    private int ID;

    AnimBool _fade;

    private List<Object> _foldersT = new List<Object>();
    private string _searchFolderByName;


    [MenuItem("Window/Asset Data Base")]
    static void CreateWindow()
    {
        ((MyAssetDataBase)GetWindow(typeof(MyAssetDataBase))).Show();
    }

    private void OnEnable()
    {
        _fade = new AnimBool(false);
        _fade.valueChanged.AddListener(Repaint);
    }

    #region on gui
    void OnGUI()
    {

        maxSize = new Vector2(445, 500);
        minSize = new Vector2(445, 500);
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, "box", GUILayout.ExpandHeight(true));

        _currentObject = EditorGUILayout.ObjectField(_currentObject, typeof(Object), true);
        EditorGUILayout.Space();
        if (_currentObject != null)
        {
            if (AssetDatabase.Contains(_currentObject))
            {
                if (GUILayout.Button("Select other scriptable object"))
                    _currentObject = null;

                EditorGUILayout.Space();
                SOThings();
                //     Move();
                SearchFolders();
                EditorGUILayout.Space();
                FoldersThings();
            }
            else EditorGUILayout.HelpBox("This isn't a Scriptable Object", MessageType.Error);
        }
        else
        {
            EditorGUILayout.HelpBox("Select a Scriptable Object", MessageType.Info);

            if (GUILayout.Button("Create a scriptable object"))
                CreatePowerConfig();
            EditorGUILayout.Space();
            LowerArea();
        }
        EditorGUILayout.EndScrollView();
    }
    #endregion

    #region SOThings
    private void SOThings()
    {
        #region open asset
        if (GUILayout.Button("Open asset"))
            AssetDatabase.OpenAsset(_currentObject);

        EditorGUILayout.Space();
        #endregion

        #region Copy
        _NameCopy = EditorGUILayout.TextField("Name of the copy", _NameCopy);

        if (_NameCopy == null || _NameCopy == "")
            EditorGUILayout.HelpBox("Enter a name for the copy", MessageType.Info);

        if (GUILayout.Button("Copy the Scriptable Object"))
        {
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(_currentObject), "Assets/Scritable Objects/" + _NameCopy + ".prefab");
            _currentName = _NameCopy;
            AssetDatabase.Refresh();
        }
        if (_NameCopy == _currentName && _currentName != null && _currentName != "")
            EditorGUILayout.HelpBox("That name already exists", MessageType.Warning);

        EditorGUILayout.Space();
        #endregion

        #region new name
        _newName = EditorGUILayout.TextField("New name", _newName);
        if (_newName == null || _newName == "")
            EditorGUILayout.HelpBox("Enter a new name", MessageType.Info);

        if (GUILayout.Button("Rename"))
        {
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(_currentObject), _newName);
            _currentName = _newName;
        }
        if (_newName == _currentName && _currentName != null && _currentName != "")
            EditorGUILayout.HelpBox("That name already exists", MessageType.Warning);

        EditorGUILayout.Space();
        #endregion

        #region destroy
        if (GUILayout.Button("Destroy asset"))
            AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(_currentObject));
        EditorGUILayout.Space();
        #endregion
    }
    #endregion

    #region  Folders Things
    private void FoldersThings()
    {
        /*  #region Create folder
          _folderName = EditorGUILayout.TextField("Name of the new folder", _folderName);
          if (_folderName == null || _folderName == "")
          {
              EditorGUILayout.HelpBox("Enter a name for the folder", MessageType.Info);
          }
          if (GUILayout.Button("Create Folder"))
          {
              AssetDatabase.CreateFolder("Assets", _folderName);
          }
          EditorGUILayout.Space();
          #endregion*/
    }
    #endregion

    #region searcher
    private void Searcher()
    {
        EditorGUILayout.LabelField("Search asset", EditorStyles.boldLabel);
        var aux = _searchAssetByName;
        _searchAssetByName = EditorGUILayout.TextField(aux);
        int i;

        string[] all = AssetDatabase.FindAssets("t: ScriptableObject");
        _assets.Clear();
        for (i = 0; i < all.Length; i++)
        {
            all[i] = AssetDatabase.GUIDToAssetPath(all[i]);
            _assets.Add(AssetDatabase.LoadAssetAtPath(all[i], typeof(Object)));
        }



        if (_assets.Count <= 0)
            EditorGUILayout.HelpBox("There isn't scriptable object to select", MessageType.Warning);

        for (i = 0; i < _assets.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_assets[i].ToString());
            if (GUILayout.Button("Select"))
                _currentObject = _assets[i];
            if (GUILayout.Button("Delete"))
                AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(_assets[i]));
            EditorGUILayout.EndHorizontal();
            if (_searchAssetByName == "")
                _assets.Clear();
        }
    }
    #endregion

    #region Move
    private void Move()
    {

        string[] test = AssetDatabase.GetSubFolders("Assets");
        _folders = test;
        ID = EditorGUILayout.Popup("Select folder", ID, _folders);
        int i;
        if (GUILayout.Button("Move asset"))
        {
            for (i = 0; i < ID; i++)
            {
                AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(_currentObject), _folders[i] + _currentName + ".prefab");
                Debug.Log(test[i]);
            }
        }
        EditorGUILayout.Space();
    }
    #endregion

    public static void CreatePowerConfig()
    {
        ScriptableObjectUtility.CreateAsset<PowerConfig>();
    }

    void LowerArea()
    {

        EditorGUILayout.BeginVertical(GUILayout.Width(432));
        _scrollPosLower = EditorGUILayout.BeginScrollView(_scrollPosLower, "box", GUILayout.ExpandHeight(true));
        Searcher();

        for (int i = 0; i < ammount; i++)
        {
            EditorGUILayout.BeginHorizontal();
            selectedPw = i;
            Repaint();
            EditorGUILayout.EndHorizontal();
        }
        Repaint();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    #region test345
    void SearchFolders()
    {
        EditorGUILayout.LabelField("Search folder", EditorStyles.boldLabel);
        var aux = _searchFolderByName;
        _searchFolderByName = EditorGUILayout.TextField(aux);
        int i;
        _foldersT.Clear();
        string[] allF = AssetDatabase.GetSubFolders("t: Assets");

        for (i = 0; i < allF.Length; i++)
        {
            allF[i] = AssetDatabase.GUIDToAssetPath(allF[i]);
            _foldersT.Add(AssetDatabase.LoadAssetAtPath(allF[i], typeof(Object)));
        }

        if (_foldersT.Count <= 0)
            EditorGUILayout.HelpBox("There isn't folder to select", MessageType.Warning);

        /*  for (i = 0; i < _foldersT.Count; i++)
          {
              EditorGUILayout.BeginHorizontal();
              EditorGUILayout.LabelField(_foldersT[i].ToString());
              if (GUILayout.Button("eeeesto"))
                  _currentFolder = _foldersT[i];
              if (_searchFolderByName == "")
                  _foldersT.Clear();
          }*/
    }
    #endregion
}