using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGunSO", menuName = "SO/PlayerGunSO", order = 2)]
public class PlayerGunSO : ScriptableObject
{
    [Header("GunPosition")]
    public Vector3 OriginPos;
    public Vector3 FineSightOriginPos;

    [Header("Stats")]
    public string GunName;  // 총의 이름
    public float Range;   // 총의 사정 거리
    public float FireRate;  // 연사 속도 (한발과 한발간의 시간 텀)
    public float ReloadTime; // 재장전 속도
    public int Damage;      // 총의 공격력
    public float RetroActionForce;  // 반동 세기
    public float RetroActionFineSightForce;    // 정조준 반동 세기
    public float Accuracy; // 정확도
    public float AccuracyFineSight; // 정조준 정확도
}
