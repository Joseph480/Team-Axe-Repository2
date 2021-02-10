using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction_Input_Data", menuName = "InteractionSystem/Input_Data")]
public class Interaction_Input_Data : ScriptableObject
{
    //Variables that store whether the interaction button was clicked or released
    private bool m_interactedClicked;
    private bool m_interactedReleased;

    //Used by Interaction_Controller.cs to return if the interaction button was pressed
    public bool InteractedClicked
    {
        get => m_interactedClicked;
        set => m_interactedClicked = value;
    }

    //Used by Interaction_Controller.cs to return if the interaction button was released
    public bool InteractedReleased
    {
        get => m_interactedReleased;
        set => m_interactedReleased = value;
    }

    //Used by Interaction_Controller.cs to reset the input data
    public void ResetInput()
    {
        m_interactedClicked = false;
        m_interactedReleased = false;
    }
}
