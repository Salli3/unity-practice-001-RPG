using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public Item_SO itemSO;
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Image itemImage;

    public int price;

    [SerializeField] private Shop_Manager shopManager;
    [SerializeField] private Shop_Info shopInfo;

    public Button buyButton;

    private void Awake()
    {
        buyButton = GetComponent<Button>();
        buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    public void Initialize(Item_SO newItemSO, int price)
    {
        //fill slot with info
        itemSO = newItemSO;
        itemImage.sprite = itemSO.icon;
        itemNameText.text = itemSO.name;
        this.price = price;
        priceText.text = price.ToString();
    }

    public void OnBuyButtonClicked()
    {
        shopManager.TryBuyItem(itemSO, price);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemSO != null)
        {
            shopInfo.ShowItemInfo(itemSO);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shopInfo.HideItemInfo();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        //if (itemSO != null)
        //{
        //    shopInfo.FollowMouse();
        //}
    }
}
