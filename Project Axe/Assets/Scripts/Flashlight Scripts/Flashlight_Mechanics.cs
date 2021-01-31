using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight_Mechanics : MonoBehaviour
{
    public AudioSource clickSound;
    public float batteryLifeInSeconds = 60f;

    public float maxIntensity = 1f;

    private float batteryLife;
    private bool isActive;

    private Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
        batteryLife = myLight.intensity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isActive = !isActive;
            clickSound.Play();

        }

        if (isActive)
        {
            myLight.enabled = true;
            myLight.intensity -= batteryLife / batteryLifeInSeconds * Time.deltaTime;

            if (myLight.intensity == 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    myLight.intensity += Random.Range(0.2f, 0.5f);

                    if (myLight.intensity > maxIntensity)
                    {
                        myLight.intensity = maxIntensity;
                    }
                }
            }
        }

        else
        {
            myLight.enabled = false;
        }
    }

}
