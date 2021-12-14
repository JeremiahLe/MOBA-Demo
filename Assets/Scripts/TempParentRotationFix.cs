using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempParentRotationFix : MonoBehaviour
{
    [SerializeField] GameObject childToFollow;

    private void Update()
    {
        transform.position = childToFollow.transform.position;
    }
}
