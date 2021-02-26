using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Interactable : Interactable_Base
{
    [SerializeField] private Slot_Data slotData;
    [SerializeField] private Item_Data correctItem;
    
    void Start()
    {
        slotData.ResetSlot();
        Debug.Log(gameObject.name + "\nSlot State: " + slotData.SlotFilled + ", Locked: " + slotData.SlotLock);
    }

    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log(gameObject.name + " Interacted");
        
        if(!slotData.SlotLock)
        {
            if(!slotData.SlotFilled)
            {
                if(Inventory.instance.items.Contains(correctItem))
                {
                    Debug.Log("Correct Item Found!");
                    Inventory.instance.Remove(correctItem);
                    slotData.SlotFilled = true;
                }
                else
                {
                    Debug.Log("Correct Item Not Found!");
                }
            }
            else
            {
                Debug.Log(correctItem.itemName + " Already Inserted!");
            }
        }
        else
        {
            Debug.Log(gameObject.name + " Locked!");
        }
    }
}