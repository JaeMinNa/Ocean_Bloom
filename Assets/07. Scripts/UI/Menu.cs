using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject MenuObject;
    public GameObject OptionMenu;
    public GameObject RewardMenu;
    public GameObject ManualPanel;
    public GameObject GameClearPanel;
    public bool stop;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!stop)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }

    }
    public void OpenMenu()
    {
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = true;
        GameManager.I.SoundManager.StartSFX("TutorialActive");
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = false;
        stop = true;
        MenuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = false;
        GameManager.I.SoundManager.StartSFX("TutorialInActive");
        stop = false;
        MenuPanel.SetActive(false);
        ManualPanel.SetActive(false);
        OptionMenu.SetActive(false);
        MenuObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = true;
    }

    public void Save()
    {
        GameManager.I.SoundManager.StartSFX("ButtonYes");
        GameManager.I.DataManager.DataSave();
    }

    public void Option()
    {
        GameManager.I.SoundManager.StartSFX("ButtonYes");
        OptionMenu.SetActive(true);
        MenuObject.SetActive(false);
    }

    public void Return()
    {
        GameManager.I.SoundManager.StartSFX("ButtonYes");
        stop = false;
        MenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = true;
    }
    public void GameExit()
    {
        GameManager.I.SoundManager.StartSFX("ButtonYes");
        // ���� ���� ȯ���� �������̸� ������ �÷��̸�� ����
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // ���� ���� ȯ���� �����Ͱ� �ƴϸ� ���α׷� ����
        #else
        Application.Quit();
        #endif      
    }

    public void ExitOption()
    {
        GameManager.I.SoundManager.StartSFX("ButtonYes");
        OptionMenu.SetActive(false);
        MenuObject.SetActive(true);
    }
    public void Manual()
    {
        GameManager.I.SoundManager.StartSFX("TutorialActive");
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = false;
        stop = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        ManualPanel.SetActive(true);
    }
    
    public void ExitMaual()
    {
        GameManager.I.SoundManager.StartSFX("TutorialInActive");
        ManualPanel.SetActive(false);
    }

    public void ExitGameClear()
    {
        GameManager.I.SoundManager.StartSFX("TutorialInActive");
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = true;
        stop = false;
        GameClearPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameManager.I.SoundManager.StopBGM();
    }

    public void ChangeStartScene()
    {
        GameManager.I.SoundManager.StartSFX("ButtonYes");
        SceneManager.LoadScene("StartScene");
    }
}