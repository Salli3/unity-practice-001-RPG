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
    [SerializeField] private Quest_SO noAvailableQuestSO;

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
        Quest_Event.OnQuestTurnInRequested += ShowQuestTurnIn;
    }

    private void OnDisable()
    {
        Quest_Event.OnQuestOfferRequested -= ShowQuestOffer;
        Quest_Event.OnQuestTurnInRequested -= ShowQuestTurnIn;
    }

    #region Show quest methods
    public void ShowQuestOffer(Quest_SO incomingQuestSO)
    {
        if (questManager.IsQuestAccepted(incomingQuestSO))
        {
            questSO = noAvailableQuestSO;

            SetCanvasState(acceptCanvas, false);
            SetCanvasState(declineCanvas, true);
            SetCanvasState(completeCanvas, false);
        }
        else
        {
            questSO = incomingQuestSO;
            SetCanvasState(acceptCanvas, true);
            SetCanvasState(declineCanvas, true);
            SetCanvasState(completeCanvas, false);
        }
        HandleQuestClicked(questSO);
        SetCanvasState(questCanvas, true);
    }

    public void ShowQuestTurnIn(Quest_SO incomingQuestSO)
    {
        Debug.Log($"Show quest turning in: {incomingQuestSO}");
        questSO = incomingQuestSO;
        HandleQuestClicked(questSO);

        SetCanvasState(questCanvas, true);
        SetCanvasState(acceptCanvas, false);
        SetCanvasState(declineCanvas, false);
        SetCanvasState(completeCanvas, true);
    }
    #endregion

    #region On button clicked methods
    public void OnAcceptQuestClicked()
    {
        questManager.AcceptQuest(questSO);
        SetCanvasState(completeCanvas, false);
        SetCanvasState(acceptCanvas, false);
        RefreshQuestList();
        HandleQuestClicked(noAvailableQuestSO);
    }

    public void OnDeclineQuestClicked()
    {
        SetCanvasState(questCanvas, false);
    }

    public void OnCompleteQuestClicked()
    {
        questManager.CompleteQuest(questSO);

        RefreshQuestList();
    }
    #endregion

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

    #region Quest info panel methods
    //display quest info on info panel
    public void HandleQuestClicked(Quest_SO questSO)
    {
        this.questSO = questSO;

        questNameText.text = questSO.questName;
        questDescriptionText.text = questSO.questDescription;

        DisplayObjectives();
        DisplayReward();
    }

    //display objectives on info panel
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
    #endregion
}
