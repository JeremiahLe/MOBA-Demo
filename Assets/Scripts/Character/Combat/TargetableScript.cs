using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableScript : MonoBehaviour
{
    public enum EnemyType { Minion, EnemyHero, AllyHero, Hero, FriendlyMinion }
    public EnemyType enemyType;
}
