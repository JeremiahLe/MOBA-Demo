using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventBypass : MonoBehaviour
{
    private HeroCombat heroCombat;

    private void Awake()
    {
        heroCombat = GameObject.FindGameObjectWithTag("MyPlayer").GetComponent<HeroCombat>();
    }

    void AlertObservers(string message)
    {
        switch (message)
        {
            case "Melee Attack Bypass":
                heroCombat.MeleeAttack();
                break;

            default:
                Debug.Log("Missing message?", this);
                break;
        }
    }
}
