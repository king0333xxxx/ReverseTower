using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TroopData
{
    public GameObject troopPrefab;
    public int troopCount;

    public TroopData(GameObject prefab, int count)
    {
        troopPrefab = prefab;
        troopCount = count;
    }
}

public class TroopHolder : MonoBehaviour
{
    private List<TroopData> troopList = new List<TroopData>();

    public void AddTroop(GameObject troopPrefab, int count)
    {
        if (troopPrefab == null)
        {
            Debug.LogError(" Gagal menambahkan troop: prefabTroop NULL!");
            return;
        }

        troopList.Add(new TroopData(troopPrefab, count));
        Debug.Log($" Troop {troopPrefab.name} berhasil ditambahkan ke troopList!");
    }

    public void RemoveTroop(GameObject troopPrefab)
    {
        troopList.RemoveAll(troop => troop.troopPrefab == troopPrefab);
    }

    public List<TroopData> GetTroops()
    {
        Debug.Log($"TroopHolder saat ini memiliki {troopList.Count} troop.");
        foreach (var troop in troopList)
        {
            Debug.Log($"- {troop.troopPrefab.name}, Jumlah: {troop.troopCount}");
        }
        return troopList;
    }

    public void ClearHolder()
    {
        troopList.Clear();
    }

    public void UpdateTroopList()
    {
        Debug.Log(" Memperbarui urutan pasukan berdasarkan inventory...");

        List<GameObject> orderedTroops = new List<GameObject>();
        Transform inventoryParent = GameObject.Find("ContentInventory").transform; // Sesuaikan nama objek inventory di scene

        foreach (Transform slot in inventoryParent)
        {
            if (slot.childCount > 0)
            {
                GameObject item = slot.GetChild(0).gameObject;
                ItemDragHandler itemHandler = item.GetComponent<ItemDragHandler>();

                if (itemHandler != null && itemHandler.prefabTroop != null)
                {
                    orderedTroops.Add(itemHandler.prefabTroop);
                    Debug.Log($" {itemHandler.prefabTroop.name} ditambahkan ke daftar.");
                }
                else
                {
                    Debug.LogWarning($" Item {item.name} tidak memiliki ItemDragHandler atau prefabTroop NULL!");
                }
            }
        }

        troopList.Clear(); // Kosongkan list lama sebelum update
        foreach (var troop in orderedTroops)
        {
            troopList.Add(new TroopData(troop, 1));
        }

        Debug.Log(" TroopList berhasil diperbarui!");
        GetTroops();
    }


}
