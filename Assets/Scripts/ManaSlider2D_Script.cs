using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSlider2D_Script : MonoBehaviour
{
    [SerializeField] private Slider playerSlider3D;
    Slider playerSlider2D;

    [SerializeField] private int mana;

    // Start is called before the first frame update
    void Start()
    {
        playerSlider2D = GetComponent<Slider>();

        playerSlider2D.maxValue = mana;
        playerSlider3D.maxValue = mana;
    }

    // Update is called once per frame
    void Update()
    {
        playerSlider2D.value = mana;
        playerSlider3D.value = playerSlider2D.value;
    }
}
