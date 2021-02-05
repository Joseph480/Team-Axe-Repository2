using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction_Controller : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Interaction_Input_Data interactionInputData;

    [SerializeField] private Interaction_Data interactionData;

    [Space, Header("UI")]
    [SerializeField] private Interaction_UI_Panel uiPanel;

    [Space, Header("Ray Settings")]
    [SerializeField] private float rayDistance;
    [SerializeField] private float raySphereRadius;

    public LayerMask interactableLayer;

    private Camera m_cam;
    private bool m_interacting;
    private float m_holdTimer = 0f;

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

        if(_hitSomething)
        {
            Interactable_Base _interactable = _hitInfo.transform.GetComponent<Interactable_Base>();

            if(_interactable != null)
            {
                if(interactionData.IsEmpty())
                {
                    interactionData.Interactable = _interactable;
                    uiPanel.SetTooltip(_interactable.TooltipMessage);
                }
                else
                {
                    if(!interactionData.IsSameInteractable(_interactable))
                    {
                        interactionData.Interactable = _interactable;
                        uiPanel.SetTooltip(_interactable.TooltipMessage);
                    }
                }
            }
        }
        else
        {
            uiPanel.ResetUI();
            interactionData.ResetData();
        }

        Debug.DrawRay(_ray.origin, _ray.direction * rayDistance, _hitSomething ? Color.green : Color.red);
    }

    void CheckForInteractableInput()
    {
        if(interactionData.IsEmpty())
            return;

        if(interactionInputData.InteractedClicked)
        {
            m_interacting = true;
            m_holdTimer = 0f;
        }

        if(interactionInputData.InteractedReleased)
        {
            m_interacting = false;
            m_holdTimer = 0f;
            uiPanel.UpdateProgressBar(0f);
        }

        if(m_interacting)
        {
            if (!interactionData.Interactable.IsInteractable)
                return;

            if(interactionData.Interactable.HoldInteract)
            {
                m_holdTimer += Time.deltaTime;

                float heldPercent = m_holdTimer / interactionData.Interactable.HoldDuration;
                uiPanel.UpdateProgressBar(heldPercent);

                if(heldPercent > 1f)
                {
                    interactionData.Interact();
                    m_interacting = false;
                }
            }
            else
            {
                interactionData.Interact();
                m_interacting = false;
            }
        }
    }
}
