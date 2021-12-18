using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText_UI : MonoBehaviour
{
    //public float DestroyTime = 1.5f;
    //public Vector3 Offset = new Vector3(0, 0, 0);
    public Vector3 RandomizeIntensity = new Vector3(0.07f, 0, 0);

    public TextMeshPro damageNumber;

    void Start()
    {
        //Destroy(gameObject, DestroyTime);

        //transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y), 0);

        damageNumber = GetComponent<TextMeshPro>();
    }

    public void AlertObservers(string message)
    {
        switch (message)
        {
            case ("Animation End"):
                Destroy(gameObject);
                break;

            default:
                break;
        }
    }
}
