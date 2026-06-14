using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Slot : MonoBehaviour
{
    public Skill_SO skillSO;
    public List<Skill_Slot> prerequisiteSkillSlots;
    
    public int currentLevel;
    public bool isUnlocked;

    public Image skillIcon;
    public TMP_Text skillLevelText;
    public Button skillButton;

    public static event Action<Skill_Slot> OnSkillPointSpent;
    public static event Action<Skill_Slot> OnSkillMax;

    private void OnValidate()
    {
        if (skillSO != null && skillLevelText != null)
        { 
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;

        if (isUnlocked && currentLevel < skillSO.maxLevel)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else if (currentLevel == skillSO.maxLevel)
        {
            skillButton.interactable = false;
            skillLevelText.text = "Maxed";
            skillIcon.color = Color.magenta;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.grey;
        }
    }

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel)
        {
            currentLevel++;
            OnSkillPointSpent?.Invoke(this);

            if(currentLevel >= skillSO.maxLevel)
            {
                OnSkillMax?.Invoke(this);
            }

            UpdateUI();          
        }
    }

    public bool CanUnlockSkill()
    {
        foreach (Skill_Slot slot in prerequisiteSkillSlots)
        {
            if (!slot.isUnlocked || slot.currentLevel < slot.skillSO.maxLevel)
            {
                return false;
            }
        }
        return true;
    }

    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }
}
