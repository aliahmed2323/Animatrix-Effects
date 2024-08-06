using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace UIAnimatrix
{
    public class AnimatrixButton : Button 
    {
        /////////////////// Animatrix Effects Vars /////////////////////////
        [Header("Animatrix Properties")]
        public bool ShouldPlayEffect = true;

        [HideInInspector]
        public EffectTypes _effectType;

        [HideInInspector]
        public Vector2 punch;

        [HideInInspector]
        public float duration;

        [HideInInspector]
        public bool isPlayingEffect;

        [System.Serializable]
        public enum EffectTypes
        {
            PunchInEffect,
            PunchOutEffect,
            LightShake,
        }

        /////////////////// Functionality Vars /////////////////
        public Action _onClick;
        public new OnClick onClick = new();

        protected override void Awake()
        {
            base.Awake();
            onClick.a = this;

            base.onClick.AddListener(() => PlayEffect());
        }

        void PlayEffect()
        {
            if (!ShouldPlayEffect) return;
            if (isPlayingEffect) return;

            isPlayingEffect = true;
            switch (_effectType)
            {
                case EffectTypes.PunchInEffect:
                    PunchInEffect();
                    break;
                case EffectTypes.PunchOutEffect:
                    PunchOutEffect();
                    break;
                case EffectTypes.LightShake:
                    LightShakeEffect();
                    break;
            }

            Invoke(nameof(InvokeOnClick), duration);
        }

        void InvokeOnClick()
        {
            _onClick?.Invoke();
        }

        void PunchInEffect()
        {
            transform.DOPunchScale(punch, duration).OnComplete(() => isPlayingEffect = false);
        }

        void PunchOutEffect()
        {
            transform.DOPunchScale(punch, duration).OnComplete(() => isPlayingEffect = false);
        }

        void LightShakeEffect()
        {
            //GetComponent<RectTransform>().DOPunchAnchorPos(new Vector2(1.5f, 1.5f), 0.2f).OnComplete(() => isPlayingEffect = false);
            GetComponent<RectTransform>().DOPunchAnchorPos(punch, duration).OnComplete(() => isPlayingEffect = false);
        }
    }

    public class OnClick
    {
        public AnimatrixButton a;

        public void AddListener(Action action)
        {
            a._onClick += action;
        }
        public void RemoveListener(Action action)
        {
            a._onClick -= action;
        }
        public void RemoveAllListeners()
        {
            a._onClick = null;
        }

    }
}