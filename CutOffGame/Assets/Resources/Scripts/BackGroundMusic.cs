using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundMusic : MonoBehaviour
{
    public AudioClip[] music = new AudioClip[3];
    public Dropdown MusicdropDown;
    public Slider VolumeSlider;
    private AudioSource _audioSource;
    private int choice = 0;
    // Use this for initialization
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.clip = music[SSDirector.CurrentMusic];
        _audioSource.volume = SSDirector.Volume;
        _audioSource.Play();
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void SetMusic()
    {
        SSDirector.CurrentMusic = MusicdropDown.value;
        _audioSource.clip = music[SSDirector.CurrentMusic];
        _audioSource.Play();
    }

    public void SetVolume()
    {
        SSDirector.Volume = VolumeSlider.value;
        _audioSource.volume = SSDirector.Volume;
    }
}

