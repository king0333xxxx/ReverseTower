using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasukanShop : MonoBehaviour
{
    public TextMeshProUGUI TextAmountTroop;
    public GameObject prefabTroop; // Prefab pasukan
    public int amountTroop = 0;    // Jumlah pasukan yang ingin dibeli
    public TroopHolder troopHolder; // Referensi ke TroopHolder

    public PlayerManager playerManager; // Referensi ke PlayerManager

    private void Start()
    {
        UpdateAmountText(); // Pastikan UI diperbarui saat awal permainan
    }

    // Method untuk memperbarui teks jumlah pasukan di UI
    private void UpdateAmountText()
    {
        TextAmountTroop.text = amountTroop.ToString();
    }
}
