using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class GameMap : MonoBehaviour
{
    void Start()
    {
        // Cek apakah sudah pernah diset, kalau belum set default ke 1
        if (!PlayerPrefs.HasKey("unlockedLevel"))
        {
            PlayerPrefs.SetInt("unlockedLevel", 1); // Level 1 terbuka
            PlayerPrefs.Save();
            Debug.Log("First time playing — Level 1 unlocked by default");
        }
    }

}
