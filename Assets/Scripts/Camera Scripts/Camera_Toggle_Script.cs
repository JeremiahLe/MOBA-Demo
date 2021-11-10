using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Toggle_Script : MonoBehaviour
{
    CameraFollow_Script cameraFollowScript;
    CameraRoam cameraRoamScript;

    public enum CameraType {Locked, Unlocked}
    public CameraType cameraType;

    [SerializeField] public KeyCode cameraToggleHotkey;

    // Start is called before the first frame update
    void Start()
    {
        cameraType = CameraType.Locked;

        cameraFollowScript = GetComponent<CameraFollow_Script>();
        cameraRoamScript = GetComponent<CameraRoam>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (cameraType)
        {
            case (CameraType.Locked):
                if (Input.GetKeyDown(cameraToggleHotkey))
                {
                    cameraFollowScript.enabled = false;
                    cameraRoamScript.enabled = true;
                    cameraType = CameraType.Unlocked;
                }
                break;

            case (CameraType.Unlocked):
                if (Input.GetKeyDown(cameraToggleHotkey))
                {
                    cameraFollowScript.enabled = true;
                    cameraRoamScript.enabled = false;
                    cameraType = CameraType.Locked;
                }
                break;

            default:
                Debug.Log("Camera not found?!");
                break;
        }
    }
}
