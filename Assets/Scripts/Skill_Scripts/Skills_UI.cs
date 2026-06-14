using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills_UI : MonoBehaviour
{
    public CanvasGroup skillsCanvas;
    public Stats_UI statsUI;

    [SerializeField] private RectTransform skillsPanel;


    public bool skillTreeOpen = false;

    private void Start()
    {
        skillsCanvas.alpha = 0;
        skillsCanvas.blocksRaycasts = false;

        skillsPanel.anchoredPosition = new Vector2(0, skillsPanel.anchoredPosition.y);
    }
    void Update()
    {
        if(Input.GetButtonDown("Toggle Skills Panel"))
        {
            //if press when skilltree is open => close it and continue the game
            if (skillTreeOpen)
            {
                Time.timeScale = 1;
                skillsCanvas.alpha = 0;
                skillsCanvas.blocksRaycasts = false;
                skillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                skillsCanvas.alpha = 1;
                skillsCanvas.blocksRaycasts = true;
                skillTreeOpen = true;

                statsUI.statsCanvas.alpha = 0;
                statsUI.statsMenuOpen = false;
            }
        }
    }
}
