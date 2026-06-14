using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Use_Item : MonoBehaviour
{
    public void ApplyItemEffects(Item_SO itemSO)
    {
        if (itemSO.currentHP > 0)
        {
            Stats_Manager.instance.UpdateCurrentHP(itemSO.currentHP);
        }

        if (itemSO.maxHP > 0)
        {
            Stats_Manager.instance.UpdateMaxHP(itemSO.maxHP);
        }

        if (itemSO.speed > 0)
        {
            Stats_Manager.instance.UpdateMovementSpeed(itemSO.speed);
        }

        if (itemSO.durration > 0)
        {
            StartCoroutine(EffectTimer(itemSO, itemSO.durration));
        }  
    }

    private IEnumerator EffectTimer(Item_SO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (itemSO.currentHP > 0)
        {
            Stats_Manager.instance.UpdateCurrentHP(-itemSO.currentHP);
        }

        if (itemSO.maxHP > 0)
        {
            Stats_Manager.instance.UpdateMaxHP(-itemSO.maxHP);
        }

        if (itemSO.speed > 0)
        {
            Stats_Manager.instance.UpdateMovementSpeed(-itemSO.speed);
        }
    }
}
