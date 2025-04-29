using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropSpawnSelection : MonoBehaviour
{
    public SpawnManager spawnManager; // Referensi ke SpawnManager
    public List<Transform> spawnPoints = new List<Transform>(); // List SpawnPoint yang bisa dipilih

    // Fungsi ini akan dipanggil dari UI Dropdown
    public void OnSpawnPointChanged(int index)
    {
        if (index >= 0 && index < spawnPoints.Count)
        {
            spawnManager.spawnPoint = spawnPoints[index];
            Debug.Log($"Spawn Point diubah ke: {spawnPoints[index].name}");
        }
        else
        {
            Debug.LogError("Index Spawn Point tidak valid!");
        }
    }
}
