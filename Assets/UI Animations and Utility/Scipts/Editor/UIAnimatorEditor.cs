using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIAnimatrix;
using UnityEditor;

[CustomEditor(typeof(UIAnimator), true)]
public class DialoguePlayerEditor : Editor
{
    int selectedIndex = 0;
    /*public override void OnInspectorGUI()
    {
        UIAnimator dropdown = (UIAnimator)target;
        DrawDefaultInspector();

        // get the dialogue scriptable objects and show them in a dropdown
        GUIContent[] options = new GUIContent[InteractionManager.Instance.GetDialogueRepository().Count];
        for (int i = 0; i < InteractionManager.Instance.GetDialogueRepository().Count; i++)
        {
            options[i] = new GUIContent(InteractionManager.Instance.GetDialogueRepository()[i].name);
        }
        selectedIndex = EditorGUILayout.Popup(new GUIContent("Dialogue Container"), selectedIndex, options);
        dropdown.dialogueRepository = InteractionManager.Instance.GetDialogueRepository()[selectedIndex];
        // update the editor
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }*/
}
