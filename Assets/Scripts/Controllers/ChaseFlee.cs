using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]

public class ChaseFlee: MonoBehaviour
{
    public enum AttackMode { Chase, Flee, Error }
    public AttackMode attackMode;

    public Transform target;
    public float fleeDistance = 1.0f;

    private TankData data;
    private TankMove move;
    private Transform tf;

    void Start()
    {
        data = GetComponent<TankData>();
        move = GetComponent<TankMove>();
        tf = GetComponent<Transform>();
    }

    void Update()
    {
        switch (attackMode)
        {
            case AttackMode.Chase:
                //rotate towards
                move.RotateTowards(target.position, data.rotateSpeed);
                //move towards
                move.Move(data.forwardSpeed);
                break;
            case AttackMode.Flee:
                //Ok so this is a line by line, step by step on how to
                //calculate how to run away from something according to AI
                Vector3 vectorToTarget = target.position - tf.position;
                Vector3 vectorAwayFromTarget = -1 * vectorToTarget;
                vectorAwayFromTarget.Normalize();
                vectorAwayFromTarget *= fleeDistance;
                Vector3 fleePosition = vectorAwayFromTarget + tf.position;
                move.RotateTowards(fleePosition, data.rotateSpeed);
                move.Move(data.forwardSpeed);
                break;
            case AttackMode.Error:
                Debug.Log("Chase/Flee Functions Failed or Terminated");
                break;
        }
    }
}