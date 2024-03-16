using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("SO")]
    public PlayerGunSO PlayerGunSO;

    [Header("Bullet")]
    public int CarryBulletCount;    // 현재 소유하고 있는 총알의 총 개수
    [field: SerializeField] public int ReloadBulletCount { get; private set; }    // 총의 재장전 개수
    public int CurrentBulletCount;  // 현재 탄창에 남아있는 총알의 개수
    [field: SerializeField] public int MaxBulletCount { get; private set; }       // 총알 최대 소유 개수

    [Header("ETC")]
    public Animator Animator;
    [field: SerializeField] public ParticleSystem MuzzleFlash { get; private set; }    // 화염구 이펙트
}