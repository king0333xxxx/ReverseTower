using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint; // Titik spawn pasukan
    public Transform targetPoint; // Target point untuk markas musuh
    public TroopHolder troopHolder; // Referensi ke TroopHolder
    public float spawnDelay = 0.5f; // Delay antar spawn
    public GameObject noTroopPanelWarn;

    public void SpawnWave()
    {
        List<TroopData> troopsToSpawn = troopHolder.GetTroops();

        if (troopsToSpawn.Count > 0)
        {
            StartCoroutine(SpawnTroopCoroutine(troopsToSpawn));
        }
        else
        {
            Debug.Log("Tidak ada pasukan untuk di-spawn.");
            noTroopPanelWarn.SetActive(true);
        }
    }

    private IEnumerator SpawnTroopCoroutine(List<TroopData> troopsToSpawn)
    {
        foreach (var troopData in troopsToSpawn)
        {
            GameObject troopPrefab = troopData.troopPrefab;
            int troopPerDivision = troopPrefab.GetComponent<UnitBase>().GetTroopPerDivision(); // Ambil jumlah per divisi

            for (int i = 0; i < troopData.troopCount * troopPerDivision; i++)
            {
                GameObject spawnedTroop = Instantiate(troopPrefab, spawnPoint.position, Quaternion.identity);
                spawnedTroop.GetComponent<UnitBase>().SetTarget(targetPoint);
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        Debug.Log("Semua pasukan telah di-spawn.");
    }

}
