using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_UI_Slot : MonoBehaviour
{
    [SerializeField] private Image itemIconSlot;

    [SerializeField] private Sprite defaultItemIcon;

    public void Start()
    {
        itemIconSlot.sprite = defaultItemIcon;
        RectTransform rt = itemIconSlot.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(3, 3);
    }

    public void UpdateItemIconSlot(Sprite newItem)
    {
        itemIconSlot.sprite = newItem;
        RectTransform rt = itemIconSlot.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(40, 40);
    }

    public void ResetItemIconSlot()
    {
        itemIconSlot.sprite = defaultItemIcon;
        RectTransform rt = itemIconSlot.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(3, 3);
    }
}
