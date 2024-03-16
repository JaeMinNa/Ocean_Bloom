using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyController _enemyController;

    private void OnTriggerEnter(Collider collision)
    {
        if(transform.parent.tag == "Partner")
        {            
            if (collision.gameObject.tag == "Enemy")
            {
                collision.GetComponent<EnemyController>().CurrentHp -= _enemyController.EnemySO.SwordDamage;
                Debug.Log("Enemy Hp : " + collision.GetComponent<EnemyController>().CurrentHp);
            }
        }
        else if(transform.parent.tag == "Enemy")
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.I.SoundManager.StartSFX("PlayerHit");
                GameManager.I.PlayerManager.Player.GetComponent<PlayerConditions>().Health.Subtract(_enemyController.EnemySO.SwordDamage);

            }
            else if(collision.gameObject.tag == "Partner")
            {
                collision.GetComponent<EnemyController>().CurrentHp -= _enemyController.EnemySO.SwordDamage;
                Debug.Log("Partner Hp : " + collision.GetComponent<EnemyController>().CurrentHp);
            }
        }
    }
}
