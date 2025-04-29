using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePasukan : UnitBase
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRadius = 2f; // Radius serangan
    [SerializeField] private LayerMask enemyMask; // Layer musuh yang bisa diserang
    [SerializeField] private LayerMask castleEnemyMask; // Layer musuh yang bisa diserang

    private float attackCooldown = 0f;

    protected override void Start()
    {
        base.Start();
        speedMove = 4f;
        speedAttack = 1.2f;
        hp = 120;
        mana = 0;
        armor = 8;
        damageAttack = 15;
        TroopPerDivision = 5;
    }

    protected override void Update()
    {
        base.Update();

        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            AttackEnemiesInRange();
            attackCooldown = speedAttack; // Reset cooldown berdasarkan attack speed
        }
    }

    private void AttackEnemiesInRange()
    {
        LayerMask combinedMask = enemyMask | castleEnemyMask;
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attackRadius, combinedMask);

        if (targets.Length > 0)
        {
            Debug.Log(gameObject.name + " menyerang " + targets.Length + " musuh!");

            foreach (var target in targets)
            {
                TowerBase tower = target.GetComponent<TowerBase>();
                if (tower != null)
                {
                    tower.TakeDamage(damageAttack);
                    continue;
                }

                CastleBase castle = target.GetComponent<CastleBase>();
                if (castle != null)
                {
                    castle.TakeDamage(damageAttack);
                    continue;
                }
            }
        }
    }



    private void OnDrawGizmosSelected()
    {
        // Menampilkan radius serangan di editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
