using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest_Log_Slots : MonoBehaviour
{
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text questProgressText;

    public Quest_SO currentQuest;

    public void SetQuest(Quest_SO questSO)
    {
        currentQuest = questSO;

        questNameText.text = questSO.questName;
        questProgressText.text = questSO.questProgress.ToString("F2") + "%";
    }
}
