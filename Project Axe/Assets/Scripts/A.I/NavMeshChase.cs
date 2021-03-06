using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshChase : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent Enemy;                           //calls to use the NavMesh

    public GameObject Player;                                            //The Player is the target

    public Transform target;                                             //The Player is the target position

    public float delay = 1f;                                             //A delay to start chasing the player when they are heard 

    private SphereCollider col;                                          //call the sphere collider

    public List<Transform> points;                                       //A list of waypoints for the idle NavMesh path to follow

    private int destPoint = 0;                                           //Each waypoint has its own set number 

    public bool playerInSight;                                           //A bool that tells if the player is in or out of LOS of enemy

    public float fieldOfViewAngle = 110f;                                //The angle of enemy field of view

    public bool isHearOrSee;                                             //A bool to switch between hearing and seeing the player


    public float time = 20;                                              //timer for when to switch between hearing and seeing

    public float maxTime;                                                //max time limit   

    public float minTime;                                                //min time limit



    void Start()
    {
        Enemy = GetComponent<UnityEngine.AI.NavMeshAgent>();                                //Enemy is equal to the NavMesh in the script
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();                 //Allows the NavMesh to make new paths
        Enemy.CalculatePath(target.position, path);                                         //Calculates the path between enemy and player
        col = GetComponent<SphereCollider>();                                               //col is the sphere collider in the script
        Enemy.autoBraking = false;                                                          //Makes the enemy not slow down when reaching a way to have a fluid motion

        GotoNextPoint();

    }


    void Update()
    {


        if (!Enemy.pathPending && Enemy.remainingDistance < 0.5f)                        //if the enemy reaches a waypoint, pick a new waypoint target
        {
            GotoNextPoint();
        }

        if (time > 0)                                                                   //If time is above zero then count down
        {
            //      time -= Time.deltaTime;                                                     //makes time count down
        }

    }

    void GotoNextPoint()                                                                //Waypoint target function
    {
        if (points.Count == 0)                                                          //if the waypoint equals 0 just reset path finding 
        {
            return;                                                                     //retuen to idle path finding
        }

        Enemy.destination = points[destPoint].position;                                 //makes a waypoint the enemies target position

        destPoint = Random.Range(0, points.Count);                                      //picks a random way point to travel too

    }

    void OnTriggerStay(Collider other)                                                  //when something enters into the enemy collider
    {
        if (time <= 0)                                                                  //if time is less than or equal to zero
        {
            time += Random.Range(minTime, maxTime);                                     //add random time between the min and max to timer
            isHearOrSee = !isHearOrSee;                                                 //switch hearing to seeing or seeing to hearing
        }

        if (isHearOrSee)                                                                //if enemy is hearing
        {
            StartCoroutine(AfterEnter());                                               //start afterenter corounite
            
        }

        if (!isHearOrSee)                                                                                             //if enemy is seeing
        {
            col.radius = 20f;                                                                                         //set sphere collider to 20 radius, also the max distance of field of view  

            if (other.gameObject == Player)                                                                           //if the player eneters the collider
            {
                playerInSight = false;                                                                                //if the player is in the collider but not in angle of view, the player is still not seen

                Vector3 direction = other.transform.position - transform.position;                                    //the field of view shoots straight in front of the enemy
                float angle = Vector3.Angle(direction, transform.forward);                                            //tha angle also shoots in front of the enemy

                if (angle < fieldOfViewAngle * 0.5f)                                                                  //a little hard to explain, the angle of the feild of view is two triangles put together
                {
                    RaycastHit hit;                                                                                   //just a raycast, pew pew

                    if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius))               //shoots the raycast in front of the player
                    {
                        if (hit.collider.gameObject == Player)                                                        //if the raycast hits the player
                        {
                            playerInSight = true;                                                                     //player is seen
                        }
                    }
                }
            }

            if (playerInSight == true)                                                                                //if the player is seen
            {
                Chase();                                                                                              //chase after player
            }
        }
    }

    void OnTriggerExit(Collider other)                                                                               //if the game object (player) leaves the trigger collider
    {

        StopCoroutine(AfterEnter());                                                                                    // stops aftereneter corounite
    }

    void Chase()                                                                               //Chase function
    {
        Enemy.isStopped = false;                                                               //if the enemy isn't chasing then chase

        Vector3 dirToPlayer = transform.position - Player.transform.position;                  //face torwards the player

        Vector3 newPos = transform.position - dirToPlayer;                                     //target position is the player position

        Enemy.SetDestination(newPos);                                                          //set destination to above line of code

    }

    float CalculatePathLength(Vector3 targetPosition)                                         //calcuates the path from enemy to player
    {
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();                   //makes a new NavMesh path

        if (Enemy.enabled)                                                                     //if the enemy is enabled by the player enetering the collider
        {
            Enemy.CalculatePath(targetPosition, path);                                         // if the distance between player and enemy
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];                         //makes new waypoints 

        allWayPoints[0] = transform.position;                                                  //changes the waypoints in the navmesh to be the target position
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++)                                              //a loop 
        {
            allWayPoints[i + 1] = path.corners[i];                                            //
        }

        float pathLength = 0f;                                                                //the length of the new path

        for (int i = 0; i < allWayPoints.Length - 1; i++)                                            //a loop 
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);             //adds the distance between all waypoints to make the new path
        }

        return pathLength;                                                                     //returns the navmesh back to idle pathfinding 
    }

    IEnumerator AfterEnter()                                                                  //this is made to help run the above function smoother
    {
        yield return new WaitForSeconds(delay);                                               //a delay to run the above function

        if (CalculatePathLength(Player.transform.position) <= col.radius)                   //if the new path inside the collider is less than or equal to the radius of the collider
        {
            Chase();                                                                          //chase the player

        }
    }
}
