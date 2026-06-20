using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    private bool isKnockedBack;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;
    public Player_Combat playerCombat;
    public Player_Bow playerBow;

    private void Update()
    {
        if (Input.GetButtonDown("Attack") && playerCombat.enabled)
        {
            playerCombat.Attack();
        }

        if (Input.GetButtonDown("Ranged Attack") && playerBow.enabled)
        {
            playerBow.DrawBow();
        }
    }

    // FixedUpdate is called 50x per frame
    void FixedUpdate()
    {
        if (Stats_Manager.instance == null)
        {
            return;
        }

        if (isKnockedBack == true)
        {
            return;
        }

        if (playerCombat.isAttacking == true) {
            rb.velocity = Vector2.zero;
            return;
        }

        if (playerBow.isShooting == true) {
            rb.velocity = Vector2.zero;
            return;
        }

        //if (isKnockedBack == false)
        //{
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 ||
                horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            rb.velocity = new Vector2(horizontal, vertical).normalized * Stats_Manager.instance.speed;
        //}
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Knockback(Transform enemy, float knockbackForce, float knockbackTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * knockbackForce;
        StartCoroutine(KnockbackCounter(knockbackTime));
    }
    IEnumerator KnockbackCounter(float knockbackTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
}
