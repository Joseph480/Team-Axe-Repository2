using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Manager : MonoBehaviour
{
    //Variable for the Button_Data ScriptableObject
    [SerializeField] private Button_Data buttonData;
    // Start is called before the first frame update
    [SerializeField] private bool lockOverrideToggle;
    void Update()
    {
        buttonData.ButtonLock = lockOverrideToggle;
    }
}
