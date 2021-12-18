using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSlider : MonoBehaviour
{
    public string typeOfObject;
    //Camera cam;
    Quaternion originalRotation;

    // Update is called once per frame
    private void Start()
    {
        //cam = Camera.main;
        originalRotation = Quaternion.Euler(70, 180, 0);
    }

    void Update()
    {
        //Vector3 targetPos = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);
        //transform.LookAt(Camera.main.transform);

        switch (typeOfObject)
        {
            case "Enemy":
                //transform.LookAt(targetPos);
                transform.rotation = originalRotation;
                break;
            case "UI":
                //if (transform.position.z < targetPos.z)
                    //transform.LookAt(transform.position - targetPos);
                //else
                    transform.rotation = originalRotation;
                break;
        }

    }
}
