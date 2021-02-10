using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Base : MonoBehaviour, I_Interactable
{
    //These are the variables and settings used by every interactable, and accessed by Interaction_Controller.cs via the following get methods
    [SerializeField] private bool holdInteract = true;
    [SerializeField] private float holdDuration = 1f;
    [SerializeField] private bool multipleUse = false;
    [SerializeField] private bool isInteractable = true;
    [SerializeField] private string tooltipMessage = "Interact";

    //These get methods are used by Interaction_Controller.cs to return the values of the properties of the interactable
    public bool HoldInteract => holdInteract;
    public bool MultipleUse => multipleUse;
    public bool IsInteractable => isInteractable; 
    public float HoldDuration => holdDuration;
    public string TooltipMessage => tooltipMessage;

    //This is the base virtual method that actually does something when an object is interacted with
    //It will be overridden by the actual interaction script attached to the interactable object!
    public virtual void OnInteract()
    {
        Debug.Log("INTERACTED: " + gameObject.name);
    }
}
