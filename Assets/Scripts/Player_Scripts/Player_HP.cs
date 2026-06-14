using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_HP : MonoBehaviour
{
    public TMP_Text hpText;
    public Animator hpTextAnim;

    private void Start()
    {
        hpText.text = "HP:\n" + Stats_Manager.instance.currentHP + "/" + Stats_Manager.instance.maxHP;
    }

    public void ChangeHP(float amount)
    {
        Stats_Manager.instance.currentHP -= amount;
        hpTextAnim.Play("HP_Text_Update");
        hpText.text = "HP:\n" + Stats_Manager.instance.currentHP + "/" + Stats_Manager.instance.maxHP;

        if (Stats_Manager.instance.currentHP <= 0)
        {
            gameObject.SetActive(false);
        }

    }
}
