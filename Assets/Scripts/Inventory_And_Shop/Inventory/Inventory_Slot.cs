using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Inventory_Slot : MonoBehaviour, IPointerClickHandler
{
    public Item_SO itemSO;
    public int quantity;

    public Image itemImage;
    public TMP_Text quantityText;
    public static Shop_Manager activeShop;

    private Inventory_Manager inventoryManager;

    //private void Awake()
    //{
    //    inventoryManager = GetComponentInParent<Inventory_Manager>();
    //}

    public void Initialize(Inventory_Manager manager)
    {
        inventoryManager = manager;
    }

    public void OnEnable()
    {
        Shopkeeper.OnShopStateChanged += HandleShopStateChanged;
    }

    public void OnDisable()
    {
        Shopkeeper.OnShopStateChanged -= HandleShopStateChanged;
    }

    public void HandleShopStateChanged(Shop_Manager shopManager, bool isOpen)
    {
        activeShop = isOpen ? shopManager : null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (activeShop != null)
                {
                    activeShop.SellItem(itemSO);
                    quantity--;
                    UpdateUI();
                }
                else
                {
                    if (itemSO.currentHP > 0 && Stats_Manager.instance.currentHP >= Stats_Manager.instance.maxHP)
                    {
                        return;
                    }
                    inventoryManager.UseItem(this);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                Debug.Log($"Dropping item: {this}");
                inventoryManager.DropItem(this);
            }
        }
    }

    public void UpdateUI()
    {
        if (itemSO != null && quantity > 0)
        {
            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString("D2");
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";

            inventoryManager.itemSlots.Remove(this);
            Destroy(gameObject);
        }
    }
}
