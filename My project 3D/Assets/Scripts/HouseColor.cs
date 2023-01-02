using UnityEngine;

public class HouseColor : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private Material _material;
    private bool _isRun = false;
    [SerializeField] private float _speed = 0.5f;
    private bool _canSwitchColor = true;

    private void Start() => _material.color = Color.green;

    private void OnEnable() => _door.PassedThief += TurnedSiren;

    private void OnDisable() => _door.PassedThief -= TurnedSiren;

    private void Update()
    {
        Color color = _material.color;

        if (_isRun)
        {
            float speedDeltaTime = Time.deltaTime * _speed;

            if (_canSwitchColor)
            {
                color.r += speedDeltaTime;
                color.g -= speedDeltaTime;
                _material.color = color;

                if (color.r > 0.9f)
                    _canSwitchColor = false;
            }
            else
            {
                color.r -= speedDeltaTime;
                color.g += speedDeltaTime;
                _material.color = color;

                if (color.r < 0.1f)
                    _canSwitchColor = true;
            }
        }
        else
        {
            _material.color = Color.green;
        }
    }

    public void TurnedSiren(bool isRun) => _isRun = isRun;
}
