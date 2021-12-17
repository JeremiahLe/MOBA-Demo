using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider2D_Script : MonoBehaviour
{
    [SerializeField] private Slider playerSlider3D;
    Slider playerSlider2D;

    HeroClass heroClassScript;

    //[SerializeField] private int health;

    // Start is called before the first frame update
    void Start()
    {
        heroClassScript = GameObject.FindGameObjectWithTag("MyPlayer").GetComponent<HeroClass>(); // in multiplayer, check if owned as well

        playerSlider2D = GetComponent<Slider>();

        playerSlider3D.maxValue = heroClassScript.heroMaxHealth;
        playerSlider2D.maxValue = heroClassScript.heroMaxHealth;

        //heroClassScript.heroHealth = heroClassScript.heroMaxHealth; // wtf is this?
    }

    // Update is called once per frame
    void Update()
    {
        // If current health lower than maximum begin regen, otherwise don't overcap
        if (heroClassScript.heroHealth < heroClassScript.heroMaxHealth)
            Regen();
        else if (heroClassScript.heroHealth > heroClassScript.heroMaxHealth)
            heroClassScript.heroHealth = heroClassScript.heroMaxHealth;

        // If mana is not below or equal to zero
        if (heroClassScript.heroHealth > 0f)
        {
            playerSlider3D.maxValue = heroClassScript.heroMaxHealth;
            playerSlider2D.maxValue = heroClassScript.heroMaxHealth;

            playerSlider2D.value = heroClassScript.heroHealth;
            playerSlider3D.value = playerSlider2D.value;
        }
    }

    int interval = 2;
    float nextTime = 0;

    void Regen()
    {
        if (Time.time >= nextTime)
        {
            heroClassScript.heroHealth += heroClassScript.heroHealthRegen;
            nextTime += interval;
        }
    }

    public void CallHealthTrigger(GameObject targetedEnemyRef)
    {
        //Debug.Log("Called health HUD trigger!");
        //playerSlider2D.value = targetedEnemyRef.GetComponent<HeroClass>().heroHealth;
        // playerSlider3D.value = playerSlider2D.value;
    }
}
