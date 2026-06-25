using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO")]
public class Dialog_SO : ScriptableObject
{
    public Dialog_Line[] lines;
    public Dialog_Option[] options;

    [Header("Conditional Requirements (Optional)")]
    public Actor_SO[] requiredNPCs;
    public Location_SO[] requiredLocations;
    public Item_SO[] requiredItems;

    [Header("Control Flags")]
    public bool removeAfterPlay;
    public List<Dialog_SO> removeTheseOnPlay;

    public bool IsConditionMet()
    {
        //Debug.Log(requiredLocations == null ? "required location is null" : "required location is not null");
        if (requiredNPCs.Length > 0)
        {
            foreach (var npc in requiredNPCs)
            {
                //check if history tracker have this npc
                if (Game_Manager.instance.dialogHistoryTracker.HasSpokenWith(npc) == false)
                {
                    return false;
                }
            }
        }
        //Debug.Log("NPC true");

        if (requiredLocations != null && requiredLocations.Length > 0)
        {
            //Debug.Log("Have required location");
            foreach (var location in requiredLocations)
            {
                //check if history tracker have this location
                if (Game_Manager.instance.locationHistoryTracker.HasVisited(location) == false)
                {
                    //Debug.Log("Have not been to: " +  location.locationName);
                    return false;
                }
            }
        }
        //Debug.Log("Location true");

        if(requiredItems != null && requiredItems.Length > 0)
        {
            foreach (var item in requiredItems)
            {
                if (Inventory_Manager.instance.HasItem(item) == false)
                {
                    return false;
                }
            }
        }


        return true;
    }
}

[System.Serializable]
public class Dialog_Line
{
    public Actor_SO speaker;
    [TextArea] public string text;
}

[System.Serializable]
public class Dialog_Option
{
    public string optionText;
    public Dialog_SO nextDialog;
}
