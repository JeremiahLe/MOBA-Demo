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

        heroClassScript.heroHealth = heroClassScript.heroMaxHealth; // wtf is this?
    }

    // Update is called once per frame
    void Update()
    {
        playerSlider2D.value = heroClassScript.heroHealth;
        playerSlider3D.value = playerSlider2D.value;
    }
}
