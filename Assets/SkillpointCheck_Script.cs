using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillpointCheck_Script : MonoBehaviour
{
    HeroClass heroClass;

    [SerializeField] Sprite NoVisual_Sprite;
    [SerializeField] Sprite Visual_Sprite;

    [SerializeField] Image Q_Learnable;
    [SerializeField] Image W_Learnable;
    [SerializeField] Image E_Learnable;
    [SerializeField] Image R_Learnable;

    private void Start()
    {
        // Init
        heroClass = GameObject.FindGameObjectWithTag("MyPlayer").GetComponent<HeroClass>();
    }

    public void AlertObservers(string message)
    {
        switch (message)
        {
            case ("Gained a skill point."):
                if (heroClass.Q_Ability.abilityLevel != heroClass.Q_Ability.abilityMaxLevel)
                {
                    Q_Learnable.sprite = Visual_Sprite;
                }

                if (heroClass.W_Ability.abilityLevel != heroClass.W_Ability.abilityMaxLevel)
                {
                    W_Learnable.sprite = Visual_Sprite;
                }

                if (heroClass.E_Ability.abilityLevel != heroClass.E_Ability.abilityMaxLevel)
                {
                    E_Learnable.sprite = Visual_Sprite;
                }

                if (heroClass.heroLevel >= 6)
                {
                    if (heroClass.R_Ability.abilityLevel != heroClass.R_Ability.abilityMaxLevel)
                    {
                        R_Learnable.sprite = Visual_Sprite;
                    }
                }
                break;

            case ("No available skill points!"):
                Q_Learnable.sprite = NoVisual_Sprite;
                W_Learnable.sprite = NoVisual_Sprite;
                E_Learnable.sprite = NoVisual_Sprite;
                R_Learnable.sprite = NoVisual_Sprite;
                break;

            case ("Available skill points but Q is maxed"):
                Q_Learnable.sprite = NoVisual_Sprite;
                break;
            case ("Available skill points but W is maxed"):
                W_Learnable.sprite = NoVisual_Sprite;
                break;
            case ("Available skill points but E is maxed"):
                E_Learnable.sprite = NoVisual_Sprite;
                break;
            case ("Available skill points but R is maxed"):
                R_Learnable.sprite = NoVisual_Sprite;
                break;
        }
    }
}
