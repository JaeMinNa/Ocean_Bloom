using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string CurrentSceneName;

    public void Init()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;

        SceneSoundStart();

        if (PlayerPrefs.GetInt("DataLoad") == 1)
        {
            if (CurrentSceneName == "1")
            {
                StartCoroutine(CODataLoad());
                GameObject.Find("CameraChanger").SetActive(false);
                GameObject.Find("Trailer Camera").SetActive(false);
                GameObject.Find("press a to start").SetActive(false);
                GameManager.I.PlayerManager.Player.SetActive(true);
            }
            else if (CurrentSceneName == "2")
            {
                if(PlayerPrefs.GetInt("IsShip") == 1)
                {
                    GameManager.I.ShipManager.Ship.SetActive(true);
                    StartCoroutine(CODataLoad());
                    //GameObject.Find("Tutorial").SetActive(false);
                }
                else
                {
                    StartCoroutine(CODataLoad());
                    //GameObject.Find("Tutorial").SetActive(false);
                    PlayerPrefs.SetInt("IsShip", 0);
                }
            }
            else
            {
                StartCoroutine(CODataLoad());
            }

            PlayerPrefs.SetInt("DataLoad", 0);
        }
        else StartCoroutine(COPlayerDataLoad());

        if(PlayerPrefs.GetInt("Tutorial3") == 1)
        {
            if (CurrentSceneName == "3")
            {
                GameObject.Find("Tutorial").SetActive(false);
            }
        }
    }

    public void Release()
    {

    }

    IEnumerator COPlayerDataLoad()
    {
        yield return new WaitForSeconds(0.1f);

        if (CurrentSceneName != "1")
        {
            Debug.Log("PlayerDataLoad 실행");
            GameManager.I.DataManager.PlayerDataLoad();
        }
    }

    IEnumerator CODataLoad()
    {
        yield return new WaitForSeconds(0.1f);

        Debug.Log("DataLoad 실행");
        GameManager.I.DataManager.DataLoad();
    }

    public void ChangeScene(string scene)
    {
        LoadingScene.LoadScene(scene);
    }

    private void SceneSoundStart()
    {
        if (CurrentSceneName == "1")
        {
        }
        else if (CurrentSceneName == "2")
        {
            GameManager.I.SoundManager.StartSFX("UninhabitedIsland");
            GameManager.I.SoundManager.StartBGM("BGM2");
        }
        else if (CurrentSceneName == "3")
        {
            GameManager.I.SoundManager.StartBGM("BGM3");
        }
        else if (CurrentSceneName == "4")
        {
            GameManager.I.SoundManager.StartBGM("BGM4");
        }
        else if (CurrentSceneName == "5")
        {
            GameManager.I.SoundManager.StartBGM("BGM5");
        }
        else if (CurrentSceneName == "6")
        {
            GameManager.I.SoundManager.StartBGM("BGM6");
        }
        else if (CurrentSceneName == "7")
        {
            GameManager.I.SoundManager.StartBGM("BGM7");
        }
    }

    public void ChangeStartScene()
    {
        GameManager.I.SoundManager.StartSFX("ButtonYes");
        SceneManager.LoadScene("StartScene");
        //LoadingScene.LoadScene("StartScene");
    }

    public void GameExit()
    {
        GameManager.I.SoundManager.StartSFX("ButtonYes");

        // 현재 실행 환경이 에디터이면 에디터 플레이모드 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // 현재 실행 환경이 에디터가 아니면 프로그램 종료
        #else
        Application.Quit();
        #endif
    }
}
