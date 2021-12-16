using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSlider2D_Script : MonoBehaviour
{
    [SerializeField] private Slider playerSlider3D;
    Slider playerSlider2D;

    HeroClass heroClassScript;

    // Start is called before the first frame update
    void Start()
    {
        heroClassScript = GameObject.FindGameObjectWithTag("MyPlayer").GetComponent<HeroClass>(); // in multiplayer, check if owned as well

        playerSlider2D = GetComponent<Slider>();

        playerSlider2D.maxValue = heroClassScript.heroMaxMana;
        playerSlider3D.maxValue = heroClassScript.heroMaxMana;
    }

    // Update is called once per frame
    void Update()
    {
        // If current mana lower than maximum begin regen, otherwise don't overcap
        if (heroClassScript.heroMana < heroClassScript.heroMaxMana)
            Regen();
        else if (heroClassScript.heroMana > heroClassScript.heroMaxMana)
            heroClassScript.heroMana = heroClassScript.heroMaxMana;

        // If mana is not below or equal to zero
        if (heroClassScript.heroMana >= 0f)
        {
            playerSlider3D.maxValue = heroClassScript.heroMaxMana;
            playerSlider2D.maxValue = heroClassScript.heroMaxMana;

            playerSlider2D.value = heroClassScript.heroMana;
            playerSlider3D.value = playerSlider2D.value;
        }
    }

    int interval = 1;
    float nextTime = 0;

    void Regen()
    {
        if (Time.time >= nextTime)
        {
            heroClassScript.heroMana += heroClassScript.heroManaRegen;
            nextTime += interval;
        }
    }
}
