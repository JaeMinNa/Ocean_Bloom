using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            if(collision.GetComponent<EnemyController>().CurrentHp > 0 /*&& !collision.GetComponent<EnemyController>().IsHit*/ 
               /* && !collision.GetComponent<EnemyController>().IsInvincibilityTime*/)
            {
                collision.GetComponent<EnemyController>().CurrentHp -= GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PlayerSO.SwordDamage;
                //collision.GetComponent<EnemyController>().IsHit = true;
                Debug.Log("검 공격! 현재 적 Hp : " + collision.GetComponent<EnemyController>().CurrentHp);
            }
        }
    }
}
