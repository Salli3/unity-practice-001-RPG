using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public float damage;
    public float attackRange;
    public LayerMask playerLayer;
    public float knockbackForce;
    public float knockbackTime;

    public Transform attackPoint;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<Player_HP>().ChangeHP(damage);
    //    }
    //}

    void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        if (hits.Length > 0)
        {
            hits[0].GetComponent<Player_HP>().ChangeHP(damage);
            hits[0].GetComponent<Player_Movement>().Knockback(transform, knockbackForce, knockbackTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
