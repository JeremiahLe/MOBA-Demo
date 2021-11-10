using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD_Text_Controller : MonoBehaviour
{
    public enum TypeOfStat {health, mana};
    public TypeOfStat typeOfStat;

    TextMeshProUGUI statText;

    HeroClass heroClassScript;

    // Start is called before the first frame update
    void Start()
    {
        statText = GetComponent<TextMeshProUGUI>();

        heroClassScript = GameObject.FindGameObjectWithTag("MyPlayer").GetComponent<HeroClass>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.typeOfStat == TypeOfStat.health)
            statText.text = (heroClassScript.heroHealth + "/" + heroClassScript.heroMaxHealth);
        else
            statText.text = (heroClassScript.heroMana + "/" + heroClassScript.heroMaxMana);
    }
}
