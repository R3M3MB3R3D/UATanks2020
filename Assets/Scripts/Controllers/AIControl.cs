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

    void Awake()
    {
        //Attaching scripts and objects.
        move = gameObject.GetComponent<TankMove>();
        data = gameObject.GetComponent<TankData>();
        attack = gameObject.GetComponent<TankAttack>();
    }

    void Update()
    {
        
    }

    public bool canDetectSight(TankData target)
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
        //Use the Racast to find colliders.
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

    public bool canDetectNoise(TankData target)
    {
        //Distance is the difference between player and AI positions.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        //If the distance is greater than we can hear:
        if (distance >= (target.noiseLevel + data.noiseDetect))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}