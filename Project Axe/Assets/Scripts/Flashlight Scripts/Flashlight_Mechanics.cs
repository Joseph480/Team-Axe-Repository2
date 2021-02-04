using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight_Mechanics : MonoBehaviour
{
    public AudioSource clickSound;                                  //Sound of button when turned on and off
    public float batteryLifeInSeconds = 60f;                        //Variable for time it takes for the light to go out

    public float maxIntensity = 1f;                                 //Variable for how bright the light can be
    public float minIntensity = 0f;                                 //Variable for how dim the light can be

    public float swayAmount;                                        //How much the flashlight can sway
    public float maxAmount;                                         //Max sway
    public float smoothAmount;                                      //How smooth the object sways

    public float maxFlickerSpeed;
    public float minFlickerSpeed;

    private float batteryLife;                                      //Light intensity on light component  
    private bool isActive;                                          //Checks if the flashligh is on 

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

            if (myLight.intensity == minIntensity)                                               //When the flashlight reaches the minIntensity, the player can use this function    
            {
                if (Input.GetKeyDown(KeyCode.R))                                                 //Press "R" to smack flashlight
                {
                    myLight.intensity += Random.Range(0.2f, 0.5f);                               //Range the flashlight can be recharged to
                    StartCoroutine(FlickerEffect());

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
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);                             //Clamps maxAmount of sway on X axis

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);                                                                                           //Puts flashlight back to starting position
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);                        //Equation to control sway of flashlight
    }
           
    IEnumerator FlickerEffect()                                                                     
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));        
            
        }
    }
}


