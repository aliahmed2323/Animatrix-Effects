using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace UIAnimatrix
{
    public class AnimatrixEffects : MonoBehaviour
    {
        public Effect _animatrixEffect;

        public Dictionary<KeyValuePair<Effect.Effects, string>, Action> EffectsRegistry = new();

        Action OnEnableEffectPlay;
        Action OnDisableEffectReset;

        public Tweener tweener;
        public bool isPlayingEffect;

        [System.Serializable]
        public struct Effect
        {
            public enum Effects { Rotate, Scale, Move, Fade }

            public Effects _effect;
            public FadeEffect _fadeEffect;
            public ScaleEffects _scaleEffect;
            public RotateEffects _rotateEffect;
            public MoveEffects _moveEffect;
        }

        [System.Serializable]
        public struct FadeEffect
        {
            public EffectTypes _effectType;
            public float endValue;
            public float duration;
            public float delay;
            public float resetCanvasAlpha;

            [System.Serializable]
            public enum EffectTypes
            {
                SimpleFade,
            }
        }

        [System.Serializable]
        public struct ScaleEffects
        {
            public EffectTypes _effectType;
            public Vector3 endValue;
            public Vector3 resetValue;
            public float duration;
            public float delay;
            public Ease easingEffect;
            public float delayIncrement;
            public float vibrato;
            public float elasticity;

            [System.Serializable]
            public enum EffectTypes
            {
                PunchScaleEffect,
                SimpleScaleEffect,
                GroupScaleEffect,
                GroupScaleWithIncrementEffect
            }
        }

        [System.Serializable]
        public struct RotateEffects
        {
            public EffectTypes _effectType;
            public Vector3 endValue;
            public float duration;
            public float delay;
            public int loopCount;
            public LoopType loopType;

            [System.Serializable]
            public enum EffectTypes
            {
                RotateEffect,
            }
        }

        [System.Serializable]
        public struct MoveEffects
        {
            public EffectTypes _effectType;
            public float duration;
            public float delay;
            public Vector2 resetPosition;
            public Ease easingEffect;
            public float delayIncrement;
            public UnityEngine.Events.UnityEvent callBack;
            [System.Serializable]
            public enum EffectTypes
            {
                AnchorSimpleMove,
                AnchorEasingMove,
                AnchorGroupMove
            }
        }

        private void Start()
        {
            InitializeEffectsRegistry();
            InitializeEffectActions();
        }

        private void OnEnable()
        {
            OnEnableEffectPlay?.Invoke();
        }

        private void OnDisable()
        {
            OnDisableEffectReset?.Invoke();
        }

        void InitializeEffectActions()
        {
            if (isPlayingEffect) return;
            isPlayingEffect = true;

            string effectType = "";

            switch(_animatrixEffect._effect)
            {
                case Effect.Effects.Rotate:
                    effectType = _animatrixEffect._rotateEffect._effectType.ToString();
                    OnDisableEffectReset = ResetRotateEffect;
                        break;
                case Effect.Effects.Scale:
                    effectType = _animatrixEffect._scaleEffect._effectType.ToString();
                    OnDisableEffectReset = ResetScaleEffect;
                    break;
                case Effect.Effects.Move:
                    effectType = _animatrixEffect._moveEffect._effectType.ToString();
                    OnDisableEffectReset = ResetMoveEffect;
                    break;
                case Effect.Effects.Fade:
                    effectType = _animatrixEffect._fadeEffect._effectType.ToString();
                    OnDisableEffectReset = ResetFadeEffect;
                    break;
            }

            OnEnableEffectPlay = EffectsRegistry[new KeyValuePair<Effect.Effects, string>(_animatrixEffect._effect, effectType)];

            OnEnableEffectPlay.Invoke();
        }

        void InitializeEffectsRegistry()
        {
            EffectsRegistry.Add(new KeyValuePair < Effect.Effects, string >(Effect.Effects.Fade, "SimpleFade") , SimpleFade);
            EffectsRegistry.Add(new KeyValuePair < Effect.Effects, string >(Effect.Effects.Scale, "PunchScaleEffect") , PunchScaleEffect);
            EffectsRegistry.Add(new KeyValuePair < Effect.Effects, string >(Effect.Effects.Scale, "SimpleScaleEffect") , SimpleScaleEffect);
            EffectsRegistry.Add(new KeyValuePair < Effect.Effects, string >(Effect.Effects.Scale, "GroupScaleEffect") , GroupScaleEffect);
            EffectsRegistry.Add(new KeyValuePair < Effect.Effects, string >(Effect.Effects.Scale, "GroupScaleWithIncrementEffect") , GroupScaleWithIncrementedDelay);
            EffectsRegistry.Add(new KeyValuePair < Effect.Effects, string >(Effect.Effects.Rotate, "RotateEffect") , RotateEffect);
            EffectsRegistry.Add(new KeyValuePair < Effect.Effects, string >(Effect.Effects.Move, "AnchorSimpleMove") , AnchorSimpleMove);
            EffectsRegistry.Add(new KeyValuePair < Effect.Effects, string >(Effect.Effects.Move, "AnchorEasingMove") , AnchorEasingMove);
            EffectsRegistry.Add(new KeyValuePair < Effect.Effects, string >(Effect.Effects.Move, "AnchorGroupMove") , AnchorGroupMove);
        }

        #region Fade Effects

        void SimpleFade()
        {
            if (gameObject.GetComponent<CanvasGroup>() == null) gameObject.AddComponent<CanvasGroup>();
                tweener = gameObject.GetComponent<CanvasGroup>().DOFade(_animatrixEffect._fadeEffect.endValue, _animatrixEffect._fadeEffect.duration).SetDelay(_animatrixEffect._fadeEffect.delay).OnComplete(() => isPlayingEffect = false);
        }

        void ResetFadeEffect()
        {
            isPlayingEffect = false;
            tweener.Kill();
            gameObject.GetComponent<CanvasGroup>().alpha = _animatrixEffect._fadeEffect.resetCanvasAlpha;
        }

        #endregion

        #region Scale Effects

        void SimpleScaleEffect()
        {
            tweener = transform.DOScale(_animatrixEffect._scaleEffect.endValue, _animatrixEffect._scaleEffect.duration).SetEase(_animatrixEffect._scaleEffect.easingEffect).SetDelay(_animatrixEffect._scaleEffect.delay).OnComplete(() => isPlayingEffect = false);
        }

        void PunchScaleEffect()
        {
            tweener = transform.DOPunchScale(_animatrixEffect._scaleEffect.endValue, _animatrixEffect._scaleEffect.duration).OnComplete(() => isPlayingEffect = false);
            Invoke(nameof(PunchScaleEffect), _animatrixEffect._scaleEffect.delay);
        }

        void GroupScaleEffect()
        {
            foreach (Transform item in transform.GetComponentsInChildren<Transform>())
            {
                //if(item.localScale.magnitude == 1)  item.DOScale(0f, 0f);
                tweener = item.DOScale(_animatrixEffect._scaleEffect.endValue, _animatrixEffect._scaleEffect.duration).SetEase(_animatrixEffect._scaleEffect.easingEffect).SetDelay(_animatrixEffect._scaleEffect.delay).OnComplete(() => isPlayingEffect = false);
            }
        }

        void GroupScaleWithIncrementedDelay()
        {
            float currentDelay = 0f;

            foreach (Transform item in transform)
            {
                RectTransform rectTransform = item.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    tweener = rectTransform.DOScale(_animatrixEffect._scaleEffect.endValue, _animatrixEffect._scaleEffect.duration).SetEase(_animatrixEffect._scaleEffect.easingEffect).SetDelay(_animatrixEffect._scaleEffect.delay).OnComplete(() => isPlayingEffect = false);
                    currentDelay += _animatrixEffect._scaleEffect.delayIncrement;
                }
            }
        }

        void ResetScaleEffect()
        {
            if (_animatrixEffect._scaleEffect._effectType == ScaleEffects.EffectTypes.GroupScaleEffect || _animatrixEffect._scaleEffect._effectType == ScaleEffects.EffectTypes.SimpleScaleEffect)
            {
                foreach (Transform item in transform.GetComponentsInChildren<RectTransform>())
                {
                    item.DOScale(_animatrixEffect._scaleEffect.resetValue, 0f);
                }

            }
            if (_animatrixEffect._scaleEffect._effectType == ScaleEffects.EffectTypes.GroupScaleWithIncrementEffect)
            {
                foreach (Transform item in transform)
                {
                    RectTransform rectTransform = item.GetComponent<RectTransform>();
                    rectTransform.DOScale(_animatrixEffect._scaleEffect.resetValue, 0f);
                }
            }
            else transform.DOScale(_animatrixEffect._scaleEffect.resetValue, 0f);

            isPlayingEffect = false;
            CancelInvoke();
            tweener.Kill();
        }

        #endregion

        #region Rotate Effects
        void RotateEffect()
        {
            tweener = GetComponent<RectTransform>().DOLocalRotate(_animatrixEffect._rotateEffect.endValue, _animatrixEffect._rotateEffect.duration, RotateMode.FastBeyond360).SetDelay(_animatrixEffect._rotateEffect.delay).SetLoops(_animatrixEffect._rotateEffect.loopCount, _animatrixEffect._rotateEffect.loopType);
            tweener.PlayForward();
            //transform.DORotate(endValue, duration).OnComplete(() => isPlayingEffect = false); ;
            //Invoke(nameof(RotateEffect), delay);
        }

        void ResetRotateEffect()
        {
            isPlayingEffect = false;
            CancelInvoke();
            tweener.Kill();
        }
        #endregion

        #region Move Effects

        void AnchorSimpleMove()
        {
            tweener = transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), _animatrixEffect._moveEffect.duration).OnComplete(() => isPlayingEffect = false);
        }

        void AnchorEasingMove()
        {
            tweener = transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), _animatrixEffect._moveEffect.duration).SetEase(_animatrixEffect._moveEffect.easingEffect).SetDelay(_animatrixEffect._moveEffect.delay).OnComplete(() => isPlayingEffect = false);
        }

        void AnchorGroupMove()
        {
            float currentDelay = 0f;

            foreach (Transform item in transform)
            {
                RectTransform rectTransform = item.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    tweener = rectTransform.DOAnchorPos(_animatrixEffect._moveEffect.resetPosition, _animatrixEffect._moveEffect.duration)
                                 .SetEase(_animatrixEffect._moveEffect.easingEffect)
                                 .SetDelay(currentDelay)
                                 .OnComplete(() => isPlayingEffect = false);

                    currentDelay += _animatrixEffect._moveEffect.delayIncrement;
                }
            }
        }

        void ResetMoveEffect()
        {
            if (_animatrixEffect._moveEffect._effectType == MoveEffects.EffectTypes.AnchorGroupMove)
                foreach (Transform item in transform)
                {
                    RectTransform rectTransform = item.GetComponent<RectTransform>();
                    rectTransform.DOAnchorPos(_animatrixEffect._moveEffect.resetPosition, 0f);
                }
            else transform.GetComponent<RectTransform>().DOAnchorPos(_animatrixEffect._moveEffect.resetPosition, 0f);

            isPlayingEffect = false;
            tweener.Kill();
        }    

/*        void OnCompleteEvent()
        {
            if (_animatrixEffect._moveEffects.callBack != null)
            {
                _animatrixEffect._moveEffects.callBack?.Invoke();
            }

            isPlayingEffect = false;
        }*/

        #endregion
    }
}
