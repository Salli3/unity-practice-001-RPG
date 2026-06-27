using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Board : MonoBehaviour
{
    [SerializeField] private Quest_SO questToOffer;
    [SerializeField] private Quest_SO questToTurnIn;

    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetButtonDown("Interact"))
        {
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
