using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Change_Class : MonoBehaviour
{
    public Player_Combat combat;
    public Player_Bow bow;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Change Class"))
        {
            combat.enabled = !combat.enabled;
            bow.enabled = !bow.enabled;
        }
    }
}
