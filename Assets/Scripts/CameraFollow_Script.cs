using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_Script : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    [SerializeField] private float cameraSmoothness = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = player.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, cameraSmoothness);
    }
}
