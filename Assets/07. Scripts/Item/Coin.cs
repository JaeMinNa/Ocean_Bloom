using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coins = 100;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Coin!!");
            collision.transform.GetComponent<PlayerController>().CoinChange(_coins);
            GameManager.I.SoundManager.StartSFX("Coin");
            Destroy(gameObject);
        }
    }
}
