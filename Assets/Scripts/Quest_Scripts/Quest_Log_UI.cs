using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest_Log_UI : MonoBehaviour
{
    [SerializeField] private Quest_Manager questManager;

    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text questDescriptionText;

    [SerializeField] private Quest_Objective_Slot[] objectiveSlots;

    private Quest_SO questSO;

    public void HandleQuestClicked(Quest_SO questSO)
    {
        this.questSO = questSO;

        questNameText.text = questSO.questName;
        questDescriptionText.text = questSO.questDescription;

        DisplayObjectives();

        foreach (var objective in questSO.objectives)
        {
            Debug.Log($"Objective: {objective.description}\n Current progress: {questManager.GetProgressText(questSO, objective)}");
        }
    }

    private void DisplayObjectives()
    {
        for (int i = 0; i < objectiveSlots.Length; i++)
        {
            if (i < questSO.objectives.Count)
            {
                var objective = questSO.objectives[i];
                questManager.UpdateObjectiveProgress(questSO, objective); //update actual progress value

                int currentAmount = questManager.GetCurrentAmount(questSO, objective);
                string progress = questManager.GetProgressText(questSO, objective);
                bool isComplete = currentAmount >= objective.requiredAmount;

                objectiveSlots[i].gameObject.SetActive(true);
                objectiveSlots[i].RefreshObjectives(objective.description, progress, isComplete);
            }
            else
            {
                objectiveSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
