using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

public class Shop_Manager : MonoBehaviour
{
    //[SerializeField] private List<ShopItems> shopItems;

    [SerializeField] private Shop_Slot[] shopSlots;

    [SerializeField] private Inventory_Manager inventoryManager;

    //public static event Action<Shop_Manager, bool> OnShopStateChanged;

    public CanvasGroup shopCanvasGroup;

    private void Start()
    {
        shopCanvasGroup.alpha = 0;
        shopCanvasGroup.blocksRaycasts = false;
        shopCanvasGroup.interactable = false;

    }

    public void PopulateShopItems(List<ShopItems> shopItems)
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Initialize(shopItem.itemSO, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }

        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(Item_SO itemSO, int price)
    {
        if (itemSO != null && inventoryManager.gold >= price)
        {
            //if (HasSpaceForItem(itemSO))
            //{
                inventoryManager.gold -= price;
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                inventoryManager.AddItem(itemSO, 1);
            //}
        }
    }

    public void SellItem(Item_SO itemSO)
    {
        if(itemSO == null)
        {
            return;
        }

        foreach (var slot in shopSlots)
        {
            if (slot.itemSO == itemSO)
            {
                inventoryManager.gold += slot.price;
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                return;
            }
        }
    }

    //private bool HasSpaceForItem(Item_SO itemSO)
    //{
    //    foreach (var slot in inventoryManager.itemSlots)
    //    {
    //        if(slot.itemSO == itemSO && slot.quantity < itemSO.stackSize)
    //        {
    //            return true;
    //        }
    //        else if (slot.itemSO == null)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
}

[System.Serializable]
public class ShopItems
{
    public Item_SO itemSO;
    public int price;
}
