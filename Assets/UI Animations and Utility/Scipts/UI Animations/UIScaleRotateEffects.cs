using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UIScaleRotateEffects : MonoBehaviour
{
    [SerializeField] EffectTypes _effectType;

    [Space]
    [Header("Settings")]

    [SerializeField] Vector3 endValue;
    [SerializeField] Vector3 resetValue;
    [SerializeField] float duration;
    [SerializeField] float delay;
    [SerializeField] float delayIncrement;
    [SerializeField] int loopCount;
    [SerializeField] LoopType loopType;
    [SerializeField] Ease easingEffect;
    [SerializeField] float vibrato;
    [SerializeField] float elasticity;
    [SerializeField] UnityEvent callBack;

    Tweener tweener;
    bool isPlayingEffect;

    [System.Serializable]
    public enum EffectTypes
    {
        RotateEffect,
        SimpleScaleEffect,
        PunchScaleEffect,
        GroupScaleEffect,
        GroupScaleWithDelay
    }

    private void OnEnable()
    {
        PlayEffect();
    }

    private void OnDisable()
    {       
        if (_effectType == EffectTypes.GroupScaleEffect || _effectType == EffectTypes.SimpleScaleEffect)
        {
            foreach (Transform item in transform.GetComponentsInChildren<RectTransform>())
            {
                item.DOScale(resetValue, 0f);
            }
           
        }
        if(_effectType == EffectTypes.GroupScaleWithDelay)
        {
            foreach (Transform item in transform)
            {
                RectTransform rectTransform = item.GetComponent<RectTransform>();
                rectTransform.DOScale(resetValue, 0f);
            }
        }
        else transform.DOScale(resetValue, 0f);

        isPlayingEffect = false;
        CancelInvoke();
        tweener.Kill();
    }

    void PlayEffect()
    {
        if (isPlayingEffect) return;
        isPlayingEffect = true;
        switch (_effectType)
        {
            case EffectTypes.RotateEffect:
                RotateEffect();
                break;
            case EffectTypes.SimpleScaleEffect:
                SimpleScaleEffect();
                break;
            case EffectTypes.PunchScaleEffect:
                PunchScaleEffect();
                break;
            case EffectTypes.GroupScaleEffect:
                GroupScaleEffect();
                break;
            case EffectTypes.GroupScaleWithDelay:
                GroupScaleWithDelay();
                break;
        }
    }

    #region TRANSITION EFFECTS

    void SimpleScaleEffect()
    {
        tweener = transform.DOScale(endValue, duration).SetEase(easingEffect).SetDelay(delay).OnComplete(() => isPlayingEffect = false);
    }


    void RotateEffect()
    {
        tweener = GetComponent<RectTransform>().DOLocalRotate(endValue, duration, RotateMode.FastBeyond360).SetDelay(delay).SetLoops(loopCount, loopType);
        tweener.PlayForward();
        //transform.DORotate(endValue, duration).OnComplete(() => isPlayingEffect = false); ;
        //Invoke(nameof(RotateEffect), delay);
    }

    void PunchScaleEffect()
    {
        tweener = transform.DOPunchScale(endValue, duration).SetLoops(loopCount, loopType).OnComplete(() => isPlayingEffect = false);
        Invoke(nameof(PunchScaleEffect), delay);
    }

    void GroupScaleEffect()
    {
        foreach (Transform item in transform.GetComponentsInChildren<Transform>())
        {
            //if(item.localScale.magnitude == 1)  item.DOScale(0f, 0f);
            tweener = item.DOScale(endValue, duration).SetEase(easingEffect).SetDelay(delay).OnComplete(() => isPlayingEffect = false);
        }
    }

    void GroupScaleWithDelay()
    {
        float currentDelay = 0f;

        foreach (Transform item in transform)
        {
            RectTransform rectTransform = item.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                tweener = rectTransform.DOScale(endValue, duration).SetEase(easingEffect).SetDelay(delay).OnComplete(() => isPlayingEffect = false);
                currentDelay += delayIncrement;
            }
        }
    }

    #endregion
}
