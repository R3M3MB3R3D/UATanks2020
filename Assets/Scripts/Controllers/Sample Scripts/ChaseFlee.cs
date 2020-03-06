using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseFlee: MonoBehaviour
{
    //Creating variables to attach to scripts and objects.
    public TankData data;
    public TankMove move;
    public Transform tf;
    public AIControl control;

    //Creating variables we will need for this function.
    public Transform target;
    public float fleeDistance = 1.0f;

    //Creating lists we will need for this function.
    public enum AttackMode { None, Chase, Flee, Error }
    public AttackMode attackMode;

    void Awake()
    {
        //Attaching scripts and objects.
        data = GetComponent<TankData>();
        move = GetComponent<TankMove>();
        tf = GetComponent<Transform>();
        control = GetComponent<AIControl>();
    }

    void Update()
    {
        switch (attackMode)
        {
            case AttackMode.None:
                //Do Nothing.
                break;
            case AttackMode.Chase:
                //rotate towards
                move.RotateTowards(target.position, data.rotateSpeed);
                //move towards
                move.Move(data.forwardSpeed);
                break;
            case AttackMode.Flee:
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
                break;
            case AttackMode.Error:
                Debug.Log("Chase/Flee Functions Failed or Terminated");
                break;
        }
    }
}