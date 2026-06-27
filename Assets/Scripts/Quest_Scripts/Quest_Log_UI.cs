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
    [SerializeField] private Quest_Reward_Slot[] rewardSlots;

    [SerializeField] private Quest_Log_Slot[] questSlots;

    private Quest_SO questSO;

    [SerializeField] private CanvasGroup questCanvas;

    [SerializeField] private CanvasGroup acceptCanvas;
    [SerializeField] private CanvasGroup declineCanvas;
    [SerializeField] private CanvasGroup completeCanvas;

    private void Awake()
    {
        questCanvas.alpha = 0;
        questCanvas.blocksRaycasts = false;
        questCanvas.interactable = false;
    }

    private void OnEnable()
    {
        Quest_Event.OnQuestOfferRequested += ShowQuestOffer;
    }

    private void OnDisable()
    {
        Quest_Event.OnQuestOfferRequested -= ShowQuestOffer;
    }

    public void ShowQuestOffer(Quest_SO incomingQuestSO)
    {
        HandleQuestClicked(incomingQuestSO);
        SetCanvasState(questCanvas, true);

        SetCanvasState(acceptCanvas, true);
        SetCanvasState(declineCanvas, true);
        SetCanvasState(completeCanvas, false);
    }

    public void OnAcceptQuestClicked()
    {
        questManager.AcceptQuest(questSO);
        SetCanvasState(completeCanvas, false);
        SetCanvasState(acceptCanvas, false);
        RefreshQuestList();
    }

    public void OnDeclineQuestClicked()
    {
        SetCanvasState(questCanvas, false);
    }

    public void OnCompleteQuestClicked()
    {
        RefreshQuestList();
    }

    public void RefreshQuestList()
    {
        List<Quest_SO> activeQuests = questManager.GetActiveQuests();

        for (int i = 0; i < questSlots.Length; i++)
        {
            if (i < activeQuests.Count)
            {
                questSlots[i].SetQuest(activeQuests[i]);
            }
            else
            {
                questSlots[i].ClearSlot();
            }
        }
    }

    private void SetCanvasState(CanvasGroup canvasGroup, bool activate)
    {
        canvasGroup.alpha = activate ? 1 : 0;
        canvasGroup.blocksRaycasts = activate;
        canvasGroup.interactable = activate;
    }

    public void HandleQuestClicked(Quest_SO questSO)
    {
        this.questSO = questSO;

        questNameText.text = questSO.questName;
        questDescriptionText.text = questSO.questDescription;

        DisplayObjectives();
        DisplayReward();
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

    private void DisplayReward()
    {
        for (int i = 0; i < rewardSlots.Length; i++)
        {
            if (i < questSO.rewards.Count)
            {
                var reward = questSO.rewards[i];
                rewardSlots[i].gameObject.SetActive(true);
                rewardSlots[i].DisplayReward(reward.item_SO.icon, reward.quantity);
            }
            else
            {
                rewardSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
