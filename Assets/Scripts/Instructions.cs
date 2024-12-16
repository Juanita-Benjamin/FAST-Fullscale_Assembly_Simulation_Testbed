using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RogoDigital.Lipsync;
//using Unity.VisualScripting;


public class Instructions : MonoBehaviour
{
    public LipSyncData[] lipSyncInstruction;
    public LipSync LipSync;
    public AudioClip popSound;
    public AudioSource popSource;
    public bool isPlayingInstructions;
    public int clipIndex = 0; //tracks the next clip to be played


    // Start is called before the first frame update
    void Start()
    {
        LipSync.Play(lipSyncInstruction[0]); //plays the first instruction

        clipIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (popSource.isPlaying) 
        {
            PlayPopSound();
        }

    }

    //check if something is playing
    public void PlayPopSound()
    {
        //if there is nothing playing
        //if the bool is false then call Instructions
        if (!isPlayingInstructions)
        {
            StartCoroutine(VoiceInstructions());
        }
    }


    //play instructions after every sound of pop
    private IEnumerator VoiceInstructions()
    {
        
        isPlayingInstructions = true;


        if (popSource.isPlaying)
        {
            yield return new WaitForSeconds(popSound.length);

            //everytime we hear a pop we play the next instructions
            LipSync.Play(lipSyncInstruction[clipIndex]);
            yield return new WaitForSeconds(lipSyncInstruction[clipIndex].clip.length);

            isPlayingInstructions = false; //reset to false
            
        }
        clipIndex += 1;

    }

}
