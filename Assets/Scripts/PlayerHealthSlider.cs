using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSlider : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        //transform.Rotate(0, 180, 0);
    }
}
