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
    [SerializeField] private GameObject Q_Ability_Range_GameObject;
    [SerializeField] private GameObject Q_Ability_Indicator_GameObject;
    [SerializeField] private GameObject Q_Ability_Parent_GameObject;
    [SerializeField] private Sprite Q_Ability_Range_Sprite;
    [SerializeField] private Sprite Q_Ability_Indicator_Sprite;
    [SerializeField] private SpriteRenderer Q_Ability_Range_SpriteRenderer;
    [SerializeField] private SpriteRenderer Q_Ability_Indicator_SpriteRenderer;
    [SerializeField] private KeyCode Q_Ability_Keycode;

    [Header("W Ability")]
    [SerializeField] private GameObject W_Ability_Range_GameObject;
    [SerializeField] private GameObject W_Ability_Indicator_GameObject;
    [SerializeField] private GameObject W_Ability_Parent_GameObject;
    [SerializeField] private Sprite W_Ability_Range_Sprite;
    [SerializeField] private Sprite W_Ability_Indicator_Sprite;
    [SerializeField] private SpriteRenderer W_Ability_Range_SpriteRenderer;
    [SerializeField] private SpriteRenderer W_Ability_Indicator_SpriteRenderer;
    [SerializeField] private KeyCode W_Ability_Keycode;

    [Header("E Ability")]
    [SerializeField] private GameObject E_Ability_Range_GameObject;
    [SerializeField] private GameObject E_Ability_Indicator_GameObject;
    [SerializeField] private GameObject E_Ability_Parent_GameObject;
    [SerializeField] private Sprite E_Ability_Range_Sprite;
    [SerializeField] private Sprite E_Ability_Indicator_Sprite;
    [SerializeField] private SpriteRenderer E_Ability_Range_SpriteRenderer;
    [SerializeField] private SpriteRenderer E_Ability_Indicator_SpriteRenderer;
    [SerializeField] private KeyCode E_Ability_Keycode;

    [Header("R Ability")]
    [SerializeField] private GameObject R_Ability_Range_GameObject;
    [SerializeField] private GameObject R_Ability_Indicator_GameObject;
    [SerializeField] private GameObject R_Ability_Parent_GameObject;
    [SerializeField] private Sprite R_Ability_Range_Sprite;
    [SerializeField] private Sprite R_Ability_Indicator_Sprite;
    [SerializeField] private SpriteRenderer R_Ability_Range_SpriteRenderer;
    [SerializeField] private SpriteRenderer R_Ability_Indicator_SpriteRenderer;
    [SerializeField] private KeyCode R_Ability_Keycode;

    // This is where unique ability input goes
    [Header("Other Ability Setup (Case by Case)")]
    [SerializeField] private Sprite NoVisual_Sprite;
    private GameObject projectilePrefab;

    // Q - Skillshot
    [Header("Q Manual")]
    public GameObject Q_Projectile;
    private Vector3 position;
    private float Q_Ability_RangeNum;
    public Transform Q_Ability_Spawn;
    public Transform Q_Ability_TargetTransform;

    // W - AOE Skillshot
    private Vector3 posUp;
    [Header("W Manual")]
    [SerializeField] private float AOESkillshot_MaxAbility_Distance;
    public GameObject W_Projectile;
    public Transform W_Ability_Spawn;
    // This is a situational adjustment to the W indicator clipping through higher terrain / may be obsolete in the future
    //RectTransform W_Pic;

    // E - Self buff
    [Header("E Manual")]
    public ParticleSystem E_VFX_ParticleSystem;

    // R - Targeted Ability
    [Header("R Manual")]
    public GameObject R_Projectile;
    public GameObject targetedEnemyRef;
    public Transform R_Ability_Spawn;
    private float R_AbilityRangeNum;
    public bool R_Ability_MoveThenInRange = false;

    #endregion

    #region Other Components
    [Header("Other Components")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask terrainMask;
    [SerializeField] private SystemNotificationManager_Script systemScript;

    public bool Ability_MoveInRangeCast = false;
    public float startHeroSpeed;
    #endregion

    #region Component Initilization
    HeroClass heroClass;
    private CharacterMovementScript moveScript;
    HeroCombat heroCombat;
    #endregion

    void Start()
    {
        // Get our hero class component, getting each ability etc.
        heroClass = GetComponent<HeroClass>();
        moveScript = GetComponent<CharacterMovementScript>();
        heroCombat = gameObject.GetComponent<HeroCombat>();
        systemScript = FindObjectOfType<SystemNotificationManager_Script>();

        // Ability GetComponents
        Q_Ability_Indicator_SpriteRenderer = Q_Ability_Indicator_GameObject.GetComponent<SpriteRenderer>();
        Q_Ability_Range_SpriteRenderer = Q_Ability_Range_GameObject.GetComponent<SpriteRenderer>();

        W_Ability_Indicator_SpriteRenderer = W_Ability_Indicator_GameObject.GetComponent<SpriteRenderer>();
        W_Ability_Range_SpriteRenderer = W_Ability_Range_GameObject.GetComponent<SpriteRenderer>();

        E_Ability_Indicator_SpriteRenderer = E_Ability_Indicator_GameObject.GetComponent<SpriteRenderer>();
        E_Ability_Range_SpriteRenderer = E_Ability_Range_GameObject.GetComponent<SpriteRenderer>();

        //R_Ability_Indicator_SpriteRenderer = R_Ability_Indicator_GameObject.GetComponent<SpriteRenderer>();
        //R_Ability_Range_SpriteRenderer = R_Ability_Range_GameObject.GetComponent<SpriteRenderer>();

        // Debug //
        // Set every Ability GameObject sprite to nonvisible at runtime.
        Q_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
        Q_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

        W_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
        W_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

        E_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
        E_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

        //R_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
        //R_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

        #region Case By Case Hero Init

        // Q Ability - Cube
        //Q_Ability_RangeNum = heroClass.Q_Ability.abilityRangeNum;
        //Q_Ability_Indicator.rectTransform.localScale = new Vector2(Q_Ability_Indicator.rectTransform.localScale.x, Q_Ability_RangeNum * 0.05f);

        // W Ability - Cube
        //AOESkillshot_MaxAbility_Distance = heroClass.W_Ability.abilityRangeNum;
        //W_Ability_Range.rectTransform.localScale = new Vector2(AOESkillshot_MaxAbility_Distance * 0.1f, AOESkillshot_MaxAbility_Distance * 0.1f);
        //RectTransform AOESkillshot_Container = W_Ability_Indicator_Image.GetComponent<RectTransform>();
        //AOESkillshot_Container.anchoredPosition3D = new Vector3(AOESkillshot_Container.anchoredPosition3D.x, AOESkillshot_Container.anchoredPosition3D.y + 10, AOESkillshot_Container.anchoredPosition3D.z);

        // R Ability - Cube
        //R_AbilityRangeNum = heroClass.R_Ability.abilityRangeNum;
        //R_Ability_Range.rectTransform.localScale = new Vector2(R_AbilityRangeNum * 0.1f, R_AbilityRangeNum * 0.1f);
        #endregion
    }

    private void Update()
    {
        // Ability Code
        Q_Ability();
        W_Ability();
        E_Ability();
        //R_Ability();

        // Made this for targeted ability casting once in range
        //if (currentAbility != null && currentAbility.typeOfAbilityCast == AbilityClass.TypeOfAbilityCast.Targeted)
            //CastAbilityOnceInRange(currentAbility, currentAbility.abilityRangeNum);

        // Canvas Control for abilities
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        #region Case By Case Hero Init

        // Ability 1 Inputs - Cube
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainMask))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        // Ability 1 Canvas Inputs - Cube
        /// Skillshot ability indicator rotation code
        Quaternion transRot = Quaternion.LookRotation(position - playerTransform.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, 0);
        Q_Ability_Parent_GameObject.transform.rotation = Quaternion.Lerp(transRot, Q_Ability_Parent_GameObject.transform.rotation, 0f);

        // Ability 2 Inputs - Cube
        if (currentAbility == heroClass.W_Ability)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainMask))
            {
                if (hit.collider.gameObject != this.gameObject)
                {
                    posUp = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    position = hit.point;
                }
            }

            // Ability 2 Canvas Inputs - Cube
            /// AOE Ability Range indicator code
            var hitPosDir = (hit.point - transform.position).normalized;
            float distance = Vector3.Distance(hit.point, transform.position);
            distance = Mathf.Min(distance, AOESkillshot_MaxAbility_Distance);

            var newHitPos = transform.position + hitPosDir * distance;
            W_Ability_Indicator_GameObject.transform.position = new Vector3(newHitPos.x, newHitPos.y + 1.1f, newHitPos.z);

            // This is a situational adjustment to the W indicator clipping through higher terrain / may be obsolete in the future
            if (currentAbility.abilityKeyCode == KeyCode.W)
            {
            //W_Pic = W_Ability_Canvas.GetComponent<RectTransform>();
            //W_Pic.anchoredPosition3D = new Vector3(W_Pic.anchoredPosition3D.x, W_Pic.anchoredPosition3D.y + 2, W_Pic.anchoredPosition3D.z);
            }
        }

        #endregion
    }

    IEnumerator ResetStatValue(string StatName, float StatToReset, float StatDefaultValue, float time)
    {
        yield return new WaitForSeconds(time);

        switch (StatName) {

            case "Speed":
            moveScript.agent.speed = StatDefaultValue;
            E_VFX_ParticleSystem.Stop();
            Debug.Log("Coroutine Executed!");
                break;

            case "Attack Speed":
            heroClass.heroAttackSpeed = StatDefaultValue;
                break;

        }
    }

    void CreateAbility(AbilityClass _ability, GameObject _prefab,
        Vector3 _abilitySpawn, GameObject targetPos, Vector3 _abilityTargetDir, bool _targeted)
    {
        Debug.Log(_ability.abilityName + " created!");
        GameObject _temp = Instantiate(_prefab, _abilitySpawn, Quaternion.Euler(-90, 0, 0));

        //Rigidbody rb = _temp.GetComponent<Rigidbody>();
        //rb.AddRelativeForce(transform.forward * _ability.abilitySpeed, ForceMode.Impulse);
        _temp.GetComponent<ProjectileScript>().target = targetPos;
        _temp.GetComponent<ProjectileScript>().targetLoc = _abilityTargetDir;
        _temp.GetComponent<ProjectileScript>().projSpeed = _ability.abilitySpeed;
        _temp.GetComponent<ProjectileScript>().projDamage = _ability.abilityBaseDamage + heroClass.heroAbilityDmg;
        _temp.GetComponent<ProjectileScript>().projAbilityTypeString = _ability.typeOfAbilityCast.ToString();
        _temp.GetComponent<ProjectileScript>().projDamageType = ProjectileScript.ProjDamageType.Ability;
        _temp.GetComponent<ProjectileScript>().projCreator = gameObject;
        _temp.GetComponent<ProjectileScript>().projRange = _ability.abilityRangeNum / 14f; // FIXME // WHEN PROJ OUT OF RANGE, DESTROY
        _temp.GetComponent<ProjectileScript>().projTargeted = _targeted;
    }

    /*
    void CastAbilityOnceInRange(AbilityClass _currentAbility, float _range) // TODO - make it so that it is more universal
    {
        if (R_Ability_MoveThenInRange && heroCombat.targetedEnemy != null)
        {
            if (Vector3.Distance(gameObject.transform.position, heroCombat.targetedEnemy.transform.position) < _range)
            {
                //Debug.Log("In range!!!!");

                // Check Mana, then Cast Ability
                if (heroClass.heroMana >= heroClass.R_Ability.abilityCost)
                {
                    // FIXME // before instantiation, create a Cast Time buffer and UI element
                    targetedEnemyRef = heroCombat.targetedEnemy;
                    moveScript.JustStopMovement(true);
                    //Vector3 relativePos = Q_Ability_TargetTransform.position - transform.position;

                    // the second argument, upwards, defaults to Vector3.up
                    //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                    //transform.rotation = rotation;

                    //transform.LookAt(Q_Ability_TargetTransform.position + transform.forward);

                    CreateAbility(heroClass.R_Ability, R_Projectile, R_Ability_Spawn.transform.position, targetedEnemyRef, targetedEnemyRef.transform.position, true);
                    heroClass.heroMana -= heroClass.R_Ability.abilityCost;

                    Debug.Log("Used R Ability!");
                    heroClass.R_Ability.isCooldown = true;
                    heroClass.R_Ability.HUDIcon.fillAmount = 1;
                    R_Ability_MoveThenInRange = false;
                }
                else // not enough mana
                {
                    Debug.Log("Not enough mana!");
                    systemScript.AlertObservers("Not enough Mana!");

                    currentAbility = null;
                    Debug.Log("R ability was canceled.");
                    R_Ability_Indicator.enabled = false;
                    R_Ability_Range.enabled = false;
                    R_Ability_MoveThenInRange = false;
                }
            }
        }
    }
    */

    #region Case by Case Ability Function Calls

    /// <summary>
    /// Ability Function Calls for - Cube
    /// </summary>

    void Q_Ability()
    {
        // Prep or cancel ability indicators
        if (Input.GetKeyDown(Q_Ability_Keycode) && heroClass.Q_Ability.isCooldown == false)
        {
            // If enough mana before showing indictors // TODO - Quick Cast check
            if (heroClass.Q_Ability.abilityCost <= heroClass.heroMana)
            { // If ability isn't prepped already
                if (currentAbility != heroClass.Q_Ability)
                {
                    currentAbility = heroClass.Q_Ability;
                    Debug.Log("Q ability was prepped.");

                    Q_Ability_Indicator_SpriteRenderer.sprite = Q_Ability_Indicator_Sprite;
                    Q_Ability_Range_SpriteRenderer.sprite = Q_Ability_Range_Sprite;

                    W_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    W_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

                    E_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    E_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

                    //R_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    //R_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;
                } // If ability was already prepped
                else if (currentAbility == heroClass.Q_Ability)
                {
                    currentAbility = null;
                    Q_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    Q_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;
                    Debug.Log("Q ability was canceled!");
                }
            } // If not enough mana to cast
            else if (heroClass.Q_Ability.abilityCost > heroClass.heroMana)
            {
                currentAbility = null;
                systemScript.AlertObservers("Not enough Mana!");
            }
        } // If ability is on cooldown
        else if (Input.GetKeyDown(Q_Ability_Keycode) && heroClass.Q_Ability.isCooldown == true)
        {
            currentAbility = null;
            Debug.Log("Q Ability is on Cooldown!");
            systemScript.AlertObservers("Ability is on cooldown!");
        }

        // Use ability?
        if (Input.GetMouseButtonDown(0) && currentAbility == heroClass.Q_Ability)
        {
            // Check Mana, then Cast Ability
            if (heroClass.heroMana >= heroClass.Q_Ability.abilityCost)
            {
                // FIXME // before instantiation, create a Cast Time buffer and UI element
                Vector3 relativePos = Q_Ability_TargetTransform.position - transform.position;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = rotation;
                //transform.LookAt(Q_Ability_TargetTransform.position + transform.forward);

                // TODO - Check if can cast while moving, check if has cast time, check if auto reset
                moveScript.JustStopMovement(true);
                heroCombat.CallAbilityCast(0.5f);
                heroCombat.ResetAutoAttack(false);

                CreateAbility(heroClass.Q_Ability, Q_Projectile, Q_Ability_Spawn.transform.position, null, relativePos, false);
                heroClass.heroMana -= heroClass.Q_Ability.abilityCost;

                currentAbility = null;
                Debug.Log("Used Q Ability!");
                heroClass.Q_Ability.isCooldown = true;
                heroClass.Q_Ability.HUDIcon.fillAmount = 1;
            }
            else
            {
                Debug.Log("Not enough mana!");
                systemScript.AlertObservers("Not enough Mana!");

                currentAbility = null;
                Debug.Log("Q ability was canceled.");
                Q_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                Q_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;
            }

        }

        // Keep track of CD and Hud Icon
        if (heroClass.Q_Ability.isCooldown)
        {
            heroClass.Q_Ability.HUDIcon.fillAmount -= 1 / heroClass.Q_Ability.abilityCooldown * Time.deltaTime;
            Q_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
            Q_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

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
            // If enough mana before showing indictors // TODO - Quick Cast check
            if (heroClass.W_Ability.abilityCost <= heroClass.heroMana)
            { // If ability isn't prepped already
                if (currentAbility != heroClass.W_Ability)
                {
                    currentAbility = heroClass.W_Ability;
                    Debug.Log("W ability was prepped.");

                    Q_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    Q_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

                    W_Ability_Indicator_SpriteRenderer.sprite = W_Ability_Indicator_Sprite;
                    W_Ability_Range_SpriteRenderer.sprite = W_Ability_Range_Sprite;

                    E_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    E_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

                    //R_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    //R_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;
                } // If ability was already prepped
                else if (currentAbility == heroClass.W_Ability)
                {
                    currentAbility = null;
                    W_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    W_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;
                    Debug.Log("W ability was canceled!");
                }
            } // If not enough mana to cast
            else if (heroClass.W_Ability.abilityCost > heroClass.heroMana)
            {
                currentAbility = null;
                systemScript.AlertObservers("Not enough Mana!");
            }
        } // If ability is on cooldown
        else if (Input.GetKeyDown(W_Ability_Keycode) && heroClass.W_Ability.isCooldown == true)
        {
            currentAbility = null;
            Debug.Log("W Ability is on Cooldown!");
            systemScript.AlertObservers("Ability is on cooldown!");
        }

        // Use ability?
        if (Input.GetMouseButtonDown(0) && currentAbility == heroClass.W_Ability)
        {
            // Check Mana, then Cast Ability
            if (heroClass.heroMana >= heroClass.W_Ability.abilityCost)
            {
                // FIXME // before instantiation, create a Cast Time buffer and UI element
                //moveScript.JustStopMovement(true);
                //Vector3 relativePos = Q_Ability_TargetTransform.position - transform.position;

                // the second argument, upwards, defaults to Vector3.up
                //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                //transform.rotation = rotation;

                //transform.LookAt(Q_Ability_TargetTransform.position + transform.forward);
                CreateAbility(heroClass.W_Ability, W_Projectile, W_Ability_Spawn.transform.position, null, Vector3.zero, false);
                heroClass.heroMana -= heroClass.W_Ability.abilityCost;

                currentAbility = null;
                Debug.Log("Used W Ability!");
                heroClass.W_Ability.isCooldown = true;
                heroClass.W_Ability.HUDIcon.fillAmount = 1;
            }
            else
            {
                Debug.Log("Not enough mana!");
                systemScript.AlertObservers("Not enough Mana!");

                currentAbility = null;
                Debug.Log("W ability was canceled.");
                W_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                W_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;
            }

        }

        // Keep track of CD and Hud Icon
        if (heroClass.W_Ability.isCooldown)
        {
            heroClass.W_Ability.HUDIcon.fillAmount -= 1 / heroClass.W_Ability.abilityCooldown * Time.deltaTime;
            W_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
            W_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

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
            // If enough mana before showing indictors // TODO - Quick Cast check
            if (heroClass.E_Ability.abilityCost <= heroClass.heroMana)
            { // If ability isn't prepped already
                if (currentAbility != heroClass.E_Ability)
                {
                    currentAbility = heroClass.E_Ability;
                    Debug.Log("E ability was prepped.");

                    Q_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    Q_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

                    W_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    W_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

                    E_Ability_Indicator_SpriteRenderer.sprite = E_Ability_Indicator_Sprite;
                    E_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

                    //R_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    //R_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;
                } // If ability was already prepped
                else if (currentAbility == heroClass.E_Ability)
                {
                    currentAbility = null;
                    E_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                    E_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;
                    Debug.Log("E ability was canceled!");
                }
            } // If not enough mana to cast
            else if (heroClass.E_Ability.abilityCost > heroClass.heroMana)
            {
                currentAbility = null;
                systemScript.AlertObservers("Not enough Mana!");
            }
        } // If ability is on cooldown
        else if (Input.GetKeyDown(E_Ability_Keycode) && heroClass.E_Ability.isCooldown == true)
        {
            currentAbility = null;
            Debug.Log("E Ability is on Cooldown!");
            systemScript.AlertObservers("Ability is on cooldown!");
        }

        // Use ability?
        if (Input.GetMouseButtonDown(0) && currentAbility == heroClass.E_Ability)
        {
            // Check Mana, then Cast Ability
            if (heroClass.heroMana >= heroClass.E_Ability.abilityCost)
            {
                // FIXME // before instantiation, create a Cast Time buffer and UI element
                //moveScript.JustStopMovement(true);
                //Vector3 relativePos = Q_Ability_TargetTransform.position - transform.position;

                // the second argument, upwards, defaults to Vector3.up
                //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                //transform.rotation = rotation;

                //transform.LookAt(Q_Ability_TargetTransform.position + transform.forward);

                /// Cast Buff
                startHeroSpeed = heroClass.heroSpeed;
                moveScript.agent.speed += moveScript.agent.speed * heroClass.E_Ability.abilityBuffPercentage;
                StartCoroutine(ResetStatValue("Speed", moveScript.agent.speed, startHeroSpeed, heroClass.E_Ability.abilityDuration));

                float startHeroAttackSpeed = heroClass.heroAttackSpeed;
                heroClass.heroAttackSpeed += heroClass.heroAttackSpeed * (heroClass.E_Ability.abilityBuffPercentage);
                StartCoroutine(ResetStatValue("Attack Speed", heroClass.heroAttackSpeed, startHeroAttackSpeed, heroClass.E_Ability.abilityDuration));

                heroCombat.ResetAutoAttack(true);

                E_VFX_ParticleSystem.Play();
                Debug.Log("Coroutine Called!");
                heroClass.heroMana -= heroClass.E_Ability.abilityCost;

                currentAbility = null;
                Debug.Log("Used E Ability!");
                heroClass.E_Ability.isCooldown = true;
                heroClass.E_Ability.HUDIcon.fillAmount = 1;
            }
            else
            {
                Debug.Log("Not enough mana!");
                systemScript.AlertObservers("Not enough Mana!");

                currentAbility = null;
                Debug.Log("E ability was canceled.");
                E_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
                E_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;
            }

        }

        // Keep track of CD and Hud Icon
        if (heroClass.E_Ability.isCooldown)
        {
            heroClass.E_Ability.HUDIcon.fillAmount -= 1 / heroClass.E_Ability.abilityCooldown * Time.deltaTime;
            E_Ability_Indicator_SpriteRenderer.sprite = NoVisual_Sprite;
            E_Ability_Range_SpriteRenderer.sprite = NoVisual_Sprite;

            if (heroClass.E_Ability.HUDIcon.fillAmount <= 0)
            {
                heroClass.E_Ability.HUDIcon.fillAmount = 0;
                heroClass.E_Ability.isCooldown = false;
            }
        }
    }
    /*
    void R_Ability()
    {
        // Prep or cancel ability indicators
        if (Input.GetKeyDown(R_Ability_Keycode) && heroClass.R_Ability.isCooldown == false)
        {
            if (R_Ability_Indicator.enabled == false)
            {
                currentAbility = heroClass.R_Ability;
                Debug.Log("R ability was prepped.");
                Q_Ability_Indicator.enabled = false;
                Q_Ability_Range.enabled = false;

                W_Ability_Indicator_Image.enabled = false;
                W_Ability_Range.enabled = false;

                E_Ability_Indicator.enabled = false;
                E_Ability_Range.enabled = false;

                R_Ability_Indicator.enabled = true;
                R_Ability_Range.enabled = true;
            }
            else
            {
                currentAbility = null;
                Debug.Log("R ability was canceled.");
                R_Ability_Indicator.enabled = false;
                R_Ability_Range.enabled = false;
            }
        }
        else if (Input.GetKeyDown(R_Ability_Keycode) && heroClass.R_Ability.isCooldown == true)
        {
            Debug.Log("R Ability is on Cooldown!");
            systemScript.AlertObservers("Ability is on cooldown!");
        }

        // Use ability?
        if (R_Ability_Indicator.enabled == true && Input.GetMouseButton(0) && currentAbility.abilityKeyCode == R_Ability_Keycode && heroCombat.targetedEnemy != null)
        {
            if (Vector3.Distance(gameObject.transform.position, heroCombat.targetedEnemy.transform.position) > R_AbilityRangeNum)
            {
                if (heroCombat.targetedEnemy != null)
                {
                    moveScript.agent.SetDestination(heroCombat.targetedEnemy.transform.position);
                    moveScript.agent.stoppingDistance = R_AbilityRangeNum;

                    //Debug.Log("Out of range!!!!");

                    R_Ability_MoveThenInRange = true;
                }
            }
            else if (Vector3.Distance(gameObject.transform.position, heroCombat.targetedEnemy.transform.position) < R_AbilityRangeNum)
            {
                //Debug.Log("In range!!!!");

                // Check Mana, then Cast Ability
                if (heroClass.heroMana >= heroClass.R_Ability.abilityCost)
                {
                    // FIXME // before instantiation, create a Cast Time buffer and UI element
                    targetedEnemyRef = heroCombat.targetedEnemy;
                    moveScript.JustStopMovement(true);
                    //Vector3 relativePos = Q_Ability_TargetTransform.position - transform.position;

                    // the second argument, upwards, defaults to Vector3.up
                    //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                    //transform.rotation = rotation;

                    //transform.LookAt(Q_Ability_TargetTransform.position + transform.forward);

                    CreateAbility(heroClass.R_Ability, R_Projectile, R_Ability_Spawn.transform.position, targetedEnemyRef, targetedEnemyRef.transform.position, true);
                    heroClass.heroMana -= heroClass.R_Ability.abilityCost;

                    Debug.Log("Used R Ability!");
                    heroClass.R_Ability.isCooldown = true;
                    heroClass.R_Ability.HUDIcon.fillAmount = 1;
                }
                else // not enough mana
                {
                    Debug.Log("Not enough mana!");
                    systemScript.AlertObservers("Not enough Mana!");

                    currentAbility = null;
                    Debug.Log("R ability was canceled.");
                    R_Ability_Indicator.enabled = false;
                    R_Ability_Range.enabled = false;
                }
            }
        }

        // Keep track of CD and Hud Icon
        if (heroClass.R_Ability.isCooldown)
        {
            heroClass.R_Ability.HUDIcon.fillAmount -= 1 / heroClass.R_Ability.abilityCooldown * Time.deltaTime;
            R_Ability_Indicator.enabled = false;
            R_Ability_Range.enabled = false;

            if (heroClass.R_Ability.HUDIcon.fillAmount <= 0)
            {
                heroClass.R_Ability.HUDIcon.fillAmount = 0;
                heroClass.R_Ability.isCooldown = false;
            }
        }
    }
    */
    #endregion

    //private void OnDrawGizmosSelected()
    //{
    //Gizmos.color = Color.red;
    //Gizmos.DrawWireSphere(transform.position, heroClass.R_Ability.abilityRangeNum);

    //Gizmos.color = Color.blue;
    //Gizmos.DrawWireSphere(transform.position, heroClass.R_Ability.abilityRangeNum);
    //}

}
