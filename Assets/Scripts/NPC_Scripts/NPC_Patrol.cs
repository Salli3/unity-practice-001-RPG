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

    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(SetPatrolPoint());
    }

    private void Update()
    {
        if (isPaused)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (Vector2.Distance(transform.position, target) < 0.1f && isPaused == false)
        {
            StartCoroutine(SetPatrolPoint());
        }

        Move();
    }

    private void Move()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        if (direction.x > 0 && transform.localScale.x < 0 ||
            direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        rb.velocity = direction * speed;
    }

    void Flip()
    {
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
