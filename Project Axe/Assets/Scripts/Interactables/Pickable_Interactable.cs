using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_Interactable : Interactable_Base
{
    public Item_Data item;

    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log(item.itemName + " Picked Up");

        bool wasPickedUp = Inventory.instance.Add(item);

        //Destroy after picking up!
        if(wasPickedUp)
            Destroy(gameObject);
            
    }
}
