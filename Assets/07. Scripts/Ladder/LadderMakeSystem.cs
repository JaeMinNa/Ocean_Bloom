using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMakeSystem : MonoBehaviour
{
    [SerializeField] private GameObject _ladderMakePanel;
    [SerializeField] private GameObject _ladderPrefab;
    [SerializeField] private Transform _ladderPosition;
    [SerializeField] private int _ladderMakePrice = 3;

    public bool IsLadder;

    private void Start()
    {
        IsLadder = false;
    }

    public void MakeLadder()
    {
        if (GameManager.I.PlayerManager.Player.GetComponent<ResourceCollector>().Resources >= _ladderMakePrice)
        {
            GameManager.I.SoundManager.StartSFX("CannonBallItem");
            GameManager.I.PlayerManager.Player.GetComponent<ResourceCollector>().Resources -= _ladderMakePrice;
            GameManager.I.PlayerManager.Player.GetComponent<ResourceCollector>().UpdateResourceText();
            Instantiate(_ladderPrefab, _ladderPosition.position, Quaternion.Euler(new Vector3(_ladderPosition.eulerAngles.x, _ladderPosition.eulerAngles.y + 90, _ladderPosition.eulerAngles.z)));
            LadderMakePanelInActive();
            IsLadder = true;
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonNo");
        }
    }

    public void LadderMakePanelActive()
    {
        GameManager.I.SoundManager.StartSFX("TutorialActive");
        _ladderMakePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = true;
    }

    public void LadderMakePanelInActive()
    {
        _ladderMakePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player")) LadderMakePanelActive();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player")) LadderMakePanelInActive();
    }
}
