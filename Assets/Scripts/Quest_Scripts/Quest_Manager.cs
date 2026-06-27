using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour
{
    private Dictionary<Quest_SO, Dictionary<Quest_Objective, int>> questProgress = new();

    private void OnEnable()
    {
        Quest_Event.IsQuestCompleted += IsQuestComplete;
    }

    private void OnDisable()
    {
        Quest_Event.IsQuestCompleted -= IsQuestComplete;
    }

    #region Quest accept logic
    public bool IsQuestAccepted(Quest_SO questSO)
    {
        return questProgress.ContainsKey(questSO);
    }

    public List<Quest_SO> GetActiveQuests()
    {
        return new List<Quest_SO>(questProgress.Keys);
    }

    public void AcceptQuest(Quest_SO questSO)
    {
        questProgress[questSO] = new Dictionary<Quest_Objective, int>();

        foreach (var objective in questSO.objectives)
        {
            UpdateObjectiveProgress(questSO, objective);
        }
    }
    #endregion

    #region Quest complete methods
    public bool IsQuestComplete(Quest_SO questSO)
    {
        if (questProgress.TryGetValue(questSO, out var progressDictionary) == false)
        {
            return false;
        }

        foreach (var objective in questSO.objectives)
        {
            UpdateObjectiveProgress(questSO, objective);
        }

        foreach (var objective in questSO.objectives)
        {
            if (progressDictionary[objective] < objective.requiredAmount)
            {
                return false;
            }
        }

        return true;
    }

    public void CompleteQuest(Quest_SO questSO)
    {
        questProgress.Remove(questSO);
        //TODO granting reward
    }
    #endregion

    public void UpdateObjectiveProgress(Quest_SO questSO, Quest_Objective objective)
    {
        if (!questProgress.ContainsKey(questSO))
        {
            return;
        }

        var progressDictionary = questProgress[questSO];
        int newAmount = 0;

        if (objective.targetItem != null)
        {
            newAmount = Inventory_Manager.instance.GetItemQuantity(objective.targetItem);
        }
        else if (objective.targetLocation != null && Game_Manager.instance.locationHistoryTracker.HasVisited(objective.targetLocation))
        {
            newAmount = objective.requiredAmount;
        }
        else if (objective.targetNPC != null && Game_Manager.instance.dialogHistoryTracker.HasSpokenWith(objective.targetNPC))
        {
            newAmount = objective.requiredAmount;
        }

        progressDictionary[objective] = newAmount;
    }

    public string GetProgressText(Quest_SO questSO, Quest_Objective objective)
    {
        int currentAmount = GetCurrentAmount(questSO, objective);

        if (currentAmount >= objective.requiredAmount)
        {
            return "Complete";
        }
        else if (objective.targetItem != null)
        {
            return $"{currentAmount}/{objective.requiredAmount}";
        }
        else
        {
            return "Incomplete";
        }
    }

    public int GetCurrentAmount(Quest_SO questSO, Quest_Objective objective)
    {
        if (questProgress.TryGetValue(questSO, out var objectiveDictionary))
        {
            if (objectiveDictionary.TryGetValue(objective, out int amount))
            {
                return amount;
            }
        }
        return 0;
    }
}
