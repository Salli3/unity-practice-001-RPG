using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Shop_Info : MonoBehaviour
{
    public CanvasGroup infoPanel;

    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    [Header ("Stats Fields")]
    public TMP_Text[] statTexts;

    private RectTransform infoPanelRect;

    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
        HideItemInfo();
    }

    public void ShowItemInfo(Item_SO itemSO)
    {
        //infoPanel.alpha = 1;
        itemNameText.text = itemSO.name;
        itemDescriptionText.text = itemSO.itemDescription;

        List<string> stats = new List<string>();

        if (itemSO.currentHP > 0)
        {
            stats.Add("Heal: " + itemSO.currentHP.ToString());
        }
        if (itemSO.maxHP > 0)
        {
            stats.Add("HP: " + itemSO.maxHP.ToString());
        }
        if (itemSO.damage > 0)
        {
            stats.Add("DMG: " + itemSO.damage.ToString());
        }
        if (itemSO.speed > 0)
        {
            stats.Add("SPD: " + itemSO.speed.ToString());
        }
        if (itemSO.cooldown > 0)
        {
            stats.Add("CD: " + itemSO.cooldown.ToString());
        }
        if (itemSO.expGain > 0)
        {
            stats.Add("EXPG: " + itemSO.expGain.ToString());
        }
        if (stats.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < statTexts.Length; i++)
        {
            if (i < stats.Count)
            {
                statTexts[i].text = stats[i].ToString();
                statTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statTexts[i].gameObject.SetActive(false);
            }
        }
    }

    public void HideItemInfo()
    {
        //infoPanel.alpha = 0;
        itemNameText.text = "";
        itemDescriptionText.text = "";

        //disable info in info panel
        for (int i = 0; i < statTexts.Length; i++)
        {
            statTexts[i].gameObject.SetActive(false);
        }
    }

    //public void FollowMouse()
    //{
    //    Vector3 mousePosition = Input.mousePosition;
    //    Vector3 offset = new Vector3(10, -10, 0);
    //    infoPanelRect.position = mousePosition + offset;
    //}
}
