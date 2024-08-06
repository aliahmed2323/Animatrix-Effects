using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace UIEffects.TransitionEffects
{
    public class UITransitionEffects : MonoBehaviour
    {
        [SerializeField] EffectTypes _effectsType;

        [Space]
        [Header("Settings")]

        [SerializeField] float duration;
        [SerializeField] float delay;
        [SerializeField] float delayIncrement;
        [SerializeField] Vector2 position;
        [SerializeField] Vector2 resetPosition;
        [SerializeField] Ease easingEffect;
        [SerializeField] UnityEvent callBack;

        Tweener tweener;
        bool isPlayingEffect;

        [System.Serializable]
        public enum EffectTypes
        {
            AnchorSimpleMove,
            AnchorEasinghMove,
            AnchorGroupMove
        }

        private void OnEnable()
        {
            PlayEffect();
        }

        private void OnDisable()
        {
            if (_effectsType == EffectTypes.AnchorGroupMove)
                foreach (Transform item in transform)
                {
                    RectTransform rectTransform = item.GetComponent<RectTransform>();
                    rectTransform.DOAnchorPos(resetPosition, 0f);
                }
            else transform.GetComponent<RectTransform>().DOAnchorPos(resetPosition, 0f);
            
            isPlayingEffect = false;
            tweener.Kill();
        }

        void PlayEffect()
        {
            if (isPlayingEffect) return;
            isPlayingEffect = true;
            switch (_effectsType)
            {
                case EffectTypes.AnchorSimpleMove:
                    AnchorSimpleMove();
                    break;
                case EffectTypes.AnchorEasinghMove:
                    AnchorEasinghMove();
                    break;
                case EffectTypes.AnchorGroupMove:
                    AnchorGroupMove();
                    break;
            }
        }

        #region TRANSITION EFFECTS

        void AnchorSimpleMove()
        {
            tweener = transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), duration).OnComplete(() => isPlayingEffect = false);
        }

        void AnchorEasinghMove()
        {
            tweener = transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), duration).SetEase(easingEffect).SetDelay(delay).OnComplete(() => isPlayingEffect = false);
        }

        //void AnchorGroupMove()
        //{
        //    foreach (Transform item in transform)
        //    {
        //        item.GetComponent<RectTransform>().DOAnchorPos(position, duration).SetEase(easingEffect).SetDelay(delay).OnComplete(() => OnCompleteEvent());
        //    }
        //}


        void AnchorGroupMove()
        {
            float currentDelay = 0f;

            foreach (Transform item in transform)
            {
                RectTransform rectTransform = item.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    tweener = rectTransform.DOAnchorPos(position, duration)
                                 .SetEase(easingEffect)
                                 .SetDelay(currentDelay)
                                 .OnComplete(() => OnCompleteEvent());

                    currentDelay += delayIncrement;
                }
            }
        }

        void OnCompleteEvent()
        {
            if (callBack != null)
                callBack?.Invoke();

            isPlayingEffect = false;
        }

        #endregion
    }
}
