using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantBook : MonoBehaviour
{
    public GameObject PlantBookPanel;
    public GameObject[] Slots;
    public int[] FindPlants;
    public bool Stop;

    void Start()
    {
        FindPlants = new int[Slots.Length];

        for (int i = 0; i < FindPlants.Length; i++)
        {
            FindPlants[i] = 0;
        }

        PlantBookPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!Stop)
            {
                OpenPlantBook();
            }
            else
            {
                ClosePlantBook();
            }
        }
    }

    public void OpenPlantBook()
    {
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = true;
        GameManager.I.SoundManager.StartSFX("TutorialActive");
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = false;
        Time.timeScale = 0f;
        Stop = true;
        PlantBookPanel.SetActive(true);

        if (FindPlants[0] == 1) Image1ON();
        if (FindPlants[1] == 1) Image2ON();
        if (FindPlants[2] == 1) Image3ON();
        if (FindPlants[3] == 1) Image4ON();
        if (FindPlants[4] == 1) Image5ON();
        if (FindPlants[5] == 1) Image6ON();
    }

    public void ClosePlantBook()
    {
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().IsUI = false;
        GameManager.I.SoundManager.StartSFX("TutorialInActive");
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = true;
        Time.timeScale = 1f;
        Stop = false;
        PlantBookPanel.SetActive(false);
    }

    public void Image1ON()
    {
        Color color0 = Slots[0].GetComponent<Image>().color;
        color0.a = 1f;
        Slots[0].GetComponent<Image>().color = color0;
    }
    public void Image2ON()
    {
        Color color2 = Slots[1].GetComponent<Image>().color;
        color2.a = 1f;
        Slots[1].GetComponent<Image>().color = color2;
    }
    public void Image3ON()
    {
        Color color2 = Slots[2].GetComponent<Image>().color;
        color2.a = 1f;
        Slots[2].GetComponent<Image>().color = color2;
    }
    public void Image4ON()
    {
        Color color3 = Slots[3].GetComponent<Image>().color;
        color3.a = 1f;
        Slots[3].GetComponent<Image>().color = color3;
    }
    public void Image5ON()
    {
        Color color4 = Slots[4].GetComponent<Image>().color;
        color4.a = 1f;
        Slots[4].GetComponent<Image>().color = color4;
    }
    public void Image6ON()
    {
        Color color5 = Slots[5].GetComponent<Image>().color;
        color5.a = 1f;
        Slots[5].GetComponent<Image>().color = color5;
    }

    public void GameClear()
    {
        for (int i = 0; i < FindPlants.Length; i++)
        {
            if (FindPlants[i] == 0) return;
        }

        GameManager.I.DataManager.GameClear();
    }
}