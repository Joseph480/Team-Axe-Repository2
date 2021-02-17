using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Interactable : Interactable_Base
{
    public AudioClip buttonSound;
    private AudioSource audioSource;

    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log(gameObject.name + " Pressed");

        //FINISH
        //Press The Object

        audioSource = GetComponent<AudioSource>();

        audioSource.clip = buttonSound;
        audioSource.Play();
    }
}