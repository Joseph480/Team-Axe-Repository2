using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight_Mechanics : MonoBehaviour
{
    public AudioSource clickSound;                                  //Sound of button when turned on and off
    public float batteryLifeInSeconds = 60f;                        //Variable for time it takes for the light to go out

    public float maxIntensity = 1f;                                 //Variable for how bright the light can be
    public float minIntensity = 0f;                                 //Variable for how dim the light can be

    public float HighPowerRange;                                    //Var for further range of light
    public float HighPowerAngle;                                    //Var for a more narrow angle of light
    public float LowPowerRange;                                     //Var for closer range of light
    public float LowPowerAngle;                                     //Var for a more wide angle of light

    public float swayAmount;                                        //How much the flashlight can sway
    public float maxAmount;                                         //Max sway
    public float smoothAmount;                                      //How smooth the object sways

    public float FlickerSpeed;                                      //How long to start flicker enumerator once it hits startFlickerat var

    public float startFlicker;                                      //When the flashlight will start to flicker

    public float maxRecharge;                                       //Max amount of recharge smacking the flashlight can give
    public float minRecharge;                                       //Min amount of recharge smacking the flashlight can give

    private float batteryLife;                                      //Light intensity on light component  
    private bool isActive;                                          //Checks if the flashligh is on 
    private bool isToggled;                                         //Is used to switch between a low power mode and a high power mode

    private Light myLight;                                          //Light component

    private Vector3 initialPosition;                                //Variable for the flashlight position as a child of the camera 


    void Start()
    {
        myLight = GetComponent<Light>();                            //Renames Light component as myLight as well as gets component
        batteryLife = myLight.intensity;                            //Intenisty of the light is the battery life

        initialPosition = transform.localPosition;                  //This transforms just the flashlight as a child
    }

    void Update()
    {
        Flashlight_Light();
        Sway();
        Toggle();
    }

    void Flashlight_Light()
    {
        if (Input.GetKeyDown(KeyCode.F))                            //Press "F" to turn on and off flashlight
        {
            isActive = !isActive;                                   //Checks if flashlight is on or off
            clickSound.Play();                                      //Plays click sound
            
        }

        if (isActive)                                                                            //If the flashlight is truned on
        {
            myLight.enabled = true;                                                              //Turn on flashlight 
            myLight.intensity -= batteryLife / batteryLifeInSeconds * Time.deltaTime;            //Equation to low the intensity over time 

            if (myLight.intensity < minIntensity)                                                //Makes the flashlight stop at the minIntensity var 
            {
                myLight.intensity = minIntensity;                                                //Makes sure the intensity can't go below minIntensity
                
            }
                                                                                                 //THIS WAS JUST A TEST
            if (myLight.intensity <= startFlicker)                                               //When intensity is less than the nuber set when the flicker starts
            {
                StartCoroutine(FlickerEffect());                                                 //Run flicker IEnumerator
            }

            if (myLight.intensity >= startFlicker)                                               //When intensity is greater than the number set when the flicker starts
            {
                StopCoroutine(FlickerEffect());                                                  //Stop flicker IEnumerator
            }


            if (myLight.intensity == minIntensity)                                               //When the flashlight reaches the minIntensity, the player can use this function    
            {
                if (Input.GetKeyDown(KeyCode.R))                                                 //Press "R" to smack flashlight
                {
                    myLight.intensity += Random.Range(minRecharge, maxRecharge);                 //Range the flashlight can be recharged to
                    
                    if (myLight.intensity > maxIntensity)                                        //If the intensity is higher than maxIntensity var
                    {
                        myLight.intensity = maxIntensity;                                        //Intensity won't go higher than maxIntensity var
                    }
                }
            }

        }

        else
        {
            myLight.enabled = false;                                                            //Turns off flashlight

        }

        
    }

    void Sway()
    {
        float movementX = -Input.GetAxis("Mouse X") * swayAmount;                              //Allows the sway on the X axis
        float movementY = -Input.GetAxis("Mouse Y") * swayAmount;                              //Allows the sway on the Y axis
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);                             //Clamps maxAmount of sway on X axis
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);                             //Clamps maxAmount of sway on Y axis

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);                                                                                           //Puts flashlight back to starting position
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);                        //Equation to control sway of flashlight
    }
           
    IEnumerator FlickerEffect()                                                                                 //Flicker function                           
    {
        myLight.enabled = true;                                                                                 //When flashlight is on
            yield return new WaitForSeconds(FlickerSpeed);                                                      //How fast the the light will flicker once it reaches min intensity

        myLight.enabled = false;                                                                                //When flashligh is off
            yield return new WaitForSeconds(FlickerSpeed);                                                      //Stops flicker

    }

    void Toggle()                                                                                   //Function to toggle the range and angle of the flashlight
    {
        if (Input.GetKeyDown(KeyCode.T) && myLight.enabled == true)                                 //If the flashilight is on and the player presses "T", it will toggle
        {
            isToggled = !isToggled;                                                                 //Checks if the toggle is set on high or low when it gets ready to switch
        }

        if (isToggled)                                                                              //If the player toggles
        {
            myLight.range = LowPowerRange;                                                          //Sets the flashlight range to low power range var
            myLight.spotAngle = LowPowerAngle;                                                      //Sets the flashlight angle to low power angle var
        }

        else
        {
            myLight.range = HighPowerRange;                                                         //Sets the flashlight range to high power range var
            myLight.spotAngle = HighPowerAngle;                                                     //Sets the flashlight angle to high power angle var
        }
    }

}


