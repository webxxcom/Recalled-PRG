using UnityEngine;

public class GolemBossController : EntityController
{
    private static readonly int GlowHash = Animator.StringToHash("Glow");
    private static readonly int ShieldHash = Animator.StringToHash("Shield");
    private static readonly int MeleeHash = Animator.StringToHash("Melee");
    private static readonly int ShootHash = Animator.StringToHash("Shoot");
    private static readonly int LaserHash = Animator.StringToHash("Laser");
    private static readonly int ImmuneHash = Animator.StringToHash("Immune");
    [SerializeField] AnimationController _animationController;

    public float _elapsed;
    public float _reloadTime;

    enum States
    {
        IMMUNE,
        LASER,
        SHOOT,
        MELEE,
        SHIELD,
        GLOW,
        END
    }

    void PlayAnimation(States state)
    {
        switch (state)
        {
            case States.IMMUNE:
                Animator.SetTrigger(ImmuneHash);
                break;
            case States.LASER:
                Animator.SetTrigger(LaserHash);
                break;
            case States.SHOOT:
                Animator.SetTrigger(ShootHash);
                break;
            case States.MELEE:
                Animator.SetTrigger(MeleeHash);
                break;
            case States.SHIELD:
                Animator.SetTrigger(ShieldHash);
                break;
            case States.GLOW:
                Animator.SetTrigger(GlowHash);
                break;
        }
    }

    void AttackMeleeIfTooClose()
    {
        var dist = Physics2D.Distance(FindAnyObjectByType<PlayerController>().Collider2D, Collider2D);

        if (dist.distance < 2f)
        {
            PlayAnimation(States.MELEE);
        }
    }

    private void Update()
    {
        AttackMeleeIfTooClose();
    }

}
