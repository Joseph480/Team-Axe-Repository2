using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item_Data : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite itemIcon = null;
    public bool isDefaultItem = false;
}
