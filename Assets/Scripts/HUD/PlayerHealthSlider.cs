using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSlider : MonoBehaviour
{
    Quaternion originalRotation = Quaternion.Euler(70, 180, 0);

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(Camera.main.transform);
        //transform.Rotate(0, 180, 0);

        transform.rotation = originalRotation;
    }
}
