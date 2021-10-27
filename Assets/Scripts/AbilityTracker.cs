using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTracker : MonoBehaviour
{
    [Header("Q Ability")]
    [SerializeField] private Image Q_AbilityImage;
    [SerializeField] private float Q_cooldown = 7f;
    [SerializeField] private KeyCode Q_Ability_Keycode;
    bool Q_isCooldown = false;

    // Ability 1 Input Variables
    Vector3 position;
    [SerializeField] private Canvas Q_Ability_Canvas;
    [SerializeField] private Image Q_Skillshot;
    [SerializeField] private Transform player;

    [Header("W Ability")]
    [SerializeField] private Image W_AbilityImage;
    [SerializeField] private float W_cooldown = 20f;
    [SerializeField] private KeyCode W_Ability_Keycode;
    bool W_isCooldown = false;

    // Ability 2 Input Variables
    [SerializeField] private Image W_targetCircleIndicator;
    [SerializeField] private Image W_rangeCircleIndicator;
    [SerializeField] private Canvas W_Ability_Canvas;
    private Vector3 posUp;
    [SerializeField] private float W_MaxAbilityDistance;

    [Header("E Ability")]
    [SerializeField] private Image E_AbilityImage;
    [SerializeField] private float E_cooldown = 13f;
    [SerializeField] private KeyCode E_Ability_Keycode;
    bool E_isCooldown = false;

    [Header("R Ability")]
    [SerializeField] private Image R_AbilityImage;
    [SerializeField] private float R_cooldown = 80f;
    [SerializeField] private KeyCode R_Ability_Keycode;
    bool R_isCooldown = false;

    [SerializeField] private LayerMask terrainMask;

    // Start is called before the first frame update
    void Start()
    {
        Q_AbilityImage.fillAmount = 0;
        W_AbilityImage.fillAmount = 0;
        E_AbilityImage.fillAmount = 0;
        R_AbilityImage.fillAmount = 0;

        Q_Skillshot.GetComponent<Image>().enabled = false;
        W_targetCircleIndicator.GetComponent<Image>().enabled = false;

        RectTransform W_Pic = W_targetCircleIndicator.GetComponent<RectTransform>();
        W_Pic.anchoredPosition3D = new Vector3(W_Pic.anchoredPosition3D.x, W_Pic.anchoredPosition3D.y + 10, W_Pic.anchoredPosition3D.z);

        W_rangeCircleIndicator.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Q_Ability();
        W_Ability();
        E_Ability();
        R_Ability();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Ability 1 Inputs
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainMask))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        // Ability 2 Inputs
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                posUp = new Vector3(hit.point.x, 10f, hit.point.z);
                position = hit.point;
            }
        }

        // Ability 1 Canvas Inputs
        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, 0);
        Q_Ability_Canvas.transform.rotation = Quaternion.Lerp(transRot, Q_Ability_Canvas.transform.rotation, 0f);

        // Ability 2 Canvas Inputs
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, W_MaxAbilityDistance);

        var newHitPos = transform.position + hitPosDir * distance;
        W_Ability_Canvas.transform.position = (newHitPos);
    }

    void Q_Ability()
    {
        if (Input.GetKey(Q_Ability_Keycode) && Q_isCooldown == false)
        {
            Q_Skillshot.GetComponent<Image>().enabled = true;
            W_targetCircleIndicator.GetComponent<Image>().enabled = false;
            W_rangeCircleIndicator.GetComponent<Image>().enabled = false;
        }

        if (Q_Skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
        {
            Q_isCooldown = true;
            Q_AbilityImage.fillAmount = 1;
            Debug.Log("Used Q Ability!");
        }

        if (Q_isCooldown)
        {
            Q_AbilityImage.fillAmount -= 1 / Q_cooldown * Time.deltaTime;
            Q_Skillshot.GetComponent<Image>().enabled = false;

            if (Q_AbilityImage.fillAmount <= 0)
            {
                Q_AbilityImage.fillAmount = 0;
                Q_isCooldown = false;
            }
        }
    }

    void W_Ability()
    {
        if (Input.GetKey(W_Ability_Keycode) && W_isCooldown == false)
        {
            Q_Skillshot.GetComponent<Image>().enabled = false;
            W_targetCircleIndicator.GetComponent<Image>().enabled = true;
            W_rangeCircleIndicator.GetComponent<Image>().enabled = true;
        }

        if (W_targetCircleIndicator.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            W_isCooldown = true;
            W_AbilityImage.fillAmount = 1;
            Debug.Log("Used W Ability!");
        }

        if (W_isCooldown)
        {
            W_AbilityImage.fillAmount -= 1 / W_cooldown * Time.deltaTime;
            W_targetCircleIndicator.GetComponent<Image>().enabled = false;
            W_rangeCircleIndicator.GetComponent<Image>().enabled = false;

            if (W_AbilityImage.fillAmount <= 0)
            {
                W_AbilityImage.fillAmount = 0;
                W_isCooldown = false;
            }
        }
    }

    void E_Ability()
    {
        if (Input.GetKey(E_Ability_Keycode) && E_isCooldown == false)
        {
            E_isCooldown = true;
            E_AbilityImage.fillAmount = 1;
            Debug.Log("Used E Ability!");
        }

        if (E_isCooldown)
        {
            E_AbilityImage.fillAmount -= 1 / E_cooldown * Time.deltaTime;

            if (E_AbilityImage.fillAmount <= 0)
            {
                E_AbilityImage.fillAmount = 0;
                E_isCooldown = false;
            }
        }
    }

    void R_Ability()
    {
        if (Input.GetKey(R_Ability_Keycode) && R_isCooldown == false)
        {
            R_isCooldown = true;
            R_AbilityImage.fillAmount = 1;
            Debug.Log("Used R Ability!");
        }

        if (R_isCooldown)
        {
            R_AbilityImage.fillAmount -= 1 / R_cooldown * Time.deltaTime;

            if (R_AbilityImage.fillAmount <= 0)
            {
                R_AbilityImage.fillAmount = 0;
                R_isCooldown = false;
            }
        }
    }
}
