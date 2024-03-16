using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _noDataPanel;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private AudioClip _buttonYes;
    [SerializeField] private AudioClip _buttonNo;
    [SerializeField] private Tmp _tmp;
    [SerializeField] private MouseController _mouseController;

    public void StartGame()
    {
        PlaySFX(_buttonYes);
        LoadingScene.LoadScene("1");
        PlayerPrefs.SetInt("Tutorial2Start", 0);
        PlayerPrefs.SetInt("Tutorial2Wood", 0);
        PlayerPrefs.SetInt("Tutorial2Plant", 0);
        PlayerPrefs.SetInt("Tutorial2Partner", 0);
        PlayerPrefs.SetInt("Tutorial2Enemys", 0);
        PlayerPrefs.SetInt("Tutorial3", 0);
        PlayerPrefs.SetInt("Tutorial3Clear", 0);
        PlayerPrefs.SetInt("Tutorial2TreasureBox", 0);
    }

    public void Load()
    {
        //SaveLoad.instance.LoadData();
        if (PlayerPrefs.GetInt("IsData") == 1)
        {
            PlaySFX(_buttonYes);
            PlayerPrefs.SetInt("DataLoad", 1);
            LoadingScene.LoadScene(PlayerPrefs.GetString("CurrentScene"));
        }
        else
        {
            PlaySFX(_buttonNo);
            _noDataPanel.SetActive(true);
        }

    }

    public void ExitGame()
    {
        PlaySFX(_buttonYes);

        // 현재 실행 환경이 에디터이면 에디터 플레이모드 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // 현재 실행 환경이 에디터가 아니면 프로그램 종료
        #else
        Application.Quit();
        #endif
    }

    public void Option()
    {
        PlaySFX(_buttonYes);
        _optionPanel.SetActive(true);
    }

    public void ReturnNoData()
    {
        PlaySFX(_buttonYes);
        _noDataPanel.SetActive(false);
    }

    public void ReturnOption()
    {
        PlaySFX(_buttonYes);
        _optionPanel.SetActive(false);
    }

    public void DeleteData()
    {
        PlaySFX(_buttonYes);
        PlayerPrefs.DeleteAll();
        _tmp.BGMSlider.value = PlayerPrefs.GetFloat("BGMValue");
        _tmp.SFXSlider.value = PlayerPrefs.GetFloat("SFXValue");
        _tmp.MasterSlider.value = PlayerPrefs.GetFloat("MasterValue");
        _mouseController.MouseSlider.value = 0.1f;
    }

    private void PlaySFX(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
