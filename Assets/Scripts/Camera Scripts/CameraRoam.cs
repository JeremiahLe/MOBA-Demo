using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoam : MonoBehaviour
{
    [SerializeField] private float camSpeed = 20f;
    [SerializeField] private float screenSizeThickness = 10f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        // Up
        if (Input.mousePosition.y >= Screen.height - screenSizeThickness)
        {
            pos.z -= camSpeed * Time.deltaTime;
        }

        // Down
        if (Input.mousePosition.y <= screenSizeThickness)
        {
            pos.z += camSpeed * Time.deltaTime;
        }

        // Left
        if (Input.mousePosition.x <= screenSizeThickness)
        {
            pos.x += camSpeed * Time.deltaTime;
        }

        // Right
        if (Input.mousePosition.x >= Screen.width - screenSizeThickness)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }

        transform.position = pos;

    }
}

