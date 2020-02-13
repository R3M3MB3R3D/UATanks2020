using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankAttack))]
[RequireComponent(typeof(TankMove))]

public class AISample : MonoBehaviour
{
    //Creating variables to attach to scripts and objects.
    public TankData data;
    public TankAttack attack;
    public TankMove move;
    public Transform tf;
    public Transform target;
    public Transform[] waypoints;
    Material material;

    //Creating Patrol variables.
    public bool patrolForward;
    public float closeEnough;
    public int currentWaypoint;

    //Creating AI Sense variables.
    public float hearingdistance;
    public float FOVangle;
    public float inSightAngle;

    public float fleeDistance;

    //Creating Rest functionality.
    public float stateEnterTime;
    public int healthRegen;
    public int gunRegen;
    public int cannonRegen;
    public int restTime;

    public enum LoopType { Stop, Loop, PingPong, Error };
    public LoopType loopType;
    public enum AIState { Patrol, Chase, ChaseAndFire, CheckForFlee, Flee, Rest }
    public AIState aiState = AIState.Chase;
    public enum Identity { Aggro, Guard , Sniper , Coward }
    public Identity identity;

    void Awake()
    {
        //Attaching scripts and objects.
        data = GetComponent<TankData>();
        attack = GetComponent<TankAttack>();
        move = GetComponent<TankMove>();
        tf = GetComponent<Transform>();
        material = GetComponent<Material>();

        //Setting some variables.
        currentWaypoint = 0;
        closeEnough = 1.0f;
        patrolForward = true;

        hearingdistance = 25f;
        FOVangle = 45.0f;
        inSightAngle = 10.0f;

        healthRegen = 10;
        gunRegen = 3;
        cannonRegen = 1;
        restTime = 5;
    }

    void Update()
    {
        switch (aiState)
        {
            case AIState.Patrol:
                //Execute function, 'Patrol' is described below.
                Patrol();
                break;

            case AIState.Chase:
                //Execute function, 'Chase' is described below.
                Chase();
                //Check if lower than half max health.
                if (data.tankCurrentLife < (data.tankMaxLife * .5))
                {
                    ChangeState(AIState.CheckForFlee);
                }
                //If 'player' is NOT in range.
                else if (!playerIsInRange())
                {
                    ChangeState(AIState.Chase);
                }
                break;

            case AIState.ChaseAndFire:
                Chase();
                attack.FireCannon();
                //Check if lower than half max heath.
                if (data.tankCurrentLife < (data.tankMaxLife * .5))
                {
                    //ChangeState is a function below
                    ChangeState(AIState.CheckForFlee);
                }
                // TODO: implement 'sight' and 'hearing' so the AI can check this.
                else if (playerIsInRange())
                {
                    //ChageState also tracks when the AI switches state.
                    ChangeState(AIState.ChaseAndFire);
                }
                break;

            case AIState.CheckForFlee:
                //If 'player' IS in range.
                if (playerIsInRange())
                {
                    ChangeState(AIState.Flee);
                }
                else
                {
                    ChangeState(AIState.Rest);
                }
                break;
            case AIState.Flee:
                Flee();
                //When the time between states has passed.
                if (Time.time >= stateEnterTime + restTime)
                {
                    ChangeState(AIState.CheckForFlee);
                }
                break;
            case AIState.Rest:
                Rest();
                if (playerIsInRange())
                {
                    ChangeState(AIState.Flee);
                }
                //Mathf.Approximately compares the 2 integers and allows for answers
                //that are not exactly the same, but close enough to be called good.
                else if (Mathf.Approximately(data.tankCurrentLife , data.tankMaxLife))
                {
                    ChangeState(AIState.Chase);
                }
                break;
        }

        switch (identity)
        {
            case Identity.Inky:
                Inky();
                break;
            case Identity.Blinky:
                Blinky();
                break;
            case Identity.Pinky:
                Pinky();
                break;
            case Identity.Clyde:
                Clyde();
                break;
        }
    }

    private bool playerIsInRange()
    {
        return true;
    }

    void ChangeState(AIState newState)
    {
        aiState = newState;
        stateEnterTime = Time.time;
    }

    void Patrol()
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

    void Chase()
    {
        //rotate towards
        move.RotateTowards(target.position, data.rotateSpeed);
        //move towards
        move.Move(data.forwardSpeed);
    }

    void ChaseAndFire ()
    {
        //rotate towards
        move.RotateTowards(target.position, data.rotateSpeed);
        //move towards
        move.Move(data.forwardSpeed);
        //fire cannon
        attack.FireCannon();
    }

    void Flee()
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

    void Rest()
    {
        //When an AI goes into rest they can regenerate health
        //and ammo for an amount of time up to designers.
        data.tankCurrentLife += healthRegen * Time.deltaTime;
        data.tankGunAmmoCurrent += gunRegen * Time.deltaTime;
        data.tankCannonAmmoCurrent += cannonRegen * Time.deltaTime;
    }

    void Aggro()
    {
        material.color = Color.magenta;
    }

    void Guard()
    {

    }

    void Sniper()
    {

    }

    void Coward()
    {

    }
}