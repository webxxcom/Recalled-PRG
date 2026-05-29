using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EntityController))]
public class HealthComponent : MonoBehaviour
{
    [SerializeField] BarScript healthSlider;
    [SerializeField] int maxHealth;
    int health;

    EntityController entityController;
    new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    public event Action<GameObject> OnDamageTaken;

    void InitSlider()
    {
        healthSlider.SetMax(maxHealth);
        healthSlider.SetCurrent(maxHealth);
    }

    private void Awake()
    {
        TryGetComponent(out rigidbody2D);
        TryGetComponent(out spriteRenderer);
        entityController = GetComponent<EntityController>();
        health = maxHealth;

        if (healthSlider)
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
        if (health - damage <= 0)
        {
            health = 0;
        }
        else
        {
            health -= damage;
        }

        if (healthSlider)
            healthSlider.Change(-damage);
        if (rigidbody2D)
            ApplyKnockback(attacker, knockbackPower);
        if (spriteRenderer)
            StartCoroutine(DamageFlash());

        OnDamageTaken?.Invoke(attacker);
    }

    public void Die()
    {
        TakeDamage(gameObject, health, 0);
    }
}
