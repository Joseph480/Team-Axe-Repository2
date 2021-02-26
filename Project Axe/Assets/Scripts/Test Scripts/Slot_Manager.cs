using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Manager : MonoBehaviour
{
    [SerializeField] private Slot_Data slotData;
    [SerializeField] private bool lockOverrideToggle;

    void Update()
    {
        slotData.SlotLock = lockOverrideToggle;
    }
}
