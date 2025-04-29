using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target; // Target point untuk markas musuh
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (_target != null)
        {
            agent.SetDestination(_target.position);
        }
    }

    // Method untuk menetapkan target secara dinamis
    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
