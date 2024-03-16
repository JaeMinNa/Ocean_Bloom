using System.Collections;
using UnityEngine;

public class EnemyHitState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private Vector3 _dir;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        _enemyController.Rigidbody.isKinematic = false;
        _dir = new Vector3(_enemyController.Target.transform.position.x - transform.position.x, 0,
                _enemyController.Target.transform.position.z - transform.position.z).normalized;
        transform.LookAt(transform.position + _dir);

        Debug.Log("Hit »óÅÂ ½ÃÀÛ");
        if (_enemyController.IsGrounded())
        {
            _enemyController.NavMeshAgent.isStopped = true;
            _enemyController.NavMeshAgent.velocity = Vector3.zero;
            //_enemyController.Rigidbody.velocity = Vector3.zero;
            _enemyController.Rigidbody.velocity = -_dir * _enemyController.EnemySO.NuckBackPower; // Àû ³Ë¹é
        }
        else
        {
            _enemyController.Rigidbody.velocity = Vector3.zero;
            _enemyController.Rigidbody.velocity = -_dir * _enemyController.EnemySO.NuckBackPower * 2.5f; // Àû ³Ë¹é
        }

        _enemyController.Animator.SetTrigger("Hit");

        StartCoroutine(COChasingStart());
        StartCoroutine(COUpdate());
    }

    IEnumerator COChasingStart()
    {
        //_enemyController.ChasingRange *= 2f;
        _enemyController.GunChasingRange *= 1.5f;

        yield return new WaitForSeconds(1f);
        _enemyController.IsHit = false;
        _enemyController.IdleStart();

        //if (_enemyController.Distance <= _enemyController.GunChasingRange)
        //{
        //    if(_enemyController.Distance <= _enemyController.ChasingRange)
        //    {
        //        _enemyController.ChasingStart();
        //    }

        //    _enemyController.GunChasingStart();
        //}
        //else
        //{
        //    _enemyController.IdleStart();
        //}
    }

    IEnumerator COUpdate()
    {
        while (true)
        {
            if (_enemyController.CurrentHp <= 0)
            {
                _enemyController.DieStart();
                StopAllCoroutines();
                break;
            }

            yield return null;
        }
    }
}
