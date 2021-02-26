using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Instance
    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    [SerializeField] private Item_UI_Slot uiItemSlot;

    public int space = 1;
    
    public List<Item_Data> items = new List<Item_Data>();

    public bool Add(Item_Data item)
    {
        if(!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not Enough Space");
                return false;
            }
            Debug.Log("Added " + item.itemName + "!");
            items.Add(item);

            uiItemSlot.UpdateItemIconSlot(item.itemIcon);

            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item_Data item)
    {
        Debug.Log("Removed " + item.itemName + "!");
        items.Remove(item);

        uiItemSlot.ResetItemIconSlot();
    }
}