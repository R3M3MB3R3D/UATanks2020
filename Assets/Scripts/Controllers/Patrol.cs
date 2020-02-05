using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private TankData data;
    private TankMove move;

    //creating a way to track tanks in between waypoints and which direction
    //they are moving between them.
    public int currentWaypoint = 0;
    public bool patrolForward = true;
    public float closeEnough = 1.0f;

    public enum LoopType { Stop, Loop, PingPong, Error };
    public LoopType loopType;

    //creating a list of positions for enemy tanks to go to.
    public Transform[] waypoints;

    private void Awake()
    {
        data = GetComponent<TankData>();
        move = GetComponent<TankMove>();
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

        //if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) <= (closeEnough * closeEnough))
        //Above is supposedly a better way to do it, but this doesn't work with what I have currently.
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