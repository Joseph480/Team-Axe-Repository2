using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class implements and extends Interactable_Base.cs, so it doesn't need to be attached to an
    //interactable object
public class Destroy_Interactable : Interactable_Base
{
    //Override the OnInteract() method in Interaction_Base.cs to add Unity's built-in Destroy() method 
        //to the interaction
    public override void OnInteract()
    {
        //excecute any 
        base.OnInteract();

        Debug.Log(gameObject.name + " Destroyed");

        Destroy(gameObject);
    }
}