using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField] private int PartnerPrice = 1000;
    [SerializeField] private int CanonBallPrice = 500;
    [SerializeField] private int BulletPrice = 200;
    [SerializeField] private GameObject _partnerTrade;
    [SerializeField] private TMP_Text _currentCoin;

    public void PartnerTradeActive()
    {
        GameManager.I.SoundManager.StartSFX("TutorialActive");
        _partnerTrade.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = false;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = true;
        Time.timeScale = 0f;
        _currentCoin.text = (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CurrentCoin).ToString();
    }

    public void BuyPartner()
    {
        if(GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CurrentCoin >= PartnerPrice)
        {
            GameManager.I.SoundManager.StartSFX("ButtonYes");
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PartnerSpawnCount++;
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PartnerSpawnCountText.text
                = (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PartnerSpawnCount).ToString();
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CoinChange(-PartnerPrice);
            _currentCoin.text = (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CurrentCoin).ToString();
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonNo");
        }
    }

    public void BuyCannonBall()
    {
        if (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CurrentCoin >= CanonBallPrice)
        {
            GameManager.I.SoundManager.StartSFX("ButtonYes");
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBall++;
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBallText.text
                = (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBall).ToString();
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CoinChange(-CanonBallPrice);
            _currentCoin.text = (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CurrentCoin).ToString();
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonNo");
        }
    }

    public void BuyBullet()
    {
        if (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CurrentCoin >= BulletPrice)
        {
            GameManager.I.SoundManager.StartSFX("ButtonYes");
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().GunController.CurrentGun.CarryBulletCount += 10;
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CoinChange(-BulletPrice);
            _currentCoin.text = (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CurrentCoin).ToString();
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonNo");
        }
    }

    public void PartnerTradeInActive()
    {
        _partnerTrade.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = true;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = false;
        Time.timeScale = 1f;
        GameManager.I.SoundManager.StartSFX("TutorialInActive");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PartnerTradeActive();
        }
    }
}
