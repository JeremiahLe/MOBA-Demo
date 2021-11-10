using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSlider : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        //transform.LookAt(Camera.main.transform);

        transform.LookAt(targetPos);
    }
}
