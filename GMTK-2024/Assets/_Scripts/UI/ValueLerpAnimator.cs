using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueLerpAnimator : MonoBehaviour
{
    [SerializeField] private string _prefix;
    [SerializeField] private TextMeshProUGUI _textObject;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private bool _includeDecimals;
    
    private Coroutine _coroutine;
    private bool _isAnimating;
    
    public void AnimateValue(float startValue, float endValue) {
        if (_isAnimating) {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(AnimateValueCoroutine(startValue, endValue, _curve, _duration, _includeDecimals));
    }

    private IEnumerator AnimateValueCoroutine(float startValue, float endValue, AnimationCurve curve, float duration, bool includeDecimals) {
        _isAnimating = true;
        float time = 0;
        string valueStr;
        while (time < duration) {
            float t = time / duration;
            float value = Mathf.Lerp(startValue, endValue, curve.Evaluate(t));
            valueStr = includeDecimals ? value.ToString() : Mathf.RoundToInt(value).ToString();
            _textObject.text = _prefix + valueStr;
            time += Time.deltaTime;
            yield return null;
        }
        _textObject.text = endValue.ToString();
        _isAnimating = false;
    }
    
    public void StopAnimation() {
        if (_isAnimating) {
            StopCoroutine(_coroutine);
            _isAnimating = false;
        }
    }
}
