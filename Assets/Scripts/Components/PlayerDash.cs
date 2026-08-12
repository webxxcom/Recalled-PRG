using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] float _dashReloadTime;
    [SerializeField] float _dashForce;
    [SerializeField] float _dashInvincibilityDuration;
    [SerializeField] ExternalVelocity _externalVelocity;
    [SerializeField] HealthResource _health;

    public void Dash(Vector2 direction)
    {
        if (_externalVelocity != null) _externalVelocity.Add(direction * _dashForce);
        if (_health != null) _health.GrantInvincibility(_dashInvincibilityDuration);
    }
}
