using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Controller : MonoBehaviour
{
    [Header("Data")]
    public Interaction_Input_Data interactionInputData;

    public Interaction_Data interactionData;

    [Space, Header("Ray Settings")]
    public float rayDistance;
    public float raySphereRadius;

    public LayerMask interactableLayer;

    private Camera m_cam;

    void Awake()
    {
        m_cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        CheckForInteractable();
        CheckForInteractableInput();
    }

    void CheckForInteractable()
    {
        Ray _ray = new Ray(m_cam.transform.position, m_cam.transform.forward);
        RaycastHit _hitInfo;

        bool _hitSomething = Physics.SphereCast(_ray, raySphereRadius, out _hitInfo, rayDistance, 
        interactableLayer);

        if (_hitSomething)
        {
            Interactable_Base _interactable = _hitInfo.transform.GetComponent<Interactable_Base>();

            if (_interactable != null)
            {
                if (interactionData.IsEmpty())
                {
                    interactionData.Interactable = _interactable;
                }
                else
                {
                    if (!interactionData.IsSameInteractable(_interactable))
                    {
                        interactionData.Interactable = _interactable;
                    }
                }
            }
        }
        else
        {
            interactionData.ResetData();
        }

        Debug.DrawRay(_ray.origin, _ray.direction * rayDistance, _hitSomething ? Color.green : Color.red);
    }

    void CheckForInteractableInput()
    {
        
    }
}
