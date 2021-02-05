using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Base : MonoBehaviour, I_Interactable
{
    [SerializeField] private bool holdInteract = true;
    [SerializeField] private float holdDuration = 1f;

    [SerializeField] private bool multipleUse = false;
    [SerializeField] private bool isInteractable = true;

    [SerializeField] private string tooltipMessage = "Interact";

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
