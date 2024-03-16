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
    public string GunName;  // ���� �̸�
    public float Range;   // ���� ���� �Ÿ�
    public float FireRate;  // ���� �ӵ� (�ѹ߰� �ѹ߰��� �ð� ��)
    public float ReloadTime; // ������ �ӵ�
    public int Damage;      // ���� ���ݷ�
    public float RetroActionForce;  // �ݵ� ����
    public float RetroActionFineSightForce;    // ������ �ݵ� ����
    public float Accuracy; // ��Ȯ��
    public float AccuracyFineSight; // ������ ��Ȯ��
}
