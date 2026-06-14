using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class Skill_Tree_Manager : MonoBehaviour
{
    public Skill_Slot[] skillSlots;
    public TMP_Text skillPointText;

    public int availablePoints;

    private void OnEnable()
    {
        Skill_Slot.OnSkillPointSpent += HandleSkillPointSpent;
        Skill_Slot.OnSkillMax += HandleSkillMaxed;
        Exp_Manager.OnLevelUp += UpdateSkillPoints;
    }

    private void OnDisable()
    {
        Skill_Slot.OnSkillPointSpent -= HandleSkillPointSpent;
        Skill_Slot.OnSkillMax -= HandleSkillMaxed;
        Exp_Manager.OnLevelUp -= UpdateSkillPoints;
    }

    private void Start()
    {
        foreach (Skill_Slot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(() => CheckAvailablePoints(slot)); 
        }
        UpdateSkillPoints(0);
    }

    private void CheckAvailablePoints(Skill_Slot slot)
    {
        if (availablePoints > 0)
        {
            slot.TryUpgradeSkill();
        }
            
    }

    private void HandleSkillPointSpent(Skill_Slot skillSlot)
    {
        if (availablePoints > 0)
        {
            UpdateSkillPoints(-1);
        }
    }

    private void HandleSkillMaxed(Skill_Slot skillSlot)
    {
        foreach (Skill_Slot slot in skillSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlock();
            }
        }
    }

    public void UpdateSkillPoints(int amount)
    {
        availablePoints += amount;
        skillPointText.text = "SkillPoint: " + availablePoints.ToString("D2");
    }
}
