using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("Attributes Stats")]
    [SerializeField] protected int healthTower = 500;
    [SerializeField] protected float targetingRange = 5f;
    [SerializeField] protected float rotationSpeed = 5f;
    [SerializeField] protected float ats = 1f; // Attack Speed

    [Header("Referense Manager")]
    public PlayerManager playerManager; // Referensi ke PlayerManager

    [Header("Reward  when Die")]
    [SerializeField] protected int GoldCoin = 500;
    [SerializeField] protected int SpiritCoin = 500;



    public virtual void TakeDamage(int damage)
    {
        healthTower -= damage;
        Debug.Log(gameObject.name + " terkena serangan! Sisa HP: " + healthTower);

        if (healthTower <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " telah hancur!");
        // Kurangi resource
        playerManager.IncreaseGold(GoldCoin);
        playerManager.IncreaseSpirit(SpiritCoin);
        Destroy(gameObject);
    }
}
