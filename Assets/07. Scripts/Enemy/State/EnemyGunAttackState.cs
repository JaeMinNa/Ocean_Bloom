using System.Collections;
using UnityEngine;

public class EnemyGunAttackState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private float _time;
    private Vector3 _dir;
    private RaycastHit _hitInfo;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("GunAttack 상태 시작");
        if (_enemyController.IsGrounded())
        {
            _enemyController.NavMeshAgent.isStopped = true;
            _enemyController.NavMeshAgent.velocity = Vector3.zero;
        }
        else
        {
            _enemyController.Rigidbody.velocity = Vector3.zero;
        }
        _time = 0;
        _enemyController.GunEquip();
        _enemyController.Animator.SetTrigger("Gun");
        _enemyController.GunChasingRange = _enemyController.OriginGunChasingRange;
        StartCoroutine(COGunAttack());

        StartCoroutine(COUpdate());
    }

    IEnumerator COUpdate()
    {
        while (true)
        {
            _time += Time.deltaTime;
            _dir = new Vector3(_enemyController.Target.transform.position.x - transform.position.x, 0,
                _enemyController.Target.transform.position.z - transform.position.z).normalized;
            transform.LookAt(transform.position + _dir);

            if (_time >= _enemyController.EnemySO.GunCoolTime && _enemyController.Distance > _enemyController.EnemySO.SwordAttackRange
                && _enemyController.Distance <= _enemyController.GunAttackRange && !_enemyController.IsGun)
            {
                _enemyController.Animator.SetTrigger("Gun");
                StartCoroutine(COGunAttack());
                _time = 0;
            }
            if (_enemyController.Distance < _enemyController.GunChasingRange
                && _enemyController.Distance > _enemyController.GunAttackRange && !_enemyController.IsGun)
            {
                _enemyController.Animator.SetBool("Run", true);
                _enemyController.GunChasingStart();
                break;
            }
            if ((_enemyController.Distance >= _enemyController.GunChasingRange && !_enemyController.IsGun)
                || _enemyController.Distance < 0f)
            {
                _enemyController.Animator.SetTrigger("Idle");
                _enemyController.IdleStart();
                break;
            }
            if(_enemyController.Distance < _enemyController.ChasingRange && _enemyController.Distance > 0 && !_enemyController.IsGun)
            {
                _enemyController.Animator.SetBool("Run", true); ;
                _enemyController.ChasingStart();
                break;
            }
            //if (_enemyController.Distance <= _enemyController.ChasingRange && !_enemyController.IsGun)
            //{
            //    _enemyController.ChasingStart();
            //    break;
            //}
            if (_enemyController.CurrentHp <= 0)
            {
                _enemyController.DieStart();
                StopAllCoroutines();
                break;
            }
            if (_enemyController.IsHit)
            {
                _enemyController.HitStart();
                //_enemyController.IsHit = false;
                break;
            }

            yield return null;
        }
    }

    IEnumerator COGunAttack()
    {
        _enemyController.IsGun = true;

        yield return new WaitForSeconds(_enemyController.EnemySO.GunFireTime);
        Debug.Log("적 총알 발사!");
        Fire();

        yield return new WaitForSeconds(_enemyController.EnemySO.GunFinishTime);
        _enemyController.IsGun = false;
    }

    private void Fire()
    {
        Vector3 randomRange = new Vector3(Random.Range(-_enemyController.EnemySO.GunAccuracy, _enemyController.EnemySO.GunAccuracy), Random.Range(-_enemyController.EnemySO.GunAccuracy, _enemyController.EnemySO.GunAccuracy), 0);

        GameManager.I.SoundManager.StartSFX("EnemyGun", transform.position);
        Debug.DrawRay(transform.position + new Vector3(0, 2, 0), (_dir + randomRange) * 100f, Color.red, 1f);
        if (Physics.Raycast(transform.position + new Vector3(0, 2, 0), _dir + randomRange, out _hitInfo, 100f))
        {
            _enemyController.GunFireParticle.Play();
            GameManager.I.ObjectPoolManager.GunEffect("GunEffect", _hitInfo.point, Quaternion.LookRotation(_hitInfo.normal));
            Debug.Log(_hitInfo.transform.name + " 명중!!");
            GunDamage();
        }
    }

    private void GunDamage()
    {
        if(transform.tag == "Partner")
        {
            if(_hitInfo.transform.tag == "Enemy")
            {
                _hitInfo.transform.GetComponent<EnemyController>().CurrentHp -= _enemyController.EnemySO.GunDamage;
                _hitInfo.transform.GetComponent<EnemyController>().IsHit = true;
            }
        }
        else if(transform.tag == "Enemy")
        {
            if(_hitInfo.transform.tag == "Player")
            {
                GameManager.I.SoundManager.StartSFX("PlayerHit");
                GameManager.I.PlayerManager.Player.GetComponent<PlayerConditions>().Health.Subtract(_enemyController.EnemySO.GunDamage);
            }
            else if(_hitInfo.transform.tag == "Partner")
            {
                _hitInfo.transform.GetComponent<EnemyController>().CurrentHp -= _enemyController.EnemySO.GunDamage;
                _hitInfo.transform.GetComponent<EnemyController>().IsHit = true;
            }

        }
    }
}

