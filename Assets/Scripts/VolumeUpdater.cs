using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeUpdater : MonoBehaviour {
    [SerializeField]
    private AudioSource musicSrc;
    //[SerializeField]
    //private AudioSource sfxSrc;

    private float musicVolume = 1f;
    //private float sfxVolume = 1f;

    private void Awake() {
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
    }

    private void Start() {
        musicSrc = GetComponent<AudioSource>();
       // sfxSrc = GetComponent<AudioSource>();
    }

    private void Update() {
        musicSrc.volume = musicVolume;
        //sfxSrc.volume = sfxVolume;
    }

    public void SetMusicVolume(float vol) {
        musicVolume = vol;
        PlayerPrefs.SetFloat("MusicVolume", vol);
    }

    /*
    public void SetSfxVolume(float vol) {
        sfxVolume = vol;
        PlayerPrefs.SetFloat("SfxVolume", vol);
    }
    */
}
