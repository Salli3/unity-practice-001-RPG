using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Board : MonoBehaviour
{
    public static Quest_Board currentQuestBoard;

    [SerializeField] private Quest_SO questToOffer;
    [SerializeField] private Quest_SO questToTurnIn;

    private bool playerInRange;
    private bool isQuestBoardOpen = false;

    public Quest_Log_UI questLogUI;
    //[SerializeField] private List<Quest_SO> questToOffers;

    private void Update()
    {
        //if (playerInRange && Input.GetButtonDown("Interact"))
        //{
        //    bool canTurnIn = questToTurnIn != null && Quest_Event.IsQuestCompleted?.Invoke(questToTurnIn) == true;

        //    if (canTurnIn)
        //    {
        //        Quest_Event.OnQuestTurnInRequested?.Invoke(questToTurnIn);
        //    }
        //    else
        //    {
        //        Quest_Event.OnQuestOfferRequested?.Invoke(questToOffer);
        //    }
        //}

        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!isQuestBoardOpen)
                {
                    Time.timeScale = 0;
                    currentQuestBoard = this;
                    isQuestBoardOpen = true;
                    bool canTurnIn = questToTurnIn != null && Quest_Event.IsQuestCompleted?.Invoke(questToTurnIn) == true;
                    if (canTurnIn)
                    {
                        Quest_Event.OnQuestTurnInRequested?.Invoke(questToTurnIn);
                    }
                    else
                    {
                        Quest_Event.OnQuestOfferRequested?.Invoke(questToOffer);
                    }
                }
                else
                {
                    Time.timeScale = 1;
                    currentQuestBoard = null;
                    isQuestBoardOpen = false;
                    questLogUI.OnCloseQuestButtonClicked();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
