using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_IconCd_Script : MonoBehaviour
{
    void Awake()
    {
        transform.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
    }
}
