using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMakeZone : MonoBehaviour
{
    [SerializeField] private int _shipMakePrice = 10;
    [SerializeField] private GameObject _shipMake;

    public void ShipMakeActive()
    {
        GameManager.I.SoundManager.StartSFX("TutorialActive");
        _shipMake.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = true;
    }

    public void ShipMakeInActive()
    {
        _shipMake.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = false;
    }

    public void MakeShipButton()
    {
        if(GameManager.I.PlayerManager.Player.GetComponent<ResourceCollector>().Resources >= _shipMakePrice)
        {
            GameManager.I.SoundManager.StartSFX("ShipMake");
            GameManager.I.PlayerManager.Player.GetComponent<ResourceCollector>().Resources -= 10;
            GameManager.I.PlayerManager.Player.GetComponent<ResourceCollector>().UpdateResourceText();
            GameManager.I.ShipManager.Ship.SetActive(true);
            ShipMakeInActive();
            PlayerPrefs.SetInt("IsShip", 1);
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonNo");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("ShipMake Active");
            ShipMakeActive();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("ShipMake Inactive");
            ShipMakeInActive();
        }
    }
}
