using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    //public float lifespan = 2;
    public float speed;

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public SpriteRenderer sr;
    public Sprite buriedSprite;

    void Start()
    {
        rb.velocity = direction * speed;
        RotateArrow();
        Destroy(gameObject, Stats_Manager.instance.attackRange);
    }

    private void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name + " | Layer: " + LayerMask.LayerToName(collision.gameObject.layer));
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<Enemy_HP>().ChangeHP(Stats_Manager.instance.damage);
            collision.gameObject.GetComponent<Enemy_Knockback>().Knockback(transform, Stats_Manager.instance.knockbackForce, Stats_Manager.instance.knockbackTime, Stats_Manager.instance.stunTime);
            //Destroy(gameObject);
            AttachToTarget(collision.gameObject.transform);
        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            AttachToTarget(collision.gameObject.transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;

        transform.SetParent(target);
    }
}
