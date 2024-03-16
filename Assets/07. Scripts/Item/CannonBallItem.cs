using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("CannonBall!!");
            GameManager.I.SoundManager.StartSFX("CannonBallItem");
            collision.transform.GetComponent<PlayerController>().CannonBall++;
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBallText.text
                = (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBall).ToString();
            Destroy(gameObject);
        }
    }
}
