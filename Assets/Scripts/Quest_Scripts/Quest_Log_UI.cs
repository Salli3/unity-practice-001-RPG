using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Log_UI : MonoBehaviour
{
    [SerializeField] private Quest_Manager questManager;

    public void HandleQuestClicked(Quest_SO questSO)
    {
        Debug.Log($"Clicked quest: {questSO.questName}");

        foreach (var objective in questSO.objectives)
        {
            questManager.UpdateObjectiveProgress(questSO, objective); //update actual progress value
            Debug.Log($"Objective: {objective.description}\n Current progress: {questManager.GetProgressText(questSO, objective)}");
        }
    }
}
