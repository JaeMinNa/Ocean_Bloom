using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    public float trailing = 0;

    [Header("Tutorial")]
    [SerializeField] private GameObject[] _tutorials;
    [HideInInspector] public bool[] IsTutorialActive;

    public void Trailing()
    {

        
    }
    void Start()
    {
        if (trailing == 1)
        {
            IsTutorialActive = new bool[_tutorials.Length];
            for (int i = 0; i < _tutorials.Length; i++)
            {
                IsTutorialActive[i] = false;
            }

            StartCoroutine(COTutorial());
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
    }
}
