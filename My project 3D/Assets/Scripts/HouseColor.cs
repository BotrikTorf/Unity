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
        bool canSwitchColor = true;
        Color color = _material.color;

        while (_isRun)
        {
            if (canSwitchColor)
            {
                for (float i = 0; i <= 1f; i += _speed)
                {
                    color.r = i;
                    color.g = 1 - i;
                    _material.color = color;
                    yield return timeDelay;
                }

                canSwitchColor = false;
            }
            else
            {
                for (float i = 0; i <= 1f; i += _speed)
                {
                    color.r = 1 - i;
                    color.g = i;
                    _material.color = color;
                    yield return timeDelay;
                }

                canSwitchColor = true;
            }
        }

        if (_isRun == false)
        {
            _material.color = Color.green;
        }
    }
}
