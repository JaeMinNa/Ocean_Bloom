using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    public Slider MouseSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MouseLookSensitivity"))
        {
            MouseSlider.value = PlayerPrefs.GetFloat("MouseLookSensitivity");
        }
    }

    public void MouseLookSensitivity()
    {
        float mouseLookSensitivity = MouseSlider.value;
        PlayerPrefs.SetFloat("MouseLookSensitivity", mouseLookSensitivity);

        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().LookSensitivity = mouseLookSensitivity;
    }

    public void MouseLookSensitivityInLoby()
    {
        float mouseLookSensitivity = MouseSlider.value;
        PlayerPrefs.SetFloat("MouseLookSensitivity", mouseLookSensitivity);
    }
}
