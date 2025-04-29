using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int _spiritCoin = 100; // Default nilai awal
    [SerializeField] private int _goldCoin = 100;   // Default nilai awal

    public TextMeshProUGUI SpiritCoinText;
    public TextMeshProUGUI GoldCoinText;

    public int SpiritCoin => _spiritCoin;
    public int GoldCoin => _goldCoin;

    private void Start()
    {
        UpdateUI();
    }

    public void ReduceGold(int amount)
    {
        _goldCoin -= amount;
        UpdateUI();
    }
    public void IncreaseGold(int amount)
    {
        _goldCoin += amount;
        UpdateUI();
    }

    public void ReduceSpirit(int amount)
    {
        _spiritCoin -= amount;
        UpdateUI();
    }
    public void IncreaseSpirit(int amount)
    {
        _spiritCoin += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        SpiritCoinText.text = _spiritCoin.ToString();
        GoldCoinText.text = _goldCoin.ToString();
    }
}
