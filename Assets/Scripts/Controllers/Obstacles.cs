using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]

public class Obstacles : MonoBehaviour
{
    //creating the variables we will attach to
    //this script and therefore the object.
    private TankData data;
    private TankMove move;
    private Transform tf;

    //creating the variables we will need
    //to create obstacle avoidance.
    private float exitTime;
    public Transform target;
    public float avoidTime = 2.0f;

    //Creating a toggle for which avoidance state
    //the AI is currently in, and will help dictate
    //how it avoids obstacles.
    public enum AvoidStage { None, Rotate, Move, Error };
    public AvoidStage avoidStage;

    void Awake()
    {
        //attaching scripts to objects
        data = GetComponent<TankData>();
        move = GetComponent<TankMove>();
        tf = GetComponent<Transform>();
    }

    void Update()
    {
        switch (avoidStage)
        {
            case AvoidStage.None:

                break;
            case AvoidStage.Rotate:
                
                break;
            case AvoidStage.Move:
                //if (CanMove(data.forwardSpeed))
                {
                    exitTime -= Time.deltaTime;
                    move.Move(data.forwardSpeed);
                    if (exitTime <= 0.0f)
                    {

                    }
                }

                break;
            case AvoidStage.Error:
                Debug.Log("Obstacle Avoidance failed or is terminated");
                break; 
        }
    }

    void CanMove()
    {
    }
}