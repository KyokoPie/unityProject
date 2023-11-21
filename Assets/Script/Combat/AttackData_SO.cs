using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Data" , menuName = "Attack/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    [Header("膀娄把计")]
    public float attackRange;

    public float skillRange;

    public float coolDown;

    public int minDamge;

    public int maxDamge;

    public float magicDamge;

    public float collisionDamge;

    [Header("脄阑把计")]
    public float criticalMultiplier;

    public float criticalChance;
}
