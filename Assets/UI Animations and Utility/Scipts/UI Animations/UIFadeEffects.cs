using UnityEngine;
using DG.Tweening;

namespace UIEffects.FadeEffeects
{

    public class UIFadeEffects : MonoBehaviour
    {
        [SerializeField] EffectTypes _effectType;

        [Space]
        [Header("Settings")]

        [SerializeField] float endValue;
        [SerializeField] float duration;
        [SerializeField] float delay;
        [SerializeField] float resetCanvasAlpha;

        Tweener tweener;
        bool isPlayingEffect;

        [System.Serializable]
        public enum EffectTypes
        {
            SimpleFade
        }

        private void Awake()
        {
            if (gameObject.GetComponent<CanvasGroup>() == null) gameObject.AddComponent<CanvasGroup>().alpha = 0;
        }

        private void OnEnable()
        {
            PlayEffect();
        }

        private void OnDisable()
        {
            isPlayingEffect = false;
            tweener.Kill();
            gameObject.GetComponent<CanvasGroup>().alpha = resetCanvasAlpha;
        }

        void PlayEffect()
        {
            if (isPlayingEffect) return;
            isPlayingEffect = true;
            switch (_effectType)
            {
                case EffectTypes.SimpleFade:
                    SimpleFade();
                    break;
            }
        }

        #region TRANSITION EFFECTS

        void SimpleFade()
        {
            //if (gameObject.GetComponent<CanvasGroup>() == null) gameObject.AddComponent<CanvasGroup>();
            tweener = gameObject.GetComponent<CanvasGroup>().DOFade(endValue, duration).SetDelay(delay).OnComplete(() => isPlayingEffect = false);
        }
        #endregion
    }
}
