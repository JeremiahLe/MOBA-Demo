using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider2D_Script : MonoBehaviour
{
    [SerializeField] private Slider playerSlider3D;
    Slider playerSlider2D;

    [SerializeField] private int health;

    // Start is called before the first frame update
    void Start()
    {
        playerSlider2D = GetComponent<Slider>();

        playerSlider2D.maxValue = health;
        playerSlider3D.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        playerSlider2D.value = health;
        playerSlider3D.value = playerSlider2D.value;
    }
}
