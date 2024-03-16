using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO", order = 0)]
public class PlayerSO : ScriptableObject
{
    [Header("PlayerStats")]
    public float MaxHp = 100f;
    public float SwordDamage = 30f;

    [Header("Movement")]
    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;
    public float JumpForce = 80f;
    public LayerMask GroundLayerMask;
    public LayerMask ShipLayerMask;
    public LayerMask EnemyShipLayerMask;
    public LayerMask WaterLayerMask;

    [Header("Look")]
    public float MinXLook = -85f;
    public float MaxXLook = 85f;

    [Header("WeaponPosition")]
    public Vector3 GunStartPosition;
    public Vector3 SwordStartPosition;
    public Vector3 AxeStartPosition;
}