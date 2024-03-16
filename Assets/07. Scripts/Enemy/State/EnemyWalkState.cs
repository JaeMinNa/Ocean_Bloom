using System.Collections;
using UnityEngine;

public class EnemyWalkState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private Vector3 _destination;
    private Vector3 _dir;
    private float _time;
    private float _walkTime;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Walk 상태 시작");
        if (_enemyController.IsGrounded())
        {
            _enemyController.NavMeshAgent.isStopped = false;
            _enemyController.NavMeshAgent.velocity = Vector3.zero;
            _enemyController.NavMeshAgent.speed = _enemyController.EnemySO.NavWalkSpeed;
        }
        else
        {
            //_enemyController.Rigidbody.velocity = Vector3.zero;
        }

        _enemyController.Animator.SetBool("Walk", true);
        _time = 0f;
        _walkTime = 5f;
        float xRandomValue = Random.Range(-_enemyController.EnemySO.MoveRange, _enemyController.EnemySO.MoveRange);
        float zRandomValue = Random.Range(-_enemyController.EnemySO.MoveRange, _enemyController.EnemySO.MoveRange);
        _destination = new Vector3(_enemyController.StartPosition.x + xRandomValue, 
            gameObject.transform.position.y, _enemyController.StartPosition.z + zRandomValue);
        _dir = (_destination - _enemyController.gameObject.transform.position).normalized;

        StartCoroutine(COUpdate());
    }

    IEnumerator COUpdate()
    {
        while (true)
        {
            if (!_enemyController.IsFix)
            {
                _time += Time.deltaTime;

                if (_enemyController.IsGrounded())
                {
                    _enemyController.NavMeshAgent.SetDestination(_destination);
                }
                else
                {
                    //_enemyController.Rigidbody.velocity = _dir * _enemyController.EnemySO.WalkSpeed * Time.deltaTime;
                    _enemyController.Rigidbody.AddForce(_dir * _enemyController.EnemySO.WalkSpeed * Time.deltaTime, ForceMode.VelocityChange);
                }
                gameObject.transform.LookAt(transform.position + _dir);

                if (_enemyController.IsWalk() == false || _time > _walkTime)
                {
                    Debug.Log("장애물 감지!");
                    _enemyController.IdleStart();
                    break;
                }
                if (Mathf.Abs(_enemyController.gameObject.transform.position.x - _destination.x) <= 0.5f
                    && Mathf.Abs(_enemyController.gameObject.transform.position.z - _destination.z) <= 0.5f)
                {
                    _enemyController.IdleStart();
                    break;
                }
                if (_enemyController.Distance > 0)
                {
                    if (_enemyController.Distance <= _enemyController.GunChasingRange)
                    {
                        if (_enemyController.Distance <= _enemyController.ChasingRange)
                        {
                            _enemyController.ChasingStart();
                            break;
                        }
                        _enemyController.GunChasingStart();
                        break;
                    }
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
                _enemyController.IdleStart();
                break;
            }
            
            yield return null;
        }
    }
}

