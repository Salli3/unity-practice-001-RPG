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
        Dialog_Manager.instance.EndDialog();
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
            if (Dialog_Manager.instance.isDialogActive == false)
            {
                CheckForNewConversation();
                Dialog_Manager.instance.StartDialog(currentConversation);
            }
        }
    }

    private void CheckForNewConversation()
    {
        for (int i = 0; i < conversations.Count; i++)
        {
            var conversation = conversations[i];
            if (conversation != null && conversation.IsConditionMet())
            {
                conversations.RemoveAt(i);
                currentConversation = conversation;
                return;
            }
        }
    }
}
