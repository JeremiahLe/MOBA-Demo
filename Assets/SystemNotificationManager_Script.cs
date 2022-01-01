using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SystemNotificationManager_Script : MonoBehaviour
{
    public TextMeshProUGUI systemMessage;
    Animator anim;

    SkillpointCheck_Script skillpointCheck_Script;
    [SerializeField] GameObject skillpointObject;

    // Start is called before the first frame update
    void Start()
    {
        systemMessage.text = "";

        anim = GetComponent<Animator>();
        systemMessage = GetComponent<TextMeshProUGUI>();
        skillpointCheck_Script = skillpointObject.GetComponent<SkillpointCheck_Script>();

        anim.SetBool("AnimTrigger", false);
    }

    public void AlertObservers(string message)
    {
        switch (message)
        {
            case ("Not enough Mana!"):
                systemMessage.text = message;
                anim.SetBool("AnimTrigger", true);
                break;

            case ("Ability is on cooldown!"):
                systemMessage.text = message;
                anim.SetBool("AnimTrigger", true);
                break;

            case ("Animation End"):
                systemMessage.text = "";
                anim.SetBool("AnimTrigger", false);
                break;

            case ("Reset AnimTrigger"):
                anim.SetBool("AnimTrigger", false);
                break;

            case ("Ability is not learned yet!"):
                systemMessage.text = message;
                anim.SetBool("AnimTrigger", true);
                break;

            case ("No available skill points!"):
                skillpointCheck_Script.AlertObservers("No available skill points!");
                systemMessage.text = message;
                anim.SetBool("AnimTrigger", true);
                break;

            case ("No available skill points! (No Popup)"):
                skillpointCheck_Script.AlertObservers("No available skill points!");
                anim.SetBool("AnimTrigger", true);
                break;

            case ("Available skill points but Q is maxed"):
                skillpointCheck_Script.AlertObservers("Available skill points but Q is maxed");
                break;

            case ("Available skill points but W is maxed"):
                skillpointCheck_Script.AlertObservers("Available skill points but W is maxed");
                break;

            case ("Available skill points but E is maxed"):
                skillpointCheck_Script.AlertObservers("Available skill points but E is maxed");
                break;

            case ("Available skill points but R is maxed"):
                skillpointCheck_Script.AlertObservers("Available skill points but R is maxed");
                break;

            case ("Ability is max level!"):
                systemMessage.text = message;
                anim.SetBool("AnimTrigger", true);
                break;

            default:
                systemMessage.text = "Missing message call?";
                break;
        }
    }
}
