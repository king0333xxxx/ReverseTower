using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    public GameObject playerInventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, playerInventoryPanel.transform);
        }
    }

    public void AddToInventory(GameObject item)
    {
        // Pindahkan item ke slot inventory player
        item.transform.SetParent(playerInventoryPanel.transform);
        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public void AddItem(GameObject item)
    {

        foreach (Transform slot in playerInventoryPanel.transform)
        {
            Slot slotComponent = slot.GetComponent<Slot>();
            if (slotComponent != null && slotComponent.currentItem == null)
            {
                Debug.Log("Menambahkan item ke inventory: " + item.name);
                item.transform.SetParent(slot);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slotComponent.currentItem = item;

                LayoutRebuilder.ForceRebuildLayoutImmediate(slot.GetComponentInParent<RectTransform>());
                return;
            }
        }
        Debug.Log("Inventory penuh!");
    }

}
