using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Handler : MonoBehaviour
{
    //Links to the appropriate input data files
    [Header("Input Data")]
    //[SerializeField] private CameraInputData cameraInputData = null;
    //[SerializeField] private MovementInputData movementInputData = null;
    public Interaction_Input_Data interactionInputData = null;
    
    //At the start, reset any and all stored input data
    void Start()
    {
        //cameraInputData.ResetInput();
        //movementInputData.ResetInput();
        interactionInputData.ResetInput();
    }

    //Update the input data from the following methods
    void Update()
    {
        //GetCameraInput();
        //GetMovementInputData();
        GetInteractionInputData();
    }

    //Check for the state of the interaction key and store the input data
    void GetInteractionInputData()
    {
        interactionInputData.InteractedClicked = Input.GetKeyDown(KeyCode.E);
        interactionInputData.InteractedReleased = Input.GetKeyUp(KeyCode.E);
    }

    /*
    //Check for the state of the camera controls and store the input data
    void GetCameraInput()
    {
        cameraInputData.InputVectorX = Input.GetAxis("Mouse X");
        cameraInputData.InputVectorY = Input.GetAxis("Mouse Y");

        cameraInputData.ZoomClicked = Input.GetMouseButtonDown(1);
        cameraInputData.ZoomReleased = Input.GetMouseButtonUp(1);
    }
    */

    /*
    //Check for the state of the movement controls and store the input data
    void GetMovementInputData()
    {
        movementInputData.InputVectorX = Input.GetAxisRaw("Horizontal");
        movementInputData.InputVectorY = Input.GetAxisRaw("Vertical");

        movementInputData.RunClicked = Input.GetKeyDown(KeyCode.LeftShift);
        movementInputData.RunReleased = Input.GetKeyUp(KeyCode.LeftShift);

        if (movementInputData.RunClicked)
            movementInputData.IsRunning = true;

        if (movementInputData.RunReleased)
            movementInputData.IsRunning = false;

        movementInputData.JumpClicked = Input.GetKeyDown(KeyCode.Space);
        movementInputData.CrouchClicked = Input.GetKeyDown(KeyCode.LeftControl);
    }
    */
    
}
