using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScoll_Script : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private float camFOV;
    [SerializeField] private float zoomSpeed;

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
        camFOV = Mathf.Clamp(camFOV, 60, 100);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFOV, zoomSpeed);
    }
}
