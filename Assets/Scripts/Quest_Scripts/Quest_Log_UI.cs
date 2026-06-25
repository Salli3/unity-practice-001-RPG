using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Log_UI : MonoBehaviour
{
    public void HandleQuestClicked(Quest_SO questSO)
    {
        Debug.Log($"Clicked quest: {questSO.questName}");

        foreach (var objective in questSO.objectives)
        {
            Debug.Log($"Objective: {objective.description}");
        }
    }
}
