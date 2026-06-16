using UnityEngine;

public class ExternalVelocityComponent : MonoBehaviour
{
    [SerializeField] private float decayRate = 8f;
    [SerializeField] private float restThreshold = 0.05f;

    private Vector2 _velocity;

    public void Add(Vector2 velocity) => _velocity += velocity;

    public Vector2 TickAndGet(float dt)
    {
        _velocity *= Mathf.Exp(-decayRate * dt);

        if (_velocity.sqrMagnitude < restThreshold * restThreshold)
            _velocity = Vector2.zero;
        return _velocity;
    }

}
