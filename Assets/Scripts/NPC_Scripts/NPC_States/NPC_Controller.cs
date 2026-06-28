using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    public NPCState currentState = NPCState.Idle;
    public NPCState defaultState;

    public NPC_Patrol patrol;
    public NPC_Wander wander;
    public NPC_Talk talk;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultState = currentState;
        ChangeState(currentState);
    }

    public void ChangeState(NPCState newState)
    {
        currentState = newState;

        if(currentState == NPCState.Idle)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic= false;
        }

            patrol.enabled = newState == NPCState.Patrol;
        wander.enabled = newState == NPCState.Wander;
        talk.enabled = newState == NPCState.Talk;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeState(NPCState.Talk);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeState(defaultState);
        }
    }

    public enum NPCState
    {
        Default,
        Idle,
        Patrol,
        Wander,
        Talk
    }
}
