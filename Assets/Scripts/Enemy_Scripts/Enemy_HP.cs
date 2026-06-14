using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HP : MonoBehaviour
{
    public float currentHP;
    public float maxHP;

    public float expReward;
    public delegate void EnemyDefeated(float exp);
    public static event EnemyDefeated OnEnemyDefeated;

    public void Start()
    {
        currentHP = maxHP;
    }

    public void ChangeHP(float amount)
    {
        currentHP -= amount;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        else if (currentHP <= 0)
        {
            OnEnemyDefeated(expReward);
            Destroy(gameObject);
        }
    }
}
