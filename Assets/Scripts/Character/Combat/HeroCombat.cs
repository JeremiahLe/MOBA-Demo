using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       

public class HeroCombat : MonoBehaviour
{
    // Hero Init
    public enum HeroAttackType { Melee, Ranged };
    public HeroAttackType heroAttackType;

    // Combat targeting and range
    public GameObject targetedEnemy;
    [SerializeField] private float heroAttackRange;
    [SerializeField] private float heroRotateSpeedForAttack;
    public bool moveToEnemy = false;

    public GameObject prevEnemyRef;
    public GameObject enemyToAttack;

    // Combat range visuals
    [SerializeField] private Sprite attackRange_Indicator_Sprite;
    [SerializeField] private Sprite NoVisual_Sprite;
    [SerializeField] private Canvas attackRange_Canvas;
    private float offset;
    [SerializeField] private KeyCode checkAttackRange;
    private float radius;
    [SerializeField] private Image attackRange_Indicator_Image;

    [Range(0.01f, 1.0f)]
    [SerializeField] private float Range_ScaleFactor;

    // Character Animator
    private CharacterMovementScript moveScript;
    private HeroClass heroClassScript;
    public Animator anim;

    AbilityClass Q_Ability;

    // Combat Variables
    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;
    public bool notCasting = true;
    public float priorHeroAttackSpeed;

    // Other
    public GameObject DamageTextPopup_object;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<CharacterMovementScript>();
        heroClassScript = GetComponent<HeroClass>();
        anim = GetComponentInChildren<Animator>();

        // Preload damage popup UI element
        DamageTextPopup_object = Resources.Load<GameObject>("Prefabs/DamagePopup_UI");

