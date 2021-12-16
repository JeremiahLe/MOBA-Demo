using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick_Script : MonoBehaviour
{
    public void AlertObservers(string message)
    {
        if (message.Equals("Animation End"))
        {
            Destroy(gameObject);
        }
    }
}
