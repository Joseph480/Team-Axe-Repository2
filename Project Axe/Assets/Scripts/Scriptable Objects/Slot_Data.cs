using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slot_Data", menuName = "InteractionSystem/Slot_Data")]
public class Slot_Data : ScriptableObject
{
    private bool m_slotFilled;
    private bool m_slotLock;

    public bool SlotFilled
    {
        get => m_slotFilled;
        set => m_slotFilled = value;
    }

    public bool SlotLock
    {
        get => m_slotLock;
        set => m_slotLock = value;
    }

    public void ResetSlot()
    {
        m_slotFilled = false;
        m_slotLock = false;
    }
}
