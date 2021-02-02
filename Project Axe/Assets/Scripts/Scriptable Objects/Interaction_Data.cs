using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction_Data", menuName = "InteractionSystem/Interaction_Data")]
public class Interaction_Data : ScriptableObject
{
    private Interactable_Base m_interactable;

    public Interactable_Base Interactable
    {
        get => m_interactable;
        set => m_interactable = value;
    }

    public void Interact()
    {
        m_interactable.OnInteract();
        ResetData();
    }

    public bool IsSameInteractable(Interactable_Base _newInteractable) => m_interactable == _newInteractable;

    public bool IsEmpty() => m_interactable == null;

    public void ResetData() => m_interactable = null;
}
