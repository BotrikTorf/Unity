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
        if (haveTurnSiren)
            _audioSource.Play();

        if (_repleseCoroutine != null)
            StopCoroutine(_repleseCoroutine);

        _repleseCoroutine = StartCoroutine(ChangesSound(haveTurnSiren));
    }

    private IEnumerator ChangesSound(bool haveChangesSound)
    {
        var timeDelay = new WaitForSeconds(_soundChangeRate);
        float soundChangeDirection;

        if (haveChangesSound)
            soundChangeDirection = _soundChangeRate;
        else
            soundChangeDirection = -_soundChangeRate;

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, soundChangeDirection);

        while (_audioSource.volume < _maxVolume && _audioSource.volume > 0)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, soundChangeDirection);
            yield return timeDelay;
        }

        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }
}
