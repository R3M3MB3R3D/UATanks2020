using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ensuring that our code works by making sure everything that works
//together are always together.
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]

public class AIControl: MonoBehaviour
{
    //creating a list of positions for enemy tanks to go to.
    public Transform[] waypoints;

    //attaching scripts together 
    private TankMove motor;
    private TankData data;
    private TankAttack attack;
    private Transform tf;

    //creating a way to track tanks in between waypoints
    public  int currentWaypoint = 0;
    public float closeEnough = 1.0f;

    void Start()
    {
        motor = gameObject.GetComponent<TankMove>();
        data = gameObject.GetComponent<TankData>();
        attack = gameObject.GetComponent<TankAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed))
        {
            //do nothing
        }
        else
        {
            motor.Move(data.forwardSpeed);
        }
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < closeEnough)
        //if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
        {
            currentWaypoint++;
        }
    }
}