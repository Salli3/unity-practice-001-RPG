using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location_History_Tracker : MonoBehaviour
{
    public static Location_History_Tracker instance;

    //HashSet dont allow dup but also dont have order record
    private readonly HashSet<Location_SO> locationsVisited = new HashSet<Location_SO>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RecordLocation(Location_SO locationSO)
    {
        locationsVisited.Add(locationSO);

        Debug.Log("Just been to " + locationSO.locationName);
    }

    public bool HasVisited(Location_SO locationSO)
    {
        Debug.Log("Checked: " + locationSO.locationName);
        return locationsVisited.Contains(locationSO);
    }
}
