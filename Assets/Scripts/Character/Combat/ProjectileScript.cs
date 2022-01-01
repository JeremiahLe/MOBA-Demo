using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileScript : MonoBehaviour
{
    [Header("Basic Projectile Init")]
    SpriteRenderer sr;
    public enum ProjDamageType { Attack, Ability, True }
    public enum ProjAbilityType { Skillshot, Targeted, AOE, AOESkillShot, SelfBuff, TargetedBuff };
    public ProjDamageType projDamageType;
    public ProjAbilityType projAbilityType;
    public string projAbilityTypeString;

    public float projSpeed;
    public float projDamage;
    public float projDamageScaling;
    public float projRange;

    public bool projTargeted;

    public GameObject target;

    public Vector3 targetLoc;
    public Vector3 targetedPosFromTarget;
    public Vector3 move;

    public bool dieAfterAnimation;
    bool hitObject;

    public GameObject projCreator;
    Rigidbody rb;
    Collider col;

    float rotationForce = 250f;

    public EnemyStatsScript enemyStats;

    GameControl_Script gsScript;

    HeroClass heroClass;

    public GameObject DamageTextPopup_object;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        heroClass = projCreator.GetComponent<HeroClass>();

        projAbilityType = (ProjAbilityType)System.Enum.Parse(typeof(ProjAbilityType), projAbilityTypeString);

        Physics.IgnoreCollision(projCreator.GetComponent<Collider>(), col);
        TargetableScript[] myItems = FindObjectsOfType(typeof(TargetableScript)) as TargetableScript[];

        //Debug.Log("Found " + myItems.Length + " instances with this script attached");

        foreach (TargetableScript item in myItems)
        {
            if (item.GetComponent<TargetableScript>().enemyType != TargetableScript.EnemyType.Minion) // needs to check for enemy champs in the future
            {
                Physics.IgnoreCollision(item.gameObject.GetComponent<Collider>(), col);
            }
        }

        switch (projAbilityType)
        {
            case (ProjAbilityType.Skillshot):
                hitObject = false;
                Invoke("Die", projRange);
                move = new Vector3(targetLoc.x, targetLoc.y, targetLoc.z);
                break;

            case (ProjAbilityType.Targeted):
                targetedPosFromTarget = target.transform.position;
                break;
            
            case (ProjAbilityType.AOESkillShot):
                break;

            default:
                Debug.Log("Missing projectile type?");
                break;
        }

    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 difference = move - transform.position;
        //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        switch (projAbilityType)
        {
            case ProjAbilityType.Skillshot:
                rb.AddForce(move * projSpeed * Time.deltaTime - rb.velocity);
                break;

            case ProjAbilityType.AOESkillShot:
                break;

            case ProjAbilityType.Targeted:
                if (target != null)
                {
                    if (targetedPosFromTarget != Vector3.zero)
                    {
                        transform.rotation = Quaternion.Slerp(
                            transform.rotation,
                            Quaternion.LookRotation(targetedPosFromTarget),
                            Time.deltaTime * rotationForce
                        );
                    }
                    transform.position = Vector3.MoveTowards(transform.position, targetedPosFromTarget, projSpeed * Time.deltaTime);
                }
                else
                    Destroy(gameObject);
                break;

            default:
                Debug.Log("Missing projectile type?");
                break;
        }
    }

    private void Update()
    {
        //rb.AddRelativeForce(move * 10 - rb.velocity);
    }

    private void LateUpdate()
    {
        // Rotation ??
        //Quaternion rotationToLookAt = Quaternion.LookRotation(move);
        //float rotationQ = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref projSpeed,
        //0.75f * (Time.deltaTime * 5));

        switch (projAbilityType)
        {
            case ProjAbilityType.Skillshot:
                transform.rotation = Quaternion.LookRotation(Vector3.up, -move); // negative flips it the correct orientation if needed
                break;

            case ProjAbilityType.Targeted:
                if (target != null)
                    transform.rotation = Quaternion.LookRotation(Vector3.up, -targetedPosFromTarget);
                break;

            case ProjAbilityType.AOESkillShot:
                break;

            default:
                Debug.Log("Missing projectile type?");
                break;
        }
    }

    private void OnCollisionEnter(Collision colEnemy)
    {
        // Checks if it already hit something or not. This prevents a singular instance skillshot from hitting multiple tightly grouped enemies. Remove this clause for AOE or Piercing Skillshots.
        #region Old damage code
        /*
        if (hitObject == false)
        {
            enemyStats = colEnemy.gameObject.GetComponent<EnemyStatsScript>();

            float damageCalc = projDamage - (enemyStats.enemyRes * 0.1f);

            damageCalc = Mathf.Round(damageCalc);
            if (damageCalc <= 1f)
            {
                enemyStats.enemyHealth -= 1f;
                //healthCallRef.CallHealthTrigger(targetedEnemy);
            }
            else
            {
                enemyStats.enemyHealth -= damageCalc;
                //healthCallRef.CallHealthTrigger(targetedEnemy);
            }
        }
        */
        #endregion

        switch (projAbilityType)
        {
            case ProjAbilityType.Skillshot:
                DealDamage(colEnemy.gameObject.GetComponent<EnemyStatsScript>());
                hitObject = true;
                Destroy(gameObject);
                break;

            case ProjAbilityType.AOESkillShot:
                DealDamage(colEnemy.gameObject.GetComponent<EnemyStatsScript>());
                break;

            case ProjAbilityType.Targeted:
                if (target == colEnemy.gameObject)
                {
                    DealDamage(colEnemy.gameObject.GetComponent<EnemyStatsScript>());
                    Destroy(gameObject);
                }
                break;

            default:
                Debug.Log("Missing projectile type?");
                break;
        }
    }

    public void DealDamage(EnemyStatsScript enemyStats) // TODO - Switch damagetype and defense type based on damage type - attack, ability, true (ignore def & res)
    {
        if (hitObject == false)
        {
            float damageCalc = projDamage + (heroClass.heroAbilityDmg * projDamageScaling) - (enemyStats.enemyRes * 0.1f);
            damageCalc = Mathf.Round(damageCalc);

            DamageTextPopup_object = Resources.Load<GameObject>("Prefabs/DamagePopup_UI");
            GameObject go = Instantiate(DamageTextPopup_object, transform.position + new Vector3(Random.Range(-1f, 1f),
        Random.Range(0, 0), 0), Quaternion.identity);
            go.GetComponent<DamageText_UI>().damageNumber.text = "-" + damageCalc.ToString();
            go.GetComponent<EnemyHealthSlider>().typeOfObject = "UI";

            if (damageCalc <= 1f)
            {
                enemyStats.enemyHealth -= 1f;
                //healthCallRef.CallHealthTrigger(targetedEnemy);
            }
            else
            {
                enemyStats.enemyHealth -= damageCalc;
                //healthCallRef.CallHealthTrigger(targetedEnemy);
            }
        }
    }

    public void AlertObservers(string message)
    {
        switch (message) {

            case ("Animation End"):
                Destroy(gameObject);
                break;

            case ("Collider Start"):
                col.enabled = true;
                break;

            case ("Collider End"):
                col.enabled = false;
                break;

            default:
                break;
        }
    }

}
