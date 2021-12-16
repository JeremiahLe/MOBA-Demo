using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float projRange;

    public bool projTargeted;
    public GameObject target;
    public Vector3 targetLoc;
    public Vector3 move;
    public bool dieAfterAnimation;
    bool hitObject;

    public GameObject projCreator;
    Rigidbody rb;
    Collider col;

    public EnemyStatsScript enemyStats;
    

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        Physics.IgnoreCollision(projCreator.GetComponent<Collider>(), col);

        TargetableScript[] myItems = FindObjectsOfType(typeof(TargetableScript)) as TargetableScript[];
        //Debug.Log("Found " + myItems.Length + " instances with this script attached");
        foreach (TargetableScript item in myItems)
        {
            if (item.GetComponent<TargetableScript>().enemyType != TargetableScript.EnemyType.Minion)
            {
                Physics.IgnoreCollision(item.gameObject.GetComponent<Collider>(), col);
            }
        }

        Invoke("Die", projRange);

        projAbilityType = (ProjAbilityType)System.Enum.Parse(typeof(ProjAbilityType), projAbilityTypeString);

        move = new Vector3(targetLoc.x, targetLoc.y, targetLoc.z);

        hitObject = false;
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

            default:
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

        transform.rotation = Quaternion.LookRotation(Vector3.up, -move); // negative flips it the correct orientation if needed
    }

    private void OnCollisionEnter(Collision colEnemy)
    {
        // Checks if it already hit something or not. This prevents a singular instance skillshot from hitting multiple tightly grouped enemies. Remove this clause for AOE or Piercing Skillshots.
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

        switch (projAbilityType)
        {
            case ProjAbilityType.Skillshot:
                hitObject = true;
                Destroy(gameObject);
                break;

            case ProjAbilityType.AOESkillShot:
                break;

            default:
                break;
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
