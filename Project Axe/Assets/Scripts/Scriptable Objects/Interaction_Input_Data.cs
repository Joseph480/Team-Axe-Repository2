using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction_Input_Data", menuName = "InteractionSystem/Input_Data")]
public class Interaction_Input_Data : ScriptableObject
{
    private bool m_interactedClicked;
    private bool m_interactedReleased;

    public bool InteractedClicked
    {
        get => m_interactedClicked;
        set => m_interactedClicked = value;
    }

    public bool InteractedReleased
    {
        get => m_interactedReleased;
        set => m_interactedReleased = value;
    }

    public void ResetInput()
    {
        m_interactedClicked = false;
        m_interactedReleased = false;
    }
}
