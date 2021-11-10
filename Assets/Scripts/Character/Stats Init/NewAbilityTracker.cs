using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewAbilityTracker : MonoBehaviour
{
    /// <summary>
    /// Hero Script for - Cube
    /// </summary>
    /// 

    #region Ability Init
    [DisplayWithoutEdit] public AbilityClass currentAbility;

    [Header("Q Ability")]
    [SerializeField] private Canvas Q_Ability_Canvas;
    [SerializeField] private Image Q_Ability_Indicator;
    [SerializeField] private Image Q_Ability_Range;
    [SerializeField] private KeyCode Q_Ability_Keycode;

    [Header("W Ability")]
    [SerializeField] private Canvas W_Ability_Canvas;
    [SerializeField] private Image W_Ability_Indicator;
    [SerializeField] private Image W_Ability_Range;
    [SerializeField] private KeyCode W_Ability_Keycode;

    [Header("E Ability")]
    [SerializeField] private Canvas E_Ability_Canvas;
    [SerializeField] private Image E_Ability_Indicator;
    [SerializeField] private Image E_Ability_Range;
    [SerializeField] private KeyCode E_Ability_Keycode;

    [Header("R Ability")]
    [SerializeField] private Canvas R_Ability_Canvas;
    [SerializeField] private Image R_Ability_Indicator;
    [SerializeField] private Image R_Ability_Range;
    [SerializeField] private KeyCode R_Ability_Keycode;

    // This is where unique ability input goes
    // Skillshot
    private Vector3 position;

    // AOE Skillshot
    private Vector3 posUp;
    [Header("Other Ability Setup (Case by Case)")]
    [SerializeField] private float AOESkillshot_MaxAbility_Distance;

    // This is a situational adjustment to the W indicator clipping through higher terrain / may be obsolete in the future
    RectTransform W_Pic;

    #endregion

    #region Other Components
    [Header("Other Components")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask terrainMask;
    #endregion

    #region Component Initilization
    HeroClass heroClass;
    private CharacterMovementScript moveScript;
    #endregion

    private void Start()
    {
        // Get our hero class component, getting each ability etc.
        heroClass = GetComponent<HeroClass>();
        moveScript = GetComponent<CharacterMovementScript>();

        // Debug //
        // Set every canvas image to nonvisible at runtime.
        Q_Ability_Indicator.enabled = false;
        Q_Ability_Range.enabled = false;

        W_Ability_Indicator.enabled = false;
        W_Ability_Range.enabled = false;

        E_Ability_Indicator.enabled = false;
        E_Ability_Range.enabled = false;

        R_Ability_Indicator.enabled = false;
        R_Ability_Range.enabled = false;

        #region Case By Case Hero Init

        // W Ability - Cube
        RectTransform AOESkillshot_Container = W_Ability_Indicator.GetComponent<RectTransform>();
        AOESkillshot_Container.anchoredPosition3D = new Vector3(AOESkillshot_Container.anchoredPosition3D.x, AOESkillshot_Container.anchoredPosition3D.y + 10, AOESkillshot_Container.anchoredPosition3D.z);

        #endregion
    }

    private void Update()
    {
        // Ability Code
        Q_Ability();
        W_Ability();
        E_Ability();

        // Canvas Control for abilities
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        #region Case By Case Hero Init

        // Ability 1 Inputs - Cube
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        // Ability 1 Canvas Inputs - Cube
        /// Skillshot ability indicator rotation code
        Quaternion transRot = Quaternion.LookRotation(position - playerTransform.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, 0);
        Q_Ability_Canvas.transform.rotation = Quaternion.Lerp(transRot, Q_Ability_Canvas.transform.rotation, 0f);

        // Ability 2 Inputs - Cube
        if (currentAbility != null)
        {
            if (currentAbility.abilityKeyCode == W_Ability_Keycode)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject != this.gameObject)
                    {
                        posUp = new Vector3(hit.point.x, 10f, hit.point.z);
                        position = hit.point;
                    }
                }

                // Ability 2 Canvas Inputs - Cube
                /// AOE Ability Range indicator code
                var hitPosDir = (hit.point - transform.position).normalized;
                float distance = Vector3.Distance(hit.point, transform.position);
                distance = Mathf.Min(distance, AOESkillshot_MaxAbility_Distance);

                var newHitPos = transform.position + hitPosDir * distance;
                W_Ability_Canvas.transform.position = (newHitPos);

                // This is a situational adjustment to the W indicator clipping through higher terrain / may be obsolete in the future
                if (currentAbility.abilityKeyCode == KeyCode.W)
                {
                    //W_Pic = W_Ability_Canvas.GetComponent<RectTransform>();
                    //W_Pic.anchoredPosition3D = new Vector3(W_Pic.anchoredPosition3D.x, W_Pic.anchoredPosition3D.y + 2, W_Pic.anchoredPosition3D.z);
                }
            }
        }

        #endregion
    }

    IEnumerator ResetStatValue(string StatName, float StatToReset, float StatDefaultValue, float time)
    {
        yield return new WaitForSeconds(time);

        if (StatName == "Speed")
        {
            moveScript.agent.speed = StatDefaultValue;
            Debug.Log("Coroutine Executed!");
        }
    }

    #region Case by Case Ability Function Calls

    /// <summary>
    /// Ability Function Calls for - Cube
    /// </summary>

    void Q_Ability()
    {
        // Prep or cancel ability indicators
        if (Input.GetKeyDown(Q_Ability_Keycode) && heroClass.Q_Ability.isCooldown == false)
        {
            if (Q_Ability_Indicator.enabled == false)
            {
                currentAbility = heroClass.Q_Ability;
                Debug.Log("Q ability was prepped.");
                Q_Ability_Indicator.enabled = true;
                Q_Ability_Range.enabled = true;

                W_Ability_Indicator.enabled = false;
                W_Ability_Range.enabled = false;

                E_Ability_Indicator.enabled = false;
                E_Ability_Range.enabled = false;

                R_Ability_Indicator.enabled = false;
                R_Ability_Range.enabled = false;
            }
            else
            {
                currentAbility = null;
                Debug.Log("Q ability was canceled.");
                Q_Ability_Indicator.enabled = false;
                Q_Ability_Range.enabled = false;
            }
        }
        else if (Input.GetKeyDown(Q_Ability_Keycode) && heroClass.Q_Ability.isCooldown == true)
        {
            Debug.Log("Q Ability is on Cooldown!");
        }

        // Use ability?
        if (Q_Ability_Indicator.enabled == true && Input.GetMouseButton(0) && currentAbility.abilityKeyCode == Q_Ability_Keycode)
        {
            Debug.Log("Used Q Ability!");
            heroClass.Q_Ability.isCooldown = true;
            heroClass.Q_Ability.HUDIcon.fillAmount = 1;
        }

        // Keep track of CD and Hud Icon
        if (heroClass.Q_Ability.isCooldown)
        {
            heroClass.Q_Ability.HUDIcon.fillAmount -= 1 / heroClass.Q_Ability.abilityCooldown * Time.deltaTime;
            Q_Ability_Indicator.enabled = false;
            Q_Ability_Range.enabled = false;

            if (heroClass.Q_Ability.HUDIcon.fillAmount <= 0)
            {
                heroClass.Q_Ability.HUDIcon.fillAmount = 0;
                heroClass.Q_Ability.isCooldown = false;
            }
        }
    }

    void W_Ability()
    {
        // Prep or cancel ability indicators
        if (Input.GetKeyDown(W_Ability_Keycode) && heroClass.W_Ability.isCooldown == false)
        {
            if (W_Ability_Indicator.enabled == false)
            {
                currentAbility = heroClass.W_Ability;
                Debug.Log("W ability was prepped.");
                Q_Ability_Indicator.enabled = false;
                Q_Ability_Range.enabled = false;

                W_Ability_Indicator.enabled = true;
                W_Ability_Range.enabled = true;

                E_Ability_Indicator.enabled = false;
                E_Ability_Range.enabled = false;

                R_Ability_Indicator.enabled = false;
                R_Ability_Range.enabled = false;
            }
            else
            {
                currentAbility = null;
                Debug.Log("W ability was canceled.");
                W_Ability_Indicator.enabled = false;
                W_Ability_Range.enabled = false;
            }
        }
        else if (Input.GetKeyDown(W_Ability_Keycode) && heroClass.W_Ability.isCooldown == true)
        {
            Debug.Log("W Ability is on Cooldown!");
        }

        // Use ability?
        if (W_Ability_Indicator.enabled == true && Input.GetMouseButton(0) && currentAbility.abilityKeyCode == W_Ability_Keycode)
        {
            Debug.Log("Used W Ability!");
            heroClass.W_Ability.isCooldown = true;
            heroClass.W_Ability.HUDIcon.fillAmount = 1;
        }

        // Keep track of CD and Hud Icon
        if (heroClass.W_Ability.isCooldown)
        {
            heroClass.W_Ability.HUDIcon.fillAmount -= 1 / heroClass.W_Ability.abilityCooldown * Time.deltaTime;
            W_Ability_Indicator.enabled = false;
            W_Ability_Range.enabled = false;

            if (heroClass.W_Ability.HUDIcon.fillAmount <= 0)
            {
                heroClass.W_Ability.HUDIcon.fillAmount = 0;
                heroClass.W_Ability.isCooldown = false;
            }
        }
    }

    void E_Ability()
    {
        // Prep or cancel ability indicators
        if (Input.GetKeyDown(E_Ability_Keycode) && heroClass.E_Ability.isCooldown == false)
        {
            if (E_Ability_Indicator.enabled == false)
            {
                currentAbility = heroClass.E_Ability;
                Debug.Log("E ability was prepped.");
                Q_Ability_Indicator.enabled = false;
                Q_Ability_Range.enabled = false;

                W_Ability_Indicator.enabled = false;
                W_Ability_Range.enabled = false;

                E_Ability_Indicator.enabled = true;
                E_Ability_Range.enabled = true;

                R_Ability_Indicator.enabled = false;
                R_Ability_Range.enabled = false;
            }
            else
            {
                currentAbility = null;
                Debug.Log("E ability was canceled.");
                E_Ability_Indicator.enabled = false;
                E_Ability_Range.enabled = false;
            }
        }
        else if (Input.GetKeyDown(E_Ability_Keycode) && heroClass.E_Ability.isCooldown == true)
        {
            Debug.Log("E Ability is on Cooldown!");
        }

        // Use ability?
        if (E_Ability_Indicator.enabled == true && Input.GetMouseButton(0) && currentAbility.abilityKeyCode == E_Ability_Keycode)
        {
            Debug.Log("Used E Ability!");
            heroClass.E_Ability.isCooldown = true;
            heroClass.E_Ability.HUDIcon.fillAmount = 1;

            ///
            float startHeroSpeed = moveScript.agent.speed;
            moveScript.agent.speed += moveScript.agent.speed * heroClass.E_Ability.abilityBuffPercentage;

            if (heroClass.E_Ability.isCooldown)
            {
                StartCoroutine(ResetStatValue("Speed", moveScript.agent.speed, startHeroSpeed, heroClass.E_Ability.abilityDuration));
                Debug.Log("Coroutine Called!");
            }
        }

        // Keep track of CD and Hud Icon
        if (heroClass.E_Ability.isCooldown)
        {
            heroClass.E_Ability.HUDIcon.fillAmount -= 1 / heroClass.E_Ability.abilityCooldown * Time.deltaTime;
            E_Ability_Indicator.enabled = false;
            E_Ability_Range.enabled = false;

            if (heroClass.E_Ability.HUDIcon.fillAmount <= 0)
            {
                heroClass.E_Ability.HUDIcon.fillAmount = 0;
                heroClass.E_Ability.isCooldown = false;
            }
        }
    }

    #endregion
}
