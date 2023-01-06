using System.Collections;
using UnityEngine;

public class Siren : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _soundChangeRate = 0.0005f;
    [SerializeField] private float _maxVolume = 1f;

    private Coroutine _repleseCoroutine;

    private void Start()
    {
        _audioSource.volume = 0;
        _audioSource.Stop();
    }

    private void OnEnable() => _door.PassedThief += OnPassedThief;

    private void OnDisable() => _door.PassedThief -= OnPassedThief;

    private void OnPassedThief(bool haveTurnSiren)
    {
        float soundChangeDirection;
        float target;

        if (haveTurnSiren)
        {
            _audioSource.Play();
            soundChangeDirection = _soundChangeRate;
            target = _maxVolume;
        }
        else
        {
            soundChangeDirection = - _soundChangeRate;
            target = 0;
        }

        if (_repleseCoroutine != null)
            StopCoroutine(_repleseCoroutine);

        _repleseCoroutine = StartCoroutine(ChangesSound(soundChangeDirection, target));
    }

    private IEnumerator ChangesSound(float soundChangeDirection, float target)
    {
        var timeDelay = new WaitForSeconds(_soundChangeRate);

        while (_audioSource.volume != target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, soundChangeDirection);
            yield return timeDelay;
        }

        if (_audioSource.volume <= _soundChangeRate)
            _audioSource.Stop();
    }
}
