using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SlowTower : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes Stats")]
    [SerializeField] private float ats = 4f; // attack speed / second
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private float slowMultiplier = 0.5f; // 50% slow

    private float timeUntilFire;

    void Update()
    {
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / ats)
        {
            Debug.Log("Freeze");
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        foreach (RaycastHit2D hit in hits)
        {
            UnitBase unit = hit.transform.GetComponent<UnitBase>();
            if (unit != null)
            {
                unit.ModifySpeed(slowMultiplier, freezeTime);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
