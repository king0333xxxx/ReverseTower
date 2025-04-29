using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleBasic : CastleBase
{
    /// <summary>
    /// ini adalah script Basic Castle Tanpa Senjata.
    /// </summary>

    [Header("UI HP Bar")]
    [SerializeField] private Image hpFillImage;


    private int maxHealth;

    void Start()
    {
        maxHealth = healthTower;
        UpdateHP();
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
}
