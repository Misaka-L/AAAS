using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace cdse_presets
{
    public class AudioVolumeChange : UdonSharpBehaviour
    {
        [Header("����һ���������ƽű�")]
        [Header("Ŀ����Դ��Audio Source��")]
        public AudioSource audioSource;
        [Header("���飨Slider��")]
        public Slider audioVolumeSlider;

        public void Start()
        {
            audioVolumeSlider.SetValueWithoutNotify(audioSource.volume);
        }

        public void OnValueChange()
        {
            audioSource.volume = audioVolumeSlider.value;
        }
    }
}