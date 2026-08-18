using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerSprinting : MonoBehaviour
{
    [SerializeField] float _initValue;
    [SerializeField] float _usage;
    [SerializeField] float _restore;
    [SerializeField] float _speedMultiplier;

    public float SpeedMultiplier => _speedMultiplier;

    PlayerMovement _playerMovement;
    bool _isActive;
    
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive != value)
            {
                if (value)
                    _playerMovement.AddSpeedCoef(_speedMultiplier);
                else
                    _playerMovement.RemoveSpeedCoef(_speedMultiplier);
            }
            _isActive = value;
        }
    }
    float _current;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _current = _initValue;
    }

    public void Sprint(bool isPressed)
        => IsActive = isPressed;

    private void Update()
    {
        // Skip all checks for little optimization
        if (!IsActive && _current - _initValue >= float.Epsilon)
            return;

        if (IsActive)
        {
            if (_current - _usage < 0f)
            {
                IsActive = false;
                _current = 0;
            }
            else
                _current -= _usage;
        }
        else
        {
            if (_current + _restore > _initValue)
                _current = _initValue;
            else
                _current += _restore;
        }
    }
}
