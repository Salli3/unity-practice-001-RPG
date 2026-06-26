using TMPro;
using UnityEngine;

public class Quest_Objective_Slot : MonoBehaviour
{
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private TMP_Text progressText;

    public void RefreshObjectives(string objectiveText, string progressText, bool isComplete)
    {
        this.objectiveText.text = objectiveText;
        this.progressText.text = progressText;


        Color color = isComplete ? Color.gray : Color.white;
        this.objectiveText.color = color;
        this.progressText.color = color;
    }
}
