using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitBase : MonoBehaviour
{
    [Header("Unit Attributes")]
    [SerializeField] protected float speedMove = 3.5f;
    [SerializeField] protected float speedAttack = 1f;
    [SerializeField] protected int hp = 100;
    [SerializeField] protected int mana = 50;
    [SerializeField] protected int armor = 5;
    [SerializeField] protected int damageAttack = 10;

    [Header("Division Settings")]
    public int TroopPerDivision = 5; // Default 5 troop per divisi


    protected NavMeshAgent agent;
    protected Transform target;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = speedMove;
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public int GetTroopPerDivision()
    {
        return TroopPerDivision;
    }

    public virtual void TakeDamage(int damage)
    {
        int finalDamage = Mathf.Max(damage - armor, 0); // Kurangi damage berdasarkan armor (damage tidak bisa negatif)
        hp -= finalDamage;
        if (hp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " has been defeated.");
        Destroy(gameObject);
    }

    public void ModifySpeed(float multiplier, float duration)
    {
        agent.speed *= multiplier; // Kurangi kecepatan berdasarkan multiplier
        StartCoroutine(ResetSpeedAfter(duration));
    }

    private IEnumerator ResetSpeedAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        agent.speed = speedMove; // Kembalikan ke kecepatan asli
    }

}
