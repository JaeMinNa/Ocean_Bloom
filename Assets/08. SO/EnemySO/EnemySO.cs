using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO", order = 1)]
public class EnemySO : ScriptableObject
{
    [Header("Stats")]
    public float MaxHp = 100f;
    public float SwordDamage = 10f;
    public float SwordCoolTime = 3f;
    public float SwordAttackRange = 2f;
    public float GunDamage = 5f;
    public float GunCoolTime = 3f;
    public float GunAccuracy = 0.05f;

    [Header("Idle")]
    public float MinIdleTime = 2f;
    public float MaxIdleTime = 5f;

    [Header("Walk")]
    public float WalkSpeed = 100f;
    public float NavWalkSpeed = 5f;
    public float MoveRange = 5f;
    public LayerMask ObstacleLayerMask;
    public LayerMask GroundLayerMask;
    public LayerMask ShipLayerMask;
    public LayerMask EnemyShipLayerMask;

    [Header("Chasing")]
    public float RunSpeed = 150f;
    public float NavRunSpeed = 8f;

    [Header("NuckBack")]
    public float NuckBackPower = 10f;

    [Header("Targets")]
    public float OverlapSphereRange = 50f;
    public float TargettingTime = 1f;

    [Header("Items")]
    public GameObject[] DropItems;
    public int ItemMaxCount = 3;

    [Header("AnimationTime")]
    public float SwordColliderActiveTime = 0.4f;
    public float SwordColliderInActiveTime = 0.2f;
    public float SwordFinishTime = 0.4f;
    public float GunFireTime = 1f;
    public float GunFinishTime = 1.1f;
}
