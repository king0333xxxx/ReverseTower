using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private CanvasGroup canvasGroup;

    public int goldCost = 10;
    public int spiritCost = 5;
    private bool isOwned = false;

    private PlayerManager playerManager;
    private ShopItem shopItem;

    public GameObject prefabTroop; // Ini prefab pasukan yang akan di-spawn

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        playerManager = FindObjectOfType<PlayerManager>();
        shopItem = GetComponent<ShopItem>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Slot dropSlot = GetDropSlot(eventData);
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null && dropSlot.transform.parent.name == "ContentInventory")
        {

            if (!isOwned)
            {
                // Beli kartu baru, tambahkan troop ke TroopHolder
                if (!dropSlot.hasItem)
                {
                    if (playerManager.GoldCoin < goldCost || playerManager.SpiritCoin < spiritCost)
                    {
                        Debug.Log("Tidak cukup resource!");
                        ResetPosition();
                        return;
                    }

                    playerManager.ReduceGold(goldCost);
                    playerManager.ReduceSpirit(spiritCost);
                    isOwned = true;

                    FindObjectOfType<ShopInventoryController>().CreateShopItem(shopItem.GetData());

                    //  Update slot ke slot baru setelah pembelian
                    Slot newSlot = dropSlot;
                    shopItem.SetSlot(newSlot);
                    Debug.Log($" Item dipindahkan ke slot: {dropSlot.name} di {dropSlot.transform.parent.name}");

                    //  Tambahkan troop ke TroopHolder setelah membeli kartu
                    TroopHolder troopHolder = FindObjectOfType<TroopHolder>();
                    if (troopHolder != null)
                    {
                        Debug.Log($" Cek prefabTroop sebelum AddTroop: {(prefabTroop != null ? prefabTroop.name : "NULL")}");

                        troopHolder.AddTroop(prefabTroop, 1);
                        Debug.Log($"Troop {prefabTroop.name} berhasil ditambahkan ke troopList!");
                        troopHolder.GetTroops();

                    }
                    else
                    {
                        Debug.LogError("TroopHolder tidak ditemukan di scene!");
                    }
                }
                else
                {
                    Debug.Log("Slot sudah terisi! Harus ke slot kosong.");
                    ResetPosition();
                }
            }

            // Swap posisi kartu di inventory tanpa menambah troop baru
            if (dropSlot.currentItem != null)
            {
                ItemDragHandler otherItemHandler = dropSlot.currentItem.GetComponent<ItemDragHandler>();

                if (isOwned && otherItemHandler != null && otherItemHandler.isOwned)
                {
                    GameObject previousItem = dropSlot.currentItem;
                    previousItem.transform.SetParent(originalSlot.transform);
                    previousItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    originalSlot.currentItem = previousItem;
                }
                else
                {
                    Debug.Log("Salah satu item belum dimiliki! Tidak bisa swap.");
                    ResetPosition();
                    return;
                }
            }
            else
            {
                originalSlot.currentItem = null;
            }

            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // **Update urutan pasukan berdasarkan urutan slot inventory**
            //UpdateTroopOrder();
        }
        else
        {
            Debug.Log("Hanya bisa dipindahkan ke Inventory Player!");
            ResetPosition();
        }
    }


    private Slot GetDropSlot(PointerEventData eventData)
    {
        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>();

        if (dropSlot == null && eventData.pointerEnter != null)
        {
            dropSlot = eventData.pointerEnter.GetComponentInParent<Slot>();
        }

        return dropSlot;
    }

    private void ResetPosition()
    {
        transform.SetParent(originalParent);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
