using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScoll_Script : MonoBehaviour
{
    public Camera cam;
    private float camFOV;
    public float zoomSpeed;

    private float mouseScrollInput;

    // Start is called before the first frame update
    void Start()
    {
        camFOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");

        camFOV -= mouseScrollInput * zoomSpeed;
        camFOV = Mathf.Clamp(camFOV, 60, 90);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFOV, zoomSpeed);
    }
}