        // Scale Attack Range Indicator Image to Attack Range variable
        attackRange_Indicator_Image.transform.localScale = new Vector3(heroAttackRange * Range_ScaleFactor, heroAttackRange * Range_ScaleFactor, heroAttackRange * Range_ScaleFactor);
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttackRange();
        CheckCombat();
    }

    void CheckCombat()
    {
        if (targetedEnemy != null)
        {
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > heroAttackRange) // if otuside range, move inside range
            {
                if (moveToEnemy == true)
                {
                    moveScript.agent.SetDestination(targetedEnemy.transform.position);
                    moveScript.agent.stoppingDistance = heroAttackRange;
                }

            }
            else if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) < heroAttackRange && moveToEnemy == true) // once inside range, and can move
            {
                if (heroAttackType == HeroAttackType.Melee)
                {
                    FaceTarget();

                    StopCoroutine(MeleeAttackInterval());

                    if (performMeleeAttack)
                    {

                        //Debug.Log("Attack the minion");

                        // Start Courotine
                        //if (targetedEnemy.GetComponent<Enemy_Combat_Script>().isEnemyAlive)
                            StartCoroutine(MeleeAttackInterval());
                    }
                }
            }
        }
        else if (targetedEnemy == null)
        {
            anim.SetBool("Basic Attack", false); // fix ghost autos?
            performMeleeAttack = true;
            prevEnemyRef = null;
            enemyToAttack = null;
        }
    }

    IEnumerator MeleeAttackInterval()
    {
        prevEnemyRef = targetedEnemy;
        performMeleeAttack = false;
        anim.SetFloat("AttackAnimSpeed", heroClassScript.heroAttackSpeed);
        anim.SetBool("Basic Attack", true);

        yield return new WaitForSeconds(heroClassScript.heroAttackTime / ((100 + heroClassScript.heroAttackTime) * 0.01f));

        if (targetedEnemy == null)
        {
            anim.SetBool("Basic Attack", false);
            performMeleeAttack = true;
        }
    }

    public void MeleeAttack()
    {
        if (targetedEnemy != null && targetedEnemy == prevEnemyRef)
        {
            //if (targetedEnemy.GetComponent<TargetableScript>().enemyType == TargetableScript.EnemyType.Minion)
            //if (targetedEnemy == prevEnemyRef)
            //if (moveToEnemy)

            // This is the damage formula
            float damageCalc = heroClassScript.heroAttackDmg - (targetedEnemy.GetComponent<EnemyStatsScript>().enemyDef * 0.1f);
            damageCalc = Mathf.Round(damageCalc);


            // Damage calcs for maximum resistance
            if (damageCalc <= 1f)
            {
                // Check if attack would leave the target dead, if so grant xp (make sure the target only grants xp once)
                if (targetedEnemy.GetComponent<EnemyStatsScript>().enemyHealth - damageCalc <= 0f && targetedEnemy.GetComponent<EnemyStatsScript>().enemyHealth >= 0)
                {
                    heroClassScript.heroExp += targetedEnemy.GetComponent<EnemyStatsScript>().enemyExpValue;
                }

                damageCalc = 1f;
                targetedEnemy.GetComponent<EnemyStatsScript>().enemyHealth -= damageCalc;
            }
            else
            {
                // Check if attack would leave the target dead, if so grant xp (make sure the target only grants xp once)
                if (targetedEnemy.GetComponent<EnemyStatsScript>().enemyHealth - damageCalc <= 0f && targetedEnemy.GetComponent<EnemyStatsScript>().enemyHealth >= 0)
                {
                    heroClassScript.heroExp += targetedEnemy.GetComponent<EnemyStatsScript>().enemyExpValue;
                }

                targetedEnemy.GetComponent<EnemyStatsScript>().enemyHealth -= damageCalc;
            }

            // Showing damage number UI popup on screen
            GameObject go = Instantiate(DamageTextPopup_object, targetedEnemy.transform.position + new Vector3(Random.Range(-1f, 1f),
        Random.Range(0, 0), 0), Quaternion.identity);
            go.GetComponent<DamageText_UI>().damageNumber.text = "-" + damageCalc.ToString();
            go.GetComponent<EnemyHealthSlider>().typeOfObject = "UI";
        }

        // Reset the basic attack corountine
        performMeleeAttack = true;
    }

    void CheckAttackRange()
    {
        if (Input.GetKeyDown(checkAttackRange) && attackRange_Indicator_Image.sprite == NoVisual_Sprite)
        {
            attackRange_Indicator_Image.sprite = attackRange_Indicator_Sprite;
            heroClassScript.GetUpdatedStats_Ekard(heroClassScript.Q_Ability, false);
        }
        else if (Input.GetKeyDown(checkAttackRange) && attackRange_Indicator_Image.sprite == attackRange_Indicator_Sprite)
            attackRange_Indicator_Image.sprite = NoVisual_Sprite;
    }

    void CheckHealth()
    {
        if (heroClassScript.heroHealth <= 0)
        {
            Destroy(gameObject);
            targetedEnemy = null;
            performMeleeAttack = false;
        }
    }

    void FaceTarget()
    {
        if (notCasting)
        {
            // Rotation ??
            Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y,
                ref moveScript.rotateVelocity,
                heroRotateSpeedForAttack * (Time.deltaTime * 5));

            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
    }

    public void CallAbilityCast(float _castTime)
    {
        notCasting = false;
        Invoke("ResetAbilityCastLockout", _castTime);
    }

    public void ResetAbilityCastLockout()
    {
        notCasting = true;
    }

    public void ResetAutoAttack(bool _AbilityCast)
    {
        priorHeroAttackSpeed = heroClassScript.heroAttackSpeed;

        if (_AbilityCast)
        {
            //Debug.Log("Reset Auto Attack Called", this);
            heroClassScript.heroAttackSpeed = 7.0f;
        }

        anim.StopPlayback();
        performMeleeAttack = false;
        anim.SetBool("Basic Attack", false);
        StopCoroutine(MeleeAttackInterval());
        Invoke("ResetCanAttack", 0.3f);
        //performMeleeAttack = true;
    }

    public void ResetCanAttack()
    {
        performMeleeAttack = true;
        heroClassScript.heroAttackSpeed = priorHeroAttackSpeed;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, heroAttackRange);
    //}
}
