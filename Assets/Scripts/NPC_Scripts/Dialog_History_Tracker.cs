using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_History_Tracker : MonoBehaviour
{
    private readonly HashSet<Actor_SO> spokenNPCs = new HashSet<Actor_SO>();

    public void RecordNPC(Actor_SO actorSO)
    {
        spokenNPCs.Add(actorSO);

        Debug.Log("Just spoke to " +  actorSO.actorName);
    }

    public bool HasSpokenWith(Actor_SO actorSO)
    {
        return spokenNPCs.Contains(actorSO);
    }
}
