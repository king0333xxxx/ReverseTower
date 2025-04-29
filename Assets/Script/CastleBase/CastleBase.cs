using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBase : MonoBehaviour
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
    [SerializeField] private GameObject WinningPanel;

    [serializefield] private bool NextRound; // di sini diliat apakah ada castle berikutnya yg harus di hancurkan




    public virtual void TakeDamage(int damage)
    {
        healthTower -= damage;
        Debug.Log(gameObject.name + " terkena serangan! Sisa HP: " + healthTower);

        if (healthTower <= 0)
        {
            Die();
            WinningPanel.SetActive(true);
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " telah hancur!");
        // Kurangi resource
        playerManager.IncreaseGold(GoldCoin);
        playerManager.IncreaseSpirit(SpiritCoin);
        Destroy(gameObject);

        if (!NextRound) 
        {
            // Unlock next level
            //Winning();

        }
    }

    /*private void Winning()
    {
        WinningPanel.SetActive(true);

        int currentLevel = GetCurrentLevelNumber(); // Misal kamu bisa set ini via inspector atau parsing nama scene
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);

        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("unlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
            Debug.Log("Level " + (currentLevel + 1) + " berhasil di-unlock!");
        }

        // Kamu bisa tambahkan scene transition, efek win, dsb di sini
    }*/
}
