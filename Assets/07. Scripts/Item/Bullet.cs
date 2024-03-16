using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Debug.Log("Bullet!!");
            collision.transform.GetComponent<PlayerController>().GunController.CurrentGun.CarryBulletCount += 3;
            GameManager.I.SoundManager.StartSFX("Bullet");
            Destroy(gameObject);
        }
    }
}
