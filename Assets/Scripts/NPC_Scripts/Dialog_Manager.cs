using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_Manager : MonoBehaviour
{
    public static Dialog_Manager instance;

    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogText;
    public Button[] choiceButtons;

    public bool isDialogActive;

    private Dialog_SO currentDialog;
    private int currentDialogIndex;

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

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void StartDialog(Dialog_SO dialogSO)
    {
        currentDialog = dialogSO;
        currentDialogIndex = 0;
        isDialogActive = true;
        ShowDialog();
    }

    private void ShowDialog()
    {
        if (currentDialogIndex < currentDialog.lines.Length)
        {
            Dialog_Line line = currentDialog.lines[currentDialogIndex];

            Dialog_History_Tracker.instance.RecordNPC(line.speaker);

            portrait.sprite = line.speaker.portrait;
            actorName.text = line.speaker.actorName;
            dialogText.text = line.text;

            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            currentDialogIndex++;

            if (currentDialogIndex == currentDialog.lines.Length)
            {
                ShowChoices();
            }
            else
            {
                choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "Next";
                choiceButtons[0].onClick.AddListener(ShowDialog);
                choiceButtons[0].gameObject.SetActive(true);
            }
        }
    }

    //public void AdvanceDialog()
    //{
    //    if (currentDialogIndex < currentDialog.lines.Length)
    //    {
    //        ShowDialog();
    //    }
    //    else
    //    {
    //        ShowChoices();
    //    }
    //}

    public void EndDialog()
    {
        currentDialogIndex = 0;
        isDialogActive = false;
        ClearChoices();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ShowChoices()
    {
        ClearChoices();
        if(currentDialog.options.Length > 0)
        {
            for(int i = 0; i < currentDialog.options.Length; i++)
            {
                var option = currentDialog.options[i];

                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choiceButtons[i].gameObject.SetActive(true);

                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialog));
            }
        }
        else
        {
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "Goodbye!";
            choiceButtons[0].onClick.AddListener(EndDialog);
            choiceButtons[0].gameObject.SetActive(true);
        }
    }

    private void ClearChoices()
    {
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }

    private void ChooseOption(Dialog_SO dialogSO)
    {
        if (dialogSO == null)
        {
            EndDialog();
        }
        else
        {
            ClearChoices();
            StartDialog(dialogSO);
        }
    }
}
