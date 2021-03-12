using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Controller : MonoBehaviour
{
    public KeyCode Forward, Back, Left, Right, Sprint, Jump;
    public float MoveSpeed, SprintMultiplier, LookSpeed, JumpPower, Drift, Drag;

    private Vector3 CamF,CamR,Mover;
    private Vector2 MinMax = new Vector2 (-89f, 89f);
    private float Yaw, Pitch, BaseSpeed;
    [HideInInspector]
    public bool Mf,Mb,Ml,Mr,Jmp,Sp;//,Moving;
    private Camera Cam;
    private Rigidbody Rb;
    
    void Start(){
        BaseSpeed = MoveSpeed;
        Cam = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Rb = this.GetComponent<Rigidbody>();
    }
    void Update(){
        ReadController();
    }
    void LateUpdate(){
        CamRotate();
    }
    void FixedUpdate(){
        Move();
        Sprinting();
    }
    void ReadController(){
        if(Input.GetKey(Forward))Mf=true;else Mf=false;
        if(Input.GetKey(Back))Mb=true;else Mb=false;
        if(Input.GetKey(Left))Ml=true;else Ml=false;
        if(Input.GetKey(Right))Mr=true;else Mr=false;
        if(Input.GetKey(Sprint))Sp=true;else Sp=false;
        if(Input.GetKeyDown(Jump) && Player_Feet.Jumps > 0)Jmp=true;
    }
    /*void CheckMoving(){
        if (Ml||Mr||Mf||Mb)Moving=true;else Moving=false;
    }*/
     void Move(){
        CamRelative();
        if (Mf){Mover += Vector3.Lerp(CamF,CamF,MoveSpeed);}
        if (Mb){Mover -= Vector3.Lerp(CamF,CamF,MoveSpeed);}
        if (Ml){Mover -= Vector3.Lerp(CamR,CamR,MoveSpeed);}
        if (Mr){Mover += Vector3.Lerp(CamR,CamR,MoveSpeed);}
        Vector3 Slide = Mover - transform.position; Vector3.Normalize(Slide);
        Rb.AddForce(Slide * (Drift + Rb.velocity.magnitude)); if (Rb.velocity.magnitude >= Drag) Rb.AddForce(-Slide * (Drift + Rb.velocity.magnitude));
        Rb.MovePosition(Vector3.Lerp(transform.position,Mover,(MoveSpeed/50)));
        if (Jmp) Jumping();
    }
    void CamRelative(){
        Mover = transform.position;
        CamF = Cam.transform.forward;
        CamR = Cam.transform.right;
        CamF.y = 0; CamF = CamF.normalized;
        CamR.y = 0; CamR = CamR.normalized;
    }
    void CamRotate(){
        Yaw += Input.GetAxis("Mouse X") * LookSpeed;
        Pitch -= Input.GetAxis("Mouse Y") * LookSpeed;
        Pitch = Mathf.Clamp(Pitch, MinMax.x, MinMax.y);
        Cam.transform.rotation = Quaternion.Euler(Pitch,Yaw,0f);
        transform.rotation = Quaternion.Euler(0f,Yaw,0f);
    }
    void Jumping(){
        Jmp = false;
        Rb.velocity = transform.up * JumpPower;
        Player_Feet.Jumps--;
        Debug.Log(Player_Feet.Jumps);
    }
    void Sprinting(){
        if (Sp) MoveSpeed = BaseSpeed * SprintMultiplier;
        else MoveSpeed = BaseSpeed;
    }
    void OnTriggerEnter (Collider other){
        switch (other.tag){
            default: break;
        }
    }
}
