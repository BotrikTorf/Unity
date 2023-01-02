using UnityEngine;

public class Siren : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _soundChangeRate = 0.1f;
    private bool isRun = false;

    private void Start()
    {
        _audioSource.volume = 0;
        _audioSource.Stop();
    }

    private void OnEnable() => _door.PassedThief += ControlsSiren;

    private void OnDisable() => _door.PassedThief -= ControlsSiren;

    private void Update()
    {
        if (isRun)
        {
            if (_audioSource.volume == 0)
            {
                _audioSource.Play();
            }
            _audioSource.volume += Time.deltaTime * _soundChangeRate;
        }
        else
        {
            if (_audioSource.volume == 0)
            {
                _audioSource.Stop();
            }
            _audioSource.volume -= Time.deltaTime * _soundChangeRate;
        }
    }
    public void ControlsSiren(bool haveTurnSiren) => isRun = haveTurnSiren;
}
