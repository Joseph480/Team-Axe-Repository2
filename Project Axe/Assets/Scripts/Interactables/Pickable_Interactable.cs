using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_Interactable : Interactable_Base
{
    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log(gameObject.name + " Picked Up");

        //FINISH
        //Pick Up The Object

        //Destroy after picking up!
        Destroy(gameObject);
    }
}
