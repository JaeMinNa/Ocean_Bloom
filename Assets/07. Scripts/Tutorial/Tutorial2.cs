using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2 : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] private GameObject[] _tutorials;
    [HideInInspector] public bool[] IsTutorialActive;

    [Header("Colliders")]
    public GameObject Area6Collider;

    [Header("Plant Book")]
    [SerializeField] private PlantBook _plantBook;

    [Header("Enemys")]
    [SerializeField] private GameObject _enemys;
    private bool _isClear;

    [Header("TreasureBox")]
    [SerializeField] private GameObject _treasureBoxes;

    void Start()
    {
        if(PlayerPrefs.GetInt("DataLoad") == 1)
        {
            gameObject.SetActive(false);
        }

        IsTutorialActive = new bool[_tutorials.Length];
        for (int i = 0; i < _tutorials.Length; i++)
        {
            IsTutorialActive[i] = false;
        }
        if(PlayerPrefs.GetInt("Tutorial2Start") != 1) StartCoroutine(COTutorial());

        _isClear = false;
    }

    void Update()
    {
        if(GameManager.I.PlayerManager.Player.GetComponent<ResourceCollector>().Resources >= 10 && !IsTutorialActive[2]
            && PlayerPrefs.GetInt("Tutorial2Wood") != 1)
        {
            TutorialActive(2);
            PlayerPrefs.SetInt("Tutorial2Wood", 1);
        }
        else if(_plantBook.FindPlants[0] == 1 && !IsTutorialActive[1] && PlayerPrefs.GetInt("Tutorial2Plant") != 1)
        {
            TutorialActive(1);
            PlayerPrefs.SetInt("Tutorial2Plant", 1);
        }
        else if(_treasureBoxes.transform.childCount == 0 && PlayerPrefs.GetInt("Tutorial2TreasureBox") != 1)
        {
            StartCoroutine(COTutorial8());
            PlayerPrefs.SetInt("Tutorial2TreasureBox", 1);
        }

        if (_enemys.transform.childCount == 0) _isClear = true;
        if (_isClear && !IsTutorialActive[7] && PlayerPrefs.GetInt("Tutorial2Enemys") != 1)
        {
            TutorialActive(7);
            PlayerPrefs.SetInt("Tutorial2Enemys", 1);
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
        PlayerPrefs.SetInt("Tutorial2Start", 1);
    }

    IEnumerator COTutorial8()
    {
        yield return new WaitForSeconds(1f);
        TutorialActive(8);
    }
}
