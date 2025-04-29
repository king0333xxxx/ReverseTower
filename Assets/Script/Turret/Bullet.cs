using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes Stats")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float bulletDurationToDestroy = 15f;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Start()
    {
        StartCoroutine(BulletDestroyDuration());
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        UnitBase targetUnit = other.gameObject.GetComponent<UnitBase>();
        if (targetUnit != null)
        {
            targetUnit.TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }

    private IEnumerator BulletDestroyDuration()
    {
        yield return new WaitForSeconds(bulletDurationToDestroy);
        Destroy(gameObject);
    }
}
