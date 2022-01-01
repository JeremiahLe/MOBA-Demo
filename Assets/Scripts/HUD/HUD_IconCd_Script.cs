using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_IconCd_Script : MonoBehaviour
{
    Image sprite;

    void Start()
    {
        //transform.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
        sprite = GetComponent<Image>();
    }

    private void Update()
    {
        sprite.transform.localScale = new Vector3(PingPong(Time.time, 0.75f, 1f), PingPong(Time.time, 0.75f, 1f), PingPong(Time.time, 0.75f, 1f));
    }

    float PingPong(float scale, float min, float max)
    {
        return Mathf.PingPong(scale, max - min) + min;
    }
}
