using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;

    private Vector2 aimDirection = Vector2.right;

    //public float shootCooldown = 0.5f;
    private float shootTimer;
    public bool isShooting;

    public Animator anim;

    void Update()
    {
        shootTimer -= Time.deltaTime;

        //if (Input.GetButtonDown("Ranged Attack") && shootTimer <= 0)
        //{
        //    anim.SetBool("isShooting", true);
        //    isShooting = true;
        //    //Shoot();
        //}
        if (!isShooting)
        {
            HandleAiming();
        }
    }

    public void DrawBow()
    {
        if (shootTimer <= 0)
        {
            anim.SetBool("isShooting", true);
            isShooting = true;
        }
    }

    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
    }

    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1);
        anim.SetLayerWeight(1, 0);
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("aimX", aimDirection.x);
            anim.SetFloat("aimY", aimDirection.y);
        }
    }

    //public cause need to access from external script
    public void Shoot()
    {
        if (shootTimer > 0)
        {
            return;
        }
        //instantiate = spawn
        Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.direction = aimDirection;
        shootTimer = Stats_Manager.instance.cooldown;
    }

    public void FinishShooting()
    {
        anim.SetBool("isShooting", false);
        isShooting = false;
    }
}
