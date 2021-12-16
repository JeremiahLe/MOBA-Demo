using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SystemNotificationManager_Script : MonoBehaviour
{
    public TextMeshProUGUI systemMessage;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        systemMessage.text = "";

        anim = GetComponent<Animator>();
        systemMessage = GetComponent<TextMeshProUGUI>();

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

            case ("Animation End"):
                systemMessage.text = "";
                anim.SetBool("AnimTrigger", false);
                break;

            case ("Reset AnimTrigger"):
                anim.SetBool("AnimTrigger", false);
                break;

            default:
                systemMessage.text = "Missing message call?";
                break;
        }
    }
}
