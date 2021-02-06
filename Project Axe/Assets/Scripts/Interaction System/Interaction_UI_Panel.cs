using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction_UI_Panel : MonoBehaviour
{
    //This is the image of the progress bar
    [SerializeField] private Image progressBar;

    //This is the text of the interaction message
    [SerializeField] private TextMeshProUGUI tooltipText;

    //This method is called by Interaction_Controller.cs to set the interaction message based on the text 
    //specified by the interaction script component on the object, the default text is "Interact"
    public void SetTooltip(string tooltip)
    {
        tooltipText.SetText(tooltip);
    }

    //This method is called by Interaction_Controller to update the progress bar based on how long the 
    //interaction button is held and the value of the holdDuration property in Interaction_Base.cs
    public void UpdateProgressBar(float fillAmount)
    {
        progressBar.fillAmount = fillAmount;
    }

    //This method is called by Interaction_Controller.cs to clear the progress bar and interaction 
    //message from the ui when the player stops interacting with or pointing at an interactable!
    public void ResetUI()
    {
        progressBar.fillAmount = 0f;
        tooltipText.SetText("");
    }
}
