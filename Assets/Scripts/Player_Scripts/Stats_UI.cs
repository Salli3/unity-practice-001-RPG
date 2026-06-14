using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats_UI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;
    public Skills_UI skillsUI;

    [SerializeField] private RectTransform statsPanel;

    public bool statsMenuOpen = false;

    private void Start()
    {
        UpdateAllStats();
        statsCanvas.alpha = 0;

        statsPanel.anchoredPosition = new Vector2(0, statsPanel.anchoredPosition.y);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Toggle Stats Panel"))
        {
            //if press when stats menu is open => close it and continue the game
            if (statsMenuOpen)
            {
                Time.timeScale = 1;                
                statsCanvas.alpha = 0;
                statsMenuOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsMenuOpen = true;

                skillsUI.skillsCanvas.alpha = 0;
                skillsUI.skillsCanvas.blocksRaycasts = false;
                skillsUI.skillTreeOpen = false;
            }
        }
    }

    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + (float)Math.Round(Stats_Manager.instance.damage, 2);
    }
    public void UpdateAttackSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "ATK Speed: " + (float)Math.Round(Stats_Manager.instance.cooldown, 2);
    }
    public void UpdateSpeed()
    {
        statsSlots[2].GetComponentInChildren<TMP_Text>().text = "Speed: " + (float)Math.Round(Stats_Manager.instance.speed, 2);
    }
    public void UpdateExpGain()
    {
        statsSlots[3].GetComponentInChildren<TMP_Text>().text = "Exp Gain: " + (float)Math.Round(Stats_Manager.instance.expGain, 2);
    }
    public void UpdateKnockback()
    {
        statsSlots[4].GetComponentInChildren<TMP_Text>().text = "Knockback: " + (float)Math.Round(Stats_Manager.instance.knockbackForce, 2);
    }
    public void UpdateRange()
    {
        statsSlots[5].GetComponentInChildren<TMP_Text>().text = "Range: " + (float)Math.Round(Stats_Manager.instance.attackRange, 2);
    }
    //public void UpdateSpeed()
    //{
    //    statsSlots[6].GetComponentInChildren<TMP_Text>().text = "Speed: " + (float)Math.Round(Stats_Manager.instance.speed, 2);
    //}
    //public void UpdateSpeed()
    //{
    //    statsSlots[7].GetComponentInChildren<TMP_Text>().text = "Speed: " + (float)Math.Round(Stats_Manager.instance.speed, 2);
    //}

    //TODO the rest of the stats

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
        UpdateAttackSpeed();
        UpdateExpGain();
        UpdateKnockback();
        UpdateRange();

    }
}
