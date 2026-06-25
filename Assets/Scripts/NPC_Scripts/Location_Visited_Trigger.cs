using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location_Visited_Trigger : MonoBehaviour
{
    [SerializeField] private Location_SO locationVisited;
    [SerializeField] private bool destroyOnTouch = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Game_Manager.instance.locationHistoryTracker.RecordLocation(locationVisited);
            if (destroyOnTouch)
            {
                Destroy(gameObject);
            }
        }
    }
}
