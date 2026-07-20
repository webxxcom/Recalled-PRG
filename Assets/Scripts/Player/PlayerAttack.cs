using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttack : EntityAttack
{
    Collider2D _collider2D;
    PlayerController _playerController;
    PlayerMovement _playerMovement;

    protected override void Awake()
    {
        base.Awake();

        _collider2D = GetComponent<Collider2D>();
        _playerMovement = Utils.FindOrThrow(GetComponentInParent<PlayerMovement>);
        _playerController = _entityController as PlayerController;
    }

    public override int DealtDamage
    {
        get
        {
            int totalDamage = AttackData.DealtDamage;
            if (_playerController.Inventory.Sword != null)
                totalDamage += _playerController.Inventory.Sword.Definition.Damage;
            return totalDamage;
        }
    }
    public override float DealtKnockbackPower
    {
        get
        {
            float totalKnockback = AttackData.KnockbackPower;
            if (_playerController.Inventory.Sword != null)
                totalKnockback += _playerController.Inventory.Sword.Definition.KnockbackPower;
            return totalKnockback;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _playerMovement.OnMovement += SetAttackCollisionOffset;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

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
        if (!_playerController.MovementComponent.IsWalking)
            return;

        float degrees = Vector2.SignedAngle(Vector2.right, _playerController.MovementComponent.MovementIntention);
        _collider2D.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }

    private void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
    }
}
