using System.Collections.Generic;
using UnityEngine;

public class ShopInventoryController : MonoBehaviour
{
    public GameObject shopInventoryPanel;
    public GameObject slotPrefab;
    public List<ShopItemData> shopItemsData;

    // Ganti jadi pakai string ID
    private Dictionary<string, Slot> shopSlots = new Dictionary<string, Slot>();

    private string GetUniqueID(ShopItemData data)
    {
        return gameObject.name + "_" + data.itemID; // Nama GameObject + ID, jadi unik per panel
    }


    void Start()
    {
        foreach (ShopItemData data in shopItemsData)
        {
            CreateShopItem(data);
        }
    }

    public void CreateShopItem(ShopItemData data)
    {
        if (string.IsNullOrEmpty(data.itemID))
        {
            Debug.LogWarning($"Item {data.name} tidak punya itemID!");
            return;
        }

        string uniqueID = GetUniqueID(data);

        Slot slot;
        if (!shopSlots.ContainsKey(uniqueID))
        {
            slot = Instantiate(slotPrefab, shopInventoryPanel.transform).GetComponent<Slot>();
            shopSlots[uniqueID] = slot;
        }
        else
        {
            slot = shopSlots[uniqueID];
        }

        // Hapus item lama sebelum spawn ulang
        if (slot.currentItem != null)
        {
            Destroy(slot.currentItem); // lebih aman
        }

        // Spawn ulang item di slot yang sama
        GameObject item = Instantiate(data.itemPrefab, slot.transform);
        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        slot.currentItem = item;

        ShopItem shopItem = item.GetComponent<ShopItem>();
        if (shopItem != null)
        {
            shopItem.SetData(data, slot);
        }
    }
}
