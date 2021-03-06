﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectController : MonoBehaviour
{
    public MonsterSpawnerController monsterController;
    public AudioSource ambiSource;
    public AudioClip ambience;
    private bool isPlaying = false;

    void Awake()
    {
        ambiSource.clip = ambience;
    }

    void Update()
    {
        if ((monsterController.monsterExists) && !isPlaying)
        {
            ambiSource.clip = ambience;
            ambiSource.Play();
            isPlaying = true;
        }
        else if (!monsterController.monsterExists)
        {
            ambiSource.Stop();
            isPlaying = false;
        }
    }
}
