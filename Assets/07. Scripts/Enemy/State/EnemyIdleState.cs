using System.Collections;
using UnityEngine;

public class EnemyIdleState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private float _idleTime;
    private float _time;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Idle 상태 시작");
        if (_enemyController.IsGrounded())
        {
            _enemyController.NavMeshAgent.isStopped = true;
            _enemyController.NavMeshAgent.velocity = Vector3.zero;
            _enemyController.Rigidbody.isKinematic = true;
        }
        else
        {
            _enemyController.Rigidbody.isKinematic = false;
            _enemyController.Rigidbody.velocity = Vector3.zero;
        }

        _enemyController.Animator.SetBool("Walk", false);
        _idleTime = Random.Range(_enemyController.EnemySO.MinIdleTime, _enemyController.EnemySO.MaxIdleTime);
        _time = 0;

        StartCoroutine(COUpdate());
    }

    IEnumerator COUpdate()
    {
        while (true)
        {
            if (!_enemyController.IsFix)
            {
                _time += Time.deltaTime;

                if (_enemyController.Distance >= 0)
                {
                    if (_enemyController.Distance <= _enemyController.GunChasingRange)
                    {
                        if (_enemyController.Distance <= _enemyController.GunAttackRange)
                        {
                            if (_enemyController.Distance <= _enemyController.ChasingRange)
                            {
                                if (_enemyController.Distance <= _enemyController.EnemySO.SwordAttackRange)
                                {
                                    _enemyController.AttackStart();
                                    break;
                                }
                                _enemyController.ChasingStart();
                                break;
                            }
                            _enemyController.GunAttackStart();
                            break;
                        }
                        _enemyController.GunChasingStart();
                        break;
                    }
                }
                if (_time >= _idleTime)
                {
                    _enemyController.WalkStart();
                    break;
                }
                if (_enemyController.IsHit)
                {
                    _enemyController.HitStart();
                    //_enemyController.IsHit = false;
                    break;
                }
                if (_enemyController.CurrentHp <= 0)
                {
                    _enemyController.DieStart();
                    StopAllCoroutines();
                    break;
                }
            }
            else
            {
                _enemyController.transform.localPosition = _enemyController.ShipLocalPosition;

                if (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanShipControll == false)
                {
                    if (transform.CompareTag("Partner"))
                    {
                        _enemyController.transform.parent = null;
                        _enemyController.IsFix = false;
                        _enemyController.IdleStart();
                        break;
                    }
                    else if(transform.CompareTag("Enemy"))
                    {
                        if(!_enemyController.transform.parent.parent.GetComponent<Follow_Player>().IsMove)
                        {
                            _enemyController.IsFix = false;
                            _enemyController.IdleStart();
                            break;
                        }
                    }
                }
            }

            yield return null;
        }
    }


}
