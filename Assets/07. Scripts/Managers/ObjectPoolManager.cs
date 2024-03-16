using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public ObjectPool ObjectPool { get; private set; }
    private GameObject _bulletEffectObj;

    public void Init()
    {
        ObjectPool = GetComponent<ObjectPool>();
    }

    public void Release()
    {

    }

    public void GunEffect(string poolName, Vector3 startPosition, Quaternion rotation)
    {
        _bulletEffectObj = ObjectPool.SpawnFromPool(poolName);

        _bulletEffectObj.transform.position = startPosition;
        _bulletEffectObj.transform.rotation = rotation;
        //RangedAttackController attackController = obj.GetComponent<RangedAttackController>();
        //attackController.InitializeAttack(direction, attackData, this);

        _bulletEffectObj.SetActive(true);
        StartCoroutine(COGunEffectInactive());
    }

    IEnumerator COGunEffectInactive()
    {
        GameObject obj = _bulletEffectObj;

        yield return new WaitForSeconds(0.5f);
        obj.SetActive(false);
    }
}