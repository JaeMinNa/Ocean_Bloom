using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("SO")]
    public PlayerGunSO PlayerGunSO;

    [Header("Bullet")]
    public int CarryBulletCount;    // ���� �����ϰ� �ִ� �Ѿ��� �� ����
    [field: SerializeField] public int ReloadBulletCount { get; private set; }    // ���� ������ ����
    public int CurrentBulletCount;  // ���� źâ�� �����ִ� �Ѿ��� ����
    [field: SerializeField] public int MaxBulletCount { get; private set; }       // �Ѿ� �ִ� ���� ����

    [Header("ETC")]
    public Animator Animator;
    [field: SerializeField] public ParticleSystem MuzzleFlash { get; private set; }    // ȭ���� ����Ʈ
}