using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    //Creating variables to attach to scripts and objects.
    public TankData data;
    public TankMove move;
    public Transform tf;
    public AIControl control;

    //Creating variables we will need for this function.
    private float exitTime;
    public Transform target;
    public float avoidTime = 2.0f;

    //Creating lists we will need for this function.
    public enum AvoidStage { None, Rotate, Forward, Error };
    public AvoidStage avoidStage;

    void Awake()
    {
        //Attaching scripts and objects
        data = GetComponent<TankData>();
        move = GetComponent<TankMove>();
        tf = GetComponent<Transform>();
        control = GetComponent<AIControl>();
    }

    void Update()
    {
        switch (avoidStage)
        {
            case AvoidStage.None:
                //Do Nothing.
                break;
            case AvoidStage.Rotate:
                if (target != null)
                {
                    move.RotateTowards(Vector3.right, data.rotateSpeed);  
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

    bool CanMove(float speed)
    {
        RaycastHit hit;
        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            target = hit.transform;
            return false;
        }
        return true;
    }
}