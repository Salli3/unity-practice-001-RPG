using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;
    public Dialog_SO dialogSO;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        rb.velocity = Vector2.zero;
        if (Input.GetButtonDown("Interact"))
        {
            if (Dialog_Manager.instance.isDialogActive)
            {
                //Dialog_Manager.instance.AdvanceDialog();
                Dialog_Manager.instance.ShowDialog();
            }
            else
            {
                Dialog_Manager.instance.StartDialog(dialogSO);
            }
        }
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
}
