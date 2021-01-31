using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Base : MonoBehaviour, I_Interactable
{
    public bool multipleUse;
    public bool isInteractable;
    public bool holdInteract;

    public float holdDuration;

    public string tooltipMessage = "interact";

    public bool HoldInteract => holdInteract;
    public bool MultipleUse => multipleUse;
    public bool IsInteractable => isInteractable; 
    
    public float HoldDuration => holdDuration;

    public string TooltipMessage => tooltipMessage;

    public virtual void OnInteract()
    {
        Debug.Log("INTERACTED: " + gameObject.name);
    }
}
