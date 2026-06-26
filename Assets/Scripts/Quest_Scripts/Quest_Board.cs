using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Board : MonoBehaviour
{
    [SerializeField] private Quest_SO questToOffer;
    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            Quest_Event.OnQuestOfferRequested?.Invoke(questToOffer);
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
