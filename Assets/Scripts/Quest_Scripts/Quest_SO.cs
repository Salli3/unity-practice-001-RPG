using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO")]
public class Quest_SO : ScriptableObject
{
    public string questName;
    [TextArea] public string questDescription;
    public float questProgress;

    public List<Quest_Objective> objectives;
    public List<Quest_Reward> rewards;
}

[System.Serializable]
public class Quest_Objective
{
    public string description;

    [SerializeField] private Object target;
    public Item_SO targetItem => target as Item_SO;
    public Actor_SO targetNPC => target as Actor_SO;
    public Location_SO targetLocation => target as Location_SO;

    //TODO enemy

    public int requiredAmount;
    //public int currentAmount;
}

[System.Serializable]
public class Quest_Reward
{
    public Item_SO item_SO;
    public int quantity;
}
