using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills_Manager : MonoBehaviour
{
    private void OnEnable()
    {
        //subscribe to spend skill point event
        Skill_Slot.OnSkillPointSpent += HandleSkillPointSpent;
    }

    private void OnDisable()
    {
        //unbscribe to spend skill point event
        //ensures that when your object is disabled or destroyed, it no longer listens to the event, preventing duplicate calls and leaks
        Skill_Slot.OnSkillPointSpent -= HandleSkillPointSpent;
    }

    //
    private void HandleSkillPointSpent(Skill_Slot slot)
    {
        //find what skill is being upgraded
        string skillName = slot.skillSO.skillName;

        //call to the stats manager to change stats
        switch (skillName)
        {
            case "Max HP Up":
                Stats_Manager.instance.UpdateMaxHP(1);
                break;
            case "Damage Up":
                Stats_Manager.instance.UpdateDamage(1);
                break;
            case "Exp Up":
                Stats_Manager.instance.UpdateExpGain(10);//%
                break;
            case "Attack Speed Up":
                Stats_Manager.instance.UpdateAttackSpeed(10);//%
                break;
            case "Stun Duration Up":
                Stats_Manager.instance.UpdateStunDuration(10);//%
                break;
            case "Movement Speed Up":
                Stats_Manager.instance.UpdateMovementSpeed(10);//%
                break;
            default:
                Debug.LogWarning("Unknown skill: " +  skillName);
                break;
            //TODO money up and extra life, also fix the damn stats change of each skill 
        }
    }
}
