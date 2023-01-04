using System.Collections;
using UnityEngine;

public class Siren : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _soundChangeRate = 0.0005f;

    private Coroutine _repleseCoroutine;
    private bool isRun = false;

    private void Start()
    {
        _audioSource.volume = 0;
        _audioSource.Stop();
    }

    private void OnEnable() => _door.PassedThief += OnPassedThief;

    private void OnDisable() => _door.PassedThief -= OnPassedThief;

    private void OnPassedThief(bool haveTurnSiren)
    {
        isRun = haveTurnSiren;

        if (haveTurnSiren)
        {
            _audioSource.volume = 0;
            _audioSource.Play();
        }

        if (_repleseCoroutine != null)
        {
            StopCoroutine(_repleseCoroutine);
        }

        _repleseCoroutine = StartCoroutine(ChangesSound());
    }

    private IEnumerator ChangesSound()
    {
        var timeDelay = new WaitForSeconds(_soundChangeRate);

        while (isRun)
        {
            _audioSource.volume += _soundChangeRate;
            yield return timeDelay;
        }

        while (_audioSource.volume > 0 && isRun == false)
        {
            _audioSource.volume -= _soundChangeRate;
            yield return timeDelay;
        }

        if (_audioSource.volume == 0 && isRun == false)
        {
            _audioSource.Stop();
        }
    }
}
