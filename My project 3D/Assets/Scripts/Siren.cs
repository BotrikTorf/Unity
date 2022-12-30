using System.Collections;
using UnityEngine;

public class Siren : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private AudioSource _audioSource;
    private Coroutine _repleseCoroutine;

    private void OnEnable() => _door.EventsAction += RestartCoroutine;

    private void OnDisable() => _door.EventsAction -= RestartCoroutine;

    private IEnumerator ChangeSound()
    {
        if (_audioSource.volume == 0)
        {
            for (float i = 0; i <= 1; i += 0.01f)
            {
                _audioSource.volume = i;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            for (float i = 1; i >= 0; i -= 0.01f)
            {
                _audioSource.volume = i;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public void RestartCoroutine(bool selector)
    {
        if (selector)
        {
            _audioSource.volume = 0;
            _audioSource.Play();
        }

        if (_repleseCoroutine != null)
        {
            StopCoroutine(_repleseCoroutine);
        }

        _repleseCoroutine = StartCoroutine(ChangeSound());
    }
}
