using System.Collections;
using UnityEngine;

public class HouseColor : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private Material _material;
    [SerializeField] private float _speed = 0.005f;

    private bool _isRun = false;
    private Coroutine _repleseCoroutine;

    private void Start() => _material.color = Color.green;

    private void OnEnable() => _door.PassedThief += OnPassedThief;

    private void OnDisable() => _door.PassedThief -= OnPassedThief;

    private void OnPassedThief(bool isRun)
    {
        _isRun = isRun;

        if (_repleseCoroutine != null)
        {
            StopCoroutine(_repleseCoroutine);
        }

        _repleseCoroutine = StartCoroutine(ChangesColor());
    }

    private IEnumerator ChangesColor()
    {
        var timeDelay = new WaitForSeconds(_speed);
        Color color = _material.color;
        float minValue = 0f;
        float maxValue = 1f;
        float stepIteration = 0f;

        while (_isRun)
        {
            color.r = Mathf.Lerp(minValue, maxValue, stepIteration);
            color.g = 1 - Mathf.Lerp(minValue, maxValue, stepIteration);
            _material.color = color;
            stepIteration += _speed;
            yield return timeDelay;

            if (stepIteration > 1)
            {
                stepIteration = maxValue;
                maxValue = minValue;
                minValue = stepIteration;
                stepIteration = 0f;
            }
        }

        if (_isRun == false)
        {
            _material.color = Color.green;
        }
    }
}
