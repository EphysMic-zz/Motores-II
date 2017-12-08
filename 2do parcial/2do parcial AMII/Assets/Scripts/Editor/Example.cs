using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Example : EditorWindow {

    void OnGUI()
    {
        maxSize = new Vector2(200, 100);
        minSize = new Vector2(200, 100);
    }
}
