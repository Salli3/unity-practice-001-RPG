using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO")]
public class Quest_SO : ScriptableObject
{
    public string questName;
    [TextArea] public string questDescription;
    public float questProgress;
}
