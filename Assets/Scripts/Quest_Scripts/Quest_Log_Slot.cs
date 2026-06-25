using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Log_Slot : MonoBehaviour
{
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text questProgressText;

    public Quest_SO currentQuest;
    public Quest_Log_UI questLogUI;
    public Button questButton;

    private void Awake()
    {
        questButton = GetComponent<Button>();
        questButton.onClick.AddListener(OnSlotClicked);
    }

    private void OnValidate()
    {
        if (currentQuest != null)
        {
            SetQuest(currentQuest);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetQuest(Quest_SO questSO)
    {
        currentQuest = questSO;

        questNameText.text = questSO.questName;
        //questProgressText.text = questSO.questProgress.ToString() + "%";
        questProgressText.text = (float)Math.Round(questSO.questProgress, 2) + "%";

        gameObject.SetActive(true);
    }

    public void OnSlotClicked()
    {
        Debug.Log("Quest button clicked");
        questLogUI.HandleQuestClicked(currentQuest);
    }
}
