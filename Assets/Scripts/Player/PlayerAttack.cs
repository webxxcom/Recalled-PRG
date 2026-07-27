using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttack : EntityAttack
{
    Collider2D _collider2D;
    PlayerMovement _playerMovement;

    protected override void Awake()
    {
        base.Awake();

        _collider2D = GetComponent<Collider2D>();
        _playerMovement = Utils.FindOrThrow(GetComponentInParent<PlayerMovement>);
    }

    protected virtual void OnEnable()
    {
        _playerMovement.OnMovement += SetAttackCollisionOffset;
    }

    protected virtual void OnDisable()
    {
        _playerMovement.OnMovement -= SetAttackCollisionOffset;
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed && _timeSinceLastAttack >= AttackData.ReloadTime)
        {
            _timeSinceLastAttack = 0;

            Attack();
        }
    }

    void SetAttackCollisionOffset()
    {
        // TODO the same is in the melee attack strategy
        if (!_playerMovement.IsWalking)
            return;

        float degrees = Vector2.SignedAngle(Vector2.right, _playerMovement.MovementIntention);
        _collider2D.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }

    private void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
    }
}
