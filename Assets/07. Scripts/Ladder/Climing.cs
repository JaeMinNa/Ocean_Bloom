using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climing : MonoBehaviour
{
    public float climbSpeed = 3f;

    private GameObject _player;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Stay");
            _player.GetComponent<PlayerController>().Rigidbody.useGravity = false;

            if (Input.GetKey(KeyCode.W))
            {
                _player.transform.position += new Vector3(0, 1f, 0) * climbSpeed * Time.deltaTime;
            }

            // �Ʒ��� �̵�
            if (Input.GetKey(KeyCode.S))
            {
                _player.transform.position += new Vector3(0, -1f, 0) * climbSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enter");
            _player.GetComponent<PlayerController>().Rigidbody.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Exit");
            //isClimbing = false;
            _player.GetComponent<PlayerController>().Rigidbody.useGravity = true;
        }
    }
}
