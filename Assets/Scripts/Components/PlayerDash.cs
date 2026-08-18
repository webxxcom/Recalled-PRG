using UnityEngine;

[DisallowMultipleComponent]
public class PlayerDash : MonoBehaviour
{
    [SerializeField] float _dashReloadTime;
    [SerializeField] float _dashForce;
    [SerializeField] float _dashInvincibilityDuration;
    [SerializeField] ExternalVelocity _externalVelocity;
    [SerializeField] HealthResource _health;

    public void Dash(Vector2 direction)
    {
        _externalVelocity.Add(direction * _dashForce);
        _health.GrantInvincibility(_dashInvincibilityDuration);
    }
}
