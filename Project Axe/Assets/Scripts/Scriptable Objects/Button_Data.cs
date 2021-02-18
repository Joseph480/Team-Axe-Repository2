using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Button_Data", menuName = "InteractionSystem/Button_Data")]
public class Button_Data : ScriptableObject
{
    //Is the button on or off
    private bool m_buttonState;

    //Is the button locked because it is still activating
    private bool m_buttonActive;

    //Is the button completely locked due to some outside condition
    private bool m_buttonLock;

    public bool ButtonState
    {
        get => m_buttonState;
        set => m_buttonState = value;
    }

    public bool ButtonActive
    {
        get => m_buttonActive;
        set => m_buttonActive = value;
    }

    public bool ButtonLock
    {
        get => m_buttonLock;
        set => m_buttonLock = value;
    }

    //Reset the button state and the locked state, but the lock override must be changed manually by conditions in the scene
    public void ResetState()
    {
        m_buttonState = false;
        m_buttonActive = false;
    }
}
