using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Turret : TowerBase
{
    [Header("Reference")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("UI HP Bar")]
    [SerializeField] private Image hpFillImage;

    private int maxHealth;
    private Transform target;
    private float timeUntilFire;

    private void Start()
    {
        maxHealth = healthTower;
        UpdateHP();
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / ats)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firingPoint == null) return;
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }

    private bool CheckTargetIsInRange()
    {
        return target != null && Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        if (target == null) return;

        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void UpdateHP()
    {
        if (hpFillImage != null)
        {
            float fillAmount = (float)healthTower / maxHealth;
            hpFillImage.fillAmount = fillAmount;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHP();
    }

    private void OnDrawGizmosSelected()
    {
        // Menampilkan radius serangan di editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
}
