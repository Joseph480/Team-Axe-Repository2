using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Change the class name to match the interaction you are making
public class Template_Interactable : Interactable_Base
{
    //This override method will add to the OnInteract() method in Interactable_Base.cs
    //This is an essential method to include in any interaction scripts, this overrides the base class
        //and is what makes any of the interactions do anything!
    public override void OnInteract()
    {
        // | --------------------------------------------------------------
        // V KEEP

        //this line will excecute any code in Interactable_Base.cs before adding excecuting your own code below
        base.OnInteract();

        // | --------------------------------------------------------------
        // V ADD WHATEVER YOU WANT THE INTERACTION TO DO BELOW THIS COMMENT

        //This is just a debug line to tell the console what the interaction did
        Debug.Log(gameObject.name + " Destroyed");

        //This line actually deletes the object and is specific to this interaction script. 
        Destroy(gameObject);
    }
}