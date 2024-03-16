using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3 : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] private GameObject[] _tutorials;
    [HideInInspector] public bool[] IsTutorialActive;

    [Header("Tutorial Enemy Ship")]
    [SerializeField] private GameObject _tutorialEnemyShip;

    [Header("Cannons")]
    [SerializeField] private Cannon _cannon0;
    [SerializeField] private Cannon _cannon1;

    [Header("Ladders")]
    [SerializeField] private LadderMakeSystem _ladderMakeSystem0;
    [SerializeField] private LadderMakeSystem _ladderMakeSystem1;

    [Header("TreasureBoxes")]
    [SerializeField] private GameObject _treasureBoxes;

    [SerializeField] private int _cannonCount;
    private bool _isCannon;
    private bool _isLadder;
    private bool _isClear;

    void Start()
    {
        IsTutorialActive = new bool[_tutorials.Length];
        for (int i = 0; i < _tutorials.Length; i++)
        {
            IsTutorialActive[i] = false;
        }
        if (PlayerPrefs.GetInt("Tutorial3") != 1) StartCoroutine(COTutorial());

        _isCannon = false;
        _isLadder = false;
        _isClear = false;
    }

    private void Update()
    { 
        if (PlayerPrefs.GetInt("Tutorial3Clear") != 1)
        {
            _cannonCount = _cannon0.CannonCount + _cannon1.CannonCount;

            if(_cannonCount >= 3 && !_isCannon)
            {
                _isCannon = true;
                StartCoroutine(COTutorial1());
            }
            else if ((_ladderMakeSystem0.IsLadder && !_isLadder) || (_ladderMakeSystem1.IsLadder && !_isLadder))
            {
                _isLadder = true;
                StartCoroutine(COTutorial2());
            }
            else if(_treasureBoxes.transform.childCount == 0 && !_isClear)
            {
                _isClear = true;
                StartCoroutine(COTutorial4());
                PlayerPrefs.SetInt("Tutorial3Clear", 1);
            }
        }
    }


    public void TutorialActive(int num)
    {
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = false;
        Cursor.lockState = CursorLockMode.None;
        _tutorials[num].SetActive(true);
        GameManager.I.SoundManager.StartSFX("TutorialActive");
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = true;
        Time.timeScale = 0f;
        IsTutorialActive[num] = true;
    }

    public void TutorialInActive(int num)
    {
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        _tutorials[num].SetActive(false);
        GameManager.I.SoundManager.StartSFX("TutorialInActive");
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = false;
        Time.timeScale = 1f;
    }

    IEnumerator COTutorial()
    {
        yield return new WaitForSeconds(0.1f);
        TutorialActive(0);
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBall += 3;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBallText.text =
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBall.ToString();
        _tutorialEnemyShip.SetActive(true);
        PlayerPrefs.SetInt("Tutorial3", 1);
    }

    IEnumerator COTutorial1()
    {
        yield return new WaitForSeconds(1f);
        TutorialActive(1);
        GameManager.I.PlayerManager.Player.GetComponent<ResourceCollector>().Resources += 3;
        GameManager.I.PlayerManager.Player.GetComponent<ResourceCollector>().UpdateResourceText();
    }
    IEnumerator COTutorial2()
    {
        yield return new WaitForSeconds(1f);
        TutorialActive(2);
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PartnerSpawnCount += 3;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PartnerSpawnCountText.text =
            (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PartnerSpawnCount).ToString();
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().GunController.CurrentGun.CarryBulletCount += 10;
    }

    IEnumerator COTutorial4()
    {
        yield return new WaitForSeconds(1f);
        TutorialActive(4);
    }

}

