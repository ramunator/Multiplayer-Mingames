using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ControllerUI))]
public class ControllerUIButtonEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ControllerUI button = (ControllerUI)target;

        if(GUILayout.Button("Get Row"))
        {
            button.GetControllerUI(1.5f);
        }
    }
}
