using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EntityController))]
public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] Slider slider;
    [SerializeField] float maxHealth;
    float health;

    EntityController entityController;
    new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    public new GameObject gameObject { get => base.gameObject; }

    void InitSlider()
    {
        slider.maxValue = maxHealth;
        slider.value = health;
    }

    private void Awake()
    {
        TryGetComponent(out rigidbody2D);
        TryGetComponent(out spriteRenderer);
        entityController = GetComponent<EntityController>();
        health = maxHealth;

        if (slider)
        {
            InitSlider();
        }
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }

    private void ApplyKnockback(GameObject attacker, float knockbackPower)
    {
        Vector2 forceVector = (transform.position - attacker.transform.position).normalized;

        rigidbody2D.linearVelocity = Vector2.zero;
        rigidbody2D.AddForce(forceVector * knockbackPower, ForceMode2D.Impulse);
        entityController.FreezeFor(0.2f * knockbackPower);
    }

    public void TakeDamage(GameObject attacker, int damage, float knockbackPower)
    {
        health -= damage;

        if (slider)
            slider.value = health;
        if (rigidbody2D)
            ApplyKnockback(attacker, knockbackPower);
        if (spriteRenderer)
            StartCoroutine(DamageFlash());
    }
}
