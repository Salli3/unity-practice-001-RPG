using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Reward_Slot : MonoBehaviour
{
    public Image rewardImage;
    public TMP_Text rewardQuantity;

    public void DisplayReward(Sprite sprite, int quantity)
    {
        rewardImage.sprite = sprite;
        rewardQuantity.text = quantity.ToString();
    }
}
