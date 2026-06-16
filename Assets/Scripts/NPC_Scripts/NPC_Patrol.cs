using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{
    public Vector2[] patrolPoints;
    public float speed = 2;
    public float pauseDuration = 1.5f;

    private bool isPaused;
    private Vector2 target;
    private int currentPatrolIndex;
    private int facingDirection = 1;

    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(SetPatrolPoint());
    }

    private void Update()
    {
        if (isPaused)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            if (target.x > transform.position.x && facingDirection == -1 ||
                target.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }

            Vector2 direction = ((Vector3)target - transform.position).normalized;
            rb.velocity = direction * speed;
            if (Vector2.Distance(transform.position, target) < 0.1f && isPaused == false)
            {
                StartCoroutine(SetPatrolPoint());
            }
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator SetPatrolPoint()
    {
        isPaused = true;
        anim.Play("Idle");

        yield return new WaitForSeconds(pauseDuration);

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        target = patrolPoints[currentPatrolIndex];
        isPaused = false;
        anim.Play("Walk");
    }
}
