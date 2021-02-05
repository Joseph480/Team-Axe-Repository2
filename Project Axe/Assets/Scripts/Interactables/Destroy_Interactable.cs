using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Interactable : Interactable_Base
{
    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log(gameObject.name + " Destroyed");

        Destroy(gameObject);
    }
}