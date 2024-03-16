using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Tmp : MonoBehaviour
{
    public AudioMixer MasterMixer;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public Slider MasterSlider;

    private void Start()
    {
        float bgm = PlayerPrefs.GetFloat("BGMValue");
        float sfx = PlayerPrefs.GetFloat("SFXValue");
        float master = PlayerPrefs.GetFloat("MasterValue");

        BGMSlider.value = bgm;
        SFXSlider.value = sfx;
        MasterSlider.value = master;

        if (bgm == -40f)
        {
            MasterMixer.SetFloat("BGM", -80);
            BGMSlider.value = -80;
        }
        else
        {
            MasterMixer.SetFloat("BGM", bgm);
            BGMSlider.value = bgm;
        }

        if (sfx == -40f)
        {
            MasterMixer.SetFloat("SFX", -80);
            SFXSlider.value = -80;
        }
        else
        {
            MasterMixer.SetFloat("SFX", sfx);
            SFXSlider.value = sfx;
        }

        if (master == -40f)
        {
            MasterMixer.SetFloat("Master", -80);
            MasterSlider.value = -80;
        }
        else
        {
            MasterMixer.SetFloat("Master", master);
            MasterSlider.value = master;
        }
    }

    public void BGMControl()
    {
        float bgm = BGMSlider.value;
        PlayerPrefs.SetFloat("BGMValue", bgm);

        if (bgm == -40f) MasterMixer.SetFloat("BGM", -80);
        else MasterMixer.SetFloat("BGM", bgm);
    }

    public void SFXControl()
    {
        float sfx = SFXSlider.value;
        PlayerPrefs.SetFloat("SFXValue", sfx);

        if (sfx == -40f) MasterMixer.SetFloat("SFX", -80);
        else MasterMixer.SetFloat("SFX", sfx);
    }

    public void MasterControl()
    {
        float master = MasterSlider.value;
        PlayerPrefs.SetFloat("MasterValue", master);

        if (master == -40f) MasterMixer.SetFloat("Master", -80);
        else MasterMixer.SetFloat("Master", master);
    }
}
