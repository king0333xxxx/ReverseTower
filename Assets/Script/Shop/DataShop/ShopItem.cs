using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    private ShopItemData itemData;
    public Slot itemSlot;

    public void SetData(ShopItemData data, Slot slot)
    {
        itemData = data;
        itemSlot = slot;
    }

    public ShopItemData GetData()
    {
        return itemData;
    }

    public void SetSlot(Slot newSlot)
    {
        itemSlot = newSlot;
        Debug.Log($"Item {gameObject.name} sekarang berada di slot {newSlot.name}");
    }

}
