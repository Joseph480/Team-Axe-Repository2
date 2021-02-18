using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction_Controller : MonoBehaviour
{
    //Links the 2 data files, Interaction_Data and Interaction_Input_Data
    [Header("Data")]
    [SerializeField] private Interaction_Input_Data interactionInputData;
    [SerializeField] private Interaction_Data interactionData;

    //Links the interaction panel asset
    [Space, Header("UI")]
    [SerializeField] private Interaction_UI_Panel uiPanel;

    //Variable settings for the Ray
    [Space, Header("Ray Settings")]
    [SerializeField] private float rayDistance;
    [SerializeField] private float raySphereRadius;

    //Sets the object layer that can be interacted with
    public LayerMask interactableLayer;

    //Variable for the main Camera
    private Camera m_cam;

    //variables for interactions to see if the player is currently interacting with an object
        //and how long they have been holding the interaction button if the interaction button needs
        //to be held for an interaction
    private bool m_interacting;
    private float m_holdTimer = 0f;

    //Find the camera at the start, to use its transform position for the ray/spherecast
    void Awake()
    {
        m_cam = FindObjectOfType<Camera>();
    }

    //Calls the methods that check for an interactable, and interaction input from the player
    void Update()
    {
        CheckForInteractable();
        CheckForInteractableInput();
    }

    //This method casts a ray forward from the camera's transform position and sets the ui text
        //to prompt the player they can interact with it
    void CheckForInteractable()
    {
        //create a new ray in front of the camera
        Ray _ray = new Ray(m_cam.transform.position, m_cam.transform.forward);
        RaycastHit _hitInfo;

        //Throw the Ray/Spherecast and check if it hits something
        bool _hitSomething = Physics.SphereCast(_ray, raySphereRadius, out _hitInfo, rayDistance, 
        interactableLayer);

        //If the Ray/Spherecast hits something, display the interaction message in the UI and store 
            //the interaction data
        if(_hitSomething)
        {
            //Create a new temporary Interactable_Base from the object the ray hit
            Interactable_Base _interactable = _hitInfo.transform.GetComponent<Interactable_Base>();

            //If there is no interactable, 
            if(_interactable != null)
            {   
                //If the data is empty, fill it with the info of the object that was hit
                if(interactionData.IsEmpty())
                {
                    interactionData.Interactable = _interactable;
                    uiPanel.SetTooltip(_interactable.TooltipMessage);
                }
                //If the data had information in it, check to see if it is the same interactable or a different one
                else
                {
                    //If the data of the current interactable is different from the new one, set the data to the
                        //new interactable and set the new interactable message
                    if(!interactionData.IsSameInteractable(_interactable))
                    {
                        interactionData.Interactable = _interactable;
                        uiPanel.SetTooltip(_interactable.TooltipMessage);
                    }
                }
            }
        }
        //If nothing was hit, reset data and the UI
        else
        {
            uiPanel.ResetUI();
            interactionData.ResetData();
        }

        //Debug the ray in the scene view, color the ray red if no interactables are hit, and green if there are
        Debug.DrawRay(_ray.origin, _ray.direction * rayDistance, _hitSomething ? Color.green : Color.red);
    }

    //This method checks to see if the object is interactable, and if the player is pressing the interact button.
        //If the object is an interactable AND the player is using the interact button, and calls OnInteract()
        //method in Interactable_Base and the override for the same method in the interactable's script. If the
        //interactable needs the button to be held, the progress bar bar will fill the longer the player holds
        //the button until it is held for the required time, as dictated by the interactable's settings.
    void CheckForInteractableInput()
    {
        //if there is no interactable, leave the method
        if(interactionData.IsEmpty())
            return;

        //If the button is pressed, reset the timer and sets m_interacting to true
        if(interactionInputData.InteractedClicked)
        {
            m_interacting = true;
            m_holdTimer = 0f;
        }

        //if the button has been released, reset the progress bar and the timer, and set m_interacting to false;
        if(interactionInputData.InteractedReleased)
        {
            m_interacting = false;
            m_holdTimer = 0f;
            uiPanel.UpdateProgressBar(0f);
        }

        //if the player is currently pressing/holding the button, determine if they are even on an interactable
            //and update the progress bar for as long as the button is held and/or call OnInteract() in
            //Interaction_Base.cs.
        if(m_interacting)
        {
            //if the button is pressed but there is no interactable, leave the method
            if (!interactionData.Interactable.IsInteractable)
                return;

            //If the button needs to be held for the interactable, update the progress bar until its full, and
                //then call the Interact() method in Interaction_Data, whcih calls OnInteract() in 
                //Interaction_Base.cs
            if(interactionData.Interactable.HoldInteract)
            {
                //Count how long the button has been held
                m_holdTimer += Time.deltaTime;

                //Update the progress bar according to how long the button needs to be held, and how long it's 
                    //been held already
                float heldPercent = m_holdTimer / interactionData.Interactable.HoldDuration;
                uiPanel.UpdateProgressBar(heldPercent);

                //When the progress bar is full, call the interaction function in Interaction_Base.cs and 
                    //reset m_interacting to false
                if(heldPercent > 1f)
                {
                    interactionData.Interact();
                    m_interacting = false;
                }
            }
            //If the button doesn't need to be held, just call OnInteract() in Interaction_Base.cs
            else
            {
                interactionData.Interact();
                m_interacting = false;
            }
        }
    }
}
