using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public Animator anim;
    
    public bool isAttacking;
    private float timer;

    private void Update()
    {
            timer -= Time.deltaTime;
    }
    public void Attack()
    {
        if (timer <= 0) 
        {
            anim.SetBool("isAttacking", true); 
            isAttacking = true;
            timer = Stats_Manager.instance.cooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, Stats_Manager.instance.attackRange, enemyLayer);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_HP>().ChangeHP(Stats_Manager.instance.damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, Stats_Manager.instance.knockbackForce, Stats_Manager.instance.knockbackTime, Stats_Manager.instance.stunTime);
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
        isAttacking = false;
    }

     private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, Stats_Manager.instance.attackRange);
    }
}
