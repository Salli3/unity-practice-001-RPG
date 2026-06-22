using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO")]
public class Dialog_SO : ScriptableObject
{
    public Dialog_Line[] lines;
    public Dialog_Option[] options;

    [Header("Conditional Requirements (Optional)")]
    public Actor_SO[] requiredNPCs;
    public Location_SO[] requiredLocations;

    //TODO:
    //Items

    public bool IsConditionMet()
    {
        if (requiredNPCs.Length > 0)
        {
            foreach (var npc in requiredNPCs)
            {
                //check if history tracker have this npc
                if (Dialog_History_Tracker.instance.HasSpokenWith(npc) == false)
                {
                    return false;
                }
            }
        }
        if (requiredLocations.Length > 0)
        {
            foreach (var location in requiredLocations)
            {
                //check if history tracker have this location
                if (Location_History_Tracker.instance.HasVisited(location) == false)
                {
                    return false;
                }
            }
        }
        //Check for items


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
