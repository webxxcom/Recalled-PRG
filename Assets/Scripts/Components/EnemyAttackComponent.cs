using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackComponent : MonoBehaviour
{
    [SerializeField] List<HealthComponent> targets;
    [SerializeField] int dealtDamage;
    [SerializeField] float attackTimeout;
    [SerializeField] GameObject projectilePrefab;

    HealthComponent currentTarget;
    Animator animator;
    float timeSinceLastAttack;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthComponent hc) && targets.Contains(hc))
        {
            currentTarget = hc;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthComponent hc) && targets.Contains(hc))
        {
            currentTarget = hc;
        }
    }

    bool CanAttack() => timeSinceLastAttack >= attackTimeout;

    private void Update()
    {
        if (currentTarget && CanAttack())
        {
            if (animator)
                animator.SetTrigger("attack");

            GameObject arrow = Instantiate(projectilePrefab);
            ProjectileScript ps = arrow.GetComponent<ProjectileScript>();
            ps.StartPos = transform.position;
            ps.Destination = currentTarget.transform.position;

            timeSinceLastAttack = 0;
        }
        timeSinceLastAttack += Time.deltaTime;
    }
}
