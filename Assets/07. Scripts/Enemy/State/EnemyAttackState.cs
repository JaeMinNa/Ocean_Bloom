using System.Collections;
using UnityEngine;

public class EnemyAttackState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private float _time;
    private Vector3 _dir;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Attack 상태 시작");
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
        _enemyController.SwordEquip();
        _enemyController.Animator.SetTrigger("Sword");
        StartCoroutine(COSwordAttack());

        StartCoroutine(COUpdate());
    }

    IEnumerator COUpdate()
    {
        while (true)
        {
            _time += Time.deltaTime;
            if (_time >= _enemyController.EnemySO.SwordCoolTime && !_enemyController.IsSword)
            {
                _enemyController.Animator.SetTrigger("Sword");
                StartCoroutine(COSwordAttack());
                _time = 0;
            }
            if((_enemyController.Distance >= _enemyController.EnemySO.SwordAttackRange && !_enemyController.IsSword)
                || _enemyController.Distance < 0)
            {
                _enemyController.Animator.SetTrigger("Idle");
                _enemyController.IdleStart();
                break;
            }
            if (_enemyController.CurrentHp <= 0)
            {
                StopAllCoroutines();
                _enemyController.DieStart();
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

    IEnumerator COSwordAttack()
    {
        _dir = new Vector3(_enemyController.Target.transform.position.x - transform.position.x, 0,
                _enemyController.Target.transform.position.z - transform.position.z).normalized;
        transform.LookAt(transform.position + _dir);

        _enemyController.IsSword = true;
        yield return new WaitForSeconds(_enemyController.EnemySO.SwordColliderActiveTime);
        _enemyController.SwordCollider.SetActive(true);
        GameManager.I.SoundManager.StartSFX("EnemySword", transform.position);

        yield return new WaitForSeconds(_enemyController.EnemySO.SwordColliderInActiveTime);
        _enemyController.SwordCollider.gameObject.SetActive(false);

        yield return new WaitForSeconds(_enemyController.EnemySO.SwordFinishTime);
        _enemyController.IsSword = false;
    }
}

