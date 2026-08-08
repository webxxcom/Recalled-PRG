using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRendererGroup))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] bool _isXFlippable;

    protected Animator _animator;
    protected SpriteRendererGroup _spriteRendererGroup;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRendererGroup = GetComponent<SpriteRendererGroup>();
    }

    public void MoveAnimation(Vector2 direction, float speed)
    {
        _animator.SetFloat(AnimatorParameters.MoveXHash, direction.x);
        _animator.SetFloat(AnimatorParameters.MoveYHash, direction.y);

        if (Mathf.Abs(speed) > float.Epsilon)
            _animator.SetFloat(AnimatorParameters.SpeedHash, speed);

        if (_isXFlippable)
        {
            if (direction.x > float.Epsilon)
                _spriteRendererGroup.SetFlipX(false);
            else if (direction.y < -float.Epsilon)
                _spriteRendererGroup.SetFlipX(true);
        }
    }
}
