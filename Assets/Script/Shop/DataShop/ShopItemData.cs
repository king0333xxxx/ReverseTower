using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Shop Item Data")]
public class ShopItemData : ScriptableObject
{
    /// <summary>
    /// Pastikan nanti kamu isi itemID di Inspector Unity, misalnya: "troop_swordman", "hero_knight", dst.
    /// </summary>

    public string itemID; // <-- Tambahkan ini
    public GameObject itemPrefab;
    public string itemName;
    public int itemPrice;
    // Tambahkan field lain kalau ada
}
