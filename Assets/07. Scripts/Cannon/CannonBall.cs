using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{ 
    public ParticleSystem CannonBallParticleSystem;
    private Rigidbody _cannonballRigidbody;
    public int Damage = 50;

    // Use this for initialization
    void Start()
    {
        _cannonballRigidbody = GetComponent<Rigidbody>();
        CannonBallParticleSystem.Stop();
        Destroy(gameObject, 10f);
    }


    
    void Update()
    {
        this.transform.forward =
        Vector3.Slerp(this.transform.forward, _cannonballRigidbody.velocity.normalized, Time.deltaTime);

    }

    private void OnTriggerEnter(Collider collider)
    {     
        
     

        if (collider.CompareTag("Enemy"))
        {
           // ExplosionDamage();
           collider.GetComponent<EnemyController>().CurrentHp-= Damage;
            
           Debug.Log("Enemy 대포 맞음");
           PlayExplosion();
           Destroy(gameObject); 
        }

        //if (collider.CompareTag("EnemyShipArea"))
        //{
        //    PlayExplosion();
        //    Destroy(gameObject);
        //}
    }

    private void PlayExplosion()
    {
        GameManager.I.SoundManager.StartSFX("CannonDestroy", transform.position);
        // ��ƼŬ �ý��� ���
        ParticleSystem explosion = Instantiate(CannonBallParticleSystem, transform.position, Quaternion.identity);
        explosion.Play();

        // ��ƼŬ �ý����� ����� �Ŀ� �ı�
        Destroy(explosion.gameObject, explosion.main.duration);
    }

    private void ExplosionDamage()
    {
        EnemyController enemyController = GetComponent<EnemyController>();
         
            
        Debug.Log("적 체력 -");
        enemyController.CurrentHp -= Damage;           
        
    }
}


