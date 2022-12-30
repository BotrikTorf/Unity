using System.Collections;

using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private Material _material;
    private bool _isRun = true;
    [SerializeField] private float _speed = 0.1f;
    private Coroutine _repleseCoroutine;

    private void OnEnable() => _door.EventsAction += RestartCoroutine;

    private void OnDisable() => _door.EventsAction -= RestartCoroutine;

    IEnumerator Replece()
    {
        bool canSwitchColor = true;

        while (_isRun)
        {
            if (canSwitchColor)
            {
                for (float i = 0; i <= 1f; i += _speed)
                {
                    Color color = _material.color;
                    color.r = i;
                    color.g = 1 - i;
                    _material.color = color;
                    yield return new WaitForSeconds(_speed);
                }

                canSwitchColor = false;
            }
            else
            {
                for (float i = 0; i <= 1f; i += _speed)
                {
                    Color color = _material.color;
                    color.r = 1 - i;
                    color.g = i;
                    _material.color = color;
                    yield return new WaitForSeconds(_speed);
                }

                canSwitchColor = true;
            }
        }

        _material.color = Color.green;
    }

    public void RestartCoroutine(bool isRun)
    {
        _isRun = isRun;

        if (_repleseCoroutine != null)
        {
            StopCoroutine(_repleseCoroutine);
        }

        _repleseCoroutine = StartCoroutine(Replece());
    }
}
