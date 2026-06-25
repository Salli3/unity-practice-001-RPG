using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;
    public List<Dialog_SO> conversations;
    public Dialog_SO currentConversation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        anim.Play("Idle");
        interactAnim.Play("Open");
    }

    private void OnDisable()
    {
        interactAnim.Play("Close");
        Game_Manager.instance.dialogManager.EndDialog();
    }

    private void Update()
    {
        rb.velocity = Vector2.zero;
        if (Input.GetButtonDown("Interact"))
        {
            //if (Dialog_Manager.instance.isDialogActive)
            //{
            //    //Dialog_Manager.instance.AdvanceDialog();
            //    Dialog_Manager.instance.ShowDialog();
            //}
            //else
            //{
            //    Dialog_Manager.instance.StartDialog(dialogSO);
            //}
            if (Game_Manager.instance.dialogManager.isDialogActive == false)
            {
                CheckForNewConversation();
                Game_Manager.instance.dialogManager.StartDialog(currentConversation);
            }
        }
    }

    private void CheckForNewConversation()
    {
        Debug.Log("Checking for new convo");
        for (int i = 0; i < conversations.Count; i++)
        {
            Debug.Log("Start for loop");
            var conversation = conversations[i];
            Debug.Log("Check convo: " + conversation.name);
            if (conversation != null && conversation.IsConditionMet())
            {
                currentConversation = conversation;

                //remove if one time only
                if (conversation.removeAfterPlay)
                {
                    conversations.RemoveAt(i);
                }

                if (conversation.removeTheseOnPlay != null && conversation.removeTheseOnPlay.Count > 0)
                {
                    foreach (var toRemove in conversation.removeTheseOnPlay)
                    {
                        conversations.Remove(toRemove);
                    }    
                }

                break;
            }
        }
    }
}
