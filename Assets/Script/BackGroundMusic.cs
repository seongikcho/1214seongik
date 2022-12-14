using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public AudioClip[] Music = new AudioClip[9];
    AudioSource audio;

    void Awake()
    {
        //if (audio.isPlaying) return; //��������� ����ǰ� �ִٸ� �н�
        audio = this.GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!audio.isPlaying)
            RandomPlay();
            //DontDestroyOnLoad(audio);
    }

    void RandomPlay()
    {
        audio.clip = Music[Random.Range(0, Music.Length)];
        audio.Play();
    }
}