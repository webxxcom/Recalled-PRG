using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EntityController))]
public class HealthComponent : ValueProvider
{
    EntityController entityController;
    new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        TryGetComponent(out rigidbody2D);
        TryGetComponent(out spriteRenderer);
        entityController = GetComponent<EntityController>();

        //OnValueChanged += (obj, val) => ApplyKnockback(obj, 1.1f);
        OnValueChanged += (obj, val) => DamageFlash();
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
        entityController.FreezeFor(0.2f);
    }
}
