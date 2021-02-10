using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Temporary_AI_Motor : MonoBehaviour
{
    public float Speed;
    private Vector3 Target;
    private Player_Move_Controller Player;
    private Rigidbody Rb;
    private NavMeshAgent Agent;

    void Start(){
        Player = FindObjectOfType<Player_Move_Controller>();
        Rb = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();
        Target = Agent.destination;
    }

    void Update(){
        Chase();
    }

    public void Chase(){
        Target = Player.transform.position;
        Agent.destination = Target;
        //Rb.MovePosition(Vector3.Lerp(transform.position,Target,Speed/50));
    }

}
