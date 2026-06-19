using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO")]
public class Dialog_SO : ScriptableObject
{
    public DialogLine[] lines;
    public DialogOption[] options;
}

[System.Serializable]
public class DialogLine
{
    public Actor_SO speaker;
    [TextArea] public string text;
}

[System.Serializable]
public class DialogOption
{
    public string optionText;
    public Dialog_SO nextDialog;
}
