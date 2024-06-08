using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerVolume : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SfxSlider;

    // Update is called once per frame
    void Update()
    {
        MusicSlider.value = AudioManager.AudioInstance.MusicSource.volume;
        SfxSlider.value = AudioManager.AudioInstance.SfxSource.volume;
    }

    public void MusicVolume() {
        AudioManager.AudioInstance.MusicVolume(MusicSlider.value);
    }

    public void SfxVolume()
    {
        AudioManager.AudioInstance.SfxVolume(SfxSlider.value);
    }
}
