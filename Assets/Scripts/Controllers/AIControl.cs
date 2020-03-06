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

    //Creating variables to hold information.
    public Transform target;
    public Transform playerTarget;
    public float fleeDistance;
    public int restTime;
    public int healthRegen;
    public float stateEnterTime;
    public float hearingdistance;
    public float FOVangle = 90.0f;
    public float inSightAngle;
    public float time;
    public float maxTime;


    //Creating a list for Avoid function
    public enum AvoidStage { None, Rotate, Forward, Error };
    public AvoidStage avoidStage;
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
        playerTarget = GameManager.instance.instantiatedPlayerTank.transform;
        time = 0.0f;
        maxTime = 2.0f;

        //Creating a way to randomly add personalities to the AI.
        identity = (Identity)Random.Range(0, System.Enum.GetNames(typeof(Identity)).Length);
        switch (identity)
        {
            case Identity.Aggro:
                gameObject.AddComponent<AggroIdentity>();
                break;
            case Identity.Guard:
                gameObject.AddComponent<GuardIdentity>();
                break;
            case Identity.Sniper:
                gameObject.AddComponent<SniperIdentity>();
                break;
            case Identity.Coward:
                gameObject.AddComponent<CowardIdentity>();
                break;
        }
    }

    public bool canDetectSight(TankData data)
    {
        //Get target by getting position and subtracting our own.
        Vector3 vectorToTarget = (transform.position - playerTarget.transform.position);
        //Create an angle to target to compare to FOV.
        float Angle = Vector3.Angle(vectorToTarget, transform.forward);
        //If the angle is bigger than FOV:
        if (Angle > data.sightFOV)
        {
            return false;
        }

        //Create a Raycast.
        RaycastHit hitInfo;
        //Use the Racast to find colliders.
        Physics.Raycast(transform.position, vectorToTarget, out hitInfo, data.sightDetect);
        //If we don't hit any colliders:
        if (hitInfo.collider == null)
        {
            return false;
        }

        //Raycast returns which collider it hits now.
        Collider targetCollider = playerTarget.GetComponent<Collider>();
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
        float distance = Vector3.Distance(transform.position, playerTarget.transform.position);
        //If the distance is greater than we can hear:
        if (distance >= (playerTarget.gameObject.GetComponent<TankData>().noiseLevel + data.noiseDetect))
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
        RaycastHit hit;
        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            target = hit.transform;
            return false;
        }
        return true;
    }

    //virtual voids will be inherited and sometimes overwritten by child scripts 
    public virtual void Chase()
    {
        if (playerIsInRange() == true)
        {
            //rotate towards
            move.RotateTowards(playerTarget.position, data.rotateSpeed);
            //move towards
            move.Move(data.forwardSpeed);
        }
    }

    public virtual void Attack()
    {
        //rotate towards
        move.RotateTowards(target.position, data.rotateSpeed);
        //move towards
        move.Move(data.forwardSpeed);
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
        switch (avoidStage)
        {
            case AvoidStage.None:
                //Do Nothing.
                break;
            case AvoidStage.Rotate:
                if (target != null)
                {
                    if ()
                        move.Rotate(data.forwardSpeed);
                }
                if (CanMove(data.forwardSpeed))
                {
                    avoidStage = AvoidStage.Forward;
                }
                break;
            case AvoidStage.Forward:
                move.Move(data.forwardSpeed);
                if (CanMove(data.forwardSpeed))
                {
                    avoidStage = AvoidStage.None;
                }
                else
                {
                    avoidStage = AvoidStage.Rotate;
                }
                break;
            case AvoidStage.Error:
                Debug.Log("Obstacle Avoidance failed or is terminated");
                break;
        }
    }

    public virtual void Rest()
    {
        //When an AI goes into rest they can regenerate health
        data.tankCurrentLife += healthRegen * Time.deltaTime;
    }
}