using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Player")]
    [field: SerializeField] public GameObject Player;
    [Header("Gun")]
    public Gun CurrentGun;

    public bool IsFindSightMode { get; private set; }
    [HideInInspector] public bool IsReload = false;
    private RaycastHit _hitInfo;
    private float _currentFireRate;
    private Vector3 _originCameaPos;

    void Update()
    {
        GunFireRateCalc();
    }

    public void Test()
    {
        Debug.Log("abc");
    }

    private void GunFireRateCalc()
    {
        if (_currentFireRate > 0)
            _currentFireRate -= Time.deltaTime;
    }

    public void TryFire()
    {
        if(_currentFireRate <= 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (!IsReload)
        {
            if (CurrentGun.CurrentBulletCount > 0)
                Shoot();
            else
            {
                CancelFineSight();
                StartCoroutine(COReload());
            }
        }
    }

    private void Shoot()
    {
        GameManager.I.SoundManager.StartSFX("PlayerGun");

        CurrentGun.CurrentBulletCount--;
        _currentFireRate = CurrentGun.PlayerGunSO.FireRate;
        CurrentGun.MuzzleFlash.Play();
        CurrentGun.Animator.SetTrigger("Fire");

        // �ǰ� ó��
        Hit();

        // �ѱ� �ݵ� �ڷ�ƾ ����
        StopAllCoroutines();
        StartCoroutine(CORetroAction());

        Debug.Log("�Ѿ� �߻�");
    }

    public void TryReload()
    {
        if (!IsReload && CurrentGun.CurrentBulletCount < CurrentGun.ReloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(COReload());
        }
    }

    private void Hit()
    {
        Vector3 randomRange = new Vector3(Random.Range(-CurrentGun.PlayerGunSO.Accuracy, CurrentGun.PlayerGunSO.Accuracy), Random.Range(-CurrentGun.PlayerGunSO.Accuracy, CurrentGun.PlayerGunSO.Accuracy), 0);
        Vector3 randomRangeFineSight = new Vector3(Random.Range(-CurrentGun.PlayerGunSO.AccuracyFineSight, CurrentGun.PlayerGunSO.AccuracyFineSight), Random.Range(-CurrentGun.PlayerGunSO.AccuracyFineSight, CurrentGun.PlayerGunSO.AccuracyFineSight), 0);

        if (!IsFindSightMode)
        {
            Debug.DrawRay(Camera.main.transform.position, (Camera.main.transform.forward + randomRange) * CurrentGun.PlayerGunSO.Range, Color.blue, 0.3f);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + randomRange, out _hitInfo, CurrentGun.PlayerGunSO.Range))
            {
                GameManager.I.ObjectPoolManager.GunEffect("GunEffect", _hitInfo.point, Quaternion.LookRotation(_hitInfo.normal));

                Debug.Log(_hitInfo.transform.name + " 명중!!");
                Attack();

            }
        }
        else
        {
            Debug.DrawRay(Camera.main.transform.position, (Camera.main.transform.forward + randomRangeFineSight) * CurrentGun.PlayerGunSO.Range, Color.blue, 0.3f);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + randomRangeFineSight, out _hitInfo, CurrentGun.PlayerGunSO.Range))
            {
                GameManager.I.ObjectPoolManager.GunEffect("GunEffect", _hitInfo.point, Quaternion.LookRotation(_hitInfo.normal));
                
                Debug.Log(_hitInfo.transform.name + " 명중!!");
                Attack();
            }
        }

    }

    private void Attack()
    {
        if(_hitInfo.transform.tag == "Enemy")
        {
            if (_hitInfo.transform.GetComponent<EnemyController>().CurrentHp > 0)
            {
                _hitInfo.transform.GetComponent<EnemyController>().CurrentHp -= Player.GetComponent<PlayerController>().GunController.CurrentGun.PlayerGunSO.Damage;
                _hitInfo.transform.GetComponent<EnemyController>().IsHit = true;
                Debug.Log("총 명중, 적 남은 Hp : " + _hitInfo.transform.gameObject.GetComponent<EnemyController>().CurrentHp);
            }
        }
    }

    public void TryFineSight()
    {
        if (!IsReload)
        {
            FineSight();
        }
    }

    private void FineSight()
    {
        GameManager.I.SoundManager.StartSFX("FineSight");
        IsFindSightMode = !IsFindSightMode;
        CurrentGun.Animator.SetBool("FineSightMode", IsFindSightMode);

        if (IsFindSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(COFineSightActivate());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(COFineSightDeActivate());
        }
    }

    public void CancelFineSight()
    {
        if (IsFindSightMode)
        {
            FineSight();
            CurrentGun.transform.localPosition = CurrentGun.PlayerGunSO.OriginPos;
        }
    }

    IEnumerator COFineSightActivate()
    {
        _originCameaPos = Camera.main.transform.localPosition;

        while (CurrentGun.transform.localPosition != CurrentGun.PlayerGunSO.FineSightOriginPos)
        {
            CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, CurrentGun.PlayerGunSO.FineSightOriginPos, 0.2f);
            Camera.main.transform.localPosition = new Vector3(0, 0, 1.5f);

            yield return null;
        }
    }

    IEnumerator COFineSightDeActivate()
    {
        while (CurrentGun.transform.localPosition != CurrentGun.PlayerGunSO.OriginPos)
        {
            CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, CurrentGun.PlayerGunSO.OriginPos, 0.2f);
            Camera.main.transform.localPosition = _originCameaPos;
            yield return null;
        }
    }

    IEnumerator COReload()
    {
        if (CurrentGun.CarryBulletCount > 0)
        {
            GameManager.I.SoundManager.StartSFX("Reload");
            IsReload = true;
            CurrentGun.Animator.SetTrigger("Reload");

            CurrentGun.CarryBulletCount += CurrentGun.CurrentBulletCount;
            CurrentGun.CurrentBulletCount = 0;

            yield return new WaitForSeconds(CurrentGun.PlayerGunSO.ReloadTime);

            if (CurrentGun.CarryBulletCount >= CurrentGun.ReloadBulletCount)
            {
                CurrentGun.CurrentBulletCount = CurrentGun.ReloadBulletCount;
                CurrentGun.CarryBulletCount -= CurrentGun.ReloadBulletCount;
            }
            else
            {
                CurrentGun.CurrentBulletCount = CurrentGun.CarryBulletCount;
                CurrentGun.CarryBulletCount = 0;
            }

            IsReload = false;
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("BulletZero");
            Debug.Log("No Bullet!!");
        }
    }

    IEnumerator CORetroAction()
    {
        Vector3 recoilBack = new Vector3(CurrentGun.PlayerGunSO.OriginPos.x, CurrentGun.PlayerGunSO.OriginPos.y, CurrentGun.PlayerGunSO.OriginPos.z - CurrentGun.PlayerGunSO.RetroActionForce); // ������ x
        Vector3 retroActionRecoilBack = new Vector3(CurrentGun.PlayerGunSO.FineSightOriginPos.x, CurrentGun.PlayerGunSO.FineSightOriginPos.y, CurrentGun.PlayerGunSO.FineSightOriginPos.z - CurrentGun.PlayerGunSO.RetroActionFineSightForce);  // ������

        if (!IsFindSightMode)  // �������� �ƴ� ����
        {
            CurrentGun.transform.localPosition = CurrentGun.PlayerGunSO.OriginPos;

            // �ݵ� ����
            while (CurrentGun.transform.localPosition.z >= CurrentGun.PlayerGunSO.OriginPos.z - CurrentGun.PlayerGunSO.RetroActionForce + 0.02f)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, recoilBack, 0.4f);
                Player.GetComponent<PlayerController>().CamCurXRot += CurrentGun.PlayerGunSO.RetroActionForce;
                yield return null;
            }

            // ����ġ
            while (CurrentGun.transform.localPosition != CurrentGun.PlayerGunSO.OriginPos)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, CurrentGun.PlayerGunSO.OriginPos, 0.1f);
                yield return null;
            }
        }
        else  // ������ ����
        {
            CurrentGun.transform.localPosition = CurrentGun.PlayerGunSO.FineSightOriginPos;

            // �ݵ� ����
            while (CurrentGun.transform.localPosition.z >= CurrentGun.PlayerGunSO.FineSightOriginPos.z - CurrentGun.PlayerGunSO.RetroActionFineSightForce + 0.02f)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                Player.GetComponent<PlayerController>().CamCurXRot += CurrentGun.PlayerGunSO.RetroActionFineSightForce;
                yield return null;
            }

            // ����ġ
            while (CurrentGun.transform.localPosition != CurrentGun.PlayerGunSO.FineSightOriginPos)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, CurrentGun.PlayerGunSO.FineSightOriginPos, 0.1f);
                yield return null;
            }
        }
    }
}