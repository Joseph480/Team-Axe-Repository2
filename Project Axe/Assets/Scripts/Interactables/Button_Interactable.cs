using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Interactable : Interactable_Base
{
    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log(gameObject.name + " Pressed");

        //FINISH
        //Press The Object
    }
}
