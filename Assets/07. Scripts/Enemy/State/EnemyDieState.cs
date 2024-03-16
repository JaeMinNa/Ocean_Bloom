using System.Collections;
using UnityEngine;

public class EnemyDieState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Die 상태 시작");
        if (_enemyController.IsGrounded())
        {
            _enemyController.NavMeshAgent.isStopped = true;
            _enemyController.NavMeshAgent.velocity = Vector3.zero;
        }
        else
        {
            _enemyController.Rigidbody.velocity = Vector3.zero;
        }

        _enemyController.Animator.SetTrigger("Die");
        _enemyController.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _enemyController.Collider.enabled = false;
        _enemyController.DropItem();
        StartCoroutine(CODie());
    }

    IEnumerator CODie()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
