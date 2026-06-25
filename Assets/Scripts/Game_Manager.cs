using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public Dialog_Manager dialogManager;
    public Dialog_History_Tracker dialogHistoryTracker;
    public Location_History_Tracker locationHistoryTracker;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
        else
        {
            CleanUpAndDestroy();
            return;
        }
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUpAndDestroy()
    {
        foreach(GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        Destroy(gameObject);
    }
}
