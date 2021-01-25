using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Feet : MonoBehaviour
{
    [HideInInspector]
    public static int Jumps;
    [HideInInspector]
	public static int JumpCap = 1;

    void Start(){
        Jumps = JumpCap;
    }
	void OnTriggerStay(){
        if (Jumps < JumpCap)
		    Jumps = JumpCap;
	}
	void OnTriggerExit(){
		if(Jumps == JumpCap)
			Invoke ("JumpDeduct", 0.1f);
	}
	void JumpDeduct(){
		Jumps--;
	}

}
