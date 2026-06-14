using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item_SO : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite icon;

    public bool isGold;
    //public int stackSize = 3;

    [Header("Stats")]

    public float attackRange;
    public float damage;
    public float knockbackForce;
    public float stunTime;
    public float cooldown;
    public float speed;
    public float currentHP;
    public float maxHP;
    public float expGain;

    [Header("For Temporary Items")]

    public float durration;
}
