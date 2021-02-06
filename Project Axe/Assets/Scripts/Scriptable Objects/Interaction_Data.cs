using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction_Data", menuName = "InteractionSystem/Interaction_Data")]
public class Interaction_Data : ScriptableObject
{
    //Instance of Interactable_Base.cs used to store an interactable's data 
    private Interactable_Base m_interactable;

    //Used by Interaction_Controller.cs to access information on the interactable
    public Interactable_Base Interactable
    {
        get => m_interactable;
        set => m_interactable = value;
    }

    //Called by Interaction_Controller.cs when an interaction is triggered, and calls OnInteract() in 
        //Interactable_Base.cs
    public void Interact()
    {
        m_interactable.OnInteract();
        ResetData();
    }

    //Used by Interaction_Controller.cs to compare whether the new interactable is the same as m_interactable
    public bool IsSameInteractable(Interactable_Base _newInteractable) => m_interactable == _newInteractable;

    //Used by Interaction_Controller.cs to check if m_interactable has an interactable stored
    public bool IsEmpty() => m_interactable == null;

    //Used by Interaction_Controller.cs and here to reset and clear the interactable stored in m_interactable
    public void ResetData() => m_interactable = null;
}
