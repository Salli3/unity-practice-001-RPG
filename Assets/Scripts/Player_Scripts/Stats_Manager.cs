using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats_Manager : MonoBehaviour
{
    public static Stats_Manager instance;
    public TMP_Text hpText;
    public Stats_UI statsUI;

    [Header("Player Combat Stats")]

    public float attackRange;
    public float damage;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    public float cooldown;
    //TODO crit

    [Header("Player Movement Stats")]

    public float speed;

    [Header("Player Hp Stats")]

    public float currentHP;
    public float maxHP;

    public float expGain;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //change player stats
    public void UpdateCurrentHP(float amount)
    {
        currentHP += amount;
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
        hpText.text = "HP: " + Mathf.RoundToInt(currentHP) + "/" + Mathf.RoundToInt(maxHP);
    }

    public void UpdateMaxHP(float amount)
    {
        maxHP += amount;
        hpText.text = "HP: " + Mathf.RoundToInt(currentHP) + "/" + Mathf.RoundToInt(maxHP);
    }

    public void UpdateDamage(int amount) {damage += amount;}
    public void UpdateExpGain(int amount) {expGain += amount;}
    public void UpdateAttackSpeed(int amount) {cooldown -= (amount/100f*2f);}
    public void UpdateStunDuration(int amount) {stunTime += (amount/100f*0.2f);}
    public void UpdateMovementSpeed(float amount) 
    {
        speed += (amount/100f*5f);
        statsUI.UpdateAllStats();
    }
    //TODO fix stats change of skill, money up and extra life
}
