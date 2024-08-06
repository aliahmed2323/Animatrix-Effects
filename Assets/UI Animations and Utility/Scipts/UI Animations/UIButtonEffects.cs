using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace UIEffects.ButtonEffects
{

    public class UIButtonEffects : MonoBehaviour
    {
        [SerializeField] EffectTypes _effectType;

        [Space]
        [Header("Settings")]

        [SerializeField] Vector2 punch;
        [SerializeField] float duration;

        bool isPlayingEffect;

        [System.Serializable]
        public enum EffectTypes
        {
            PunchInEffect,
            PunchOutEffect,
            LightShake,
        }

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => PlayEffect());
        }

        void PlayEffect()
        {
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
        }

        #region TRANSITION EFFECTS

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

        #endregion
    }
}