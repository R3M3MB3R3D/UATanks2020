using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Since all the 'data' concerning the gameObject comes from
//TankData and all 'functions' for that 'data' come from
//other scripts, we make the functions require the data.
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]
[RequireComponent(typeof(AIControl))]

public class Patrol : MonoBehaviour
{
    //Creating variables to attach to scripts and objects.
    private TankData data;
    private TankMove move;
    private Transform tf;
    private AIControl control;

    //Creating variables we will need for this function.
    public int currentWaypoint = 0;
    public float closeEnough = 1.0f;
    public bool patrolForward = true;


    //Creating lists we will need for this function.
    public Transform[] waypoints;
    public enum LoopType { Stop, Loop, PingPong, Error };
    public LoopType loopType;

    private void Awake()
    {
        //Attaching scripts and objects.
        data = GetComponent<TankData>();
        move = GetComponent<TankMove>();
        tf = GetComponent<Transform>();
        control = GetComponent<AIControl>();
    }

    void Update()
    {
        if (move.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed))
        {
            //do nothing
        }
        else
        {
            move.Move(data.forwardSpeed);
        }

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < closeEnough)
        {
            //creating a switch case to move between different types of patrol to make it less predictable.
            switch (loopType)
            {
                case LoopType.Stop:
                    if (currentWaypoint < waypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }
                    break;
                case LoopType.Loop:
                    if (currentWaypoint < waypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        currentWaypoint = 0;
                    }
                    break;
                case LoopType.PingPong:
                    if (patrolForward)
                    {
                        if (currentWaypoint < waypoints.Length - 1)
                        {
                            currentWaypoint++;
                        }
                        else
                        {
                            patrolForward = false;
                            currentWaypoint--;
                        }
                    }
                    else
                    {
                        if (currentWaypoint > 0)
                        {
                            currentWaypoint--;
                        }
                        else
                        {
                            patrolForward = true;
                            currentWaypoint++;
                        }
                    }
                    break;
                case LoopType.Error:
                    Debug.Log("Patrol Functions Failed or Terminated");
                    break;
            }
        }
    }
}