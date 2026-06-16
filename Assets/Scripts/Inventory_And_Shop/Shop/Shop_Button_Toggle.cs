using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Button_Toggle : MonoBehaviour
{
    public void OpenItemShop()
    {
        if (Shopkeeper.currentShopkeeper != null)
        {
            Shopkeeper.currentShopkeeper.OpenItemShop();
        }
    }

    public void OpenWeaponShop()
    {
        if (Shopkeeper.currentShopkeeper != null)
        {
            Shopkeeper.currentShopkeeper.OpenWeaponShop();
        }
    }

    public void OpenArmourShop()
    {
        if (Shopkeeper.currentShopkeeper != null)
        {
            Shopkeeper.currentShopkeeper.OpenArmourShop();
        }
    }
}
