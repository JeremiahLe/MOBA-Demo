using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_Script : MonoBehaviour
{
    [SerializeField] private KeyCode escapeKey;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(escapeKey))
        {
            Application.Quit();
        }
    }
}
