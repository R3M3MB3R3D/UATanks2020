using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Since all the 'data' concerning the gameObject comes from
//TankData and all 'functions' for that 'data' come from
//other scripts, we make the functions require the data.
[RequireComponent(typeof(TankData))]

public class TankMove : MonoBehaviour
{
    //Creating variables to attach to scripts and objects.
    private TankData data;
    private Transform tf;
    private CharacterController control;
    public AudioClip tankMoveSound;
    public AudioSource tankNoiseSource;

    void Awake()
    {
        //Attaching scripts and objects.
        data = GetComponent<TankData>();
        tf = GetComponent<Transform>();
        control = GetComponent<CharacterController>();
        tankNoiseSource = GetComponent<AudioSource>();
    }

    public void Move(float speed)
    {
        //Debug.Log("Moving");
        Vector3 speedVector = tf.forward * speed;
        control.SimpleMove(speedVector);
        //Debug.Log("Speed is " + speed + ".");

        //Debug.Log("speed reached");
        if (tankNoiseSource.isPlaying == false)
        {
            tankNoiseSource.clip = tankMoveSound;
            tankNoiseSource.Play();
        }
    }

    public void Rotate(float speed)
    {
        Vector3 rotateVector = Vector3.up * speed * Time.deltaTime;
        tf.Rotate(rotateVector, relativeTo: Space.Self);
        data.noiseLevel = 3;
    }

    public bool RotateTowards(Vector3 target, float speed)
    {
        //creating a variable to see the difference between AI and player location.
        Vector3 vectorToTarget = target - tf.position;
        //Quaternions allow for smoother actions, especially from AI actions.
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);
        //if our rotation is equal to our target's, then we dont need to rotate anymore.
        if (targetRotation == tf.rotation)
        {
            //If the bool is false, then the AI still needs to rotate.
            return false;
        }
        //This is where the AI actually rotates.
        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, speed * Time.deltaTime);
        //If the bool is true, then the AI doesn't need to rotate.
        return true;
    }
}