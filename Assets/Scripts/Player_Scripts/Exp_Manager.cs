using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exp_Manager : MonoBehaviour
{
    public int level;
    public float currentExp;
    public float expToLevel;
    public float expGrowthMultiplier = 1.2f; //add 20%

    public Slider expSlider;
    public TMP_Text currentLevelText;

    public static event Action<int> OnLevelUp;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExp(2);
        }    
    }

    private void OnEnable()
    {
        Enemy_HP.OnEnemyDefeated += GainExp;
    }

    private void OnDisable()
    {
        Enemy_HP.OnEnemyDefeated -= GainExp;
    }

    public void GainExp(float amount)
    {
        currentExp += amount * Stats_Manager.instance.expGain / 100;
        if (currentExp >= expToLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = expToLevel * expGrowthMultiplier;
        OnLevelUp?.Invoke(1);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level;
    }
}
