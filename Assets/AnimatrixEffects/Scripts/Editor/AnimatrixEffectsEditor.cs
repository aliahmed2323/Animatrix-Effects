using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEditorInternal;

namespace UIAnimatrix
{

    [CustomEditor(typeof(AnimatrixEffects))]
    public class AnimatrixEffectsEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            AnimatrixEffects component = (AnimatrixEffects)target;

            GUIStyle effectCategoryTitleStyle = new();
            effectCategoryTitleStyle.fontSize = 22;
            effectCategoryTitleStyle.alignment = TextAnchor.MiddleCenter;
            effectCategoryTitleStyle.normal.textColor = Color.white;
            effectCategoryTitleStyle.font = (Font)Resources.Load("Fonts/Kines");
            EditorGUILayout.LabelField("Effect Category", effectCategoryTitleStyle);

            EditorGUILayout.Space();

            component._animatrixEffect._effect = (AnimatrixEffects.Effect.Effects)EditorGUILayout.EnumPopup(component._animatrixEffect._effect);
            EditorGUILayout.Space();
            
            effectCategoryTitleStyle.fontSize = 16;
            EditorGUILayout.LabelField("Effect Properties", effectCategoryTitleStyle);

            EditorGUILayout.Space();

            switch (component._animatrixEffect._effect)
            {
                case AnimatrixEffects.Effect.Effects.Fade:
                    DrawFadeProperties(component);
                    break;
                case AnimatrixEffects.Effect.Effects.Scale:
                    DrawScaleProperties(component);
                        break;
                case AnimatrixEffects.Effect.Effects.Rotate:
                    DrawRotateProperties(component);
                    break;
                case AnimatrixEffects.Effect.Effects.Move:
                    DrawMoveProperties(component);
                    break;
            }
        }

        void DrawFadeProperties(AnimatrixEffects component)
        {
            component._animatrixEffect._fadeEffect._effectType = (AnimatrixEffects.FadeEffect.EffectTypes)EditorGUILayout.EnumPopup("Effect Type", component._animatrixEffect._fadeEffect._effectType);
            component._animatrixEffect._fadeEffect.endValue = (float)EditorGUILayout.FloatField("End Value", component._animatrixEffect._fadeEffect.endValue);
            component._animatrixEffect._fadeEffect.resetCanvasAlpha = (float)EditorGUILayout.FloatField("Reset Alpha Value", component._animatrixEffect._fadeEffect.resetCanvasAlpha);
            component._animatrixEffect._fadeEffect.duration = (float)EditorGUILayout.FloatField("Duration", component._animatrixEffect._fadeEffect.duration);
            component._animatrixEffect._fadeEffect.delay = (float)EditorGUILayout.FloatField("Delay", component._animatrixEffect._fadeEffect.delay);
        }
        void DrawScaleProperties(AnimatrixEffects component)
        {
            component._animatrixEffect._scaleEffect._effectType = (AnimatrixEffects.ScaleEffects.EffectTypes)EditorGUILayout.EnumPopup("Effect Type", component._animatrixEffect._scaleEffect._effectType);
            component._animatrixEffect._scaleEffect.endValue = EditorGUILayout.Vector3Field("End Value", component._animatrixEffect._scaleEffect.endValue);
            component._animatrixEffect._scaleEffect.resetValue = EditorGUILayout.Vector3Field("Reset Value", component._animatrixEffect._scaleEffect.resetValue);
            component._animatrixEffect._scaleEffect.duration = EditorGUILayout.FloatField("Duration", component._animatrixEffect._scaleEffect.duration);
            component._animatrixEffect._scaleEffect.delay = EditorGUILayout.FloatField("Delay", component._animatrixEffect._scaleEffect.delay);
            
            if(component._animatrixEffect._scaleEffect._effectType == AnimatrixEffects.ScaleEffects.EffectTypes.GroupScaleEffect)
            {
                component._animatrixEffect._scaleEffect.easingEffect = (DG.Tweening.Ease)EditorGUILayout.EnumPopup("Easing", component._animatrixEffect._scaleEffect.easingEffect);
            }
            if (component._animatrixEffect._scaleEffect._effectType == AnimatrixEffects.ScaleEffects.EffectTypes.GroupScaleWithIncrementEffect)
            {
                component._animatrixEffect._scaleEffect.easingEffect = (DG.Tweening.Ease)EditorGUILayout.EnumPopup("Easing", component._animatrixEffect._scaleEffect.easingEffect);
                component._animatrixEffect._scaleEffect.delayIncrement = EditorGUILayout.FloatField("Delay Increment", component._animatrixEffect._scaleEffect.delayIncrement);
            }
        }
        void DrawRotateProperties(AnimatrixEffects component)
        {
            component._animatrixEffect._rotateEffect._effectType = (AnimatrixEffects.RotateEffects.EffectTypes)EditorGUILayout.EnumPopup("Effect Type", component._animatrixEffect._rotateEffect._effectType);
            component._animatrixEffect._rotateEffect.endValue = EditorGUILayout.Vector3Field("End Value", component._animatrixEffect._rotateEffect.endValue);
            component._animatrixEffect._rotateEffect.duration = EditorGUILayout.FloatField("Duration", component._animatrixEffect._rotateEffect.duration);
            component._animatrixEffect._rotateEffect.delay = EditorGUILayout.FloatField("Delay", component._animatrixEffect._rotateEffect.delay);
            component._animatrixEffect._rotateEffect.loopType = (DG.Tweening.LoopType)EditorGUILayout.EnumPopup("Loop Type", component._animatrixEffect._rotateEffect.loopType);
            component._animatrixEffect._rotateEffect.loopCount = EditorGUILayout.IntField("Loop Count", component._animatrixEffect._rotateEffect.loopCount);
        }
        void DrawMoveProperties(AnimatrixEffects component)
        {
            component._animatrixEffect._moveEffect._effectType = (AnimatrixEffects.MoveEffects.EffectTypes)EditorGUILayout.EnumPopup("Effect Type", component._animatrixEffect._moveEffect._effectType);
            component._animatrixEffect._moveEffect.duration = EditorGUILayout.FloatField("Duration", component._animatrixEffect._moveEffect.duration);
            component._animatrixEffect._moveEffect.delay = EditorGUILayout.FloatField("Delay", component._animatrixEffect._moveEffect.delay);
            component._animatrixEffect._moveEffect.resetPosition = EditorGUILayout.Vector2Field("Reset Position", component._animatrixEffect._moveEffect.resetPosition);
           
            if(component._animatrixEffect._moveEffect._effectType == AnimatrixEffects.MoveEffects.EffectTypes.AnchorEasingMove)
            {
                component._animatrixEffect._moveEffect.easingEffect = (DG.Tweening.Ease)EditorGUILayout.EnumPopup("Easing", component._animatrixEffect._moveEffect.easingEffect);
            }

            if(component._animatrixEffect._moveEffect._effectType == AnimatrixEffects.MoveEffects.EffectTypes.AnchorGroupMove)
            {
                component._animatrixEffect._moveEffect.easingEffect = (DG.Tweening.Ease)EditorGUILayout.EnumPopup("Easing", component._animatrixEffect._moveEffect.easingEffect);
                component._animatrixEffect._moveEffect.delayIncrement = EditorGUILayout.FloatField("Delay Increment", component._animatrixEffect._moveEffect.delayIncrement);
               /* component._animatrixEffect._moveEffects.callBack = (UnityEngine.Events.UnityEvent)EditorGUILayout.ObjectField("Call Back", component._animatrixEffect._moveEffects.callBack, typeof(UnityEngine.Events.UnityEvent), true);*/
            }
        }
    }
}