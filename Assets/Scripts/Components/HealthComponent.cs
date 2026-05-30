using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EntityController))]
public class HealthComponent : MonoBehaviour
{
    [SerializeField] BarScript healthSlider;
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int Health { get; private set; }

    EntityController entityController;
    new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    public event Action<GameObject> OnDamageTaken;
    public event Action OnDeath;

    void InitSlider()
    {
        healthSlider.SetMax(MaxHealth);
        healthSlider.SetCurrent(MaxHealth);
    }

    private void Awake()
    {
        TryGetComponent(out rigidbody2D);
        TryGetComponent(out spriteRenderer);
        entityController = GetComponent<EntityController>();
        Health = MaxHealth;

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

        rigidbody2D.AddForce(forceVector * knockbackPower, ForceMode2D.Impulse);
    }

    public void TakeDamage(GameObject attacker, int damage, float knockbackPower)
    {
        if (Health - damage <= 0)
        {
            Health = 0;

            OnDeath?.Invoke();
        }
        else
        {
            Health -= damage;
            if (rigidbody2D)
                ApplyKnockback(attacker, knockbackPower);
        }

        if (healthSlider)
            healthSlider.Change(-damage);
        //if (spriteRenderer)
        //    StartCoroutine(DamageFlash());

        OnDamageTaken?.Invoke(attacker);
    }
}
