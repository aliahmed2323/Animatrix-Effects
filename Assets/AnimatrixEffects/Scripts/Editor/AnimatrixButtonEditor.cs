using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

namespace UIAnimatrix
{

    [CustomEditor(typeof(AnimatrixButton))]
    public class AnimatrixButtonEditor : UnityEditor.UI.ButtonEditor
    {


        public override void OnInspectorGUI()
        {

            AnimatrixButton component = (AnimatrixButton)target;


            component.ShouldPlayEffect = (bool)EditorGUILayout.Toggle("Use Animatrix", component.ShouldPlayEffect);
            if (EditorGUILayout.BeginFadeGroup(component.ShouldPlayEffect ? 1 : 0))
            {
                component._effectType = (AnimatrixButton.EffectTypes)EditorGUILayout.EnumPopup("Effect Type", component._effectType);
                component.punch = (Vector2)EditorGUILayout.Vector2Field("Punch Effect Magnitude", component.punch);
                component.duration = (float)EditorGUILayout.FloatField("Effect Duration", component.duration);
                GUI.enabled = false;
                component.isPlayingEffect = (bool)EditorGUILayout.Toggle("Is the effect playing?", component.isPlayingEffect);
                GUI.enabled = true;
            }
            EditorGUILayout.EndFadeGroup();
            EditorGUILayout.Space();
            base.OnInspectorGUI();

        }
    }
}

