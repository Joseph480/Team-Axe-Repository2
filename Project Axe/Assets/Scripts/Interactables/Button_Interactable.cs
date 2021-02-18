using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Interactable : Interactable_Base
{
    //Variables for the sound
    [SerializeField] private AudioClip buttonSound;
    private AudioSource audioSource;

    //Variable for the Button_Data ScriptableObject
    [SerializeField] private Button_Data buttonData;

    //Variables for the button timer
    [SerializeField] private bool hasActivationTime;
    [SerializeField] private float activationTime;
    //private bool m_buttonActive;
    private float m_holdTimer = 0f;

    //When the button is initialized, reset the button state to false
    void Start()
    {
        buttonData.ResetState();
        Debug.Log(gameObject.name + "\nButton State: " + buttonData.ButtonState + ", Active: " + buttonData.ButtonActive + ", Locked: " + buttonData.ButtonLock);
    }

    void Update()
    {
        //if the button is active
        if (buttonData.ButtonActive)
        {
            //if it needs to be active for a time
            if (hasActivationTime)
            {
                //Count how long the button has been active
                m_holdTimer += Time.deltaTime;

                //calculate how long the button has been active and how much longer it needs to be active for
                float activePercent = m_holdTimer / activationTime;

                //once the button has been active for long enough, set the button as not active
                if (activePercent > 1f)
                {
                    buttonData.ButtonActive = false;
                    m_holdTimer = 0;
                    //Debug.Log("Button Inactive");
                }
            }
            //
            else
            {
                buttonData.ButtonActive = false;
                m_holdTimer = 0;
                //Debug.Log("Button Inactive");
            }
        }
    }

    public override void OnInteract()
    {
        base.OnInteract();

        //Find the audio source of the object
        audioSource = GetComponent<AudioSource>();

        //Set the sound of this button
        audioSource.clip = buttonSound;
        

        //As long as the button isn't active or locked, toggle the state
        if(!buttonData.ButtonActive && !buttonData.ButtonLock)
        {
            //Set this button's state to true if it's false, and vice versa
            if (!buttonData.ButtonState)
            {
                buttonData.ButtonState = true;
                buttonData.ButtonActive = true;
                audioSource.Play();
                //Debug.Log("Button Active");
            }
            else
            {
                buttonData.ButtonState = false;
                buttonData.ButtonActive = true;
                audioSource.Play();
                //Debug.Log("Button Active");
            }
        }


        //Tell the console we've interacted with this button
        Debug.Log(gameObject.name + "\nButton State: " + buttonData.ButtonState + ", Active: " + buttonData.ButtonActive + ", Locked: " + buttonData.ButtonLock);
    }
}