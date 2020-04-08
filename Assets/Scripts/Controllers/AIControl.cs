using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Since all the 'data' concerning the gameObject comes from
//TankData and all 'functions' for that 'data' come from
//other scripts, we make the functions require the data.
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]
[RequireComponent(typeof(TankAttack))]

public class AIControl : MonoBehaviour
{
    //Creating variables to attach to scripts and objects.
    public TankMove move;
    public TankData data;
    public TankAttack attack;
    public Transform tf;
    public Material color;

    //Creating variables to hold information.
    public Transform target;

    public float fleeDistance;

    public int restTime;
    public int healthRegen;

    public float stateEnterTime;
    public float exitTime;
    public float avoidTime;

    //Create variables for sight and hearing.
    public float hearingdistance;
    public float FOVangle;
    public float inSightAngle;

    //Create variables for Wandering.
    public int currentWaypoint;
    public float closeEnough;
    public bool patrolForward = true;

    //Create a list for waypoints.
    public List<Transform> waypoints;

    //Create a list for wander methods.
    public enum LoopType { Stop, Loop, PingPong, Error };
    public LoopType loopType;
    //creating a list for AI identity
    public enum Identity { Aggro, Guard, Sniper, Coward }
    public Identity identity;

    void Start()
    {
        //Attaching scripts and objects.
        move = GetComponent<TankMove>();
        data = GetComponent<TankData>();
        attack = GetComponent<TankAttack>();
        tf = GetComponent<Transform>();
        color = GetComponent<MeshRenderer>().material;

        //Creating a way to randomly add personalities to the AI.
        identity = (Identity)Random.Range(0, System.Enum.GetNames(typeof(Identity)).Length);
        loopType = LoopType.PingPong;
        //When the identity is selected, pull the appropriate Script from Unity.
        switch (identity)
        {
            case Identity.Aggro:
                gameObject.AddComponent<AggroIdentity>();
                color.color = Color.red;
                break;
            case Identity.Guard:
                gameObject.AddComponent<GuardIdentity>();
                color.color = Color.white;
                break;
            case Identity.Sniper:
                gameObject.AddComponent<SniperIdentity>();
                color.color = Color.yellow;
                break;
            case Identity.Coward:
                gameObject.AddComponent<CowardIdentity>();
                color.color = Color.black;
                break;
        }

        foreach (GameObject waypointGo in GameManager.instance.waypoints)
        {
            waypoints.Add(waypointGo.transform);
        }        
    }

    //bools are true/false statements that we will use to help make decisions.
    public bool canDetectSight(TankData data)
    {
        //Get target by getting position and subtracting our own.
        Vector3 vectorToTarget = (target.transform.position - transform.position);
        //Create an angle to target to compare to FOV.
        float Angle = Vector3.Angle(vectorToTarget, transform.forward);
        //If the angle is bigger than FOV:
        if (Angle > data.sightFOV)
        {
            return false;
        }

        //Create a Raycast.
        RaycastHit hitInfo;
        //Use the Raycast to find colliders.
        Physics.Raycast(transform.position, vectorToTarget, out hitInfo, data.sightDetect);
        //If we don't hit any colliders:
        if (hitInfo.collider == null)
        {
            return false;
        }

        //Raycast returns which collider it hits now.
        Collider targetCollider = target.GetComponent<Collider>();
        //If we hit something that isn't the Player:
        if (targetCollider != hitInfo.collider)
        {
            return false;
        }

        //If none of these are true, then the AI can see the player.
        return true;
    }

    public bool canDetectNoise(TankData data)
    {
        //Distance is the difference between player and AI positions.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        //If the distance is greater than we can hear:
        if (distance >= (target.gameObject.GetComponent<TankData>().noiseLevel + data.noiseDetect))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool playerIsInRange()
    {
        if (canDetectSight(data))
        {
            return true;
        }
        if (canDetectNoise(data))
        {
            return true;
        }
        return false;
    }

    public bool CanMove(float speed)
    {
        //Debug.DrawRay(transform.position, transform.forward * 5, Color.blue);
        if (Physics.Raycast(tf.position, tf.forward, 5))
        {
            //Debug.Log("Cannot Move");
            return false;
        }
        //Debug.Log("Can Move");
        return true;
    }

    //virtual voids will be inherited and sometimes overwritten by child scripts 
    public virtual void Wander()
    {
        if (CanMove(data.forwardSpeed))
        {
            move.Move(data.forwardSpeed);
            move.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed);
        }
        else
        {
            move.Rotate(data.rotateSpeed);
        }

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < closeEnough)
        {
            //creating a switch case to move between different types of patrol to make it less predictable.
            switch (loopType)
            {
                case LoopType.Stop:
                    if (currentWaypoint < waypoints.Count - 1)
                    {
                        currentWaypoint++;
                    }
                    break;
                case LoopType.Loop:
                    if (currentWaypoint < waypoints.Count - 1)
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
                        if (currentWaypoint < waypoints.Count - 1)
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

    public virtual void Attack()
    {
        //move towards
        if (CanMove(data.forwardSpeed))
        {
            move.Move(data.forwardSpeed);
            move.RotateTowards(target.position, data.rotateSpeed);
        }
        else
        {
            //rotate towards
            move.Rotate(data.rotateSpeed);
        }
        //attack
        attack.FireCannon();
    }

    public virtual void Flee()
    {
        //Get our target position by subtracting it from our own.
        Vector3 vectorToTarget = target.position - tf.position;
        //Get the opposite direction by multiplying by negative one.
        Vector3 vectorAwayFromTarget = -1 * vectorToTarget;
        //Normalize the answer.
        vectorAwayFromTarget.Normalize();
        //AwayFromTarget become fleeDistance.
        vectorAwayFromTarget *= fleeDistance;
        //Determine which direction is "opposite" of our target.
        Vector3 fleePosition = vectorAwayFromTarget + tf.position;
        //Rotate function called.  Rotate away from target.
        move.RotateTowards(fleePosition, data.rotateSpeed);
        //Move function called.  Move away from target.
        move.Move(data.forwardSpeed);
    }

    public virtual void Avoid()
    {
        // If there are no obstacles obstructing our movement path, move forward
        if (CanMove(data.forwardSpeed))
        {
            //Debug.Log("Moving");
            move.Move(data.forwardSpeed * Time.deltaTime);
        }
        // Otherwise, turn until there is no obstacle.
        else
        {
            //Debug.Log("Rotating");
            move.Rotate(data.rotateSpeed * Time.deltaTime);
        }
    }

    public virtual void Rest()
    {
        //When an AI goes into rest they can regenerate health
        data.tankCurrentLife += healthRegen * Time.deltaTime;
    }
}