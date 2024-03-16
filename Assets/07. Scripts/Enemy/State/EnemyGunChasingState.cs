using System.Collections;
using UnityEngine;

public class EnemyGunChaisngState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private Vector3 _dir;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("GunChasing 상태 시작");
        if (_enemyController.IsGrounded())
        {
            _enemyController.NavMeshAgent.isStopped = false;
            _enemyController.NavMeshAgent.velocity = Vector3.zero;
            _enemyController.NavMeshAgent.speed = _enemyController.EnemySO.NavRunSpeed;
        }
        else
        {
            _enemyController.Rigidbody.velocity = Vector3.zero;
        }

        _enemyController.GunEquip();
        _enemyController.Animator.SetBool("Run", true);

        StartCoroutine(COUpdate());
    }

    IEnumerator COUpdate()
    {
        while (true)
        {
            _dir = new Vector3(_enemyController.Target.transform.position.x - transform.position.x, 0,
                _enemyController.Target.transform.position.z - transform.position.z).normalized;

            if (_enemyController.IsGrounded())
            {
                _enemyController.NavMeshAgent.SetDestination(_enemyController.Target.transform.position);
            }
            else
            {
                _enemyController.Rigidbody.velocity = _dir * _enemyController.EnemySO.RunSpeed * Time.deltaTime;
            }
            transform.LookAt(transform.position + _dir);

            if (_enemyController.Distance <= _enemyController.OriginGunChasingRange)
            {
                _enemyController.GunChasingRange = _enemyController.OriginGunChasingRange;
                _enemyController.ChasingRange = _enemyController.OriginChasingRange;

                if (_enemyController.Distance <= _enemyController.GunAttackRange && _enemyController.Distance > 0)
                {
                    _enemyController.Animator.SetBool("Run", false);
                    _enemyController.IdleStart();
                    break;
                }
            }
            if (_enemyController.Distance >= _enemyController.GunChasingRange * 1.2f || _enemyController.Distance < 0f)
            {
                _enemyController.Animator.SetBool("Run", false);
                _enemyController.IdleStart();
                break;
            }
            if (_enemyController.IsHit)
            {
                _enemyController.Animator.SetBool("Run", false);
                _enemyController.HitStart();
                //_enemyController.IsHit = false;
                break;
            }
            if (_enemyController.CurrentHp <= 0)
            {
                _enemyController.Animator.SetBool("Run", false);
                _enemyController.DieStart();
                StopAllCoroutines();
                break;
            }

            yield return null;
        }
    }

}
