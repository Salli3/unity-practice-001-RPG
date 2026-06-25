using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour
{
    private Dictionary<Quest_SO, Dictionary<Quest_Objective, int>> questProgress = new();

    public void UpdateObjectiveProgress(Quest_SO questSO, Quest_Objective objective)
    {
        if (!questProgress.ContainsKey(questSO))
        {
            questProgress[questSO] = new Dictionary<Quest_Objective, int>();

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
        if(questProgress.TryGetValue(questSO, out var objectiveDictionary))
        {
            if (objectiveDictionary.TryGetValue(objective, out int amount))
            {
                return amount;
            }
        }
        return 0;
    }
}
