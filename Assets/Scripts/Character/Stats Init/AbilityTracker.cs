using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTracker : MonoBehaviour
{
    #region New Ability Refactoring
    [DisplayWithoutEdit] public AbilityClass currentAbility;
    [Header("Ability Canvas Inputs")]
    [SerializeField] private Canvas abilityCanvas;
    [SerializeField] private Image abilityIndicator;
    [SerializeField] private Image abilityRange;

    Sprite abilityIndicatorSprite;
    Sprite abilityRangeSprite;
    #endregion

    #region Ability Code
    [Header("Q Ability")]
    //[SerializeField] private Image Q_AbilityImage;
    //[SerializeField] private float Q_cooldown;
    [SerializeField] private KeyCode Q_Ability_Keycode;

    // Ability 1 Input Variables
    Vector3 position;
    //[SerializeField] private Canvas Q_Ability_Canvas;
    //[SerializeField] private Image Q_Skillshot;

    [Header("W Ability")]
    //[SerializeField] private Image W_AbilityImage;
    //[SerializeField] private float W_cooldown;
    [SerializeField] private KeyCode W_Ability_Keycode;

    // Ability 2 Input Variables
    //[SerializeField] private Image W_targetCircleIndicator;
    //[SerializeField] private Image W_rangeCircleIndicator;
    [SerializeField] private Canvas W_Ability_Canvas;
    private Vector3 posUp;
    [SerializeField] private float W_MaxAbilityDistance;

    [Header("E Ability")]
    //[SerializeField] private Image E_AbilityImage;
    //[SerializeField] private float E_cooldown;
    [SerializeField] private KeyCode E_Ability_Keycode;

    [Header("R Ability")]
    //[SerializeField] private Image R_AbilityImage;
    //[SerializeField] private float R_cooldown;
    [SerializeField] private KeyCode R_Ability_Keycode;

    #endregion

    #region Misc. Code
    [Header("Other Components")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask terrainMask;
    #endregion

    #region Component Initilization
    HeroClass heroClass;

    //Image Q_Skillshot_Indicator_Image;
    Image W_TargetCircleIndicator_Image;
    Image W_RangeCircleIndicator_Image;
    #endregion

    #region Debug
    #endregion

    void Start()
    {
        #region Get Component Initialization

        heroClass = GetComponent<HeroClass>();
        abilityIndicatorSprite = abilityIndicator.GetComponent<UnityEngine.UI.Image>().sprite;

        #endregion

        #region Old Canvas Components 


        // Initialize ability indicator image components
        //Q_Skillshot_Indicator_Image = Q_Skillshot.GetComponent<Image>();

        //W_TargetCircleIndicator_Image = W_targetCircleIndicator.GetComponent<Image>();
        //W_RangeCircleIndicator_Image = W_rangeCircleIndicator.GetComponent<Image>();

        //W_TargetCircleIndicator_Image = heroClass.W_Ability.Indicator;

        // Make all canvas images (ability indicators) to non-visible at the start of the game
        //Q_Skillshot_Indicator_Image.enabled = false;
        W_TargetCircleIndicator_Image.enabled = false;
        W_RangeCircleIndicator_Image.enabled = false;

        // This is a situational adjustment to the W indicator clipping through higher terrain / may be obsolete in the future
        //RectTransform W_Pic = W_targetCircleIndicator.GetComponent<RectTransform>();
        //W_Pic.anchoredPosition3D = new Vector3(W_Pic.anchoredPosition3D.x, W_Pic.anchoredPosition3D.y + 10, W_Pic.anchoredPosition3D.z);
        #endregion

        #region New Ability Refactoring
        //heroAssignerScript = GetComponent<HeroAssignerScript>();
        

        #endregion
    }

    void Update()
    {
        Check_Ability_Input(Q_Ability_Keycode, W_Ability_Keycode, E_Ability_Keycode, R_Ability_Keycode);

        if (currentAbility != null)
        {
            if (currentAbility.abilityKeyCode == KeyCode.Q)
                Update_AbilityUI(heroClass.Q_Ability);
            else if (currentAbility.abilityKeyCode == KeyCode.W)
                Update_AbilityUI(heroClass.W_Ability);
        }

        UpdateHUDIcons(heroClass.Q_Ability);
        //UpdateHUDIcons(heroClass.W_Ability);
        //UpdateHUDIcons(heroClass.E_Ability);
        //UpdateHUDIcons(heroClass.R_Ability);

        //Update_AbilityUI(heroClass.Q_Ability);
        //Update_AbilityUI(heroClass.W_Ability);

        #region Canvas Control For Ability Targeting
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Ability 1 Inputs
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainMask))
        //{
        //    position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        //}

        // Ability 2 Inputs
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                posUp = new Vector3(hit.point.x, 10f, hit.point.z);
                position = hit.point;
            }
        }

        /// Ability 1 Canvas Inputs
        /// Skillshot ability indicator rotation code
        //Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        //transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, 0);
        //Q_Ability_Canvas.transform.rotation = Quaternion.Lerp(transRot, Q_Ability_Canvas.transform.rotation, 0f);

        /// Ability 2 Canvas Inputs
        /// AOE Ability Range indicator code
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, W_MaxAbilityDistance);

        var newHitPos = transform.position + hitPosDir * distance;
        abilityCanvas.transform.position = (newHitPos);
        #endregion
    }

    #region ToDo: Ability Refactoring

    // Refactored Ability Function Calls

    void Check_Ability_Input(KeyCode Q_Keycode, KeyCode W_Keycode, KeyCode E_Keycode, KeyCode R_Keycode)
    {
        // KeyCode Initialization ~ this is where hotkeys are setup, if any
        Q_Ability_Keycode = Q_Keycode;
        W_Ability_Keycode = W_Keycode;
        E_Ability_Keycode = E_Keycode;
        R_Ability_Keycode = R_Keycode;

        // Call generic ability function with Q Ability Class, which should have all of its data
        if (Input.GetKeyDown(Q_Ability_Keycode)) {
            Prep_Ability(heroClass.Q_Ability); 
        }
        else
        if (Input.GetKeyDown(W_Ability_Keycode))
        {
            Prep_Ability(heroClass.W_Ability);
        }

        if (Input.GetMouseButtonDown(0)) {
            if (currentAbility != null)
            {
                switch (currentAbility.abilityKeyCode)
                {
                    case KeyCode.Q: // this might need to be changed
                        Cast_Ability(heroClass.Q_Ability);
                        break;

                    default:
                        Debug.Log("No Ability was found?");
                        break;
                }
            }
        }
    }

    void Update_AbilityUI(AbilityClass ability)
    {
        if (currentAbility != null)
        {
            if (ability.typeOfAbilityCast == AbilityClass.TypeOfAbilityCast.Skillshot)
            {
                // Update the sprite of the image to whatever ability's indicator was passed in
                // Should look like an arrow because its a skillshot
                abilityIndicator.sprite = ability.Indicator;
                //abilityIndicator.transform.position = new Vector2(76.808f, 82.784f);

                // Raycast Input
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainMask))
                {
                    position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }

                // Skillshot Indicator Rotation Around Player Code
                Quaternion transRot = Quaternion.LookRotation(position - playerTransform.transform.position);
                transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, 0);
                abilityCanvas.transform.rotation = Quaternion.Lerp(transRot, abilityCanvas.transform.rotation, 0f);
            }
            else 
            if (ability.typeOfAbilityCast == AbilityClass.TypeOfAbilityCast.AOESkillShot)
            {
                // Update the sprite of the image to whatever ability's indicator was passed in
                // Should look like an arrow because its a skillshot
                abilityIndicator.sprite = ability.Indicator;

                // Raycast Input
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject != this.gameObject)
                    {
                        posUp = new Vector3(hit.point.x, 10f, hit.point.z);
                        position = hit.point;
                    }
                }

                // AOE Skillshot on mouse position plus range code
                var hitPosDir = (hit.point - transform.position).normalized;
                float distance = Vector3.Distance(hit.point, transform.position);
                distance = Mathf.Min(distance, W_MaxAbilityDistance);

                var newHitPos = transform.position + hitPosDir * distance;
                abilityCanvas.transform.position = (newHitPos);
            }
        }
    }

    void Cast_Ability(AbilityClass ability)
    {
        Debug.Log("Used " + ability.abilityKeyCode.ToString() + " Ability");
        currentAbility = null;
        abilityIndicator.enabled = false;
        abilityRange.enabled = false;
        ability.isCooldown = true;
        ability.HUDIcon.fillAmount = 1;
    }

    void Prep_Ability(AbilityClass ability)
    {
        // what ability was prepped?

        if (ability.isCooldown == false)
        {
            if (abilityIndicator.enabled == false)
            {
                currentAbility = ability;
                Debug.Log(ability.abilityKeyCode + " was prepped.");
                abilityIndicator.enabled = true;
                abilityRange.enabled = true;
            }
            else
            {
                currentAbility = null;
                Debug.Log(ability.abilityKeyCode + " was canceled.");
                abilityIndicator.enabled = false;
                abilityRange.enabled = false;
            }
        }
        else
            Debug.Log(ability.abilityKeyCode + " Ability is on cooldown!");
    }

    void UpdateHUDIcons(AbilityClass ability)
    {
        if (ability.isCooldown == true)
        {
            ability.HUDIcon.fillAmount -= 1 / ability.abilityCooldown * Time.deltaTime;

            if (ability.HUDIcon.fillAmount <= 0)
            {
                ability.HUDIcon.fillAmount = 0;
                ability.isCooldown = false;
            }
        }
    }

    AbilityClass GetAbility(KeyCode abilityKeycode, AbilityClass ac)
    {
        return ac;
    }

    #endregion

    #region Old Ability Function Calls

    // Old Ability Function Calls 

    /*
    void Q_Ability()
    {
        Debug.Log("Used Q Ability");

        
        if (Input.GetKey(Q_Ability_Keycode) && Q_isCooldown == false)
        {
            Q_Skillshot_Indicator_Image.enabled = true;
            W_TargetCircleIndicator_Image.enabled = false;
            W_RangeCircleIndicator_Image.enabled = false;
        }

        if (Q_Skillshot_Indicator_Image.enabled == true && Input.GetMouseButton(0))
        {
            Q_isCooldown = true;
            Q_AbilityImage.fillAmount = 1;
            Debug.Log("Used Q Ability!");
        }

        if (Q_isCooldown)
        {
            Q_AbilityImage.fillAmount -= 1 / Q_cooldown * Time.deltaTime;
            Q_Skillshot_Indicator_Image.enabled = false;

            if (Q_AbilityImage.fillAmount <= 0)
            {
                Q_AbilityImage.fillAmount = 0;
                Q_isCooldown = false;
            }
        }
    }
    */

    void W_Ability()
    {
        if (Input.GetKey(W_Ability_Keycode) && heroClass.W_Ability.isCooldown == false)
        {
            //Q_Skillshot_Indicator_Image.enabled = false;
            W_TargetCircleIndicator_Image.enabled = true;
            W_RangeCircleIndicator_Image.enabled = true;
        }

        if (W_TargetCircleIndicator_Image.enabled == true && Input.GetMouseButtonDown(0))
        {
            heroClass.W_Ability.isCooldown = true;
            heroClass.W_Ability.HUDIcon.fillAmount = 1;
            Debug.Log("Used W Ability!");
        }

        if (heroClass.W_Ability.isCooldown)
        {
            heroClass.W_Ability.HUDIcon.fillAmount -= 1 / heroClass.W_Ability.abilityCooldown * Time.deltaTime;
            W_TargetCircleIndicator_Image.enabled = false;
            W_RangeCircleIndicator_Image.enabled = false;

            if (heroClass.W_Ability.HUDIcon.fillAmount <= 0)
            {
                heroClass.W_Ability.HUDIcon.fillAmount = 0;
                heroClass.W_Ability.isCooldown = false;
            }
        }
    }
    /*
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
    */

    #endregion
}
